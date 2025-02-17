using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Input
    {
        public static(int,int) getNumbers()
        {
            int num1;
            while (true)
            {

                Console.WriteLine("Enter the first number: ");
                bool result1 = int.TryParse(Console.ReadLine(), out num1);
                if (result1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid input.");
                }
            }
            int num2;
            while (true)
            {

                Console.WriteLine("Enter the second number:");
                bool result2 = int.TryParse(Console.ReadLine(), out num2);
                if (result2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Invalid input.");
                }
            }
            return (num1, num2);
        }








    }
}
