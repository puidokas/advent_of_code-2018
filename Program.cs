using System;
using System.Linq;
using advent_of_code_2018.solutions;

namespace advent_of_code_2018
{
    class Program
    {
        const int LastDayInAdvent = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2018 by Jonas Hoffmann");

            var daysToSolve = Enumerable.Range(1, LastDayInAdvent);

            foreach (int day in daysToSolve)
            {
                solve(day);
            }
        }

        private static void solve(int dayNo)
        {
            Console.Write("\nDay {0:D} Solution: ", dayNo);

            switch (dayNo)
            {
                case 1:
                    Day_01_Frequencies.Run();
                    break;
                case 2:
                    Day_02_Inventories.Run();
                    break;
                case 3:
                    Day_03_Fabric.Run();
                    break;
                default:
                    Console.WriteLine("Unknown day");
                    break;
            }
        }


    }
}
