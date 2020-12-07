// Level class methods:
    // handle level selection
    // display level info
    // allow user to wait in van
using System;
using System.Collections.Generic;
using System.Linq;
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.Location;
using CSharpHeists.UI;

namespace CSharpHeists.GameSections.MainLoop
{
    public class Level
    {
        public static void LevelSelect(List<BaseCriminal> crew, List<BaseLocation> locations)
        {
            // While there are levels not yet completed, allow user to continue selecting levels
            List<BaseLocation> locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();

            BaseCriminal player = crew.Find(c => c.IsPlayer);

            while (locationsLeftToRob.Count() > 0)
            {
                Console.Clear();
                Console.Write(Heading.DisplayPlanning());
                CrewManagement.DisplayCrewInfo(crew);
                Console.WriteLine(LevelArt.DisplayNashville());

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
                int selection = Menu.MenuInput(7);

                switch (selection)
                {
                    case 1:
                        if (player.PlayerContactCount == 0 && crew.Count() == 1) LevelSelect(crew, locations);
                        else CrewManagement.ManageCrew(crew, locations);
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
                        if (player.CrewTotalCash > 0) Outro.SplitCash(Outro.PrepCrewForEndGame(crew), locations);
                        break;
                }

                locationsLeftToRob = locations.Where(l => l.Completed == false).ToList();
            }

            Outro.SplitCash(Outro.PrepCrewForEndGame(crew), locations);
        }

        public static void StakeOutLocation(List<BaseCriminal> crew, List<BaseLocation> locations, int userSelected)
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
                    List<BaseLocation> isHouseCompleted = locations.Where(l => l.Name == houseSelected && !l.Completed).ToList();
                    // If location is in list, load the locationInfo
                    if (isHouseCompleted.Count() == 1) LocationInfo(locations, houseSelected, crew);
                    // If it's been completed, return to LevelSelect
                    else LevelSelect(crew, locations);
                    break;
                case 3:
                    string gasSelected = "Corner 7-Eleven";
                    List<BaseLocation> isGasCompleted = locations.Where(l => l.Name == gasSelected && !l.Completed).ToList();
                    if (isGasCompleted.Count() == 1) LocationInfo(locations, gasSelected, crew);
                    else LevelSelect(crew, locations);
                    break;
                case 4:
                    string wfSelected = "Welts Fargo";
                    List<BaseLocation> isWfCompleted = locations.Where(l => l.Name == wfSelected && !l.Completed).ToList();
                    if (isWfCompleted.Count() == 1) LocationInfo(locations, wfSelected, crew);
                    else LevelSelect(crew, locations);
                    break;
                case 5:
                    string pnbSelected = "Pinnackle National Bank";
                    List<BaseLocation> isPnbCompleted = locations.Where(l => l.Name == pnbSelected && !l.Completed).ToList();
                    if (isPnbCompleted.Count() == 1) LocationInfo(locations, pnbSelected, crew);
                    else LevelSelect(crew, locations);
                    break;
                case 6:
                    string boaSelected = "Bank of Amereeka";
                    List<BaseLocation> isBoaCompleted = locations.Where(l => l.Name == boaSelected && !l.Completed).ToList();
                    if (isBoaCompleted.Count() == 1) LocationInfo(locations, boaSelected, crew);
                    else LevelSelect(crew, locations);
                    break;
            }
        }

        public static void LocationInfo(List<BaseLocation> locations, string locName, List<BaseCriminal> crew)
        {
            Console.Clear();
            // Get the selected location
            BaseLocation selectedLoc = locations.Find(l => l.Name == locName);
            // Display the Stakeout heading
            Console.WriteLine(Heading.DisplayStakeout());
            CrewManagement.DisplayCrewInfo(crew);
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
            int selection = Menu.MenuInput(3);
            // Switch based on user input
            switch (selection)
            {
                case 1:
                    LocationInfo(WaitInVan(locations, locName), locName, crew);
                    break;
                case 2:
                    Heist.BeginHeist(crew, locations, locName);
                    break;
                case 3:
                    LevelSelect(crew, locations);
                    break;
            }
        }

        public static List<BaseLocation> WaitInVan(List<BaseLocation> locations, string locName)
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
                        int difficultyModifier = r.Next(-100, 81);
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
    }
}
