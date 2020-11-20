﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************");
            Console.WriteLine("PLAN THE HEIST");
            Console.WriteLine("****************");
            Console.Write("Press any key to begin recruiting criminals for this heist: ");
            Console.ReadLine();
            Console.WriteLine("");
            DisplayCriminalRoster(CreateCriminalRoster());

            // Name the bank you want to rob
            // Set a difficulty int for this bank
            // Tell your criminals to go rob the bank
            // If the sum of their skills is greater than the bank's difficulty
                // display a success message (ASCII of bag of money)
            // If failed, display someone behind bars

            // CREATE AN ASCII art class
            // and display different art in the functions
        }

        static void DisplayCriminalRoster(List<Criminal> roster)
        {
            Console.WriteLine("************************");
            Console.WriteLine("YOUR CREW FOR THIS HEIST");
            Console.WriteLine("************************");
            roster.ForEach(c => Console.WriteLine($@"
{c.Name}
 skill level: {c.SkillLevel}
 courage factor: {c.CourageFactor}
"));         
        }

        static List<Criminal> CreateCriminalRoster()
        {
            bool recruiting = true;
            // Instantiate empty list of criminals
            List<Criminal> CriminalRoster = new List<Criminal>();

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

        static Criminal RecruitNewCriminal()
        {
            Console.Write("Enter new criminal's name: ");
            // User can enter any string, including a string of numbers
            string enteredName = Console.ReadLine();
            int enteredSkill = InputSkill();
            double enteredCourage = InputCourage();
            Criminal newCriminal = new Criminal(enteredName, enteredSkill, enteredCourage);

            Console.WriteLine($@"
{newCriminal.Name} recruited!
 skill level: {newCriminal.SkillLevel}
 courage factor: {newCriminal.CourageFactor}
");

            return newCriminal;
        }

        static int InputSkill()
        {
            // Declares variable we will be re-assigning 
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