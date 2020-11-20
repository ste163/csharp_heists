using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Plan Your Heist!");
            CreateCriminalRoster();


  
            // Display all criminal's info
        }

        static List<Criminal> CreateCriminalRoster()
        {
            bool hiring = true;
            // Instantiate empty list of criminals
            List<Criminal> CriminalRoster = new List<Criminal>();

            // While we are hiring, prompt user to continue hiring
            // display current amount of criminals hired
            while(hiring)
            {
                CriminalRoster.Add(HireNewCriminal());

                string countInRoster = CriminalRoster.Count() == 1 ? $"{CriminalRoster.Count()} criminal hired." : $"{CriminalRoster.Count()} criminals hired.";
                Console.WriteLine(countInRoster);
                
                Console.Write("Continue hiring? [y/n]: ");
                string response = Console.ReadLine().ToLower();
                Console.WriteLine("");

                while(response != "y" && response != "n")
                {
                    Console.Write("Continue hiring? [y/n]: ");
                    response = Console.ReadLine().ToLower();
                }

                hiring = response == "y" ? true : false;

            }
            return CriminalRoster;
        }

        static Criminal HireNewCriminal()
        {
            Console.Write("Enter new criminal's name: ");
            // User can enter any string, including a string of numbers
            string enteredName = Console.ReadLine();
            int enteredSkill = InputSkill();
            double enteredCourage = InputCourage();
            Criminal newCriminal = new Criminal(enteredName, enteredSkill, enteredCourage);

            Console.WriteLine($@"
{newCriminal.Name} hired!
Skill level: {newCriminal.SkillLevel}
Courage Factor: {newCriminal.CourageFactor}
");

            return newCriminal;
        }

        static int InputSkill()
        {
            // Declares the variable we will be re-assigning 
            int entered;

            // When user first enters the skill input, ensure they type only a number
            while(true)
            {
                try
                {
                    Console.Write("Enter new criminal's skill level: ");
                    entered = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Must enter a whole number.");
                }
            }

            // After user has entered a number, if it is less than or equal to 0, user must re-enter number
            while(entered <= 0)
            {
                try
                {
                    Console.Write("Enter whole number greater than zero: ");
                    entered = int.Parse(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("Must enter a whole number.");
                }
            }

            return entered;
        }

        static double InputCourage()
        {
            double entered;

            while(true)
            {
                try
                {
                    Console.Write("Enter new criminal's courage (0.0 - 2.0): ");
                    entered = double.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Must enter number.");
                }
            }

            while(entered < 0.0 || entered > 2.0)
            {
                try
                {
                    Console.Write("Enter number between 0.0 - 2.0: ");
                    entered = double.Parse(Console.ReadLine());
                }
                catch(FormatException)
                {
                    Console.WriteLine("Must enter a number.");
                }
            }

            return entered;
        }
    }
}