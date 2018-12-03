using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018.solutions
{
    class Day_02_Inventories
    {
        public static void Run()
        {
            string[] lines = InputReader.getInput("input2");

            int checksum = GetChecksum(lines);
            string commonLetters = GetCommonLetters(lines);

            Console.WriteLine((checksum, commonLetters));
        }

        private static int GetChecksum(string[] lines)
        {
            int twice = 0, thrice = 0;
            bool twiceThisLine = false, thriceThisLine = false;

            // What is the checksum for your list of box IDs?

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

            int checksum = twice * thrice;

            return checksum;
        }

        private static string GetCommonLetters(string[] lines)
        {
            // What letters are common between the two correct box IDs?

            List<char> commonLetters = null;

            for (int i = 0; i < lines.Length - 1; i++)
            {
                string line1 = lines[i];

                for (int j = i + 1; j < lines.Length; j++)
                {
                    int diffs = 0;
                    List<char> diffLetters = new List<char>();
                    string line2 = lines[j];

                    for (int k = 0; k < line1.Length; k++)
                    {
                        if (line1[k] != line2[k])
                        {
                            diffs++;
                        }
                        else
                        {
                            diffLetters.Add(line1[k]);
                        }
                    }

                    if (diffs == 1)
                    {
                        commonLetters = diffLetters;
                    }
                }

            }

            return (new string(commonLetters.ToArray()));
        }
    }
}
