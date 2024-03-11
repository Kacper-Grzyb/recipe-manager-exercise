using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipe_manager_exercise
{
    public static class InputValidator
    {
        public static int GetIntInputInRange(int lowerLimit, int upperLimit) 
        { 
            while(true)
            {
                if(int.TryParse(Console.ReadLine(), out int userInput))
                {
                    if(userInput >= lowerLimit && userInput <= upperLimit) return userInput;
                    else Console.WriteLine($"The provided number was out of specified range. Try inputing the number again in the range between {lowerLimit} and {upperLimit} (inclusive)");
                }
                else
                {
                    Console.WriteLine($"The provided input was not a number. Try inputing a number again in the range between {lowerLimit} and {upperLimit} (inclusive)");
                }
            }
        }

        public static string GetStringInput()
        {
            string? output;
            while(true)
            {
                try
                {
                    output = Console.ReadLine();
                    if(output!=null) return output;
                }
                catch
                {
                    Console.WriteLine("Wrong input. Please try again.");
                }
            }
        }

    }
}
