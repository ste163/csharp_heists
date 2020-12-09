// CrewManagement class methods:
// recruit new associates
// Modify the crew
// Display crew info
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.Location;
using CSharpHeists.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpHeists.GameSections.MainLoop
{
    public class CrewManagement
    {
        // Main view for managing crew
        public static void ManageCrew(List<BaseCriminal> crew, List<BaseLocation> locations)
        // Must always return the current crew and the current locations
        {
            Color.DefaultGray();

            List<BaseCriminal> modifiedCrew = crew;

            DisplayCurrentCrew(modifiedCrew);

            BaseCriminal player = crew.Find(c => c.IsPlayer);

            // If only the player is in the crew & player has 0 contacts, return to level select
            if (crew.Count == 1 && player.PlayerContactCount == 0) Level.LevelSelect(crew, locations);

            Console.WriteLine();
            if (player.PlayerContactCount > 0) Console.WriteLine($"1) recruit an associate [{player.PlayerContactCount} associates available to contact]");
            else if (player.PlayerContactCount == 0) Console.WriteLine("You've contacted every associate you know.");
            if (crew.Count() > 1)
            {
                if (player.HasPlayerEncouragedCrew == false) Console.WriteLine("2) give an encouraging speech");
                else if (player.HasPlayerEncouragedCrew == true) Console.WriteLine("You can give another speech after a successful heist.");
                Console.WriteLine("3) ice crew member");
            };
            Console.WriteLine("4) return to planning");

            int input = Menu.MenuInput(4);

            switch (input)
            {
                case 1:
                    modifiedCrew = CrewManagement.ModifyCrew(modifiedCrew);
                    ManageCrew(modifiedCrew, locations);
                    break;
                case 2:
                    if (crew.Count() > 1) modifiedCrew = EncourageCrew(modifiedCrew, false);
                    ManageCrew(modifiedCrew, locations);
                    break;
                case 3:
                    modifiedCrew = Ice.IceCrewMember(modifiedCrew, locations, false);
                    ManageCrew(modifiedCrew, locations);
                    break;
                case 4:
                    Level.LevelSelect(modifiedCrew, locations);
                    break;
            }
        }

        public static List<BaseCriminal> SortCrew(List<BaseCriminal> crew)
        {
            crew.Sort((x, y) =>
            {
                if (y.IsPlayer) return 0;
                else return y.BaseSkill.CompareTo(x.BaseSkill);
            });
            return crew;
        }

        // Add associates to crew both on initial crew creation and later in game
        public static BaseCriminal RecruitNewAssociate(List<BaseCriminal> crew)
        {
            bool nameAvailable = true;
            // Set a blank new criminal that we can return after the loop completes
            BaseCriminal newAssociate = null;

            while (nameAvailable)
            {
                Console.Write("Enter associate's nickname: ");
                string enteredName = Console.ReadLine();

                BaseCriminal isNameTaken = crew.Find(c => c.Name == enteredName);

                if (isNameTaken == null && !string.IsNullOrWhiteSpace(enteredName))
                {
                    newAssociate = new BaseCriminal(enteredName, false, crew);
                    nameAvailable = false;
                    DisplayCreatedAssociate(newAssociate);
                    // Reset isNameTaken so more associates can be created
                    isNameTaken = null;
                }
            }  
            // Exit loop and return new criminal
            return newAssociate;
        }

        // Display who the associate is
        static void DisplayCreatedAssociate(BaseCriminal associate)
        {
            Console.WriteLine($@"{associate.Face}

{associate.Name} recruited!
_________
   
   base skill: {associate.BaseSkill}
 ");
        }

        // Modify the current crew
        public static List<BaseCriminal> ModifyCrew(List<BaseCriminal> crew)
        {
            List<BaseCriminal> modifiedCrew = new List<BaseCriminal>();
            int playerContactsLeft;

            // To save playerContactsLeft count, must loop through
            // incoming crew, update the player, then re-save everyone in a new list
            crew.ForEach(c =>
            {
                playerContactsLeft = c.PlayerContactCount;
                if (c.IsPlayer && playerContactsLeft > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"{playerContactsLeft} associates left to hire.");
                    modifiedCrew.Add(c);
                    modifiedCrew.Add(RecruitNewAssociate(crew));
                    c.PlayerContactCount = --playerContactsLeft;
                }
                else modifiedCrew.Add(c);
            });
            return modifiedCrew;
        }

        // Display crew info summary
        public static void DisplayCrewInfo(List<BaseCriminal> crew)
        {
            // Get crew's skills
            List<int> TotalSkills = new List<int>();
            // Get morale skill bonus
            List<int> MoraleSkillBonus = new List<int>();
            // Get crew's cash
            int TotalCash;

            crew.ForEach(c =>
            {
                TotalSkills.Add(c.BaseSkill);
                MoraleSkillBonus.Add(c.MoraleSkillBonus);
            });

            BaseCriminal player = crew.Find(c => c.IsPlayer);
            TotalCash = player.CrewTotalCash;

            int CurrentSplit = (TotalCash / crew.Count());

            int CrewSkill = TotalSkills.Sum();
            if (player.PlayerContactCount > 0) Console.WriteLine($"Total associates available to hire: {player.PlayerContactCount}");
            Console.WriteLine($@"Total associates in crew: {crew.Count()}");
            Console.WriteLine($"Crew's base skill level: {CrewSkill}");
            if (crew.Count() > 1) Console.WriteLine($"Morale bonus to skill: {MoraleSkillBonus.Sum()}");
            Console.WriteLine("--------");
            Console.WriteLine($"Cash: ${TotalCash}");
            if (crew.Count() > 1) Console.WriteLine($"Current cut: ${CurrentSplit}");
            Console.WriteLine("--------");
        }

        // Display current crew as a list
        public static void DisplayCurrentCrew(List<BaseCriminal> crew)
        {
            Console.WriteLine(Heading.DisplayCrewHeading());
            DisplayCrewInfo(crew);
            List<BaseCriminal> sortedCrew = SortCrew(crew);
            sortedCrew.ForEach(c => {
                if (c.IsPlayer)
                {
                    if (crew.Count() <= 3) Console.WriteLine(c.Face);
                    Console.WriteLine($@"
You: {c.Name}
   base skill: {c.BaseSkill}
 ___________");
                }
                else
                {
                    if (crew.Count() <= 3) Console.WriteLine(c.Face);
                    Console.WriteLine($@"
{c.Name}:
   {c.MoraleDescription}
   base skill: {c.BaseSkill}
___________");
                }
            });
        }

        // Display a shortened list of crew's info
        public static void DisplayCrewInfoShortened(List<BaseCriminal> crew)
        {
            // Get crew's cash
            int TotalCash;

            BaseCriminal player = crew.Find(c => c.IsPlayer);
            TotalCash = player.CrewTotalCash;

            int CurrentSplit = (TotalCash / crew.Count());

            Console.WriteLine($@"Total associates in crew: {crew.Count()}");
            Console.WriteLine("--------");
            Console.WriteLine($"Cash: ${TotalCash}");
            if (crew.Count() > 1) Console.WriteLine($"Current cut: ${CurrentSplit}");
            Console.WriteLine("--------");
        }

        // Allow player to encourage crew
        public static List<BaseCriminal> EncourageCrew(List<BaseCriminal> crew, bool isSplitMenu)
        {
            // Takes an isSplitMenu because different encouragements require different speeches
            BaseCriminal player = crew.Find(c => c.IsPlayer);
            if (player.HasPlayerEncouragedCrew == false)
            {
                Random r = new Random();
                crew.ForEach(c =>
                {
                    if (!c.IsPlayer)
                    {
                        int improvedMorale = c.Morale + r.Next(4, 25);
                        if (improvedMorale > c.MoraleMax) c.Morale = c.MoraleMax;
                        else c.Morale = improvedMorale;
                    }
                    if (c.IsPlayer) c.HasPlayerEncouragedCrew = true;
                });
                DisplayEncouragingSpeech(player, isSplitMenu);
                return crew;
            }
            return crew;
        }

        // Display player's speech
        public static void DisplayEncouragingSpeech(BaseCriminal player, bool isSplitMenu)
        {
            Color.SuccessGreen();
            Console.WriteLine(Heading.DisplaySubheadingSpeech());
            Console.WriteLine(player.Face);
            Console.WriteLine("");
            // for the speeches, get names of the crew members and saying something dramatic about each person
            // will need to make some random compliments
            if (isSplitMenu == false)
            {
                Console.WriteLine("You give a big speech complimenting the crew's skills.\n");
                Console.WriteLine("You promise everyone will get rich.");
            }

            if (isSplitMenu == true)
            {
                Console.WriteLine("You give a big speech congratulating the crew's heist expertise.");
            }

            Console.WriteLine("");
            Console.WriteLine("Crew morale increased.");
            Console.WriteLine("");

            Menu.Continue();
        }
    }
}
