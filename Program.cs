using System;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Plan Your Heist!");
            Member Terra = HireNewCriminal();
            Member Tristan = HireNewCriminal();
            Member Sam = HireNewCriminal();
  
            // Display team member's info

        }

        static Member HireNewCriminal()
        {
            Console.Write("Enter new team member's name: ");
            // User can enter any string, including a string of numbers
            string enteredName = Console.ReadLine();
            int enteredSkill = InputSkill();
            double enteredCourage = InputCourage();
            Member newCriminal = new Member(enteredName, enteredSkill, enteredCourage);

            Console.WriteLine($@"
{newCriminal.Name} hired!
Skill level: {newCriminal.SkillLevel}
Courage Factor: {newCriminal.CourageFactor}
");

            return newCriminal;
        }

        static int InputSkill()
        {
            int entered;

            while(true)
            {
                try
                {
                    Console.Write("Enter new team member's skill level: ");
                    entered = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Must enter a whole number.");
                }
            }

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
                    Console.Write("Enter new team member's courage (0.0 - 2.0): ");
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