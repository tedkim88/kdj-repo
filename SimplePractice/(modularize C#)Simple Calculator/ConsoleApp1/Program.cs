using System;
using ConsoleApp1;


class Program
{
    static void Main()
    {
        Exercise exercise = new Exercise();
        (int num1, int num2) = Input.getNumbers();
        exercise.SimpleCalculator((num1, num2));

        
    }
}