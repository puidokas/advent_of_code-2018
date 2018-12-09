using System;
using System.Collections.Generic;

namespace advent_of_code_2018.solutions
{
    class Day_01_Frequencies
    {
        public static void Run()
        {
            var result = GetFrequencies();
            Console.WriteLine(result);
        }

        // Starting with a frequency of zero, 
        // what is the resulting frequency after all of the changes in frequency have been applied?

        // What is the first frequency your device reaches twice?

        private static (int, int) GetFrequencies()
        {
            string[] lines = InputReader.GetInput("input1");

            int num, resultFrequency = 0, currentFrequency = 0, duplicateFrequency = 0;
            List<int> frequencies = new List<int>();
            bool duplicateFrequencyFound = false, resultFrequencyFound = false;

            while (!duplicateFrequencyFound)
            {
                foreach (string line in lines)
                {
                    num = Int32.Parse(line);
                    currentFrequency += num;

                    if (!duplicateFrequencyFound)
                        if (frequencies.Contains(currentFrequency))
                        {
                            duplicateFrequency = currentFrequency;
                            duplicateFrequencyFound = true;
                        }
                        else
                            frequencies.Add(currentFrequency);
                }

                if (!resultFrequencyFound)
                {
                    resultFrequency = currentFrequency;
                    resultFrequencyFound = true;
                }
            }

            return (resultFrequency, duplicateFrequency);
        }
    }
}
