using System;
using System.Linq;
using advent_of_code_2018.solutions;

namespace advent_of_code_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2018 solved by Jonas Hoffmann");

            IRunnable[] solutions = {
                //new Day_01_Frequencies(),
                //new Day_02_Inventories(),
                //new Day_03_Fabric(),
                //new Day_04_Guards(),
                //new Day_05_Polymers(),
                new Day_06_Chronal_Coordinates(),
                //new Day_07_The_Sum_of_Its_Parts()
            };

            for(int i = 0; i < solutions.Length; i++)
            {
                Console.Write("\nDay {0:D} Solution: ", i+1);
                solutions[i].Run();
            }
        }
    }
}
