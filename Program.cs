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

                    Console.Write("Enter new team member's skill level: ");
                    int enteredSkill = int.Parse(Console.ReadLine());

                    Console.Write("Enter new team member's courage (0.0 - 2.0): ");
                    double enteredCourage = int.Parse(Console.ReadLine());

                    Member newTeamMember = new Member(enteredName, enteredSkill, enteredCourage);
                    break;
                }
                // Have specific catches for
                // each error message based on
                // which variable we are declaring & assigning
                catch(FormatException)
                {
                    Console.WriteLine("You must enter a number");
                }
            }

            // Display team member's info
        }
    }
}