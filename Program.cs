using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of code 2018 by Jonas Hoffmann");

            var daysToSolve = Enumerable.Range(1, 2);

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
                    Console.WriteLine(getResultDay1());
                    break;
                case 2:
                    Console.WriteLine(getResultDay2());
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

        private static string[] getInput(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\resources\" + fileName + ".txt");
            return lines;
        }

        private static int getResultDay1()
        {
            string[] lines = getInput("input1");
            int num, result = 0;
            foreach (string line in lines)
            {
                num = Int32.Parse(line);
                result += num;
            }

            return result;
        }

        private static int getResultDay2()
        {
            string[] lines = getInput("input2");

            int twice = 0, thrice = 0;
            bool twiceThisLine = false, thriceThisLine = false;

            foreach (string line in lines)
            {
                twiceThisLine = false;
                thriceThisLine = false;

                foreach (char char_ in line)
                {
                    int charNumberOfTimesAppears = line.Count(x => x == char_);

                    if (charNumberOfTimesAppears == 2 && !twiceThisLine)
                    {
                        twiceThisLine = true;
                        twice++;
                    }
                    else if (charNumberOfTimesAppears == 3 && !thriceThisLine)
                    {
                        thriceThisLine = true;
                        thrice++;
                    }
                }
            }

            int result = twice * thrice;

            return result;
        }
    }
}
