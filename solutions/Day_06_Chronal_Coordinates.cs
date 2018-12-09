
using System;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018.solutions
{
    class Day_06_Chronal_Coordinates
    {
        public static void Run()
        {
            string[] lines = InputReader.GetInput("input6");

            List<(int x, int y)> testPoints = new List<(int x, int y)>() {
                (1, 1),
                (1, 6),
                (8, 3),
                (3, 4),
                (5, 5),
                (8, 9)
            };

            List<(int x, int y)> chronalPoints = GetChronalPoints(lines);
            //List<(int x, int y)> chronalPoints = testPoints;

            (int x, int y) maxCoordinates = GetMaxCoordinates(chronalPoints);
            (int x, int y) minCoordinates = GetMinCoordinates(chronalPoints);

            List<int> infinitePoints = GetInfinitePoints(chronalPoints, maxCoordinates, minCoordinates);

            int[,] chronalSpace = GetChronalSpace(maxCoordinates, minCoordinates, chronalPoints);

            int largestArea = GetLargestArea(chronalSpace, chronalPoints, infinitePoints);

            Console.WriteLine(largestArea);
        }

        private static List<int> GetInfinitePoints(List<(int x, int y)> chronalPoints, (int x, int y) maxCoordinates, (int x, int y) minCoordinates)
        {
            List<int> infinitePoints = new List<int>();

            for(int i = 0; i < chronalPoints.Count; i++)
            {
                var point = chronalPoints[i];

                if (point.x == minCoordinates.x || point.x == maxCoordinates.x ||
                    point.y == minCoordinates.y || point.y == maxCoordinates.y)
                    infinitePoints.Add(i);
            }

            return infinitePoints;
        }

        private static int[,] GetChronalSpace((int x, int y) maxCoordinates, (int x, int y) minCoordinates, List<(int x, int y)> chronalPoints)
        {

            int[,] chronalSpace = new int[maxCoordinates.x, maxCoordinates.y];

            for (int y = 0; y < maxCoordinates.y; y++) {
                for(int x = 0; x < maxCoordinates.x; x++)
                {
                    if(y < minCoordinates.y || x < minCoordinates.x)
                    {
                        chronalSpace[x, y] = -1;
                        continue;
                    }

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

                    if (multiplePointsClose)
                        chronalSpace[x, y] = -1;
                    else
                        chronalSpace[x, y] = closestPoint;

                }
            }
            return chronalSpace;
        }

        private static int GetManhattanDistance((int x, int y) vector1, (int x, int y) vector2)
        {
            int result = Math.Abs(vector1.x - vector2.x) + Math.Abs(vector1.y - vector2.y);

            return result;
        }

        private static List<(int, int)> GetChronalPoints(string[] lines)
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

        private static (int, int) GetMaxCoordinates(List<(int x, int y)> chronalPoints)
        {
            int maxX = chronalPoints.Select(c => c.x).Max();
            int maxY = chronalPoints.Select(c => c.y).Max();

            return (maxX, maxY);
        }

        private static (int, int) GetMinCoordinates(List<(int x, int y)> chronalPoints)
        {
            int minX = chronalPoints.Select(c => c.x).Min();
            int minY = chronalPoints.Select(c => c.y).Min();

            return (minX, minY);
        }

        private static int GetLargestArea(int[,] chronalSpace, List<(int x, int y)> chronalPoints, List<int> infinitePoints)
        {
            int[] pointAreas = new int[chronalPoints.Count];

            for (int y = 0; y < chronalSpace.GetLength(1); y++)
            {
                for (int x = 0; x < chronalSpace.GetLength(0); x++)
                {
                    int closestPointId = chronalSpace[x, y];

                    if (closestPointId != -1 && !infinitePoints.Contains(closestPointId))
                        pointAreas[closestPointId]++;
                }
            }

            int largestArea = pointAreas.Max();

            return largestArea;
        }
    }

}
