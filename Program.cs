using System;
using System.Linq;
using advent_of_code_2018.solutions;
using System.Diagnostics;

namespace advent_of_code_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2018 solved by Jonas Hoffmann");

            IRunnable[] solutions = {
                new Day_01_Chronal_Calibration(),
                new Day_02_Inventory_Management_System(),
                new Day_03_No_Matter_How_You_Slice_It(),
                new Day_04_Repose_Record(),
                new Day_05_Alchemical_Reduction(),
                new Day_06_Chronal_Coordinates(),
                new Day_07_The_Sum_of_Its_Parts()
            };

            for(int i = 0; i < solutions.Length; i++)
            {
                Console.Write("\nDay {0:D} Solution: ", i+1);

                Stopwatch stopWatch = Stopwatch.StartNew();

                solutions[i].Run();

                stopWatch.Stop();

                long elapsedMs = stopWatch.ElapsedMilliseconds;

                Console.WriteLine(" (" + elapsedMs/1000.0 + " sec)");
            }
        }
    }
}
