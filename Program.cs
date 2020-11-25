using System;
using System.Collections.Generic;
using System.Linq;

// TAKE USER TO SPLIT THE CASH MENU

// PLAYER NEEDS A COUNTER FOR HOW MANY CRIMINALS PLAYER CAN HIRE
// LIke start at 10, have a counter that tells you how many more criminals you can hire
// So that way you can't kill and hire infinitely


// Show different locations with ASCII art to rob
    // LOCATION? (dif 0 - 300) ($100 - $2,000)
    // Nashville Software School (dif 0 - 300) ($50-$2,000)
        // They've probably got some computers. Bunch of nerds.
    // Bank of America (dif 300 - 900) ($5,000 - $10,000)
        // {NAME} has cased the place. This will be the toughest job, but banks have all the money, right?

// Crew Select
    // Trust random from 10 - 50 (max of 100)
        // After every succesful heist, do a trust check for each crew member
        // IF any criminal DOES turn, it lowers everyone's trust by -20
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

            // While there are levels not yet completed, allow user to continue selecting levels
            List<Location> LocationsLeftToRob = Locations.Where(l => l.Completed == false).ToList();

            // Put while loop instide the LevelSelect function. 
            while (LocationsLeftToRob.Count() > 0)
            {
                LevelSelect(currentCrew, LocationsLeftToRob);

                LocationsLeftToRob = Locations.Where(l => l.Completed == false).ToList();
            }

            Console.WriteLine("ALL HEISTS COMPLETED");
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

            // Iterate through locations, for those that are not completed,
                // Check if it's name has been completed, if not, show that option
            locations.ForEach(l =>
            {
                if (!l.Completed)
                {
                    if (l.Name == "Annoying Neighbor's House") Console.WriteLine("2) stakeout Annoying Neighbor's House");
                    if (l.Name == "Corner 7-Eleven") Console.WriteLine("3) stock-up at Corner 7-Eleven");
                }
            });
            
            int selection = MenuInput(3);

            switch (selection)
            {
                case 1:
                    ManageCrew(crew, locations);
                    break;
                case 2:
                    // Annoying Neighbor
                    StakeOutLocation(crew, locations, 2);
                    break;
                case 3:
                    // 7-Eleven
                    StakeOutLocation(crew, locations, 3);
                    break;
            }
        }

        static void StakeOutLocation(List<Criminal> crew, List<Location> locations, int userSelected)
        // Must always return the current crew and the current locations
        // LocationInfo names MUST match those in Location.cs
        {
            Console.Clear();

            switch (userSelected)
            {
                case 2:
                    string houseSelected = "Annoying Neighbor's House";
                    // If the house hasn't been completed, add it to list
                    List<Location> isHouseCompleted = locations.Where(l => l.Name == houseSelected && !l.Completed).ToList();

                    if (isHouseCompleted.Count() == 1)
                    {
                        LocationInfo(locations, houseSelected, crew);
                    }
                    else
                    {
                        LevelSelect(crew, locations);
                    }
                    break;
                case 3:
                    string gasSelected = "Corner 7-Eleven";

                    List<Location> isGasCompleted = locations.Where(l => l.Name == gasSelected && !l.Completed).ToList();

                    if (isGasCompleted.Count() == 1)
                    {
                        LocationInfo(locations, gasSelected, crew);
                    }
                    else
                    {
                        LevelSelect(crew, locations);
                    }
                    break;
            }
        }

        static void LocationInfo(List<Location> locations, string locName, List<Criminal> crew)
        {
            Console.Clear();
            List<Location> loc = locations.Where(l => l.Name == locName).ToList();

            Console.WriteLine(@"
-------------------------------
▄▀▀ ▀█▀ ▄▀▄ █▄▀ ██▀ ▄▀▄ █ █ ▀█▀
▄██  █  █▀█ █ █ █▄▄ ▀▄▀ ▀▄█  █ 
-------------------------------");

            DisplayCrewInfo(crew);
            loc.ForEach(l => Console.WriteLine($@"
{l.Image}

{l.Name}
-----
{l.Summary}
{l.DifficultyDescription}
"));

            Console.WriteLine("1) keep watching from van");
            Console.WriteLine("2) begin heist");
            Console.WriteLine("3) return to planning");
            int selection = MenuInput(3);

            switch (selection)
            {
                case 1:
                // Waiting, add/subtract a random value from -50 to +50
                int r = new Random().Next(-50, 51);
                List<Location> updatedLocations = locations.Select(l =>
                {
                    if (l.Name == locName)
                    {
                        // NEED TO ENSURE DIFFICULTY CAN NEVER BE BELOW THE MIN FOR THAT LOCATION
                        // PROBABLY NEED A CALCULATE/COMPUTATED PROPERTY?
                        l.Difficulty = l.Difficulty + r;
                    }
                    return l;
                }).ToList();
                    LocationInfo(updatedLocations, locName, crew);
                    break;
                case 2:
                    BeginHeist(crew, locations, locName);
                    break;
                case 3:
                    LevelSelect(crew, locations);
                    break;
            }
        }

        static void BeginHeist(List<Criminal> crew, List<Location> locations, string locName)
        {
            List<Criminal> crewSuccess = crew;

            List<Location> updatedLocations = locations.Select(l =>
            {
                if (l.Name == locName)
                {
                    // When we have the current location
                    // Compare the location's difficulty with the crew's max skills
                    double crewTotalSkill = crew.Sum(c => c.SkillLevel);
                    
                    // If the crew succeeds, add the total cash to each crew member
                        // This is to save the cash, it will be split later
                    if (l.Difficulty < crewTotalSkill)
                    {
                        crewSuccess = crew.Select(c => 
                        {
                            c.CrewTotalCash = c.CrewTotalCash + l.Cash;
                            return c;
                        }).ToList();

                        l.Completed = true;
                    }

                    return l;
                }
                else
                {
                    return l;
                }
            }).ToList();

            LevelSelect(crewSuccess, updatedLocations);
        }

        static void ManageCrew(List<Criminal> crew, List<Location> locations)
        // Must always return the current crew and the current locations
        {
            Console.Clear();

            List<Criminal> updatedCrew = crew;

            DisplayCurrentCrew(updatedCrew);
   
            Criminal player = crew.Find(c => c.IsPlayer);
            if (player.PlayerContactCount > 0) Console.WriteLine("1) recruit crew member");
            if (crew.Count() > 1) Console.WriteLine("2) ice crew member");
            Console.WriteLine("3) return to planning");           
            
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
            Console.Write("Enter name of who you will ice [leave blank to cancel]: ");
            string name = Console.ReadLine();

            int subtractTrust = new Random().Next(1, 31);

            List<Criminal> iceMember = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

            // Only lower trust if we have iced a crew member
            if (iceMember.Count() < crew.Count()) 
            {
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
            else
            {
                return iceMember;
            }

        }

        static int MenuInput(int maxOptions)
        // Ensures user can only enter a number between 1 and maxOptions for this menu
        {
            // Declares variable we will be re-assigning 
            int entered;
            string message = "Enter number to perform action: ";
            // When user first enters the skill input, ensure they type only a number
            while(true)
            {
                try
                {
                    Console.Write(message);
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
                    Console.Write(message);
                    entered = int.Parse(Console.ReadLine());
                }
                catch {}
            }

            return entered;
        }

        static void DisplayCrewInfo(List<Criminal> crew)
        {
            // Get entire crew's skills
            List<int> TotalSkills = new List<int>();
            // Get crew's cash
            int TotalCash;

            crew.ForEach(c =>
            {
                TotalSkills.Add(c.BaseSkill);
            });

            Criminal player = crew.Find(c => c.IsPlayer);
            TotalCash = player.CrewTotalCash;

            int CurrentSplit = (TotalCash / crew.Count());

            int CrewSkill = TotalSkills.Sum();
            int MaxCrewSkill = (TotalSkills.Count() * 100);
            if (player.PlayerContactCount > 0) Console.WriteLine($"Total associates available to hire: {player.PlayerContactCount}");
            Console.WriteLine($@"Total associates in crew: {crew.Count()}");
            Console.WriteLine($"Crew skill level: {CrewSkill} / {MaxCrewSkill}");
            Console.WriteLine("");
            Console.WriteLine($"Cash: ${TotalCash}");
            if (crew.Count() > 1) Console.WriteLine($"current cut: ${CurrentSplit}");
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
 base skill: {c.BaseSkill} / 100
 heist skill {c.SkillLevel}
");
                }
                else
                {
                    Console.WriteLine($@"
{c.Face}

{c.Name}
 {c.TrustDescription}

 skill level: {c.BaseSkill} / 100
 heist skill {c.SkillLevel}
 trust: {c.Trust}
");
                }
            });         
        }

        static List<Criminal> CreateCrew(List<Criminal> crew)
        {
            ASCII art = new ASCII();
            bool recruiting = true;
            List<Criminal> newCrew = new List<Criminal>();
            int playerContactsLeft;

            // After player created, can only add new criminals (this is for manage crew view)
            if (crew.Count != 0)
            {
                List<Criminal> updatedCrew = new List<Criminal>();

                // To save the playerContactsLeft count, must loop through
                // the incoming crew, update the player, then resave everyone to
                // a new array
                crew.ForEach(c =>
                {
                    playerContactsLeft = c.PlayerContactCount;
                    if (c.IsPlayer && playerContactsLeft > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine($"{playerContactsLeft} associates left to hire.");
                        updatedCrew.Add(c);
                        updatedCrew.Add(RecruitNewCriminal());
                        c.PlayerContactCount = --playerContactsLeft;
                    }
                    else 
                    {
                        updatedCrew.Add(c);
                    }
                });

                return updatedCrew;
            }
            else
            {
                // Create the player and add them first to the roster
                newCrew.Add(CreatePlayer());

                string hireMessage = "Go solo or hire a crew? [solo/hire]: ";

                Console.WriteLine(hireMessage);
                string solo = Console.ReadLine().ToLower();

                Console.Clear();

                while(solo != "solo" && solo != "hire")
                {
                    Console.Write(hireMessage);
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
                    while(recruiting)
                    {
                        string recruitingMessage = "Continue recruiting? [y/n]: ";
                        // Loop through the newCrew, checking how many
                        // Criminals the player has left to contact
                        List<Criminal> updatedCrew = new List<Criminal>();
                       
                        // Loop over the new criminal list 
                        newCrew.ForEach(c =>
                        {
                            playerContactsLeft = c.PlayerContactCount;
                            if (c.IsPlayer && playerContactsLeft > 0)
                            {
                                updatedCrew.Add(c);
                                updatedCrew.Add(RecruitNewCriminal());
                                c.PlayerContactCount = --playerContactsLeft;
                               
                                Console.WriteLine($"{playerContactsLeft} associates available to contact.");
                                
                                // Check based on if the player wants to continue hiring
                                if (playerContactsLeft > 0)
                                {
                                    Console.WriteLine($"{newCrew.Count()} criminals in crew.");
                                    Console.Write(recruitingMessage);
                                    string response = Console.ReadLine().ToLower();
                                    Console.WriteLine("");

                                    while(response != "y" && response != "n")
                                    {
                                        Console.Write(recruitingMessage);
                                        response = Console.ReadLine().ToLower();
                                    }

                                    recruiting = response == "y" ? true : false;
                                }

                                if (playerContactsLeft == 0) recruiting = false;

                                newCrew = updatedCrew;
                            }
                            else
                            {
                                updatedCrew.Add(c);

                                newCrew = updatedCrew;
                            } 
                        });
                    }
                    return newCrew;
                }
            }
        }

        static Criminal RecruitNewCriminal()
        {
            Console.Write("Enter new criminal's nickname: ");
            string enteredName = Console.ReadLine();
            Criminal newCriminal = new Criminal(enteredName, false);

            Console.WriteLine($@"
{newCriminal.Face}

{newCriminal.Name} recruited!
 base skill: {newCriminal.BaseSkill} / 100
 heist skill {newCriminal.SkillLevel}
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
 skill level: {player.BaseSkill} / 100
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