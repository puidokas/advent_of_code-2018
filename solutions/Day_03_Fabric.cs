using System;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.solutions
{
    class Day_03_Fabric
    {
        public static void Run()
        {
            string[] lines = InputReader.getInput("input3");

            const int dim = 1000;
            int[,] squareInches = new int[dim, dim];

            int[][] claims = new int[lines.Length][];

            int numberOfOverlappingSquareInches = GetNumberOfOverlappingSquareInches(lines, ref claims, ref squareInches);
            int idOfNotOverlappingClaim = GetIdOfNotOverlappingClaim(claims, squareInches);

            Console.WriteLine((numberOfOverlappingSquareInches, idOfNotOverlappingClaim));
        }

        // How many square inches of fabric are within two or more claims?

        private static int GetNumberOfOverlappingSquareInches(string[] lines, ref int[][] claims, ref int[,] squareInches)
        {
            Regex regex = new Regex(@"#(\d+)\s@\s(\d+),(\d+):\s(\d+)x(\d+)");

            int claimId, leftEdge, topEdge, wide, tall;
            int numOfsquareInchesOverlapping = 0;

            foreach (string line in lines)
            {
                Match match = regex.Match(line);

                if (match.Success)
                {
                    claimId = Int32.Parse(match.Groups[1].Value);
                    leftEdge = Int32.Parse(match.Groups[2].Value);
                    topEdge = Int32.Parse(match.Groups[3].Value);
                    wide = Int32.Parse(match.Groups[4].Value);
                    tall = Int32.Parse(match.Groups[5].Value);

                    claims[claimId - 1] = new int[] { leftEdge, topEdge, wide, tall };


                    for (int y = 0; y < tall; y++)
                    {
                        for (int x = 0; x < wide; x++)
                        {
                            squareInches[leftEdge + x, topEdge + y]++;

                            if (squareInches[leftEdge + x, topEdge + y] == 2)
                                numOfsquareInchesOverlapping++;
                        }
                    }
                }
            }

            return numOfsquareInchesOverlapping;
        }

        // What is the ID of the only claim that doesn't overlap?

        private static int GetIdOfNotOverlappingClaim(int[][] claims, int[,] squareInches)
        {
            int claimId, leftEdge, topEdge, wide, tall;
            bool claimOverlapping = false;
            int claimNotOverlappingId = 0;

            for (int i = 0; i < claims.Length; i++)
            {
                claimId = i + 1;
                leftEdge = claims[i][0];
                topEdge = claims[i][1];
                wide = claims[i][2];
                tall = claims[i][3];

                claimOverlapping = false;

                for (int y = 0; y < tall; y++)
                {
                    for (int x = 0; x < wide; x++)
                    {
                        if (squareInches[leftEdge + x, topEdge + y] > 1)
                            claimOverlapping = true;
                    }
                }

                if (!claimOverlapping)
                    claimNotOverlappingId = claimId;
            }

            return claimNotOverlappingId;
        }
    }
}
