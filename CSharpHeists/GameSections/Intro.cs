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

namespace CSharpHeists.GameSections
{
    public class Intro
    {
        public static void DisplayIntro()
        {
            Console.WriteLine(Heading.DisplayTitle());
        }

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
    }
}
