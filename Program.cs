using System;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Plan Your Heist!");

            while(true)
            {
                try
                {
                    Console.Write("Enter new team member's name: ");
                    // User can enter any string, including a string of numbers
                    string enteredName = Console.ReadLine();

                    // User can still enter a negative number
                    Console.Write("Enter new team member's skill level: ");
                    int enteredSkill = int.Parse(Console.ReadLine());

                    // User can still enter ANY number outside the range
                    Console.Write("Enter new team member's courage (0.0 - 2.0): ");
                    double enteredCourage = int.Parse(Console.ReadLine());

                    Member newTeamMember = new Member(enteredName, enteredSkill, enteredCourage);
                    break;
                }
                catch(FormatException)
                {
                    Console.WriteLine("You must enter a number.");
                }
            }

            // Display team member's info
        }
    }
}