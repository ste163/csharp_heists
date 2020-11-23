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
        public int Cash { get; set; }

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
            int houseCash = (new Random().Next(10_000, 200_000));

            string gasName = "Corner 7-Eleven";
            string gasSummary = @"Need gas and to fill up the van. Might as well get some cash, too.";
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