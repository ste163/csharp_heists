using System;
using System.Collections.Generic;
using System.Linq;
    
// Crew management
    // IF player ices a crew member
        // Run morale check to see if any crew member will turn
            // If crew member turns, they take a large % of cash
            // If there is no cash, then say the crew member ran in fear

// Splitting the cash view
    // after all heists attempted, go to new view with a list of all criminals
    // Say how much they've earned, and how much everyone's cut is
    // Player can decide to either ICE a crew member, or split the money evenly
        // IF player ices a crew member, then do a morale check to see if anyone is going to turn
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
            // From level select, go to the split money view, then game over
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
                        if (l.Name == "Welts Fargo") Console.WriteLine("4) stakeout Welts Fargo");
                        if (l.Name == "Pinnackle National Bank") Console.WriteLine("5) stakeout Pinnackle National Bank");
                        if (l.Name == "Bank of Amereeka") Console.WriteLine("6) stakeout Bank of Amereeka");
                    }
                });

                if (crew.Count() == 1)
                {
                    Console.WriteLine("7) end heist spree");
                }
                else
                {
                    Console.WriteLine("7) end heist spree and split cash");
                }
                
                int selection = MenuInput(7);

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
                        // split
                        SplitCash(crew,locations);
                        break;
                }

                locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();
            }

            SplitCash(crew,locations);     
        }

        static void SplitCash(List<Criminal> crew, List<Location> locations)
        {
            // If there is a crew, split cash. Else, end game
            if (crew.Count() > 1)
            {
                Console.WriteLine("SPLIT THE CASH");
                Console.ReadLine();
                // HEADING split cash
                // SUBHEADING - Part ways or open fire
                // display current crew info
                // IMAGE OF SOMETHING???
                // display everyone's name and current morale description
                // Options to
                // 1) split money evenly - ENDS GAME
                // 2) ice a crew member
                // Iceing a crew member here lowers trust by 60
                    // Run a check to see if any crew member will open fire
                        // IF YES - randomly shoot an index value, including player
                            // if player - message,the game over
                        // IF no
                            // return to player getting to decide
                GameOver(crew, locations);
            }
            else
            {
                GameOver(crew, locations); 
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
                case 4:
                    string wfSelected = "Welts Fargo";

                    List<Location> isWfCompleted = locations.Where(l => l.Name == wfSelected && !l.Completed).ToList();

                    if (isWfCompleted.Count() == 1)
                    {
                        LocationInfo(locations, wfSelected, crew);
                    }
                    else
                    {
                        LevelSelect(crew, locations);
                    }
                    break;
                case 5:
                    string pnbSelected = "Pinnackle National Bank";

                    List<Location> isPnbCompleted = locations.Where(l => l.Name == pnbSelected && !l.Completed).ToList();

                    if (isPnbCompleted.Count() == 1)
                    {
                        LocationInfo(locations, pnbSelected, crew);
                    }
                    else
                    {
                        LevelSelect(crew, locations);
                    }
                    break;
                case 6:
                    string boaSelected = "Bank of Amereeka";

                    List<Location> isBoaCompleted = locations.Where(l => l.Name == boaSelected && !l.Completed).ToList();

                    if (isBoaCompleted.Count() == 1)
                    {
                        LocationInfo(locations, boaSelected, crew);
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
                    int crewTotalSkill = crew.Sum(c => c.TotalSkillLevel);
                    
                    
                    // If the crew succeeds, add the total cash to each crew member
                        // This is to save the cash, it will be split later
                    if (l.Difficulty <= crewTotalSkill)
                    {
                        crewSuccess = crew.Select(c => 
                        {
                            // Every crew member gets a random skill+
                            int skillIncrease = new Random().Next(8, 38);
                            int moraleIncrease = new Random().Next(7, 34);

                            c.CrewTotalCash = c.CrewTotalCash + l.Cash;
                            c.BaseSkill = c.BaseSkill + skillIncrease;
                            c.Morale = c.Morale + moraleIncrease;
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
            string msg = "Press any key to continue";
            string moraleMsg = "Crew morale decreased";
            // 50-50 chance for arrested or escaped
            int r = new Random().Next(2, 3);
            ASCII ASCII = new ASCII();
           
            // Arrested
            if (r == 1)
            {
                // Generate arrested summary
                Console.WriteLine(ASCII.DisplayHeadingArrested());
                Console.WriteLine(ASCII.DisplaySubheadingArrested());
                Console.WriteLine(ASCII.DisplayArrested());
                if (crew.Count() > 1) Console.WriteLine(moraleMsg);
                
                // If only player is in crew
                if (crew.Count == 1)
                {
                    Console.Write(msg);
                    Console.ReadLine();
                    Console.WriteLine();
                    GameOver(crew, locations);
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
                    Console.WriteLine("");
                    Console.Write(msg);
                    Console.ReadLine();

                    crew.RemoveAt(randomCrewMember);
                    // Randomly lower every non-player's morale
                    crew.ForEach(c =>
                    {
                        if (c.IsPlayer == false) c.Morale = c.Morale - new Random().Next(25, 54);
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
                    if (c.IsPlayer == false) c.Morale = c.Morale - new Random().Next(15, 35);
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

        static void GameOver(List<Criminal> crew, List<Location> locations)
        {
            ASCII ASCII = new ASCII();
            
            Criminal player = crew.Find(c => c.IsPlayer);
            int cashStolen = player.CrewTotalCash;
            int totalCashAvailable = 0;
            locations.ForEach(l => totalCashAvailable = l.Cash + totalCashAvailable);

            Console.Clear();
            Console.WriteLine(ASCII.DisplayHeadingGameOver());
            Console.WriteLine(ASCII.DisplaySubHeadingSummary());
            Console.WriteLine($"Total cash stolen: ${cashStolen} / ${totalCashAvailable}");
            
            if (crew.Count() > 1)
            {
                Console.WriteLine($"Crew members survived: {crew.Count()}");
                Console.WriteLine($"The cut per member: ${cashStolen / crew.Count()}");
            }

            if (player.PlayerContactCount > 0) Console.WriteLine($"Associates you could have hired: {player.PlayerContactCount}");
            
            Console.WriteLine("");
            Console.Write("Press any key to close C# Heists ");
            Console.ReadLine();
            Environment.Exit(0);
        }

        static void WillCrewMemberTurnAfterHeist(List<Criminal> crew, List<Location> locations, string locName)
        {
            // THIS check is ONLY for if they're going to turn on a HEIST
                // Break out some of this functinality so it can be used on
                // WillCrewMemberTurnAfterIce

            // Get the selected location
            Location selectLocation = locations.Find(l => l.Name == locName);
            // Loop through criminals to see if any fail the morale check
            crew.ForEach(c =>
            {
                int morale = c.Morale;
                // if morale is less than 40 and another crew member hasn't turned and stole the money first
                if (morale <= 40 && selectLocation.crewMemberStoleCash == false)
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
                    if (morale >= 20 && morale <= 29) chance50 = r.Next(51);
                    if (morale >= 10 && morale <= 19) chance70 = r.Next(71);
                    // Chance100 equals the randomNumber so if they're at that level, they'll always turn
                    if (morale <= 9) chance100 = randomNumber;

                    // If their generated number is greater
                    if (chance30 != 0 && chance30 >= randomNumber) 
                        {
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

                            // Set selectLocation to true for the looping in this local scope
                            // because the selecLocation is created OUTSIDE the forEach loop
                            selectLocation.crewMemberStoleCash = true;
                            // Get the traitor's index value
                            int traitorsIndex = crew.IndexOf(traitor);
                            // Remove that index value
                            crew.RemoveAt(traitorsIndex);
                                                    
                            TraitorScreen(crew, locations, locName, traitor);    
                        }
                    if (chance50 != 0 && chance50 >= randomNumber)
                    {
                        // Repeated from chance30
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

                        // Set selectLocation to true for the looping in this local scope
                        // because the selecLocation is created OUTSIDE the forEach loop
                        selectLocation.crewMemberStoleCash = true;
                        // Get the traitor's index value
                        int traitorsIndex = crew.IndexOf(traitor);
                        // Remove that index value
                        crew.RemoveAt(traitorsIndex);
                                                
                        TraitorScreen(crew, locations, locName, traitor); 
                    }
                    if (chance70 != 0 && chance70 >= randomNumber)
                    {
                        // Repeated from chance30
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

                        // Set selectLocation to true for the looping in this local scope
                        // because the selecLocation is created OUTSIDE the forEach loop
                        selectLocation.crewMemberStoleCash = true;
                        // Get the traitor's index value
                        int traitorsIndex = crew.IndexOf(traitor);
                        // Remove that index value
                        crew.RemoveAt(traitorsIndex);
                                                
                        TraitorScreen(crew, locations, locName, traitor); 
                    }
                    if (chance100 !=  0)
                    {
                        // Repeated from chance30
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

                        // Set selectLocation to true for the looping in this local scope
                        // because the selecLocation is created OUTSIDE the forEach loop
                        selectLocation.crewMemberStoleCash = true;
                        // Get the traitor's index value
                        int traitorsIndex = crew.IndexOf(traitor);
                        // Remove that index value
                        crew.RemoveAt(traitorsIndex);
                                                
                        TraitorScreen(crew, locations, locName, traitor); 
                    }
                }
            });
            // If a crew member doesn't turn, don't do anything
            // and let the rest of the heist success method run
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
                    DisplayCrewInfo(crew);
                    Console.WriteLine("");
                    Console.WriteLine("Icing a crew member will upset the rest of the crew.");
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
                    name = Console.ReadLine();

                    // Make a new list of criminals WITHOUT the iced crew member
                    List<Criminal> iceMember = crew.Where(c => c.Name != name || c.IsPlayer == true).ToList();

                    // Only lower morale if we have iced a crew member
                    if (iceMember.Count() < crew.Count()) 
                    {
                        List<Criminal> newCrew = iceMember.Select(c =>
                        {
                            int subtractMorale = new Random().Next(4, 31);

                            int loweredMorale = c.Morale - subtractMorale;
                            if (loweredMorale < 0)
                            {
                                c.Morale = 0;
                            }
                            else
                            {
                                c.Morale = loweredMorale;
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
            int MaxCrewSkill = (TotalSkills.Count() * 100);
            if (player.PlayerContactCount > 0) Console.WriteLine($"Total associates available to hire: {player.PlayerContactCount}");
            Console.WriteLine($@"Total associates in crew: {crew.Count()}");
            Console.WriteLine($"Crew's base skill level: {CrewSkill} / {MaxCrewSkill}");
            if (crew.Count() > 1) Console.WriteLine($"Morale bonus to skill: {MoraleSkillBonus.Sum()}");
            Console.WriteLine("--------");
            Console.WriteLine($"Cash: ${TotalCash}");
            if (crew.Count() > 1) Console.WriteLine($"current cut: ${CurrentSplit}");
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
                    Console.WriteLine($@"{c.Face}

You: {c.Name}
------------
 base skill: {c.BaseSkill} / 100");
                }
                else
                {
                    Console.WriteLine($@"{c.Face}

{c.Name}
{c.MoraleDescription}
------------
 base skill: {c.BaseSkill} / 100");
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
                                    if (updatedCrew.Count() > 1)Console.WriteLine($"{newCrew.Count() + 1} criminals in crew.");
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
                    Console.WriteLine($" base skill level: {c.BaseSkill} / 100");
                } 
                
                if (c.IsPlayer == false)
                {
                    Console.WriteLine($@"{c.Name}");
                    Console.WriteLine($@" {c.MoraleDescription}
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
 ");
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