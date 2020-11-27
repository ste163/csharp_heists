using System;
using System.Collections.Generic;

namespace heist
{
    public class Location
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public int Difficulty { get; set; }
        public bool crewMemberStoleCash { get; set; } = false;
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
                if (Difficulty >= 850) d = "A cop cruiser is parked in front. On the bank steps, cops chat with armed bank security guards. [+850 skill level required]";

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

        public Location(string img, string name, string summary, int diff, int cash)
        {
            Image = img;
            Name = name;
            Summary = summary;
            Difficulty = diff;
            Cash = cash;
        }

        public List<Location> GenerateAllLocations()
        {
            ASCII ASCII = new ASCII();

            List<Location> Locations = new List<Location>();

            // Store values for locations
            string houseName = "Annoying Neighbor's House";
            string houseSummary = @"He always talks about hating banks and keeping money in the guest room mattress.";
            int houseDiff = (new Random().Next(10, 100));
            int houseCash = (new Random().Next(2_000, 150_000));

            string gasName = "Corner 7-Eleven";
            string gasSummary = @"Need to fill up the van and get some snacks. Might as well take their cash, too.";
            int gasDiff = (new Random().Next(80, 200));
            int gasCash = (new Random().Next(10, 3_000));

            string wfName = "Welts Fargo";
            string wfSummary = "An older building with what looks like lack security.";
            int wfDiff = (new Random().Next(250, 450));
            int wfCash = (new Random().Next(88_000, 478_000));

            string pnName = "Pinnackle National Bank";
            string pnSummary = "Opened a couple years ago, latest security.";
            int pnDiff = (new Random().Next(580, 800));
            int pnCash = (new Random().Next(250__000, 888_000));

            string baName = "Bank of Amereeka";
            string baSummary = "Just opened a couple days ago. This will be hard.";
            int baDiff = (new Random().Next(800, 900));
            int baCash = (new Random().Next(1_000_000, 3_000_000));

            // Instantiate locations
            Location houseLocation = new Location(ASCII.DisplayHouse(), houseName, houseSummary, houseDiff, houseCash); 
            Location gasLocation = new Location(ASCII.Display711(), gasName, gasSummary, gasDiff, gasCash); 
            Location wfLocation = new Location(ASCII.DisplayWF(), wfName, wfSummary, wfDiff, wfCash);
            Location pnLocation = new Location(ASCII.DisplayPNB(), pnName, pnSummary, pnDiff, pnCash);
            Location baLocation = new Location(ASCII.DisplayBOA(), baName, baSummary, baDiff, baCash);
            
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