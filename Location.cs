using System;
using System.Collections.Generic;

namespace heist
{
    public class Location
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int WaitsInVanAvailable { get; set; } = 3;
        public bool crewMemberStoleCash { get; set; } = false;
        public int Difficulty { get; set; }
        public int DifficultyMin { get; set; }
        public int DifficultyMax { get; set;}
        public string DifficultyDescription
        {
            get
            {
                string d = "";

                if (Difficulty <= 30) d = "Looks like no one is around. [~30 skill level required]";
                if (Difficulty >= 31 && Difficulty <= 80) d = "Saw movement, maybe one or two people inside. [~30-80 skill level required]";
                if (Difficulty >= 81 && Difficulty <= 150) d = "Definitely people inside, can't see how tough they might be. [~80-150 skill level required]";
                if (Difficulty >= 151 && Difficulty <= 250) d = "Mean looking people inside. [~150-250 skill level required]";
                if (Difficulty >= 251 && Difficulty <= 350) d = "Tough guy by the door is definitly an armed guard. [~250-350 skill level required]";
                if (Difficulty >= 351 && Difficulty <= 450) d = "Armed guard and a mean looking dog by the door. [~350-450 skill level required]";
                if (Difficulty >= 451 && Difficulty <= 550) d = "No guards outside, but there are security cameras. [~450-550 skill level required]";
                if (Difficulty >= 551 && Difficulty <= 650) d = "Two security cameras and a guard outside. [~550-650 skill level required]";
                if (Difficulty >= 651 && Difficulty <= 750) d = "Many cameras, guards inside and out. [~650-750 skill level required]";
                if (Difficulty >= 751 && Difficulty <= 850) d = "Guards outside have rifles. It's almost like they're expecting a robbery. [~750-850 skill level required]";
                if (Difficulty >= 850) d = "A cop cruiser is parked in front. A cop leans out the window and chats with the security guards. [+850 skill level required]";

                return d;
            }
        }
        public int Cash { get; set; }
        public bool Completed{ get; set; } = false;

        public Location()
        {
            // Blank location constructor to instantiate an initial location
            // So we can use the GenerateAllLocations method
        }

        public Location(string img, string name, string summary, int diff, int diffMin, int diffMax, int cash)
        {
            Image = img;
            Name = name;
            Summary = summary;
            Difficulty = diff;
            DifficultyMin = diffMin;
            DifficultyMax = diffMax;
            Cash = cash;
        }

        public List<Location> GenerateAllLocations()
        {
            ASCII ASCII = new ASCII();

            List<Location> Locations = new List<Location>();

            // Store values for locations
            string houseName = "Annoying Neighbor's House";
            string houseSummary = @"He always talks about hating banks and keeping money in the guest room mattress.";
            int houseDiffMin = 10;
            int houseDiffMax = 80;
            int houseDiff = (new Random().Next(houseDiffMin, houseDiffMax));
            int houseCash = (new Random().Next(2_000, 150_000));

            string gasName = "Corner 7-Eleven";
            string gasSummary = @"Need to fill up the van and get some snacks. Might as well take their cash, too.";
            int gasDiffMin = 80;
            int gasDiffMax = 240;
            int gasDiff = (new Random().Next(gasDiffMin, gasDiffMax));
            int gasCash = (new Random().Next(10, 3_000));

            string wfName = "Welts Fargo";
            string wfSummary = "An older building with what looks like lack security.";
            int wfDiffMin = 200;
            int wfDiffMax = 500;
            int wfDiff = (new Random().Next(wfDiffMin, wfDiffMax));
            int wfCash = (new Random().Next(88_000, 478_000));

            string pnName = "Pinnackle National Bank";
            string pnSummary = "Opened a couple years ago, latest security.";
            int pnDiffMin = 475;
            int pnDiffMax = 750;
            int pnDiff = (new Random().Next(pnDiffMin, pnDiffMax));
            int pnCash = (new Random().Next(250__000, 888_000));

            string baName = "Bank of Amereeka";
            string baSummary = "Just opened a couple days ago. This will be hard.";
            int baDiffMin = 600;
            int baDiffMax = 900;
            int baDiff = (new Random().Next(baDiffMin, baDiffMax));
            int baCash = (new Random().Next(1_000_000, 4_000_000));

            // Instantiate locations
            Location houseLocation = new Location(ASCII.DisplayHouse(), houseName, houseSummary, houseDiff, houseDiffMin, houseDiffMax, houseCash); 
            Location gasLocation = new Location(ASCII.Display711(), gasName, gasSummary, gasDiff, gasDiffMin, gasDiffMax, gasCash); 
            Location wfLocation = new Location(ASCII.DisplayWF(), wfName, wfSummary, wfDiff, wfDiffMin, wfDiffMax, wfCash);
            Location pnLocation = new Location(ASCII.DisplayPNB(), pnName, pnSummary, pnDiff, pnDiffMin, pnDiffMax, pnCash);
            Location baLocation = new Location(ASCII.DisplayBOA(), baName, baSummary, baDiff, baDiffMin, baDiffMax, baCash);
            
            // Add locations to list
            Locations.Add(houseLocation);
            Locations.Add(gasLocation);
            Locations.Add(wfLocation);
            Locations.Add(pnLocation);
            Locations.Add(baLocation);

            return Locations;
        }
    }
}