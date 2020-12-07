// Menu class handles player inputs
using System;

namespace CSharpHeists.UI
{
    public class Menu
    {
        public static int MenuInput(int maxOptions)
        // Ensures user can only enter a number between 1 and maxOptions for this menu
        {
            // Declares variable we will be re-assigning 
            int entered;
            Console.WriteLine("");
            string message = "Enter number to perform action: ";
            // When user first enters the skill input, ensure they type only a number
            while (true)
            {
                try
                {
                    Console.Write(message);
                    entered = int.Parse(Console.ReadLine());
                    break;
                }
                catch { }
            }
            // After user has entered a number, if it is less than or equal to 0, user must re-enter number
            while (entered <= 0 || entered > maxOptions)
            {
                try
                {
                    Console.Write(message);
                    entered = int.Parse(Console.ReadLine());
                }
                catch { }
            }

            return entered;
        }
    }
}
