using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            DisplayIntro();
            Location getLocations = new Location(); 
            List<Location> Locations = getLocations.GenerateAllLocations();
            List<Criminal> currentCrew = CreateCrew(new List<Criminal>());

            // While there are still levels not yet completed
                // continue the select & related scripts
                // Need to create a Level/Heist class with a property of completed
    
            LevelSelect(currentCrew, Locations);

            // Show different locations with ASCII art to rob
                // Annoying Neighbor's House (dif 0 - 50) ($100 - $100,000)
                    // Summary: My neighbor is almost always outside and wants to talk. Luckily
                                // my neighbor talks about hating banks. Maybe there is money in the house.
                // 7-Eleven (dif 0 - 100) ($50 - $1,000)
                    // Summary: {Random Crew Member, if we have a crew} works part-time here. Usually cash in the register.
                        // Should be an easy heist. 
                        // We can get some snacks, too.
                // LOCATION? (dif 0 - 300) ($100 - $2,000)
                // Nashville Software School (dif 0 - 300) ($50-$2,000)
                    // They've probably got some computers. Bunch of nerds.
                // Bank of America (dif 300 - 900) ($5,000 - $10,000)
                    // {NAME} has cased the place. This will be the toughest job, but banks have all the money, right?

            // Crew Select
                // User types names
                // Skill is randomly generated from 1 - 50 (max of 100)
                // Trust random from 10 - 50 (max of 100)
                    // After every succesful heist, do a trust check for each crew member
                    // IF any criminal DOES turn, it lowers everyone's trust by -20
                    // The Trust level is shown to user as a sentence about how the member is feeling
                        // Trust 1 - 10 - always try to shoot another member and take their money
                        // Trust 11 - 20 - high chance of shooting another member and taking their money
                        // Trust 21 - 30 - low chance of turning on crew
                        // Trust 30 - 40 - very low chance of turning on crew, 0.1 Courage
                        // Trust 41 - 49 - almost no chance of turning on crew, 0.2 Courage
                        // Trust 50 - 59 - no chance of turning, 0.3 courage
                        // Trust 60 - 69 - 0.4 Courage
                        // Trust 70 -79 - 0.5 Courage
                        // Trust 80 - 89 - 0.7 Courage
                        // Trust 90 - 99 - 0.9 Courage
                        // Trust 100 - 1.0 Courage
                    // INCREASES after every successful heist by +30
                    // ADDING a new crew member after a heist randomizes trust for members by -30 to + 30
                        // The crew will say either "Screw the new guy," "Seems like an okay pick," "We really got {Name}?!"
                    // Icing a crew member lowers trust by - 70. Can Ice multiple crew members at a time to increase cash.
                // Courage Factor (0.1 - 1.0), courage is added to the skill by
                    // (((Courage Factor * Skill) / 10) + Skill Level).RoundUp() = Skill Level for Heist
                // Each crew member gets a random ASCII face

            // Level Select
                // View of ASCII city skyline
                // Display for how much money you've currently stolen
                // Have option to check on crew, see their stats, ice someone, or recruit new people
                // With heading underneath say choose a location to rob
                    // 1. Annoying Neighbors house...
                    // Selecting any location will take you to the big view of the ASCII art for that location
                    // With a summary about it, telling you what to expect
                        // Current Status: sentence about what it appears to look like (a hint for current difficulty)
                        // Ability to stake out to see if the situtation will change
                        // Option to Rob the place
                        // Option to return to City view 
                
            // Heist
                // DISPLAYs sum of the crew's skills
                    // Difficulty of the location
                // New ascii art??
                // Show everyone's name and face?
                // IF Successfull
                    // Run trust check for each criminal to see if they're turn
                    // Show how much total cash you got, then everyone's cut
                // IF Failure
                    // Chance based on a skill check roll for if any crew members died in the cross-fire/got arrested
                        // instead of you
                    // If no one else was arrested chance for you to be arrested and the game over
                    // 10% chance of everyone getting away okay

            // If the sum of their skills is greater than the locations's difficulty
                // display a success message (ASCII of bag of money) - say how much $$ was stolen
                // Go back to the location select screen, but with the last place removed
        }

        static void LevelSelect(List<Criminal> crew, List<Location> locations)
        {
            Console.Clear();
            // Only allowed to select the levels you HAVEN'T already robbed
            ASCII art = new ASCII();
            Console.Write(art.DisplayPlanning());
            DisplayCrewInfo(crew);
            Console.WriteLine(art.DisplayNashville());
            Console.WriteLine("1) manage crew");
            Console.WriteLine("2) recon Annoying Neighbor's House");
            Console.WriteLine("3) stock-up at corner 7-Eleven");
            int selection = MenuInput(3);

            switch (selection)
            {
                case 1:
                    ManageCrew(crew, locations);
                    break;
                case 2:
                    // Annoying Neighbor
                    ReconnoiterLocation(crew, locations, 2);
                    break;
                case 3:
                    // 7-Eleven
                    ReconnoiterLocation(crew, locations, 3);
                    break;
            }
        }

        static void ReconnoiterLocation(List<Criminal> crew, List<Location> locations, int userSelected)
        // Must always return the current crew and the current locations
        // LocationInfo names MUST match those in Location.cs
        {
            Console.Clear();
            switch (userSelected)
            {
                case 2:
                    string houseSelected = "Annoying Neighbor's House";
                    LocationInfo(locations, houseSelected, crew);
                    break;
                case 3:
                    string gasSelected = "Corner 7-Eleven";
                    LocationInfo(locations, gasSelected, crew);
                    break;
            }
        }

        static void LocationInfo(List<Location> locations, string locName, List<Criminal> crew)
        {
            List<Location> loc = locations.Where(l => l.Name == locName).ToList();
            // EACH LOCATION NEEDS THE HEADER TEXT
            DisplayCrewInfo(crew);
            loc.ForEach(l => Console.WriteLine($@"
{l.Image}

{l.Name}
{l.Summary}
DIFFICULTY:{l.Difficulty}
${l.Cash}"));

            Console.WriteLine("1) stay in van and watch location.");
            Console.WriteLine("2) begin heist");
            Console.WriteLine("3) return to planning");
            int selection = MenuInput(3);

            switch (selection)
            {
                case 1:
                // Get the currently selected location
                // Generate a random number between 50 and 100 to add or subtract
                // from the difficulty
                // Update the locations to a new variable
                // Final step is to move the location into the LocationInfo()
                    Console.WriteLine("WAITING");
                    break;
                case 2:
                    Console.WriteLine("BEGIN");
                    break;
                case 3:
                    LevelSelect(crew, locations);
                    break;
            }
        }

        static void ManageCrew(List<Criminal> crew, List<Location> locations)
        // Must always return the current crew and the current locations
        {
            Console.Clear();

            List<Criminal> updatedCrew = crew;

            DisplayCurrentCrew(updatedCrew);
            // Display their face, name, and the how they're doing text based on trust
   
            if (crew.Count() > 1)
            {
                Console.WriteLine("1) recruit crew member");
                Console.WriteLine("2) ice crew member");
                Console.WriteLine("3) return to planning");
            } else
            {
                Console.WriteLine("1) recruit crew member");
                Console.WriteLine("3) return to planning");                
            }

            int input = MenuInput(3);

            switch (input)
            {
                case 1:
                    updatedCrew = CreateCrew(updatedCrew);
                    ManageCrew(updatedCrew, locations);
                    break;
                case 2:
                    updatedCrew = IceCrewMember(updatedCrew);
                    ManageCrew(updatedCrew, locations);           
                    break;
                case 3:
                    LevelSelect(updatedCrew, locations);
                    break;
            }
        }

        static List<Criminal> IceCrewMember(List<Criminal> crew)
        {
            Console.Write("Enter the name of the person you'd like to ice: ");
            string name = Console.ReadLine();

            int subtractTrust = new Random().Next(1, 31);

            List<Criminal> iceMember = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

            List<Criminal> newCrew = iceMember.Select(c =>
            {
                int loweredTrust = c.Trust - subtractTrust;
                if (loweredTrust < 0)
                {
                    c.Trust = 0;
                }
                else
                {
                    c.Trust = loweredTrust;
                }
                return c;
            }).ToList();

            return newCrew;
        }

        static int MenuInput(int maxOptions)
        // Ensures user can only enter a number between 1 and maxOptions for this menu
        {
            // Declares variable we will be re-assigning 
            int entered;
            // When user first enters the skill input, ensure they type only a number
            while(true)
            {
                try
                {
                    Console.Write("Enter number to perform action: ");
                    entered = int.Parse(Console.ReadLine());
                    break;
                }
                catch {}
            }
            // After user has entered a number, if it is less than or equal to 0, user must re-enter number
            while(entered <= 0 || entered > maxOptions)
            {
                try
                {
                    Console.Write("Enter number to perform action: ");
                    entered = int.Parse(Console.ReadLine());
                }
                catch {}
            }

            return entered;
        }

        static void DisplayCrewInfo(List<Criminal> crew)
        {
            // Get the entire crew's skills
            List<int> TotalSkills = new List<int>();

            crew.ForEach(c =>
            {
                TotalSkills.Add(c.SkillLevel);
            });

            int CrewSkill = TotalSkills.Sum();
            int MaxCrewSkill = (TotalSkills.Count() * 100);
            Console.WriteLine($@"Total crew members: {crew.Count()}");
            Console.WriteLine($"Crew skill level: {CrewSkill} / {MaxCrewSkill}");
        }

        static void DisplayCurrentCrew(List<Criminal> crew)
        {
            ASCII art = new ASCII();

            Console.WriteLine(art.DisplayCrewHeading());
            DisplayCrewInfo(crew);
            crew.ForEach(c => {
                if (c.IsPlayer)
                {
                    Console.WriteLine($@"
{c.Face}

You: {c.Name}
 skill level: {c.SkillLevel} / 100
 courage factor: {c.CourageFactor} / 1.0
");
                }
                else
                {
                    Console.WriteLine($@"
{c.Face}

{c.Name}
 skill level: {c.SkillLevel} / 100
 courage factor: {c.CourageFactor} / 1.0
 trust: {c.Trust}
");
                }
            });         
        }

        static List<Criminal> CreateCrew(List<Criminal> crew)
        {
            ASCII art = new ASCII();
            bool recruiting = true;
            // Instantiate empty list of criminals
            List<Criminal> newCrew = new List<Criminal>();

            if (crew.Count != 0)
            {
                Console.WriteLine("");
                crew.Add(RecruitNewCriminal());
                return crew;
            }
            else
            {
                // Create the player and add them first to the roster
                newCrew.Add(CreatePlayer());
                Console.WriteLine("Go solo or hire a crew? [solo/hire]: ");
                // OPTION TO GO SOLO OR HIRE CREW
                string solo = Console.ReadLine().ToLower();

                Console.Clear();

                while(solo != "solo" && solo != "hire")
                {
                    Console.Write("Go solo or hire a crew? [solo/hire]: ");
                    solo = Console.ReadLine().ToLower();
                }

                if (solo == "solo")
                {
                    return newCrew;
                }
                else
                {
                    Console.WriteLine(art.DisplayCrewHire());

                    // While we are recruiting, prompt user to continue recruiting
                    // display current amount of criminals recruited
                    while(recruiting)
                    {
                        newCrew.Add(RecruitNewCriminal());

                        Console.WriteLine($"{newCrew.Count()} criminals in crew.");
                        
                        Console.Write("Continue recruiting? [y/n]: ");
                        string response = Console.ReadLine().ToLower();
                        Console.WriteLine("");

                        while(response != "y" && response != "n")
                        {
                            Console.Write("Continue recruiting? [y/n]: ");
                            response = Console.ReadLine().ToLower();
                        }

                        recruiting = response == "y" ? true : false;
                    }
                    return newCrew;
                }
            }

        }

        static Criminal RecruitNewCriminal()
        {
            Console.Write("Enter new criminal's nickname: ");
            // User can enter any string, including a string of numbers
            string enteredName = Console.ReadLine();
            Criminal newCriminal = new Criminal(enteredName, false);

            Console.WriteLine($@"
{newCriminal.Face}

{newCriminal.Name} recruited!
 skill level: {newCriminal.SkillLevel} / 100
 courage factor: {newCriminal.CourageFactor} / 1.0
 trust: {newCriminal.Trust}
");

            return newCriminal;
        }

        static Criminal CreatePlayer()
        {
            Console.WriteLine(@"
-----------------------------------------
█   █ █▄█ ▄▀▄   ▄▀▄ █▀▄ ██▀   ▀▄▀ ▄▀▄ █ █
▀▄▀▄▀ █ █ ▀▄▀   █▀█ █▀▄ █▄▄    █  ▀▄▀ ▀▄█
-----------------------------------------");
            Console.Write("Enter your name: ");

            string playerName = Console.ReadLine();
            Criminal player = new Criminal(playerName, true);

            Console.WriteLine($@"
{player.Face}

{player.Name}
 skill level: {player.SkillLevel} / 100
 courage factor: {player.CourageFactor} / 1.0
");
            return player;
        }

        static void DisplayIntro()
        {
            Console.WriteLine(@"


  ______    __  __        __    __          __            __              
 /      \  |  \|  \      |  \  |  \        |  \          |  \             
|  ▓▓▓▓▓▓\_| ▓▓| ▓▓_     | ▓▓  | ▓▓ ______  \▓▓ _______ _| ▓▓_    _______ 
| ▓▓   \▓▓   ▓▓  ▓▓ \    | ▓▓__| ▓▓/      \|  \/       \   ▓▓ \  /       \
| ▓▓      \▓▓▓▓▓▓▓▓▓▓    | ▓▓    ▓▓  ▓▓▓▓▓▓\ ▓▓  ▓▓▓▓▓▓▓\▓▓▓▓▓▓ |  ▓▓▓▓▓▓▓
| ▓▓   __|   ▓▓  ▓▓ \    | ▓▓▓▓▓▓▓▓ ▓▓    ▓▓ ▓▓\▓▓    \  | ▓▓ __ \▓▓    \ 
| ▓▓__/  \\▓▓▓▓▓▓▓▓▓▓    | ▓▓  | ▓▓ ▓▓▓▓▓▓▓▓ ▓▓_\▓▓▓▓▓▓\ | ▓▓|  \_\▓▓▓▓▓▓\
 \▓▓    ▓▓ | ▓▓| ▓▓      | ▓▓  | ▓▓\▓▓     \ ▓▓       ▓▓  \▓▓  ▓▓       ▓▓
  \▓▓▓▓▓▓   \▓▓ \▓▓       \▓▓   \▓▓ \▓▓▓▓▓▓▓\▓▓\▓▓▓▓▓▓▓    \▓▓▓▓ \▓▓▓▓▓▓▓                                                                                     


 ▄▀▄ █▄ █ ██▀   █▀▄ ▄▀▄ ▀▄▀     █▀ █ █ █ ██▀   █▄█ ██▀ █ ▄▀▀ ▀█▀ ▄▀▀     ▄▀▄ █▄ █ ██▀   ▄▀▀ █▀▄ █   █ ▀█▀
 ▀▄▀ █ ▀█ █▄▄   █▄▀ █▀█  █  █   █▀ █ ▀▄▀ █▄▄   █ █ █▄▄ █ ▄██  █  ▄██ █   ▀▄▀ █ ▀█ █▄▄   ▄██ █▀  █▄▄ █  █ 

---------------------------------------------------------------------------------------------------------
");
        }
    }
}

// INPUTS FOR PUTTING IN YOUR OWN SKILL OR COURAGE
    // THIS WAS BEFORE THEY BECAME RANDOMIZED
        // static int InputSkill()
        // {
        //     // Declares variable we will be re-assigning 
        //     int entered;
        //     // When user first enters the skill input, ensure they type only a number
        //     while(true)
        //     {
        //         try
        //         {
        //             Console.Write("Enter new criminal's skill level: ");
        //             entered = int.Parse(Console.ReadLine());
        //             break;
        //         }
        //         catch
        //         {
        //             Console.WriteLine("Must enter a whole number.");
        //         }
        //     }
        //     // After user has entered a number, if it is less than or equal to 0, user must re-enter number
        //     while(entered <= 0)
        //     {
        //         try
        //         {
        //             Console.Write("Enter whole number greater than zero: ");
        //             entered = int.Parse(Console.ReadLine());
        //         }
        //         catch(FormatException)
        //         {
        //             Console.WriteLine("Must enter a whole number.");
        //         }
        //     }

        //     return entered;
        // }

        // static double InputCourage()
        // {
        //     double entered;

        //     while(true)
        //     {
        //         try
        //         {
        //             Console.Write("Enter new criminal's courage (0.0 - 2.0): ");
        //             entered = double.Parse(Console.ReadLine());
        //             break;
        //         }
        //         catch
        //         {
        //             Console.WriteLine("Must enter number.");
        //         }
        //     }

        //     while(entered < 0.0 || entered > 2.0)
        //     {
        //         try
        //         {
        //             Console.Write("Enter number between 0.0 - 2.0: ");
        //             entered = double.Parse(Console.ReadLine());
        //         }
        //         catch(FormatException)
        //         {
        //             Console.WriteLine("Must enter a number.");
        //         }
        //     }

        //     return entered;
        // }