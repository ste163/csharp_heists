﻿using System;
using System.Collections.Generic;
using System.Linq;

// Locations
    // Nashville Software School (dif 0 - 500) ($50-$2,000)
        // Probably have a lot of computers
    // LOCATION? (dif 0 - 300) ($100 - $2,000)
    // Bank of America (dif 500 - 1000) ($5,000 - $10,000)
        // {NAME} has cased the place. This will be the toughest job, but banks have all the money, right?

// Crew Select
    // Trust random from 10 - 50 (max of 100)
        // After every succesful heist OR a failure, do a trust check for each crew member
        // IF any criminal DOES turn, it lowers everyone's trust by -20
        // ADDING a new crew member after a heist randomizes trust for members by -30 to + 30
            // The crew will say either "Screw the new guy," "Seems like an okay pick," "We really got {Name}?!"
    // Courage Factor (0.1 - 1.0), courage is added to the skill by
        // (((Courage Factor * Skill) / 10) + Skill Level).RoundUp() = Skill Level for Heist
    
// Heist
    // IF Successfull
        // Run trust check for each criminal to see if they're turn on crew
            // If they turn, no money earned for this location
    // IF Failure
        // And arrested - GAME OVER screen

// Splitting the cash view
    // after all heists attempted, go to new view with a list of all criminals
    // Say how much they've earned, and how much everyone's cut is
    // Player can decide to either ICE a crew member, or split the money evenly
        // IF player ices a crew member, then do a trust check to see if anyone is going to turn
            // Turning at this point means shooting another crew member
            // If a crew member shoots another crew member, display a message that this occured
            // then give the player control to decide whether to ice another crew member or split the cash
            // THERE IS A RANDOM CHANCE PLAYER WILL BE SHOT AND DIES
            // REPEAT until game over 

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            DisplayIntro();
            Location getLocations = new Location(); 
            List<Location> locations = getLocations.GenerateAllLocations();
            List<Criminal> currentCrew = CreateCrew(new List<Criminal>());
            ShowCrewCreatedMsg(currentCrew);
            LevelSelect(currentCrew, locations);

            Console.WriteLine("ALL HEISTS COMPLETED/ATTEMPTED");
            Console.WriteLine("GO TO THE SPLIT THE MONEY VIEW");
        }

        static void LevelSelect(List<Criminal> crew, List<Location> locations)
        {
            // While there are levels not yet completed, allow user to continue selecting levels
            List<Location> locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();

            while (locationsLeftToRob.Count() > 0)
            {
                Console.Clear();
                // Only allowed to select the levels you HAVEN'T attempted
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

                locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();
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
            ASCII ASCII = new ASCII();

            List<Location> loc = locations.Where(l => l.Name == locName).ToList();

            Console.WriteLine(ASCII.DisplayStakeout());

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
            bool heistSuccess = false;
            List<Criminal> crewSuccess = crew;

            List<Location> updatedLocations = locations.Select(l =>
            {
                // find the selected location
                if (l.Name == locName)
                {
                    // When we have the current location
                    // Compare the location's difficulty with the crew's max skills
                    double crewTotalSkill = crew.Sum(c => c.SkillLevel);
                    
                    
                    // If the crew succeeds, add the total cash to each crew member
                        // This is to save the cash, it will be split later
                    if (l.Difficulty <= crewTotalSkill)
                    {
                        crewSuccess = crew.Select(c => 
                        {
                            // Every crew member gets a random skill+
                            int skillIncrease = new Random().Next(8, 38);
                            int trustIncrease = new Random().Next(10, 40);

                            c.CrewTotalCash = c.CrewTotalCash + l.Cash;
                            c.BaseSkill = c.BaseSkill + skillIncrease;
                            c.Trust = c.Trust + trustIncrease;
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

        static void HeistSuccess(List<Criminal> crew, List<Location> locations, string locName)
        {
            Console.Clear();
            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplayHeistSuccess());
            Console.WriteLine(ASCII.DisplayMoney());
            Console.WriteLine(ASCII.DisplaySuccessOveriew());

            Location currentLocation = locations.Find(l => l.Name == locName);
            Criminal player = crew.Find(c => c.IsPlayer);
            int cashEarned = player.CrewTotalCash;

            // Display how much money we earned from location
            Console.WriteLine($"Crew stole: ${currentLocation.Cash}");
            // earned total
            if (cashEarned != currentLocation.Cash) Console.WriteLine($"Total cash: ${cashEarned}");
            // how much the current split will be if it's more than one crew member
            if (crew.Count() > 1)
            {
                Console.WriteLine("----");
                Console.WriteLine($"The split will be: ${cashEarned / crew.Count()}");
                Console.WriteLine("----");
                Console.WriteLine("Crew skill increased");
                Console.WriteLine("Crew morale increased");
            }
            else if (crew.Count() == 1)
            {
                Console.WriteLine("Your skill increased");
            }
            Console.WriteLine("---------------------");
            Console.Write("Press any key to continue");
            Console.ReadLine();
            LevelSelect(crew, locations);
        }

        static void HeistFailure(List<Criminal> crew, List<Location> locations)
        {
            Console.Clear();
            string msg = "Press any key to continue";
            string moraleMsg = "Crew morale decreased";
            int r = new Random().Next(1, 3);
            ASCII ASCII = new ASCII();
           
            if (r == 1)
            {
                // If it's only the player, arrest the player
                // and show the game over message
                // If it's more than the player
                // arrest a random crew member
                    // get the count for the crew
                    // then a random index value from
                    // r = new Random().Next(2, (crew.Count() + 1))
                // Display the person's name and picture at index R
                    // crew.Remove([whoever is at index[r]])
                // Lower everyone's trust by 10, 30
                Console.WriteLine(ASCII.DisplayHeadingArrested());
                Console.WriteLine(ASCII.DisplaySubheadingArrested());
                Console.WriteLine(ASCII.DisplayArrested());
                if (crew.Count() > 1) Console.WriteLine(moraleMsg);
                Console.Write(msg);
                Console.ReadLine();
                LevelSelect(crew, locations);
            }
            else if (r == 2)
            {
                // Lower everyone's trust 10, 20
                Console.WriteLine(ASCII.DisplayHeadingEscaped());
                Console.WriteLine(ASCII.DisplaySubheadingEscaped());
                Console.WriteLine(ASCII.DisplayPoliceCar());
                if (crew.Count() > 1) Console.WriteLine(moraleMsg);
                Console.Write(msg);
                Console.ReadLine();
                LevelSelect(crew, locations);
            }
        }

        static void ManageCrew(List<Criminal> crew, List<Location> locations)
        // Must always return the current crew and the current locations
        {
            Console.Clear();

            List<Criminal> updatedCrew = crew;

            DisplayCurrentCrew(updatedCrew);
   
            Criminal player = crew.Find(c => c.IsPlayer);
            Console.WriteLine();
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
            if (crew.Count() > 1)
            {
                string name = "not empty";

                List<Criminal> updatedCrew = new List<Criminal>();

                while (name != "" && crew.Count() > 1)
                {
                    Console.Clear();
                    ASCII ASCII = new ASCII();
                    Console.WriteLine(ASCII.DisplayIce());
                    Console.WriteLine("Icing a crew member will probably upset the rest of the crew.");
                    Console.WriteLine(ASCII.DisplayGun());

                    crew.ForEach(c =>
                    {
                        if (c.IsPlayer == false)
                        {
                            Console.WriteLine($"{c.Name} - {c.TrustDescription}");
                        }
                    });
                    Console.WriteLine();
                    Console.Write("Enter name of who you will ice [leave blank to cancel]: ");
                    name = Console.ReadLine();

                    // Make a new list of criminals WITHOUT the iced crew member
                    List<Criminal> iceMember = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

                    // Only lower trust if we have iced a crew member
                    if (iceMember.Count() < crew.Count()) 
                    {
                        List<Criminal> newCrew = iceMember.Select(c =>
                        {
                            int subtractTrust = new Random().Next(1, 31);

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

                        crew = newCrew;
                        
                    }
                    else
                    {
                        crew = iceMember;
                    }
                }
                return crew;
            }
            else return crew;
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
                    Console.WriteLine($@"{c.Face}

You: {c.Name}
------------
 base skill: {c.BaseSkill} / 100
 bonus skill: {c.SkillLevel}");
                }
                else
                {
                    Console.WriteLine($@"{c.Face}

{c.Name}
{c.TrustDescription}
------------
 base skill: {c.BaseSkill} / 100
 bonus skill: {c.SkillLevel}");
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
                        Console.WriteLine("");
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
                                Console.WriteLine("");
                                Console.WriteLine($"{playerContactsLeft} associates available to contact.");
                                
                                // Check based on if the player wants to continue hiring
                                if (playerContactsLeft > 0)
                                {
                                    Console.WriteLine($"{newCrew.Count()} criminals in crew.");
                                    Console.Write(recruitingMessage);
                                    string response = Console.ReadLine().ToLower();

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

        static void ShowCrewCreatedMsg(List<Criminal> crew)
        {
            ASCII ASCII = new ASCII();
            Console.Clear();
            Console.WriteLine(ASCII.DisplayCrewCreated());
            crew.ForEach(c =>
            {
                if (crew.Count() <= 3) Console.WriteLine($@"{c.Face}
                ");
                if (c.IsPlayer == true)
                {
                    Console.WriteLine($@"You: {c.Name}");
                    Console.WriteLine($" base skill level: {c.BaseSkill} / 100");
                } 
                
                if (c.IsPlayer == false)
                {
                    Console.WriteLine($@"{c.Name}");
                    Console.WriteLine($@" {c.TrustDescription}
 base skill level: {c.BaseSkill} / 100");
                }
                Console.WriteLine("");
            });
            
            Console.Write("Press any key to begin planning heists");
            Console.ReadLine();
        }

        static Criminal RecruitNewCriminal()
        {
            Console.Write("Enter new criminal's nickname: ");
            string enteredName = Console.ReadLine();
            Criminal newCriminal = new Criminal(enteredName, false);

            Console.WriteLine($@"{newCriminal.Face}

{newCriminal.Name} recruited!
------------
 base skill: {newCriminal.BaseSkill} / 100
 heist skill {newCriminal.SkillLevel}");

            return newCriminal;
        }

        static Criminal CreatePlayer()
        {
            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplayWhoAreYou());
            Console.Write("Enter your name: ");

            string playerName = Console.ReadLine();
            Criminal player = new Criminal(playerName, true);

            Console.WriteLine($@"{player.Face}

{player.Name}
------------
 skill level: {player.BaseSkill} / 100
 ");
            return player;
        }

        static void DisplayIntro()
        {
            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplayTitle());
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