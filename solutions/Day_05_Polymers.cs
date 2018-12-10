
using System;

namespace advent_of_code_2018.solutions
{
    class Day_05_Polymers : IRunnable
    {
        public void Run()
        {
            string inputText = InputReader.GetInput(5)[0];

            int numberOfUnitsLeft = GetNumberOfUnitsLeft(inputText);
            int shortestProducablePolymer = GetShortestProducablePolymer(inputText);

            Console.WriteLine((numberOfUnitsLeft, shortestProducablePolymer));
        }

        private int GetNumberOfUnitsLeft(string inputText)
        {
            bool changed = true;

            while(changed)
            {
                changed = false;

                for(int i = 0; i < inputText.Length-1; i++)
                {
                    char letter1Lowercase = Char.ToLower(inputText[i]);
                    char letter2Lowercase = Char.ToLower(inputText[i + 1]);

                    if (letter1Lowercase == letter2Lowercase)
                    {
                        char letter1 = inputText[i];
                        char letter2 = inputText[i + 1];

                        if (letter1 == letter2)
                            continue;
                        else
                        {
                            inputText = inputText.Remove(i, 2);
                            changed = true;
                        }
                    }
                    else
                        continue;
                }
            }

            int numberOfUnitsLeft = inputText.Length;

            return numberOfUnitsLeft;
        }

        private int GetShortestProducablePolymer(string inputText)
        {
            int shortestPolymer = Int32.MaxValue;

            for(int i = 65; i < 91; i++)
            {
                char characterUpper = Convert.ToChar(i);
                char characterLower = Char.ToLower(characterUpper);
                string polymerText = String.Copy(inputText);

                polymerText = polymerText.Replace(Char.ToString(characterLower), string.Empty);
                polymerText = polymerText.Replace(Char.ToString(characterUpper), string.Empty);

                int result = GetNumberOfUnitsLeft(polymerText);

                if (result < shortestPolymer)
                    shortestPolymer = result;
            }

            return shortestPolymer;
        }
    }
}
