// Ice class methods:
    // allow player to ice crew members
    // displays what occurs
    // handles all crew logic on if
        // crew members open fire or run in fear
using System;
using System.Collections.Generic;
using System.Linq;
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.Location;
using CSharpHeists.UI;

namespace CSharpHeists.GameSections.MainLoop
{
    public class Ice
    {
        public static void DisplayIceAssociateWarning(List<BaseCriminal> crew)
        {
            Console.WriteLine("");
            Console.WriteLine("Icing crew members upsets the rest of the crew.");
            Console.WriteLine(Misc.DisplayGun());

            Console.WriteLine("Crew members");
            Console.WriteLine("------------------");
            crew.ForEach(c =>
            {
                if (c.IsPlayer == false) Console.WriteLine($"{c.Name} - {c.MoraleDescription}");
            });
            Console.WriteLine("");

            Console.Write("Enter name of who you will ice [leave blank to cancel]: ");
        }

        public static List<BaseCriminal> IceCrewMember(List<BaseCriminal> crew, List<BaseLocation> locations, bool splitCashMenu)
        {
            if (crew.Count() > 1) return IceAssociateCheck(crew, locations, splitCashMenu);
            else return crew;
        }

        public static List<BaseCriminal> IceAssociateCheck(List<BaseCriminal> crew, List<BaseLocation> locations, bool splitCashMenu)
        {
            Console.Clear();
            string name = "not empty for a base value";
            Console.WriteLine(Heading.DisplayIce());

            if (!splitCashMenu)
            {
                while (name != "" && crew.Count() > 1)
                {
                    CrewManagement.DisplayCrewInfo(crew);
                    DisplayIceAssociateWarning(crew);
                    name = Console.ReadLine();

                    // Store the criminal who was iced for use in display
                    BaseCriminal whoWasIced = crew.Find(c => c.Name == name);
                    // Make a new list of criminals WITHOUT the iced crew member
                    List<BaseCriminal> icedCrew = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

                    // Only lower morale if we have iced a crew member
                    if (icedCrew.Count() < crew.Count())
                    {
                        // Lower the crews morale
                        List<BaseCriminal> newCrew = LowerMoraleFromIce(icedCrew);
                        // Display Who was iced, if not null
                        if (whoWasIced != null) DisplayWhoWasIced(whoWasIced);
                        WillCrewMemberRunAfterIce(newCrew, locations);
                        crew = newCrew;
                    }
                    // We didn't type in a name, so return the new crew
                    else crew = icedCrew;
                }
            }
            else if (splitCashMenu)
            {
                CrewManagement.DisplayCrewInfoShortened(crew);
                DisplayIceAssociateWarning(crew);
                name = Console.ReadLine();

                // Store the criminal who was iced for use in display
                BaseCriminal whoWasIced = crew.Find(c => c.Name == name);
                // Make a new list of criminals WITHOUT the iced crew member
                List<BaseCriminal> icedCrew = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

                // Only lower morale if we have iced a crew member
                if (icedCrew.Count() < crew.Count())
                {
                    // Lower the crews morale
                    List<BaseCriminal> newCrew = LowerMoraleFromIce(icedCrew);
                    // Display Who was iced, if not null
                    if (whoWasIced != null) DisplayWhoWasIced(whoWasIced);
                    newCrew.ForEach(c =>
                    {
                        if (c.IsPlayer) c.PlayerFiredWeapon = true;
                    });
                    crew = newCrew;
                }
                // We didn't type in a name, so return the new crew, and do not let anyone shoot
                else crew = icedCrew.Select(c =>
                {
                    if (c.IsPlayer)
                    {
                        c.PlayerFiredWeapon = false;
                        return c;
                    }
                    else return c;
                }).ToList();
            }
            // If the first check is false, return a crew
            return crew;
        }

        public static List<BaseCriminal> LowerMoraleFromIce(List<BaseCriminal> icedCrew)
        {
            return icedCrew.Select(c =>
            {
                int subtractMorale = new Random().Next(35, 70);
                int loweredMorale = c.Morale - subtractMorale;

                if (loweredMorale < c.MoraleMin) c.Morale = c.MoraleMin;
                else c.Morale = loweredMorale;
                return c;
            }).ToList();
        }

        public static void DisplayWhoWasIced(BaseCriminal whoWasIced)
        {
            Console.Clear();

            // Display ASCII artwork for who was killed 
            Console.WriteLine(Heading.DisplayHeadingIced());
            Console.WriteLine($"{whoWasIced.FaceIced}");
            Console.WriteLine("");

            // Summary of events
            Console.WriteLine($"You iced {whoWasIced.Name}!");
            Console.WriteLine("");
            Console.WriteLine("Crew morale decreased.");
            Console.WriteLine("Everyone's cut increased.");

            // Player input to continue
            Console.WriteLine("");
            Menu.Continue();
        }

        public static void WillCrewMemberRunAfterIce(List<BaseCriminal> crew, List<BaseLocation> locations)
        {
            // SIMILAR to WillCrewMemberTurnAfterHeist
            // But this checks EVERY member instead of until one turns
            List<BaseCriminal> checkedCrew = new List<BaseCriminal>();
            List<BaseCriminal> notScaredCrew = new List<BaseCriminal>();

            BaseCriminal player = crew.Find(c => c.IsPlayer);
            int currentCashTotal = player.CrewTotalCash;
            // Loop through criminals & check their morale
            checkedCrew = crew.Select(c =>
            {
                if (!c.IsPlayer)
                {
                    int morale = c.Morale;
                    // if morale is less than 40, chance to run    
                    if (morale <= 40)
                    {
                        BaseCriminal possibleTraitor = c;

                        // Get new random
                        Random r = new Random();
                        int randFrom100 = r.Next(101);
                        int chance10 = 0;
                        int chance20 = 0;
                        int chance40 = 0;
                        int chance100 = 0;

                        // Based on this member's morale, generate a number by chance
                        if (morale >= 30 && morale <= 40) chance10 = r.Next(11);
                        else if (morale >= 20 && morale <= 29) chance20 = r.Next(21);
                        else if (morale >= 10 && morale <= 19) chance40 = r.Next(41);
                        // Chance100 equals the randomNumber so if they're at that level, they'll always turn
                        else if (morale <= 9) chance100 = randFrom100;

                        possibleTraitor = ChanceToRun(chance10, randFrom100, possibleTraitor);
                        possibleTraitor = ChanceToRun(chance20, randFrom100, possibleTraitor);
                        possibleTraitor = ChanceToRun(chance40, randFrom100, possibleTraitor);
                        possibleTraitor = ChanceToRun(chance100, randFrom100, possibleTraitor);

                        return possibleTraitor;
                    }
                }
                return c;
            }).ToList();

            // Check if any traitors stole cash
            if (currentCashTotal > 2)
            {
                checkedCrew.ForEach(c =>
                {
                    // Did anyone run in fear?
                    if (c.HasRanInFear)
                    {
                        // Reset the cash to the current amount
                        c.CrewTotalCash = currentCashTotal;
                        // Remove 50% of cash
                        currentCashTotal = currentCashTotal - (currentCashTotal / 2);
                    }
                });
            }

            // Updated everyone's cash amount
            checkedCrew.ForEach(c => c.CrewTotalCash = currentCashTotal);

            // Filter only the crew members who didn't run in fear
            notScaredCrew = checkedCrew.Where(c => c.HasRanInFear == false).ToList();

            CrewManagement.ManageCrew(notScaredCrew, locations);
        }

        public static BaseCriminal ChanceToRun(
            int chanceInt,
            int randFrom100,
            BaseCriminal possibleTraitor)
        {
            // Check if they're a traitor
            if (chanceInt != 0 && chanceInt >= randFrom100)
            {
                possibleTraitor.HasRanInFear = true;
                AssociateRanInFear(possibleTraitor);
                return possibleTraitor;
            }
            // Not a traitor
            return possibleTraitor;
        }

        static void AssociateRanInFear(BaseCriminal scared)
        {
            Console.Clear();
            Console.WriteLine(Heading.DisplayHeadingFled());
            if (scared.CrewTotalCash == 0)
            {
                Console.WriteLine(Vehicle.DisplayAssociateRanInFear());
                Console.WriteLine(scared.Face);
                Console.WriteLine("");
                Console.WriteLine($"{scared.Name} fled from your horrifically violent action!");
            }
            else
            {
                Console.WriteLine(Vehicle.DisplayAssociateRanWithCash());
                Console.WriteLine(scared.Face);
                Console.WriteLine("");
                Console.WriteLine($"Your horrifically violent act terrified {scared.Name}.");
                Console.WriteLine($"{scared.Name} fled with a large portion of the cash!");
            }

            Console.WriteLine("");
            Menu.Continue();
        }

        public static void WillCrewMemberShoot(List<BaseCriminal> crew, List<BaseLocation> locations, bool isPlayerAttemptingMoneySplit)
        // Check every crew member's morale to see if they will turn
        {
            // SIMILAR to WillCrewMemberTurnAfterHeist
            BaseCriminal player = crew.Find(c => c.IsPlayer);

            crew.ForEach(c =>
            {
                int morale = c.Morale;
                if (c.IsPlayer == false && morale <= 40)
                {
                    // Save as traitor for easy understanding
                    BaseCriminal traitor = c;

                    // Generate a new random number between 1-10 to handle the % a person will turn
                    Random r = new Random();
                    int randomNumber = r.Next(101);
                    int chance30 = 0;
                    int chance50 = 0;
                    int chance70 = 0;
                    int chance100 = 0;

                    // Based on this member's morale, generate a number by chance
                    if (morale >= 30 && morale <= 40) chance30 = r.Next(31);
                    else if (morale >= 20 && morale <= 29) chance50 = r.Next(51);
                    else if (morale >= 10 && morale <= 19) chance70 = r.Next(71);
                    // Chance100 equals the randomNumber so if they're at that level, they'll always turn
                    else if (morale <= 9) chance100 = randomNumber;

                    ChanceToShoot(chance30, randomNumber, crew, locations, traitor, isPlayerAttemptingMoneySplit);
                    ChanceToShoot(chance50, randomNumber, crew, locations, traitor, isPlayerAttemptingMoneySplit);
                    ChanceToShoot(chance70, randomNumber, crew, locations, traitor, isPlayerAttemptingMoneySplit);
                    ChanceToShoot(chance100, randomNumber, crew, locations, traitor, isPlayerAttemptingMoneySplit);
                }
            });
            // Find criminals WHERE they have not been marked as ICED and set them as the new crew
            crew = crew.Where(c => !c.isAssociateIced).ToList();
            // After we have looped through every associate, go to the correct view
            if (isPlayerAttemptingMoneySplit) Outro.GameOver(crew, locations, true, player);
            if (!isPlayerAttemptingMoneySplit) Outro.SplitCash(crew, locations);
        }

        public static void ChanceToShoot(
            int chanceInt,
            int randFrom100,
            List<BaseCriminal> crew,
            List<BaseLocation> locations,
            BaseCriminal traitor,
            bool isPlayerAttemptingMoneySplit)
        {
            if (chanceInt != 0 && chanceInt >= randFrom100 && !traitor.isAssociateIced)
            {
                // This traitor WILL shoot
                // Get the traitor's index value
                int traitorIndex = crew.IndexOf(traitor);
                // Declare a target's index
                BaseCriminal target = null;
                Random r = new Random();
                bool lookingForTarget = true;
                // WHILE we do not have a person to shoot
                while (lookingForTarget)
                {
                    int possibleTargetIndex = 0;
                    // Player is a possible target, only if the crew is smaller than a certain number
                    if (crew.Count() > 2) possibleTargetIndex = r.Next(1, crew.Count());               
                    else possibleTargetIndex = r.Next(0, crew.Count());                   
                    // If the target is not the shooter
                    if (possibleTargetIndex != traitorIndex)
                    {
                        target = crew[possibleTargetIndex];
                        lookingForTarget = false;
                    }
                }

                // Instead of removing the target, mark the target as "Iced."
                // Otherwise we crash because we've 'Modified a collection' while attempting to loop over it.
                crew.ForEach(c =>
                {
                    if (target.Name == c.Name) c.isAssociateIced = true;
                });

                // pass traitor and target into display message
                DisplayWhoIcedWho(crew, locations, traitor, target, isPlayerAttemptingMoneySplit);
            }
        }

        public static void DisplayWhoIcedWho(List<BaseCriminal> crew,
            List<BaseLocation> locations,
            BaseCriminal traitor,
            BaseCriminal gotShot,
            bool isPlayerAttemptingMoneySplit)
        {
            Console.Clear();

            Console.WriteLine(Heading.DisplayHeadingIced());
            Console.WriteLine(gotShot.FaceIced);
            Console.WriteLine("");
            if (gotShot.IsPlayer)
            {
                Console.WriteLine($"{traitor.Name} iced YOU in the face!");
                Console.WriteLine("");
                Menu.Continue();
                Outro.GameOver(crew, locations, false, gotShot);
            }

            else if (!gotShot.IsPlayer)
            {
                Console.WriteLine($"{traitor.Name} iced {gotShot.Name} in the face!");
                Console.WriteLine("");
                Console.WriteLine("Survivor's morale decreased.");
                Console.WriteLine("");
                Menu.Continue();

                // If player is NOT attempting to split the cash, return to SplitCash view
                if (!isPlayerAttemptingMoneySplit)
                {
                    // Loop through the crew and find the members WHERE they have not been iced
                    crew = crew.Where(c => !c.isAssociateIced).ToList();
                    Outro.SplitCash(crew, locations);
                }
                // Otherwise, continue the forEach check for each crew member
            }
        }
    }
}
