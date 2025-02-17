using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Exercise
    {
        public void SimpleCalculator((int num1, int num2) numbers)
        {
            // TODO: Implement the calculator logic here
            int num1 = numbers.num1;
            int num2 = numbers.num2;

            while (true) { 
            Console.WriteLine("Choose an operation: +, -, *, /");
            string operation = Console.ReadLine();
            if (operation == "+")
            {
                Console.WriteLine($"Result: {num1 + num2}");
                    break;
            }
            else if (operation == "-")
            {
                Console.WriteLine($"Result: {num1 - num2}");
                    break;
            }
            else if (operation == "*")
            {
                Console.WriteLine($"Result: {num1 * num2}");
                    break;
            }
            else if (operation == "/")
            {
                if (num2 == 0)
                {
                    Console.WriteLine($"Error: Division by zero is not allowed.");
                }
                else
                {
                    Console.WriteLine($"Result: {num1 / num2}");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Invalid operation. Please choose +, -, *, or /.");
            }
            }

        }

    }
}
