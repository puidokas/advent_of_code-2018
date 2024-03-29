﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018.solutions
{
    class Day_02_Inventory_Management_System : IRunnable
    {
        public void Run()
        {
            string[] lines = InputReader.GetInput(2);

            int checksum = GetChecksum(lines);
            string commonLetters = GetCommonLetters(lines);

            Console.Write((checksum, commonLetters));
        }

        // What is the checksum for your list of box IDs?

        private int GetChecksum(string[] lines)
        {
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

            int checksum = twice * thrice;

            return checksum;
        }

        // What letters are common between the two correct box IDs?

        private string GetCommonLetters(string[] lines)
        {
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
                            diffs++;
                        else
                            diffLetters.Add(line1[k]);
                    }

                    if (diffs == 1)
                        commonLetters = diffLetters;
                }

            }

            return (new string(commonLetters.ToArray()));
        }
    }
}
