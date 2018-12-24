using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.solutions
{
    class Day_07_The_Sum_of_Its_Parts : IRunnable
    {
        public void Run()
        {
            string[] lines = InputReader.GetInput(7);

            Dictionary<char, List<char>> steps = GetSteps(lines);

            (string orderOfSteps, _) = GetSolution(steps, 1);

            (_, int timeToWork) = GetSolution(steps, 5);

            Console.Write((orderOfSteps, timeToWork));
        }

        private (string, int) GetSolution(Dictionary<char, List<char>> steps, int numberOfWorkers)
        {
            StringBuilder path = new StringBuilder();
            List<char> keysWaitingToBeActivated = new List<char>();
            List<(char, int)> nextKeys = new List<(char, int)>();
            int timeToWork = 0;

            List<char> firstPositions = GetFirstPositions(steps);

            foreach (char position in firstPositions)
                keysWaitingToBeActivated.Add(position);

            while (path.Length < steps.Count)
            {
                if(keysWaitingToBeActivated.Count > 1)
                    keysWaitingToBeActivated.Sort((x, y) => x.CompareTo(y));

                for (int j = 0; j < keysWaitingToBeActivated.Count; j++)
                {
                    if (ArePrerequisitesSatisfied(keysWaitingToBeActivated[j], steps, path.ToString()))
                    {
                        if (nextKeys.Count < numberOfWorkers)
                        {
                            int secondsToWork = GetSecondsToWork(keysWaitingToBeActivated[j]);
                            nextKeys.Add((keysWaitingToBeActivated[j], 60 + secondsToWork));
                        }
                        else
                            break;
                    }
                }

            foreach((char key, int count) in nextKeys)
                keysWaitingToBeActivated.Remove(key);

            int minValue = nextKeys.Select(o => o.Item2).Min();

            timeToWork += minValue;

            nextKeys = nextKeys.Select(o => {
                int count = o.Item2 - minValue;

                if(count == 0)
                {
                    char nextKey = o.Item1;

                    List<char> dependencies = steps[nextKey];

                    foreach (char dependency in dependencies)
                    {
                        if (!keysWaitingToBeActivated.Contains(dependency))
                            keysWaitingToBeActivated.Add(dependency);
                    }

                    path.Append(nextKey);
                }

                return (o.Item1, count);

                }).ToList();

                nextKeys = nextKeys.Where(o => !path.ToString().Contains(o.Item1)).ToList();
            }

            return (path.ToString(), timeToWork);
        }

        private int GetSecondsToWork(char inputChar)
        {
            return(inputChar - 64);
        }

        private Dictionary<char, List<char>> GetSteps(string[] lines)
        {
            Dictionary<char, List<char>> steps = new Dictionary<char, List<char>>();

            foreach (string step in lines)
            {
                Regex regex = new Regex(@"Step\s(\w)\smust\sbe\sfinished\sbefore\sstep\s(\w)\scan\sbegin.");
                Match match = regex.Match(step);

                if (match.Success)
                {
                    char step1 = Char.Parse(match.Groups[1].Value);
                    char step2 = Char.Parse(match.Groups[2].Value);

                    if (!steps.ContainsKey(step1))
                        steps[step1] = new List<char>();
                    if (!steps.ContainsKey(step2))
                        steps[step2] = new List<char>();

                    steps[step1].Add(step2);
                }
            }

            return steps;
        }

        private bool ArePrerequisitesSatisfied(char inputKey, Dictionary<char, List<char>> steps, String path)
        {
            bool prerequisitesSatisfied = true;
            foreach (char key in steps.Keys)
            {
                if (steps[key].Contains(inputKey) && !path.Contains(key))
                {
                    prerequisitesSatisfied = false;
                    break;
                }
            }

            return prerequisitesSatisfied;
        }

        private List<char> GetFirstPositions(Dictionary<char, List<char>> steps)
        {
            List<char> firstPositions = new List<char>();

            foreach (char currentKey in steps.Keys)
            {
                bool waitingForKey = false;
                foreach (char key in steps.Keys)
                {
                    if (steps[key].Contains(currentKey))
                    {
                        waitingForKey = true;
                        break;
                    }
                }

                if (!waitingForKey)
                    firstPositions.Add(currentKey);
            }

            firstPositions.Sort((x, y) => x.CompareTo(y));

            return firstPositions;
        }
    }
}
