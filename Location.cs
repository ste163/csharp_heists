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

            // Store values for the house location
            string houseName = "Annoying Neighbor's House";
            string houseSummary = @"My neighbor is so annoying and almost always outside, wanting to talk. Luckily
my neighbor always mentions hating banks. I know there's money in there. Probably in the mattress.";
            int houseDiff = (new Random().Next(1, 100));
            int houseCash = (new Random().Next(10_000, 200_000));

            // Instantiate house location
            Location houseLocation = new Location(ASCII.DisplayHouse(), houseName, houseSummary, houseDiff, houseCash); 

            // Add locations to list
            Locations.Add(houseLocation);

            return Locations;
        }
    }
}