using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.solutions
{
    class Day_04_Guards : IRunnable
    {
        private enum Action { BeginsShift, FallsAsleep, WakesUp };

        public void Run()
        {
            string[] lines = InputReader.GetInput(4);
            List<(int, Action, DateTime)> sortedLogs = SortInput(lines);
            Dictionary<int, int[]> sleepingGuards = GetGuardSleepingData(sortedLogs);
            (int, int) mostAsleepGuard = GetMostAsleepGuard(sleepingGuards);
            Console.WriteLine(mostAsleepGuard);
        }

        private List<(int, Action, DateTime)> SortInput(string[] lines)
        {
            Regex regex = new Regex(@"\[(\d+)-(\d+)-(\d+)\s(\d+):(\d+)]\s(.+)");

            List<(int, Action, DateTime)> logDates = new List<(int, Action, DateTime)>();

            foreach (string line in lines)
            {
                Match match = regex.Match(line);

                if (match.Success)
                {
                    DateTime dateTime = 
                        DateTime.Parse(match.Groups[1].Value + "-" + 
                        match.Groups[2].Value + "-" + 
                        match.Groups[3].Value + " " +
                        match.Groups[4].Value + ":" +
                        match.Groups[5].Value);

                    string actionStr = match.Groups[6].Value;
                    Action action;
                    int guardId = -1;

                    if (actionStr == "wakes up")
                        action = Action.WakesUp;
                    else if (actionStr.Contains("begins shift"))
                    {
                        Regex shiftRegex = new Regex(@"Guard\s#(\d+).+");
                        Match shiftMatch = shiftRegex.Match(line);

                        if (shiftMatch.Success)
                            guardId = Int32.Parse(shiftMatch.Groups[1].Value);
                        else
                            throw new Exception("Guard id not matching");

                        action = Action.BeginsShift;
                    }
                    else if (actionStr.Contains("falls asleep"))
                        action = Action.FallsAsleep;
                    else
                        throw new Exception("Invalid input");

                    logDates.Add((guardId, action, dateTime));
                }
            }
            
            logDates.Sort((a, b) => a.Item3.CompareTo(b.Item3));

            return logDates;
        }

        private Dictionary<int, int[]> GetGuardSleepingData(List<(int, Action, DateTime)> sortedLogs)
        {
            int fellAsleep = -1, wokeUp = -1;
            Dictionary<int, int[]> guards = new Dictionary<int, int[]>();

            int i = 0;

            while(i < sortedLogs.Count)
            {
                (int, Action, DateTime) logEntry = sortedLogs[i];
                int[] minutes = new int[60];

                if (guards.ContainsKey(logEntry.Item1))
                    minutes = guards[logEntry.Item1];

                i++;

                while(i < sortedLogs.Count && sortedLogs[i].Item2 != Action.BeginsShift)
                {
                    if (sortedLogs[i].Item2 == Action.FallsAsleep)
                        fellAsleep = sortedLogs[i].Item3.Minute;
                    else if (sortedLogs[i].Item2 == Action.WakesUp)
                    {
                        wokeUp = sortedLogs[i].Item3.Minute;

                        for (int k = fellAsleep; k < wokeUp; k++)
                        {
                            minutes[k]++;
                        }
                    }

                    i++;
                }

                guards[logEntry.Item1] = minutes;
            }

            return guards;
        }

        private (int, int) GetMostAsleepGuard(Dictionary<int, int[]> sleepingGuards)
        {
            int mostAsleepGuardId = -1, mostNumberOfHours = 0, mostPopularMinute, maxIndex;
            int mostSleptMinute = -1, minuteIndex = -1, mostSleptMinuteGuardId = -1;

            foreach(int key in sleepingGuards.Keys)
            {
                int[] minutes = sleepingGuards[key];
                int totalMinutes = 0;

                for(int i = 0; i < minutes.Length; i++)
                {
                    totalMinutes += minutes[i];

                    if (minutes[i] > mostSleptMinute)
                    {
                        mostSleptMinute = minutes[i];
                        minuteIndex = i;
                        mostSleptMinuteGuardId = key;
                    }
                }

                if(totalMinutes > mostNumberOfHours)
                {
                    mostNumberOfHours = totalMinutes;
                    mostAsleepGuardId = key;
                }
            }

            mostPopularMinute = sleepingGuards[mostAsleepGuardId].Max();
            maxIndex = sleepingGuards[mostAsleepGuardId].ToList().IndexOf(mostPopularMinute);

            int solution1 = maxIndex * mostAsleepGuardId;
            int solution2 = minuteIndex * mostSleptMinuteGuardId;

            return (solution1, solution2);
        }
    }
}
