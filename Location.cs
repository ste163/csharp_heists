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
        public string DifficultyDescription
        {
            get
            {
                string d = "";

                if (Difficulty <= 30) d = "Looks like no one is around. [~30 skill level required]";
                if (Difficulty >= 31 && Difficulty <= 80) d = "Saw movement, maybe one or two people inside. [~30-80 skill level required]";
                if (Difficulty >= 81 && Difficulty <= 150) d = "Definitely people inside, can't see how tough they might be. [~80-150 skill level required]";
                if (Difficulty >= 151 && Difficulty <= 250) d = "Mean looking people inside, going to be hard. [~150-250 skill level required]";
                if (Difficulty >= 251) d = "People outside, people inside, everyone looks like a badass. This will be difficult [+250 skill level required]";

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
            int houseDiff = (new Random().Next(1, 100));
            int houseCash = (new Random().Next(2_000, 150_000));

            string gasName = "Corner 7-Eleven";
            string gasSummary = @"Need to fill up the van and get some snacks. Might as well take their cash, too.";
            int gasDiff = (new Random().Next(50, 200));
            int gasCash = (new Random().Next(10, 3_000));

            // Instantiate locations
            Location houseLocation = new Location(ASCII.DisplayHouse(), houseName, houseSummary, houseDiff, houseCash); 
            Location gasLocation = new Location(ASCII.Display711(), gasName, gasSummary, gasDiff, gasCash); 

            // Add locations to list
            Locations.Add(houseLocation);
            Locations.Add(gasLocation);

            return Locations;
        }
    }
}