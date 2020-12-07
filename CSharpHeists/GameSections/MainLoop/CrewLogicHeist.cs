// CrewLogicHeist class methods:
    // handle crew logic on if they'll turn on player after a successful heist
using System;
using System.Collections.Generic;
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.Location;

namespace CSharpHeists.GameSections.MainLoop
{
    public class CrewLogicHeist
    {
        // Check if any crew members will turn on player/crew
        public static void WillCrewMemberTurnAfterHeist(List<BaseCriminal> crew, List<BaseLocation> locations, string locName)
        {
            // Get selected location
            BaseLocation selectedLocation = locations.Find(l => l.Name == locName);

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
                        BaseCriminal traitor = c;

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

        // If an associate turns, lower morale
        public static void ChanceToTurnAfterHeist(
            int chanceInt,
            int randFrom100,
            List<BaseCriminal> crew,
            List<BaseLocation> locations,
            BaseCriminal traitor,
            string locName)
        {
            // If the percentage chance is under the random value picked
            if (chanceInt != 0 && chanceInt >= randFrom100)
            {
                // Get selected location
                BaseLocation selectedLocation = locations.Find(l => l.Name == locName);

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

        // Display traitor info
        public static void TraitorScreen(List<BaseCriminal> crew, List<BaseLocation> locations, string locName, BaseCriminal traitor)
        {
            BaseLocation currentLocation = locations.Find(l => l.Name == locName);

            Console.WriteLine(Heading.DisplayHeadingTraitor());
            Console.WriteLine(Heading.DisplaySubheadingTraitor());
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
            Level.LevelSelect(crew, locations);
        }
    }
}
