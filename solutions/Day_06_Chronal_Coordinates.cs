
using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018.solutions
{
    class Day_06_Chronal_Coordinates : IRunnable
    {
        const int TotalDistanceToAllPoints = 10000;

        public void Run()
        {
            string[] lines = InputReader.GetInput(6);

            List<(int x, int y)> chronalPoints = GetChronalPoints(lines);

            (int x, int y) maxCoordinates = GetMaxCoordinates(chronalPoints);

            (int largestArea, int region) = GetSolution(maxCoordinates, chronalPoints);

            Console.WriteLine((largestArea, region));
        }

        private (int, int) GetSolution((int x, int y) maxCoordinates, List<(int x, int y)> chronalPoints)
        {
            int[] pointAreas = new int[chronalPoints.Count];
            int region = 0;

            for (int y = 0; y <= maxCoordinates.y; y++) {
                for(int x = 0; x <= maxCoordinates.x; x++)
                {
                    int closestPoint = -1;
                    int closestDistance = Int32.MaxValue;
                    bool multiplePointsClose = false;
                    int totalDistance = 0;

                    for(int pointIndex = 0; pointIndex < chronalPoints.Count; pointIndex++)
                    {
                        int distance = GetManhattanDistance((x, y), chronalPoints[pointIndex]);
                        totalDistance += distance;

                        if (distance == closestDistance)
                            multiplePointsClose = true;
                        else if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPoint = pointIndex;
                            multiplePointsClose = false;
                        }
                    }

                    if (totalDistance < TotalDistanceToAllPoints)
                        region++;

                    if (x == 0 || x == maxCoordinates.x || y == 0 || y == maxCoordinates.y)
                        pointAreas[closestPoint] = -1;
                    else if(!multiplePointsClose && pointAreas[closestPoint] != -1)
                        pointAreas[closestPoint]++;

                }
            }

            return (pointAreas.Max(), region);
        }

        private int GetManhattanDistance((int x, int y) vector1, (int x, int y) vector2)
        {
            return Math.Abs(vector1.x - vector2.x) + Math.Abs(vector1.y - vector2.y);
        }

        private List<(int, int)> GetChronalPoints(string[] lines)
        {
            List<(int, int)> chronalPoints = new List<(int, int)>();

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(',');
                int x = Int32.Parse(splitLine[0]);
                int y = Int32.Parse(splitLine[1].TrimStart());
                chronalPoints.Add((x, y));
            }

            return chronalPoints;
        }

        private (int, int) GetMaxCoordinates(List<(int x, int y)> chronalPoints)
        {
            int maxX = chronalPoints.Select(c => c.x).Max();
            int maxY = chronalPoints.Select(c => c.y).Max();

            return (maxX, maxY);
        }

    }

}
