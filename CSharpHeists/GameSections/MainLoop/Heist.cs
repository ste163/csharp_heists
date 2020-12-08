// Heist class methods:
    // Run through heist success/failure checks
    // runs appropriate checks
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.Location;
using CSharpHeists.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpHeists.GameSections.MainLoop
{
    public class Heist
    {
        // Run through checks to see if crew succeeds
        public static void BeginHeist(List<BaseCriminal> crew, List<BaseLocation> locations, string locName)
        {
            // Regardless of success/failure, reset all waits in van
            locations.ForEach(l => l.WaitsInVanAvailable = 3);

            bool heistSuccess = false;

            List<BaseCriminal> crewSuccess = crew;

            List<BaseLocation> updatedLocations = locations.Select(l =>
            {
                // find the selected location
                if (l.Name == locName)
                {
                    // When we have the current location
                    // Compare the location's difficulty with the crew's max skills
                    int crewTotalSkill = crew.Sum(c => c.TotalSkillLevel);

                    // If the crew succeeds, add the total cash to each crew member
                    // This is to save the cash, it will be split later
                    if (l.Difficulty <= crewTotalSkill)
                    {
                        crewSuccess = crew.Select(c =>
                        {
                            // Every crew member gets a random skill+
                            int skillIncrease = new Random().Next(8, 38);
                            int moraleIncrease = new Random().Next(7, 24);
                            int locationCash = l.Cash;
                            c.CrewTotalCash = c.CrewTotalCash + l.Cash;
                            c.BaseSkill = c.BaseSkill + skillIncrease;
                            if (c.Morale + moraleIncrease > c.MoraleMax)
                            {
                                c.Morale = c.MoraleMax;
                            }
                            else
                            {
                                c.Morale = c.Morale + moraleIncrease;
                            }
                            c.HasPlayerEncouragedCrew = false;
                            // If the new morale is over 100, set it to only 100
                            if (c.Morale > 100)
                            {
                                c.Morale = 100;
                            }
                            return c;
                        }).ToList();

                        l.Completed = true;
                        heistSuccess = true;
                    }
                    else
                    {
                        l.Completed = true;
                        heistSuccess = false;
                    }

                    return l;
                }
                else
                // location is not the selected one, so return it to the location list
                {
                    return l;
                }
            }).ToList();

            if (heistSuccess) HeistSuccess(crewSuccess, updatedLocations, locName);
            else HeistFailure(crewSuccess, updatedLocations);
        }

        public static void HeistSuccess(List<BaseCriminal> crew, List<BaseLocation> locations, string locName)
        {
            Color.SuccessGreen();

            // Morale check before continuing
            CrewLogicHeist.WillCrewMemberTurnAfterHeist(crew, locations, locName);

            Console.WriteLine(Heading.DisplayHeistSuccess());
            Console.WriteLine(Misc.DisplayMoney());
            Console.WriteLine(Heading.DisplaySuccessOveriew());

            BaseLocation currentLocation = locations.Find(l => l.Name == locName);
            BaseCriminal player = crew.Find(c => c.IsPlayer);
            int cashEarned = player.CrewTotalCash;

            // Display how much money we earned from location
            Console.WriteLine($"Crew stole: ${currentLocation.Cash}");
            // Total earned
            if (cashEarned != currentLocation.Cash) Console.WriteLine($"Total cash: ${cashEarned}");
            // how much the current split will be if it's more than one crew member
            if (crew.Count() > 1)
            {
                Console.WriteLine($"The split per crew member will be: ${cashEarned / crew.Count()}");
                Console.WriteLine("----");
                Console.WriteLine("Crew skill increased");
                Console.WriteLine("Crew morale increased");
            }
            else if (crew.Count() == 1)
            {
                Console.WriteLine("Your skill increased");
            }
            Console.WriteLine("");
            Menu.Continue();
            Level.LevelSelect(crew, locations);
        }

        public static void HeistFailure(List<BaseCriminal> crew, List<BaseLocation> locations)
        {
            Color.PoliceBlue();
            BaseCriminal player = crew.Find(c => c.IsPlayer);
            string moraleMsg = "Crew morale decreased.";
            // 50-50 chance for arrested or escaped
            Random random = new Random();
            int r = random.Next(1, 3);

            // Arrested
            if (r == 1)
            {
                // Generate arrested summary
                Console.WriteLine(Heading.DisplayHeadingArrested());
                Console.WriteLine(Heading.DisplaySubheadingArrested());

                // If only player is in crew
                if (crew.Count == 1)
                {
                    crew.ForEach(c => c.IsPlayerArrested = true);
                    Console.WriteLine(Face.DisplayArrested());
                    Menu.Continue();
                    Outro.GameOver(crew, locations, true, player);
                }
                // If multiple crew members
                else if (crew.Count > 1)
                {
                    // arrest a random crew member
                    int crewSize = crew.Count();
                    int randomCrewMember = new Random().Next(1, crewSize);

                    BaseCriminal arrestedMember = crew.ElementAt(randomCrewMember);
                    Console.WriteLine("");
                    Console.WriteLine($"{arrestedMember.Face}");
                    Console.WriteLine("");
                    Console.WriteLine($"The cops got {arrestedMember.Name}!\n");
                    Console.WriteLine(moraleMsg);
                    Console.WriteLine("");
                    Menu.Continue();

                    crew.RemoveAt(randomCrewMember);
                    // Randomly lower every non-player's morale
                    crew.ForEach(c =>
                    {
                        if (c.IsPlayer == false)
                        {
                            int loweredMorale = c.Morale - new Random().Next(25, 54);
                            if (loweredMorale < c.MoraleMin) c.Morale = c.MoraleMin;                            
                            else c.Morale = loweredMorale; 
                        }
                    });
                    // Return to level select menu
                    Level.LevelSelect(crew, locations);
                }
            }
            // Escaped
            else if (r == 2)
            {
                // Randomly lower every non-player's morale
                crew.ForEach(c =>
                {
                    if (c.IsPlayer == false)
                    {
                        int loweredMorale = c.Morale - new Random().Next(15, 35);
                        if (loweredMorale < c.MoraleMin) c.Morale = c.MoraleMin;
                        else c.Morale = loweredMorale;
                    }
                });
                // Display escaped summary
                Console.WriteLine(Heading.DisplayHeadingEscaped());
                Console.WriteLine(Heading.DisplaySubheadingEscaped());
                Console.WriteLine(Vehicle.DisplayPoliceCar());
                if (crew.Count() > 1) Console.WriteLine(moraleMsg);
                Console.WriteLine("");
                Menu.Continue();
                // Return to level select menu
                Level.LevelSelect(crew, locations);
            }
        }
    }
}
