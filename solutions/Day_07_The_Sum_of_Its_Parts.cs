using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.solutions
{
    class Day_07_The_Sum_of_Its_Parts : IRunnable
    {
        public void Run()
        {
            //string[] lines = InputReader.GetInput(7);

            string[] lines = {
                "Step C must be finished before step A can begin.",
                "Step C must be finished before step F can begin.",
                "Step A must be finished before step B can begin.",
                "Step A must be finished before step D can begin.",
                "Step B must be finished before step E can begin.",
                "Step D must be finished before step E can begin.",
                "Step F must be finished before step E can begin."
            };

            Dictionary<char, List<char>> steps = GetSteps(lines);

            string orderOfSteps = GetOrderOfSteps(steps, 2);

            //int timeToWork = 0;
            //foreach (char step in orderOfSteps)
            //{
            //    timeToWork += GetSecondsToWork(step);
            //}

            Console.WriteLine(orderOfSteps);
        }

        private string GetOrderOfSteps(Dictionary<char, List<char>> steps, int numberOfWorkers)
        {
            List<char> path = new List<char>();
            List<char> keysWaitingToBeActivated = new List<char>();

            List<(char, int)> nextKeys;
            List<char> firstPositions = GetFirstPositions(steps);

            foreach (char position in firstPositions)
                keysWaitingToBeActivated.Add(position);

            int timeToWork = 0;

            while (path.Count < steps.Count)
            {
                keysWaitingToBeActivated.Sort((x, y) => x.CompareTo(y));

                //char nextKey = '\0';
                nextKeys = new List<(char, int)>();

                for (int j = 0; j < keysWaitingToBeActivated.Count; j++)
                {
                    if (ArePrerequisitesSatisfied(keysWaitingToBeActivated[j], steps, path))
                    {
                        if (nextKeys.Count > 0)
                        {
                            foreach ((char _char, int _count) nextKey in nextKeys)
                            {
                                if (nextKey._count == 0)
                                {
                                    int secondsToWork = GetSecondsToWork(keysWaitingToBeActivated[j]);
                                    //nextKey
                                }
                            }
                        }
                        else
                        {
                            if (nextKeys.Count < numberOfWorkers)
                            {
                                int secondsToWork = GetSecondsToWork(keysWaitingToBeActivated[j]);
                                nextKeys.Add((keysWaitingToBeActivated[j], secondsToWork));
                                timeToWork += secondsToWork;
                            }
                        }
                        //else
                        //    break;
                        //nextKey = keysWaitingToBeActivated[j];
                        //break;
                    }
                }

                for(int i = 0; i < nextKeys.Count; i++)
                {
                    // key is idle
                    if (nextKeys[i].Item2 > 0)
                    {
                        nextKeys[i] = (nextKeys[i].Item1, nextKeys[i].Item2 - 1);
                        continue;
                    } else
                    {
                        char nextKey = nextKeys[i].Item1;

                        List<char> dependencies = steps[nextKey];

                        foreach (char dependency in dependencies)
                        {
                            if (!keysWaitingToBeActivated.Contains(dependency))
                                keysWaitingToBeActivated.Add(dependency);
                        }

                        path.Add(nextKey);

                        keysWaitingToBeActivated.Remove(nextKey);
                    }
                }

            }

            var orderOfSteps = String.Concat(path.Select(o => o));

            return orderOfSteps;
        }

        private int GetSecondsToWork(char inputChar)
        {
            return((int)inputChar - 64);
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

        private bool ArePrerequisitesSatisfied(char keyA, Dictionary<char, List<char>> steps, List<char> path)
        {
            List<char> nextPositions = new List<char>();

            bool waitingForKey = false;
            foreach (char key in steps.Keys)
            {
                if (steps[key].Contains(keyA) && !path.Contains(key))
                {
                    waitingForKey = true;
                    break;
                }
            }

            return !waitingForKey;
        }

        private List<char> GetFirstPositions(Dictionary<char, List<char>> steps)
        {
            List<char> nextPositions = new List<char>();

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
                    nextPositions.Add(currentKey);
            }

            nextPositions.Sort((x, y) => x.CompareTo(y));

            return nextPositions;
        }
    }



    //class Node
    //{
    //    public SortedList<char> waitingFor;

    //    public Node()
    //    {

    //    }
    //}

    class LinkedList
    {
        private Node head;

        public LinkedList(Node head)
        {
            this.head = head;
        }

        public Node GetHeadNode()
        {
            return head;
        }
    }

    class Node
    {
        private List<char> connectedNodes;
        private char value;

        public Node(char value)
        {
            this.connectedNodes = new List<char>();
            this.value = value;
        }

        public void AddConnection(char node)
        {
            connectedNodes.Add(node);
        }

        public bool Contains(char node)
        {
            return connectedNodes.Contains(node);
        }

        public char GetValue()
        {
            return value;
        }
    }
}
