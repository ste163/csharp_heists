using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayIntro();
            List<Criminal> CurrentRoster = CreateCriminalRoster();
            DisplayCriminalRoster(CurrentRoster);
            LevelSelect();

            // Show different locations with ASCII art to rob
                // Annoying Neighbor's House (dif 0 - 50) ($0 - $100,000)
                // 7-Eleven (dif 0 - 150) ($50 - $1,000)
                // Chucky E Cheese (dif 0 - 200) ($100 - $2,000)
                // Nashville Software School (dif 0 - infinity) ($0-$30)
                // Bank of America (dif 300 - 900) ($5,000 - $10,000)

            // Crew Select
                // User types names
                // Skill is randomly generated from 1 - 50 (max of 100)
                // Trust random from 10 - 50 (max of 100)
                    // After every succesful heist, do a trust check for each crew member
                    // IF any criminal DOES turn, it lowers everyone's trust by -20
                    // The Trust level is shown to user as a sentence about how the member is feeling
                        // Trust 1 - 10 - always try to shoot another member and take their money
                        // Trust 11 - 20 - high chance of shooting another member and taking their money
                        // Trust 21 - 30 - low chance of turning on crew
                        // Trust 30 - 40 - very low chance of turning on crew, 0.1 Courage
                        // Trust 41 - 49 - almost no chance of turning on crew, 0.2 Courage
                        // Trust 50 - 59 - no chance of turning, 0.3 courage
                        // Trust 60 - 69 - 0.4 Courage
                        // Trust 70 -79 - 0.5 Courage
                        // Trust 80 - 89 - 0.7 Courage
                        // Trust 90 - 99 - 0.9 Courage
                        // Trust 100 - 1.0 Courage
                    // INCREASES after every successful heist by +30
                    // ADDING a new crew member after a heist randomizes trust for members by -30 to + 30
                        // The crew will say either "Screw the new guy," "Seems like an okay pick," "We really got {Name}?!"
                    // Icing a crew member lowers trust by - 70. Can Ice multiple crew members at a time to increase cash.
                // Courage Factor (0.1 - 1.0), courage is added to the skill by
                    // (((Courage Factor * Skill) / 10) + Skill Level).RoundUp() = Skill Level for Heist
                // Each crew member gets a random ASCII face

            // Level Select
                // View of ASCII city skyline
                // Display for how much money you've currently stolen
                // Have option to check on crew, see their stats, ice someone, or recruit new people
                // With heading underneath say choose a location to rob
                    // 1. Annoying Neighbors house...
                    // Selecting any location will take you to the big view of the ASCII art for that location
                    // With a summary about it, telling you what to expect
                        // Current Status: sentence about what it appears to look like (a hint for current difficulty)
                        // Ability to stake out to see if the situtation will change
                        // Option to Rob the place
                        // Option to return to City view 
                
            // Heist
                // New ascii art??
                // Show everyone's name and face?
                // IF Successfull
                    // Run trust check for each criminal to see if they're turn
                    // Show how much total cash you got, then everyone's cut
                // IF Failure
                    // Chance based on a skill check roll for if any crew members died in the cross-fire/got arrested
                        // instead of you
                    // If no one else was arrested chance for you to be arrested and the game over
                    // 10% chance of everyone getting away okay

            // If the sum of their skills is greater than the locations's difficulty
                // display a success message (ASCII of bag of money) - say how much $$ was stolen
                // Go back to the location select screen, but with the last place removed
        }

        static void LevelSelect()
        {
            // Only allowed to select the levels you HAVEN'T already robbed
            ASCII art = new ASCII();
            Console.WriteLine(art.DisplayNashville());
        }

        static void DisplayCriminalRoster(List<Criminal> roster)
        {
            Console.WriteLine(@"
-------------------------------
▀█▀ █▄█ ██▀   ▄▀▀ █▀▄ ██▀ █   █
 █  █ █ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀
-------------------------------");
            roster.ForEach(c => {
                if (c.IsPlayer)
                {
                    Console.WriteLine($@"
{c.Face}

You: {c.Name}
 skill level: {c.SkillLevel} / 100
 courage factor: {c.CourageFactor} / 1.0
");
                }
                else
                {
                    Console.WriteLine($@"
{c.Face}

{c.Name}
 skill level: {c.SkillLevel} / 100
 courage factor: {c.CourageFactor} / 1.0
 trust: {c.Trust}
");
                }
            });         
        }

        static List<Criminal> CreateCriminalRoster()
        {
            bool recruiting = true;
            // Instantiate empty list of criminals
            List<Criminal> CriminalRoster = new List<Criminal>();

            // Create the player and add them first to the roster
            CriminalRoster.Add(CreatePlayer());

            // OPTION TO GO SOLO OR HIRE CREW
            Console.WriteLine("Go solo or hire a crew? [solo/hire]: ");
            string solo = Console.ReadLine().ToLower();

            while(solo != "solo" && solo != "hire")
            {
                Console.Write("Go solo or hire a crew? [solo/hire]: ");
                solo = Console.ReadLine().ToLower();
            }

            if (solo == "solo")
            {
                return CriminalRoster;
            }
            else
            {
                Console.WriteLine(@"
---------------------------------
█▄█ █ █▀▄ ██▀   ▄▀▀ █▀▄ ██▀ █   █
█ █ █ █▀▄ █▄▄   ▀▄▄ █▀▄ █▄▄ ▀▄▀▄▀                   
---------------------------------");

                // While we are recruiting, prompt user to continue recruiting
                // display current amount of criminals recruited
                while(recruiting)
                {
                    CriminalRoster.Add(RecruitNewCriminal());

                    string countInRoster = CriminalRoster.Count() == 1 ? $"{CriminalRoster.Count()} criminal recruited." : $"{CriminalRoster.Count()} criminals recruited.";
                    Console.WriteLine(countInRoster);
                    
                    Console.Write("Continue recruiting? [y/n]: ");
                    string response = Console.ReadLine().ToLower();
                    Console.WriteLine("");

                    while(response != "y" && response != "n")
                    {
                        Console.Write("Continue recruiting? [y/n]: ");
                        response = Console.ReadLine().ToLower();
                    }

                    recruiting = response == "y" ? true : false;
                }
                return CriminalRoster;
            }
        }

        static Criminal RecruitNewCriminal()
        {
            Console.Write("Enter new criminal's nickname: ");
            // User can enter any string, including a string of numbers
            string enteredName = Console.ReadLine();
            Criminal newCriminal = new Criminal(enteredName, false);

            Console.WriteLine($@"
{newCriminal.Face}

{newCriminal.Name} recruited!
 skill level: {newCriminal.SkillLevel} / 100
 courage factor: {newCriminal.CourageFactor} / 1.0
 trust: {newCriminal.Trust}
");

            return newCriminal;
        }

        static Criminal CreatePlayer()
        {
            Console.WriteLine(@"
-----------------------------------------
█   █ █▄█ ▄▀▄   ▄▀▄ █▀▄ ██▀   ▀▄▀ ▄▀▄ █ █
▀▄▀▄▀ █ █ ▀▄▀   █▀█ █▀▄ █▄▄    █  ▀▄▀ ▀▄█
-----------------------------------------");
            Console.Write("Enter your name: ");

            string playerName = Console.ReadLine();
            Criminal player = new Criminal(playerName, true);
            return player;
        }

        static void DisplayIntro()
        {
            Console.WriteLine(@"


  ______    __  __        __    __          __            __              
 /      \  |  \|  \      |  \  |  \        |  \          |  \             
|  ▓▓▓▓▓▓\_| ▓▓| ▓▓_     | ▓▓  | ▓▓ ______  \▓▓ _______ _| ▓▓_    _______ 
| ▓▓   \▓▓   ▓▓  ▓▓ \    | ▓▓__| ▓▓/      \|  \/       \   ▓▓ \  /       \
| ▓▓      \▓▓▓▓▓▓▓▓▓▓    | ▓▓    ▓▓  ▓▓▓▓▓▓\ ▓▓  ▓▓▓▓▓▓▓\▓▓▓▓▓▓ |  ▓▓▓▓▓▓▓
| ▓▓   __|   ▓▓  ▓▓ \    | ▓▓▓▓▓▓▓▓ ▓▓    ▓▓ ▓▓\▓▓    \  | ▓▓ __ \▓▓    \ 
| ▓▓__/  \\▓▓▓▓▓▓▓▓▓▓    | ▓▓  | ▓▓ ▓▓▓▓▓▓▓▓ ▓▓_\▓▓▓▓▓▓\ | ▓▓|  \_\▓▓▓▓▓▓\
 \▓▓    ▓▓ | ▓▓| ▓▓      | ▓▓  | ▓▓\▓▓     \ ▓▓       ▓▓  \▓▓  ▓▓       ▓▓
  \▓▓▓▓▓▓   \▓▓ \▓▓       \▓▓   \▓▓ \▓▓▓▓▓▓▓\▓▓\▓▓▓▓▓▓▓    \▓▓▓▓ \▓▓▓▓▓▓▓                                                                                     


 ▄▀▄ █▄ █ ██▀   █▀▄ ▄▀▄ ▀▄▀     █▀ █ █ █ ██▀   █▄█ ██▀ █ ▄▀▀ ▀█▀ ▄▀▀     ▄▀▄ █▄ █ ██▀   ▄▀▀ █▀▄ █   █ ▀█▀
 ▀▄▀ █ ▀█ █▄▄   █▄▀ █▀█  █  █   █▀ █ ▀▄▀ █▄▄   █ █ █▄▄ █ ▄██  █  ▄██ █   ▀▄▀ █ ▀█ █▄▄   ▄██ █▀  █▄▄ █  █ 

---------------------------------------------------------------------------------------------------------
");
        }
    }
}

// INPUTS FOR PUTTING IN YOUR OWN SKILL OR COURAGE
    // THIS WAS BEFORE THEY BECAME RANDOMIZED
        // static int InputSkill()
        // {
        //     // Declares variable we will be re-assigning 
        //     int entered;
        //     // When user first enters the skill input, ensure they type only a number
        //     while(true)
        //     {
        //         try
        //         {
        //             Console.Write("Enter new criminal's skill level: ");
        //             entered = int.Parse(Console.ReadLine());
        //             break;
        //         }
        //         catch
        //         {
        //             Console.WriteLine("Must enter a whole number.");
        //         }
        //     }
        //     // After user has entered a number, if it is less than or equal to 0, user must re-enter number
        //     while(entered <= 0)
        //     {
        //         try
        //         {
        //             Console.Write("Enter whole number greater than zero: ");
        //             entered = int.Parse(Console.ReadLine());
        //         }
        //         catch(FormatException)
        //         {
        //             Console.WriteLine("Must enter a whole number.");
        //         }
        //     }

        //     return entered;
        // }

        // static double InputCourage()
        // {
        //     double entered;

        //     while(true)
        //     {
        //         try
        //         {
        //             Console.Write("Enter new criminal's courage (0.0 - 2.0): ");
        //             entered = double.Parse(Console.ReadLine());
        //             break;
        //         }
        //         catch
        //         {
        //             Console.WriteLine("Must enter number.");
        //         }
        //     }

        //     while(entered < 0.0 || entered > 2.0)
        //     {
        //         try
        //         {
        //             Console.Write("Enter number between 0.0 - 2.0: ");
        //             entered = double.Parse(Console.ReadLine());
        //         }
        //         catch(FormatException)
        //         {
        //             Console.WriteLine("Must enter a number.");
        //         }
        //     }

        //     return entered;
        // }