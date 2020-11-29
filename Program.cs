﻿using System;
using System.Collections.Generic;
using System.Linq;

// C# Heists
    // is a text-based heist adventure game created to learn the basics of C#.
    // The disorganized code is because I had not learned proper C# project management at the time.
    // I am fully aware no project should ever be written in such a gigantic file.

// BUGS
    // If a crew member shoots someone, it doesn't lower morale
    // On attempting to split cash - the check for who gets to shoot is NOT random, it always follows the same for each loop order

namespace heist
{
    class Program
    {

        static void Main(string[] args)
        {
            StartGame();
        }

        static void StartGame()
        {
            Console.Clear();
            DisplayIntro();
            Location getLocations = new Location(); 
            List<Location> locations = getLocations.GenerateAllLocations();
            List<Criminal> currentCrew = CreateCrew(new List<Criminal>());
            ShowCrewCreatedMsg(currentCrew);
            LevelSelect(currentCrew, locations);
            // From level select, go to the split money view, then game over
        }

        static void LevelSelect(List<Criminal> crew, List<Location> locations)
        {
            // While there are levels not yet completed, allow user to continue selecting levels
            List<Location> locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();
            
            Criminal player = crew.Find(c => c.IsPlayer);

            while (locationsLeftToRob.Count() > 0)
            {
                Console.Clear();
                ASCII art = new ASCII();
                Console.Write(art.DisplayPlanning());
                DisplayCrewInfo(crew);
                Console.WriteLine(art.DisplayNashville());

                if (player.PlayerContactCount == 0 && crew.Count() == 1)
                {
                    Console.WriteLine("No crew left to manage");
                    Console.WriteLine("______________________");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("1) manage crew");
                    Console.WriteLine("______________");
                    Console.WriteLine("");
                }

                // Iterate through locations, for those that are not completed, then show those options
                locations.ForEach(l =>
                {
                    if (!l.Completed)
                    {
                        if (l.Name == "Annoying Neighbor's House") Console.WriteLine("2) stakeout Annoying Neighbor's House");
                        if (l.Name == "Corner 7-Eleven") Console.WriteLine("3) stock-up at Corner 7-Eleven");
                        if (l.Name == "Welts Fargo") Console.WriteLine("4) stakeout Welts Fargo");
                        if (l.Name == "Pinnackle National Bank") Console.WriteLine("5) stakeout Pinnackle National Bank");
                        if (l.Name == "Bank of Amereeka") Console.WriteLine("6) stakeout Bank of Amereeka");
                    }
                });

                // IF you have ANY money, option 7 is available

                if (player.CrewTotalCash > 0)
                {
                    Console.WriteLine("____________");
                    Console.WriteLine("");
                    if (crew.Count() == 1)
                    {
                        Console.WriteLine("7) end heist spree");
                    }
                    else
                    {
                        Console.WriteLine("7) end heist spree and split cash");
                    }
                }
                
                // Check to ensure user typed only a number
                int selection = MenuInput(7);

                switch (selection)
                {
                    case 1:
                        if (player.PlayerContactCount == 0 && crew.Count() == 1) LevelSelect(crew, locations);
                        else ManageCrew(crew, locations);
                        break;
                    case 2:
                        // Annoying Neighbor
                        StakeOutLocation(crew, locations, 2);
                        break;
                    case 3:
                        // 7-Eleven
                        StakeOutLocation(crew, locations, 3);
                        break;
                    case 4:
                        // Welts Fargo
                        StakeOutLocation(crew, locations, 4);
                        break;
                    case 5:
                        // Pinnackle
                        StakeOutLocation(crew, locations, 5);
                        break;
                    case 6:
                        // Amereeka
                        StakeOutLocation(crew, locations, 6);
                        break;
                    case 7:
                        // End game - split cash
                        if (player.CrewTotalCash > 0) SplitCash(PrepCrewForEndGame(crew),locations);
                        break;
                }

                locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();
            }

            SplitCash(PrepCrewForEndGame(crew),locations);     
        }

        static List<Criminal> PrepCrewForEndGame(List<Criminal> crew)
        {
            // Ensure player can give a speech
            // Lower everyone's morale 20-30 points each because all that money is tempting.
            Random r = new Random();
            return crew.Select(c => {
                c.HasPlayerEncouragedCrew = false;
                c.Morale = c.Morale - r.Next(25, 41);
                return c;
            }).ToList();
        }

        static void SplitCash(List<Criminal> crew, List<Location> locations)
        {
            // get crew cash
            int totalCash = 0;
            crew.ForEach(c => totalCash = c.CrewTotalCash);
            Criminal player = crew.Find(c => c.IsPlayer);

            if (totalCash > 0)
            {
                // If there is a crew, split cash. Else, end game
                if (crew.Count() > 1)
                {
                    Console.Clear();
                    ASCII art = new ASCII();
                    // Display headings and crew info
                    Console.WriteLine(art.DisplayHeadingSplit());
                    Console.WriteLine(art.DisplaySubheadingSplit());
                    DisplayCrewInfoShortened(crew);
                    // Money bag image
                    Console.WriteLine(art.DisplayMoneyBag());

                    // Display crews name and current morale status
                    Console.WriteLine("Crew");
                    Console.WriteLine("-----");
                    crew.ForEach(c => 
                        {
                            if (!c.IsPlayer)
                            {
                                Console.WriteLine($"{c.Name} - {c.MoraleDescription}");
                            }
                        });

                    Console.WriteLine("");
                    Console.WriteLine("The more money you take, the better off you'll be when you skip town.");
                    Console.WriteLine("");
                    Console.WriteLine("1) attempt to split cash evenly among crew members and part ways");
                    Console.WriteLine("2) ice a crew member");
                    if (player.HasPlayerEncouragedCrew == false ) Console.WriteLine("3) give congratulations speech");

                    int select = MenuInput(3);

                    switch (select)
                    {
                        case 1:
                        // Player gives up chance to shoot. Otherwise the player will always have the chance to ice the last person
                            WillCrewMemberShoot(crew, locations, true);
                            GameOver(crew, locations, true, player);
                            break;
                        case 2:
                            // Player ices an associate 
                            List<Criminal> smallerCrew = IceCrewMember(crew, locations, true);
                            if (player.PlayerFiredWeapon) WillCrewMemberShoot(smallerCrew, locations, false);
                            smallerCrew.ForEach(c => 
                            {
                                if (c.IsPlayer) c.PlayerFiredWeapon = false;
                            });
                            SplitCash(smallerCrew, locations);
                            break;
                        case 3:
                            SplitCash(EncourageCrew(crew, true), locations);
                            break;
                    }
                    GameOver(crew, locations, true, player);
                }
                else
                {
                    GameOver(crew, locations, true, player); 
                }
            }
            else
            {
                GameOver(crew, locations, true, player);
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
                    // Use this string to check the locations with 
                    string houseSelected = "Annoying Neighbor's House";
                    // Check if the location has been completed, if not, add to list
                    List<Location> isHouseCompleted = locations.Where(l => l.Name == houseSelected && !l.Completed).ToList();
                    // If location is in list, load the locationInfo
                    if (isHouseCompleted.Count() == 1) LocationInfo(locations, houseSelected, crew);
                    // If it's been completed, return to LevelSelect
                    else LevelSelect(crew, locations);                  
                    break;
                case 3:
                    string gasSelected = "Corner 7-Eleven";
                    List<Location> isGasCompleted = locations.Where(l => l.Name == gasSelected && !l.Completed).ToList();
                    if (isGasCompleted.Count() == 1) LocationInfo(locations, gasSelected, crew);
                    else LevelSelect(crew, locations);                   
                    break;
                case 4:
                    string wfSelected = "Welts Fargo";
                    List<Location> isWfCompleted = locations.Where(l => l.Name == wfSelected && !l.Completed).ToList();
                    if (isWfCompleted.Count() == 1) LocationInfo(locations, wfSelected, crew);
                    else LevelSelect(crew, locations);                   
                    break;
                case 5:
                    string pnbSelected = "Pinnackle National Bank";
                    List<Location> isPnbCompleted = locations.Where(l => l.Name == pnbSelected && !l.Completed).ToList();
                    if (isPnbCompleted.Count() == 1) LocationInfo(locations, pnbSelected, crew);
                    else LevelSelect(crew, locations);           
                    break;
                case 6:
                    string boaSelected = "Bank of Amereeka";
                    List<Location> isBoaCompleted = locations.Where(l => l.Name == boaSelected && !l.Completed).ToList();
                    if (isBoaCompleted.Count() == 1) LocationInfo(locations, boaSelected, crew);
                    else  LevelSelect(crew, locations);            
                    break;
            }
        }

        static void LocationInfo(List<Location> locations, string locName, List<Criminal> crew)
        {
            Console.Clear();
            ASCII ASCII = new ASCII();
            // Get the selected location
            Location selectedLoc = locations.Find(l => l.Name == locName);
            // Display the Stakeout heading
            Console.WriteLine(ASCII.DisplayStakeout());
            DisplayCrewInfo(crew);
            // Display location info
            Console.WriteLine($@"
{selectedLoc.Image}

{selectedLoc.Name}
_____

{selectedLoc.Summary}
{selectedLoc.DifficultyDescription}
");
            // Options for stakeouts
            if (selectedLoc.WaitsInVanAvailable >= 1)
            {
                if (selectedLoc.WaitsInVanAvailable > 1) Console.WriteLine($"1) keep watching from van [{selectedLoc.WaitsInVanAvailable} waits left]");
                else Console.WriteLine($"1) keep watching from van [{selectedLoc.WaitsInVanAvailable} wait left]");
            }
            else if (selectedLoc.WaitsInVanAvailable == 0) Console.WriteLine("Come back later to contine staking out from van. Don't wait to raise suspicion");
            Console.WriteLine("2) begin heist");
            Console.WriteLine("3) return to planning");
            int selection = MenuInput(3);
            // Switch based on user input
            switch (selection)
            {
                case 1:
                    LocationInfo(WaitInVan(locations, locName), locName, crew);
                    break;
                case 2:
                    BeginHeist(crew, locations, locName);
                    break;
                case 3:
                    LevelSelect(crew, locations);
                    break;
            }
        }

        static List<Location> WaitInVan(List<Location> locations, string locName)
        {
            // Instantiate a Random object
            Random r = new Random();
            // Loop through locations to find selected one
            locations.ForEach(l =>
            {
                // Find selected location
                if (l.Name == locName)
                {
                    // If we have turns left, randomize the difficulty based on the min & max
                    if (l.WaitsInVanAvailable > 0)
                    {
                        int difficultyModifier = r.Next(-90, 91);
                        // Add the modifier to the current location difficulty
                        int newDifficulty = l.Difficulty + difficultyModifier;
                        // If the difficulty is below the min, set to min
                        if (newDifficulty < l.DifficultyMin) l.Difficulty = l.DifficultyMin;
                        // If the difficulty is above the max, set to max
                        else if (newDifficulty > l.DifficultyMax) l.Difficulty = l.DifficultyMax;
                        // Otherwise, set to the modified value
                        else l.Difficulty = newDifficulty;
                        // Remove 1 wait in van
                        l.WaitsInVanAvailable = --l.WaitsInVanAvailable;
                    }
                }
            });
            
            return locations;
        }

        static void BeginHeist(List<Criminal> crew, List<Location> locations, string locName)
        {
            // Regardless of success/failure, reset all waits in van
            locations.ForEach(l => l.WaitsInVanAvailable = 3);

            bool heistSuccess = false;

            List<Criminal> crewSuccess = crew;

            List<Location> updatedLocations = locations.Select(l =>
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

        static void HeistSuccess(List<Criminal> crew, List<Location> locations, string locName)
        {
            Console.Clear();

            // Morale check before continuing
            WillCrewMemberTurnAfterHeist(crew, locations, locName);

            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplayHeistSuccess());
            Console.WriteLine(ASCII.DisplayMoney());
            Console.WriteLine(ASCII.DisplaySuccessOveriew());

            Location currentLocation = locations.Find(l => l.Name == locName);
            Criminal player = crew.Find(c => c.IsPlayer);
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
            Console.Write("Press any key to continue ");
            Console.ReadLine();
            LevelSelect(crew, locations);
        }

        static void HeistFailure(List<Criminal> crew, List<Location> locations)
        {
            Console.Clear();
            Criminal player = crew.Find(c => c.IsPlayer);
            string msg = "Press any key to continue ";
            string moraleMsg = "Crew morale decreased.";
            // 50-50 chance for arrested or escaped
            Random random = new Random();
            int r = random.Next(1,3);
            ASCII ASCII = new ASCII();
           
            // Arrested
            if (r == 1)
            {
                // Generate arrested summary
                Console.WriteLine(ASCII.DisplayHeadingArrested());
                Console.WriteLine(ASCII.DisplaySubheadingArrested());
                
                // If only player is in crew
                if (crew.Count == 1)
                {
                    crew.ForEach(c => c.IsPlayerArrested = true);
                    Console.WriteLine(ASCII.DisplayArrested());
                    Console.Write(msg);
                    Console.ReadLine();
                    Console.WriteLine();
                    GameOver(crew, locations, true, player);
                }
                // If multiple crew members
                else if (crew.Count > 1)
                {
                    // arrest a random crew member
                    int crewSize = crew.Count();
                    int randomCrewMember = new Random().Next(1, crewSize);

                    Criminal arrestedMember = crew.ElementAt(randomCrewMember);
                    Console.WriteLine("");
                    Console.WriteLine($"{arrestedMember.Face}");
                    Console.WriteLine("");
                    Console.WriteLine($"The cops got {arrestedMember.Name}!");
                    Console.WriteLine(moraleMsg);
                    Console.WriteLine("");
                    Console.Write(msg);
                    Console.ReadLine();

                    crew.RemoveAt(randomCrewMember);
                    // Randomly lower every non-player's morale
                    crew.ForEach(c =>
                    {
                        if (c.IsPlayer == false)
                        {
                            int loweredMorale = c.Morale - new Random().Next(25, 54);
                            if (loweredMorale < c.MoraleMin)
                            {
                                c.Morale = c.MoraleMin;
                            } 
                            else
                            {
                                c.Morale = loweredMorale;
                            }
                        }
                    });
                    // Return to level select menu
                    LevelSelect(crew, locations);
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
                        if (loweredMorale < c.MoraleMin)
                        {
                            c.Morale = c.MoraleMin;
                        } 
                        else
                        {
                            c.Morale = loweredMorale;
                        }
                    }
                });
                // Display escaped summary
                Console.WriteLine(ASCII.DisplayHeadingEscaped());
                Console.WriteLine(ASCII.DisplaySubheadingEscaped());
                Console.WriteLine(ASCII.DisplayPoliceCar());
                if (crew.Count() > 1) Console.WriteLine(moraleMsg);
                Console.WriteLine("");
                Console.Write(msg);
                Console.ReadLine();
                // Return to level select menu
                LevelSelect(crew, locations);
            }
        }

        static void GameOver(List<Criminal> crew, List<Location> locations, bool playerAlive, Criminal player)
        {
            ASCII ASCII = new ASCII();
            Criminal firstIndex = crew[0];
            int cashStolen = firstIndex.CrewTotalCash;
            int playersCut = cashStolen / crew.Count();
            int totalCashAvailable = 0;
            locations.ForEach(l => totalCashAvailable = l.Cash + totalCashAvailable);

            Console.Clear();
            Console.WriteLine(ASCII.DisplayHeadingGameOver());
            Console.WriteLine(ASCII.DisplaySubHeadingSummary());
            Console.WriteLine($"Total cash stolen: ${cashStolen} / ${totalCashAvailable}");

            if (playerAlive)
            {
                // Arrested Ending
                if (player.IsPlayerArrested == true)
                {
                    // If we have a crew
                    if (crew.Count() > 1)
                    {
                        // Minus you from crew.Count
                        Console.WriteLine($"The cut per members not in jail: ${cashStolen / crew.Count() - 1}");
                        DisplayCrewMembersWhoSurvived(crew);
                    }

                    if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");     
                    
                    Console.WriteLine("");
                    Console.WriteLine(ASCII.DisplayArrested());

                    Console.WriteLine("You should have known, crime never pays.");

                    ExitGame();
                }

                if (player.CrewTotalCash == 0)
                {
                    DisplayCrewMembersWhoSurvived(crew);
                    if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");

                    Console.WriteLine(ASCII.DisplaySubheadingWanted());
                    Console.WriteLine($"{player.Name} for multiple attempts at robbery.");
                    Console.WriteLine($"{player.Face}");
                    Console.WriteLine("");
                    Console.WriteLine("You somehow ended up with nothing. You can't even get out of town.");
                    Console.WriteLine("Police all over Tennessee and the ajoining states are hunting you.");
                    Console.WriteLine($"Enjoy your freedom while you can, {player.Name}.");
                    
                    ExitGame();
                }
                // the crew does have cash
                else
                {
                if (crew.Count() > 1)
                {
                    Console.WriteLine($"Cut per member: ${cashStolen / crew.Count()}");

                    DisplayCrewMembersWhoSurvived(crew);

                    if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");               
                }
                
                // Based on how much the playersCut was, display ending message
                if (playersCut > 0 && playersCut <= 10_000) Console.WriteLine(ASCII.DisplayEndingCamp());
                if (playersCut >= 10_001 && playersCut <= 999_999) Console.WriteLine(ASCII.DisplayEndingRoad());
                if (playersCut > 1_000_000) Console.WriteLine(ASCII.DisplayEndingBeach());
                }
            }

            else if (!playerAlive)
            {
                // REMOVE the player from the crew
                crew = crew.Where(c => !c.IsPlayer || !c.isAssociateIced).ToList();

                Console.WriteLine($"The cut per member: ${cashStolen / crew.Count()}");

                if (crew.Count() == 1) Console.WriteLine($"The only survivor was: ");
                else Console.WriteLine($"Crew members who survived:");

                crew.ForEach(c => Console.WriteLine($"  {c.Name}"));     

                if (player.PlayerContactCount > 0) Console.WriteLine($"Number of associates left you could have hired: {player.PlayerContactCount}");               
                
                Console.WriteLine(player.FaceIced);
                Console.WriteLine("");
                Console.WriteLine("Crime doesn't pay when you're dead.");
            }

             ExitGame();
        }

        static void DisplayCrewMembersWhoSurvived(List<Criminal> crew)
        {
            Console.WriteLine($"Crew members (besides you) who survived:");
            if (crew.Count() == 1)
            {
                Console.WriteLine("  All your crew members either ran off, were arrested, or shot dead.");
            }
            else
            {
                crew.ForEach(c =>
                {
                    if (!c.IsPlayer)
                    {
                        Console.WriteLine($"  {c.Name}");
                    }
                });   
            }
        }

        static void ExitGame()
        {
            Console.WriteLine("");
            Console.WriteLine("_________");
            Console.WriteLine("");
            Console.Write("Enter any key to play again or leave blank to exit C# Heists ");
            string input = Console.ReadLine();
            if (input != "") StartGame();
            else if (input == "") Environment.Exit(0);
        }

        static void WillCrewMemberTurnAfterHeist(List<Criminal> crew, List<Location> locations, string locName)
        {
            // Get selected location
            Location selectedLocation = locations.Find(l => l.Name == locName);

            int locationCash = selectedLocation.Cash;
            // Loop through criminals to see if any fail the morale check
            // If criminal fails, heist is a success
            crew.ForEach(c =>
            {
                if (!c.IsPlayer)
                {
                    int morale = c.Morale;
                    // if morale is less than 40 and another crew member hasn't turned and stole the money first
                    if (morale <= 40 && selectedLocation.crewMemberStoleCash == false)
                    {
                        // Save as traitor for easy understanding
                        Criminal traitor = c;

                        // Generate a new random number between 1-10 to handle the % a person will turn
                        Random r = new Random();
                        int randomNumber = r.Next(101);
                        int chance30 = 0;
                        int chance50 = 0;
                        int chance70 = 0;
                        int chance100 = 0;

                        // Based on this member's morale, generate a number by chance
                        if (morale >= 30 && morale <= 40) chance30 = r.Next(31) + 10;
                        else if (morale >= 20 && morale <= 29) chance50 = r.Next(51) + 20;
                        else if (morale >= 10 && morale <= 19) chance70 = r.Next(71) + 30;
                        // Chance100 equals the randomNumber so if they're at that level, they'll always turn
                        else if (morale <= 9) chance100 = randomNumber;

                        ChanceToTurnAfterHeist(chance30, randomNumber, crew, locations, traitor, locName);
                        ChanceToTurnAfterHeist(chance50, randomNumber, crew, locations, traitor, locName);
                        ChanceToTurnAfterHeist(chance70, randomNumber, crew, locations, traitor, locName);
                        ChanceToTurnAfterHeist(chance100, randomNumber, crew, locations, traitor, locName);
                    }
                }
            });
        }

        static void ChanceToTurnAfterHeist(
            int chanceInt,
            int randFrom100,
            List<Criminal> crew,
            List<Location> locations,
            Criminal traitor,
            string locName)
        {
            // If the percentage chance is under the random value picked
            if (chanceInt != 0 && chanceInt >= randFrom100) 
            {
                // Get selected location
                Location selectedLocation = locations.Find(l => l.Name == locName);

                // Lower everyone's Morale
                crew.ForEach(c =>
                {
                    if (c.IsPlayer == false) c.Morale = c.Morale - new Random().Next(10, 35);
                });

                // Set the current location to stolen
                locations.ForEach(l =>
                {
                    if (l.Name == locName) l.crewMemberStoleCash = true;
                }); 

                // Get the traitor's index value
                int traitorsIndex = crew.IndexOf(traitor);
                // Remove that index value
                crew.RemoveAt(traitorsIndex);
                // Remove the cash from the crew
                crew.ForEach(c => c.CrewTotalCash = c.CrewTotalCash - selectedLocation.Cash);

                TraitorScreen(crew, locations, locName, traitor);    
            }
        }

        static void TraitorScreen(List<Criminal> crew, List<Location> locations, string locName, Criminal traitor)
        {
            ASCII art = new ASCII();

            Location currentLocation = locations.Find(l => l.Name == locName);

            Console.WriteLine(art.DisplayHeadingTraitor());
            Console.WriteLine(art.DisplaySubheadingTraitor());
            Console.WriteLine("");
            Console.WriteLine($"{traitor.Face}");
            Console.WriteLine("");
            Console.WriteLine($"{traitor.Name} stole ${currentLocation.Cash}");
            Console.WriteLine("--------------------");
            Console.WriteLine("Crew skill improved.");
            Console.WriteLine("Crew morale decreased.");
            Console.WriteLine("");
            Console.WriteLine("Watch the crew. Unhappy members may turn.");
            Console.WriteLine("");
            Console.Write("Press any key to continue ");

            Console.ReadLine();
            LevelSelect(crew, locations);
        }

        static void ManageCrew(List<Criminal> crew, List<Location> locations)
        // Must always return the current crew and the current locations
        {
            Console.Clear();


            List<Criminal> updatedCrew = crew;

            DisplayCurrentCrew(updatedCrew);
   
            Criminal player = crew.Find(c => c.IsPlayer);

            // If only the player is in the crew & player has 0 contacts, return to level select
            if (crew.Count == 1 && player.PlayerContactCount == 0) LevelSelect(crew, locations);

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
            
            int input = MenuInput(4);

            switch (input)
            {
                case 1:
                    updatedCrew = CreateCrew(updatedCrew);
                    ManageCrew(updatedCrew, locations);
                    break;
                case 2:
                    if (crew.Count() > 1) updatedCrew = EncourageCrew(updatedCrew, false);
                    ManageCrew(updatedCrew, locations);
                    break;
                case 3:
                    updatedCrew = IceCrewMember(updatedCrew, locations, false);
                    ManageCrew(updatedCrew, locations);           
                    break;
                case 4:
                    LevelSelect(updatedCrew, locations);
                    break;
            }
        }

        static List<Criminal> EncourageCrew(List<Criminal> crew, bool isSplitMenu)
        {
            // Takes an isSplitMenu because different encouragements require different speeches
            Criminal player = crew.Find(c => c.IsPlayer);
            if (player.HasPlayerEncouragedCrew == false)
            {
                Random r = new Random();
                crew.ForEach(c =>
                {
                    if (!c.IsPlayer)
                    {
                        int improvedMorale = c.Morale + r.Next(4, 25);
                        if (improvedMorale > c.MoraleMax)
                        {
                            c.Morale = c.MoraleMax;
                        }
                        else
                        {
                            c.Morale = improvedMorale;
                        }
                    }
                    if (c.IsPlayer) c.HasPlayerEncouragedCrew = true;
                });
                DisplayEncouragingSpeech(player, isSplitMenu);
                return crew;
            }
            return crew;
        }

        static void DisplayEncouragingSpeech(Criminal player, bool isSplitMenu)
        {
            Console.Clear();
            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplaySubheadingSpeech());  
            Console.WriteLine(player.Face);
            Console.WriteLine("");
            // for the speeches, get names of the crew members and saying something dramatic about each person
                // will need to make some random compliments
            if (isSplitMenu == false)
            {
                Console.WriteLine("You give a big speech complimenting the crew's skills.");
                Console.WriteLine("You promise everyone will get rich.");
            }

            if (isSplitMenu == true)
            {
                Console.WriteLine("You give a big speech congratulating the crew's heist expertise");
                Console.WriteLine("and talk about how much money you all have made as a team.");
            }
        
            Console.WriteLine("");
            Console.WriteLine("Crew morale increased.");
            Console.WriteLine("");

            Console.Write("Press any key to continue ");
            Console.ReadLine();
        }

        static void DisplayIceAssociateWarning(List<Criminal> crew)
        {
            ASCII ASCII = new ASCII();
            Console.WriteLine("");
            Console.WriteLine("Icing crew members upsets the rest of the crew.");
            Console.WriteLine(ASCII.DisplayGun());

            Console.WriteLine("Crew members");
            Console.WriteLine("------------------");
            crew.ForEach(c =>
            {
                if (c.IsPlayer == false)
                {
                    Console.WriteLine($"{c.Name} - {c.MoraleDescription}");
                }
            });
            Console.WriteLine("");

            Console.Write("Enter name of who you will ice [leave blank to cancel]: ");
        }

        static List<Criminal> IceCrewMember(List<Criminal> crew, List<Location> locations, bool splitCashMenu)
        {
            if (crew.Count() > 1)  return IceAssociateCheck(crew, locations, splitCashMenu);
            else return crew;
        }

        static List<Criminal> IceAssociateCheck(List<Criminal> crew, List<Location> locations, bool splitCashMenu)
        {
            Console.Clear();
            string name = "not empty for a base value";
            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplayIce());

            if (!splitCashMenu)
            {
                while (name != "" && crew.Count() > 1)
                {
                    DisplayCrewInfo(crew);
                    DisplayIceAssociateWarning(crew);
                    name = Console.ReadLine();

                    // Store the criminal who was iced for use in display
                    Criminal whoWasIced = crew.Find(c => c.Name == name);
                    // Make a new list of criminals WITHOUT the iced crew member
                    List<Criminal> icedCrew = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();
                    
                    // Only lower morale if we have iced a crew member
                    if (icedCrew.Count() < crew.Count()) 
                    {
                        // Lower the crews morale
                        List<Criminal> newCrew = LowerMoraleFromIce(icedCrew);
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
                DisplayCrewInfoShortened(crew);
                DisplayIceAssociateWarning(crew);
                name = Console.ReadLine();

                // Store the criminal who was iced for use in display
                Criminal whoWasIced = crew.Find(c => c.Name == name);
                // Make a new list of criminals WITHOUT the iced crew member
                List<Criminal> icedCrew = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

                // Only lower morale if we have iced a crew member
                if (icedCrew.Count() < crew.Count()) 
                {
                    // Lower the crews morale
                    List<Criminal> newCrew = LowerMoraleFromIce(icedCrew);
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

        static List<Criminal> LowerMoraleFromIce(List<Criminal> icedCrew)
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

        static void DisplayWhoWasIced(Criminal whoWasIced)
        {
            Console.Clear();
            ASCII art = new ASCII();
            
            // Display ASCII artwork for who was killed 
            Console.WriteLine(art.DisplayHeadingIced());
            Console.WriteLine($"{whoWasIced.FaceIced}");
            Console.WriteLine("");

            // Summary of events
            Console.WriteLine($"You iced {whoWasIced.Name}!");
            Console.WriteLine("");
            Console.WriteLine("Crew morale decreased.");
            Console.WriteLine("Everyone's cut increased.");

            // Player input to continue
            Console.WriteLine("");
            Console.Write("Press any key to return to crew management ");
            Console.ReadLine();
        }

        static void WillCrewMemberRunAfterIce(List<Criminal> crew, List<Location> locations)
        {
            // SIMILAR to WillCrewMemberTurnAfterHeist
            // But this checks EVERY member instead of until one turns
            List<Criminal> checkedCrew = new List<Criminal>();
            List<Criminal> notScaredCrew = new List<Criminal>();

            Criminal player = crew.Find(c => c.IsPlayer);
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
                        Criminal possibleTraitor = c;

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

            ManageCrew(notScaredCrew, locations);
        }

        static Criminal ChanceToRun(
            int chanceInt,
            int randFrom100,
            Criminal possibleTraitor)
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

        static void AssociateRanInFear(Criminal scared)
        {
            Console.Clear();
            ASCII ASCII = new ASCII();
            Console.WriteLine(ASCII.DisplayHeadingFled());
            if (scared.CrewTotalCash == 0)
            {
                Console.WriteLine(ASCII.DisplayAssociateRanInFear());
                Console.WriteLine(scared.Face);
                Console.WriteLine("");
                Console.WriteLine($"{scared.Name} fled from your horrifically violent action!");
            }
            else
            {
                Console.WriteLine(ASCII.DisplayAssociateRanWithCash());
                Console.WriteLine(scared.Face);
                Console.WriteLine("");
                Console.WriteLine($"Your horrifically violent act terrified {scared.Name}.");
                Console.WriteLine($"{scared.Name} fled with a large portion of the cash!");
            }

            Console.WriteLine("");
            Console.Write("Press any key to continue ");
            Console.ReadLine();
        }

    static void WillCrewMemberShoot(List<Criminal> crew, List<Location> locations, bool isPlayerAttemptingMoneySplit)
    // Check every crew member's morale to see if they will turn
    {
        // SIMILAR to WillCrewMemberTurnAfterHeist
        Criminal player = crew.Find(c => c.IsPlayer);

        crew.ForEach(c =>
        {
            int morale = c.Morale;
            if (c.IsPlayer == false && morale <= 40)
            {
                // Save as traitor for easy understanding
                Criminal traitor = c;

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
        if (isPlayerAttemptingMoneySplit) GameOver(crew, locations, true, player);
        if (!isPlayerAttemptingMoneySplit) SplitCash(crew, locations);
    }

    // CHANCE TO SHOOT METHOD
        static void ChanceToShoot(
            int chanceInt,
            int randFrom100,
            List<Criminal> crew,
            List<Location> locations,
            Criminal traitor,
            bool isPlayerAttemptingMoneySplit)
        {
            if (chanceInt != 0 && chanceInt >= randFrom100 && !traitor.isAssociateIced)
            {
                // This traitor WILL shoot
                // Get the traitor's index value
                int traitorIndex = crew.IndexOf(traitor);
                // Declare a target's index
                Criminal target = null;
                Random r = new Random();
                bool lookingForTarget = true;
                // WHILE we do not have a person to shoot
                while(lookingForTarget)
                {
                    int possibleTargetIndex = 0;
                    // Player is a possible target, only if the crew is smaller than a certain number
                    if (crew.Count() > 2)
                    {
                        possibleTargetIndex = r.Next(1, crew.Count());
                    }
                    else
                    {
                        possibleTargetIndex = r.Next(0, crew.Count());
                    }
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

    // MESSAGE METHOD
        static void DisplayWhoIcedWho(List<Criminal> crew,
            List<Location> locations,
            Criminal traitor,
            Criminal gotShot,
            bool isPlayerAttemptingMoneySplit)
        {
            ASCII ASCII = new ASCII();
            Console.Clear();

            Console.WriteLine(ASCII.DisplayHeadingIced());
            Console.WriteLine(gotShot.FaceIced);
            Console.WriteLine("");
            if (gotShot.IsPlayer) 
            {
                Console.WriteLine($"{traitor.Name} iced YOU in the face!");
                Console.WriteLine("Press any key to continue ");
                Console.ReadLine();
                GameOver(crew, locations, false, gotShot);
            }
            
            else if (!gotShot.IsPlayer)
            {
                Console.WriteLine($"{traitor.Name} iced {gotShot.Name} in the face!");
                Console.WriteLine("");
                Console.WriteLine("Survivor's morale decreased.");
                Console.WriteLine("");
                Console.WriteLine("Press any key to return to splitting the cash ");
                Console.ReadLine();

                // If player is NOT attempting to split the cash, return to SplitCash view
                if (!isPlayerAttemptingMoneySplit)
                {
                    // Loop through the crew and find the members WHERE they have not been iced
                    crew = crew.Where(c => !c.isAssociateIced).ToList();
                    SplitCash(crew, locations);
                }
                // Otherwise, continue the forEach check for each crew member
            } 
        }

        static int MenuInput(int maxOptions)
        // Ensures user can only enter a number between 1 and maxOptions for this menu
        {
            // Declares variable we will be re-assigning 
            int entered;
            Console.WriteLine("");
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

            Criminal player = crew.Find(c => c.IsPlayer);
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

        static void DisplayCrewInfoShortened(List<Criminal> crew)
        {
            // Get crew's cash
            int TotalCash;

            Criminal player = crew.Find(c => c.IsPlayer);
            TotalCash = player.CrewTotalCash;

            int CurrentSplit = (TotalCash / crew.Count());

            Console.WriteLine($@"Total associates in crew: {crew.Count()}");
            Console.WriteLine("--------");
            Console.WriteLine($"Cash: ${TotalCash}");
            if (crew.Count() > 1) Console.WriteLine($"Current cut: ${CurrentSplit}");
            Console.WriteLine("--------");
        }

        static void DisplayCurrentCrew(List<Criminal> crew)
        {
            ASCII art = new ASCII();

            Console.WriteLine(art.DisplayCrewHeading());
            DisplayCrewInfo(crew);
            crew.ForEach(c => {
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
                        updatedCrew.Add(RecruitNewCriminal(crew));
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
                newCrew.Add(CreatePlayer(crew));

                string hireMessage = @"Go solo or hire a crew (you can recruit associates later)? [solo/hire]: ";

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
                                updatedCrew.Add(RecruitNewCriminal(newCrew));

                                c.PlayerContactCount = --playerContactsLeft;
                                Console.WriteLine("");
                                Console.WriteLine($"{playerContactsLeft} associates available to contact.");
                                
                                // Check based on if the player wants to continue hiring
                                if (playerContactsLeft > 0)
                                {
                                    if (updatedCrew.Count() > 1)Console.WriteLine($"{newCrew.Count() + 1} associates in crew.");
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
                if (crew.Count() <= 2) Console.WriteLine($@"{c.Face}
                ");
                if (c.IsPlayer == true)
                {
                    Console.WriteLine($@"You: {c.Name}");
                    Console.WriteLine($" base skill level: {c.BaseSkill}");
                } 
                
                if (c.IsPlayer == false)
                {
                    Console.WriteLine($@"{c.Name}");
                    Console.WriteLine($@" {c.MoraleDescription}
 base skill level: {c.BaseSkill}");
                }
                Console.WriteLine("");
            });
            
            Console.Write("Press any key to begin planning heists");
            Console.ReadLine();
        }

        static Criminal RecruitNewCriminal(List<Criminal> crew)
        {
            bool nameAvailable = true;
            // Set a blank new criminal that we can return after the loop completes
            Criminal newCriminal = null;

            while (nameAvailable)
            {
                Console.Write("Enter associate's nickname: ");
                string enteredName = Console.ReadLine();

                Criminal isNameTaken = crew.Find(c => c.Name == enteredName);

                if (isNameTaken == null && enteredName != "")
                {
                    newCriminal = new Criminal(enteredName, false, crew);
                    nameAvailable = false;
                    Console.WriteLine($@"{newCriminal.Face}

{newCriminal.Name} recruited!
_________
   
   base skill: {newCriminal.BaseSkill}
 ");
                }
                // Reset isNameTaken
                isNameTaken = null;
            }
            // Exit loop and return new criminal
            return newCriminal;
        }

        static Criminal CreatePlayer(List<Criminal> crew)
        {
            string playerName = "this is not an empty string";

            // Ask user to enter a name until they enter anything other than an empty string
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Who are you?");
                Console.Write("Enter your name: ");

                playerName = Console.ReadLine();
                if (playerName != "")
                {
                    break;
                }
            }
            // Create player object
            Criminal player = new Criminal(playerName, true, crew);

            Console.WriteLine($@"{player.Face}

{player.Name}
_________
  
  skill level: {player.BaseSkill}
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