// Intro class methods create:
    // Title text
    // Player
    // Initial crew
    // Displays who's in your crew before entering LevelSelect()
using System;
using System.Collections.Generic;
using System.Linq;
using CSharpHeists.ASCII;
using CSharpHeists.Criminal;
using CSharpHeists.GameSections.MainLoop;

namespace CSharpHeists.GameSections
{
    public class Intro
    {
        // Display the intro ASCII
        public static void DisplayIntro()
        {
            Console.WriteLine(Heading.DisplayTitle());
        }

        // Create the player and display the created player
        public static BaseCriminal CreatePlayer(List<BaseCriminal> crew)
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
            BaseCriminal player = new BaseCriminal(playerName, true, crew);

            // Display created player
            Console.WriteLine($@"{player.Face}

{player.Name}
_________
  
  skill level: {player.BaseSkill}
 ");
            return player;
        }

        // After player has created a crew, display all crew members
        public static void ShowCrewCreatedMsg(List<BaseCriminal> crew)
        {
            Console.Clear();
            Console.WriteLine(Heading.DisplayCrewCreated());
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
            Console.ReadKey();
        }

        public static List<BaseCriminal> CreateInitialCrew()
        {
            List<BaseCriminal> initialCrew = new List<BaseCriminal>();
            bool recruiting = true;
            int playerContactsLeft;

            // Create the player
            initialCrew.Add(Intro.CreatePlayer(initialCrew));

            // Ask player if they want to add crew members or go solo
            string recruitMessage = "Go solo or recruit a crew (you can recruit associates later)? [solo/crew]: ";
            Console.WriteLine(recruitMessage);
            // Store their response
            string playerInput = Console.ReadLine().ToLower();

            while (playerInput != "solo" && playerInput != "crew")
            {
                Console.Write(recruitMessage);
                playerInput = Console.ReadLine().ToLower();
            }

            if (playerInput == "solo") return initialCrew;

            else if (playerInput == "crew")
            {
                // Clear the player info froms screen
                Console.Clear();

                Console.WriteLine(Heading.DisplayCrewHire());

                // While we are recruiting, prompt user to continue recruiting
                while (recruiting)
                {
                    Console.WriteLine("");
                    string recruitingMessage = "Continue recruiting? [y/n]: ";

                    // Loop through the initialCrew, checking how many
                    // Criminals the player has left to contact
                    List<BaseCriminal> modifiedCrew = new List<BaseCriminal>();

                    // Loop over the initial criminal list 
                    initialCrew.ForEach(c =>
                    {
                        // If the player has contacts available, allow them to recruit criminals
                        playerContactsLeft = c.PlayerContactCount;
                        if (c.IsPlayer && playerContactsLeft > 0)
                        {
                            modifiedCrew.Add(c);
                            modifiedCrew.Add(CrewManagement.RecruitNewAssociate(initialCrew));

                            c.PlayerContactCount = --playerContactsLeft;
                            Console.WriteLine("");
                            Console.WriteLine($"{playerContactsLeft} associates available to contact.");

                            // Check based on if the player wants to continue hiring
                            if (playerContactsLeft > 0)
                            {
                                if (modifiedCrew.Count() > 1) Console.WriteLine($"{modifiedCrew.Count() + 1} associates in crew.");
                                Console.Write(recruitingMessage);
                                string response = Console.ReadLine().ToLower();

                                while (response != "y" && response != "n")
                                {
                                    Console.Write(recruitingMessage);
                                    response = Console.ReadLine().ToLower();
                                }

                                recruiting = response == "y" ? true : false;
                            }

                            if (playerContactsLeft == 0) recruiting = false;

                            initialCrew = modifiedCrew;
                        }
                        // If the player is out of contacts, add the current crew member to the list
                        // then set the updated crew to the initial crew
                        else
                        {
                            modifiedCrew.Add(c);
                            initialCrew = modifiedCrew;
                        }
                    });
                }
                return initialCrew;
            }
            return initialCrew;
        }
    }
}
