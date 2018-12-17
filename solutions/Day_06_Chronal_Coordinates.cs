
using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018.solutions
{
    class Day_06_Chronal_Coordinates : IRunnable
    {
        public void Run()
        {
            string[] lines = InputReader.GetInput(6);

            //List<(int x, int y)> testPoints = new List<(int x, int y)>() {
            //    (1, 1),
            //    (1, 6),
            //    (8, 3),
            //    (3, 4),
            //    (5, 5),
            //    (8, 9)
            //};

            List<(int x, int y)> testBPoints = new List<(int x, int y)>() {
                (1, 1),
                (1, 101),
                (48, 51),
                (51, 48),
                (51, 51),
                (51, 54),
                (54, 51),
                (101, 1),
                (101, 101)
            };

            //List<(int x, int y)> chronalPoints = GetChronalPoints(lines);
            //List<(int x, int y)> chronalPoints = testPoints;
            List<(int x, int y)> chronalPoints = testBPoints;

            (int x, int y) maxCoordinates = GetMaxCoordinates(chronalPoints);
            (int x, int y) minCoordinates = GetMinCoordinates(chronalPoints);

            chronalPoints = chronalPoints.Where(p => !IsInfinite(maxCoordinates, minCoordinates, p)).ToList();

            maxCoordinates = GetMaxCoordinates(chronalPoints);
            minCoordinates = GetMinCoordinates(chronalPoints);

            int[] pointAreas = GetPointAreas(maxCoordinates, minCoordinates, chronalPoints);
            int largestArea = GetLargestArea(pointAreas);

            Console.WriteLine(largestArea);
        }

        private int GetLargestArea(int[] pointAreas)
        {
            return pointAreas.Max();
        }

        private bool IsInfinite((int x, int y) maxCoordinates, (int x, int y) minCoordinates, (int x, int y) coord)
        {
            if (coord.y == minCoordinates.y || coord.y == maxCoordinates.y ||
                        coord.x == minCoordinates.x || coord.x == maxCoordinates.x)
                return true;
            else
                return false;
        }

        private int[] GetPointAreas((int x, int y) maxCoordinates, (int x, int y) minCoordinates, List<(int x, int y)> chronalPoints)
        {
            int[] pointAreas = new int[chronalPoints.Count];

            for (int y = minCoordinates.y; y <= maxCoordinates.y; y++) {
                for(int x = minCoordinates.x; x <= maxCoordinates.x; x++)
                {
                    int closestPoint = -1;
                    int closestDistance = Int32.MaxValue;
                    bool multiplePointsClose = false;

                    for(int pointIndex = 0; pointIndex < chronalPoints.Count; pointIndex++)
                    {
                        int distance = GetManhattanDistance((x, y), chronalPoints[pointIndex]);

                        if(distance == closestDistance)
                            multiplePointsClose = true;
                        else if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestPoint = pointIndex;
                            multiplePointsClose = false;
                        }
                    }

                    if (!multiplePointsClose)
                        pointAreas[closestPoint]++;

                }
            }
            return pointAreas;
        }

        private int GetManhattanDistance((int x, int y) vector1, (int x, int y) vector2)
        {
            int result = Math.Abs(vector1.x - vector2.x) + Math.Abs(vector1.y - vector2.y);

            return result;
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

        private (int, int) GetMinCoordinates(List<(int x, int y)> chronalPoints)
        {
            int minX = chronalPoints.Select(c => c.x).Min();
            int minY = chronalPoints.Select(c => c.y).Min();

            return (minX, minY);
        }
    }

}
