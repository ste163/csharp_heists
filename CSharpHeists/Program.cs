// Program holds the StartGame method and invokes it on initial load
using System;
using System.Collections.Generic;
using System.Linq;
using CSharpHeists.Location;
using CSharpHeists.Criminal;
using CSharpHeists.GameSections;
using CSharpHeists.GameSections.MainLoop;
using CSharpHeists.UI;

namespace CSharpHeists
{
    class Program
    {
        static void Main(string[] args)
        {
            // By taking the game method out of Main, we are able to restart gameplay whenever we want by invoking StartGame();
            Console.Clear();
            StartGame();
        }

        public static void StartGame()
        {
            // Displays intro, creates levels, player, crew, and enters the LevelSelect loop
            Color.DefaultGray();
            Console.Clear();
            Intro.DisplayIntro();

            // Create all the levels in memory
            BaseLocation getLocations = new BaseLocation();
            List<BaseLocation> locations = getLocations.GenerateAllLocations();

            // Create the player & initial crew
            List<BaseCriminal> currentCrew = Intro.CreateInitialCrew();

            // Only show the crew created message if you made a crew
            if (currentCrew.Count() > 1) Intro.ShowCrewCreatedMsg(currentCrew);
            
            // Begin LevelSelect while loop that runs until the player runs out of locations to rob, then begin end game
            Level.LevelSelect(currentCrew, locations);
        }               
    }
}
