using System;
using System.Linq;
using advent_of_code_2018.solutions;

namespace advent_of_code_2018
{
    class Program
    {
        const int LastDayInAdvent = 6;

        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2018 solved by Jonas Hoffmann");

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
                    //Day_01_Frequencies.Run();
                    break;
                case 2:
                    //Day_02_Inventories.Run();
                    break;
                case 3:
                    //Day_03_Fabric.Run();
                    break;
                case 4:
                    //Day_04_Guards.Run();
                    break;
                case 5:
                    //Day_05_Polymers.Run();
                    break;
                case 6:
                    Day_06_Chronal_Coordinates.Run();
                    break;
                default:
                    Console.WriteLine("Unknown day");
                    break;
            }
        }


    }
}
