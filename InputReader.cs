using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2018
{
    class InputReader
    {
        public static string[] getInput(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\resources\" + fileName + ".txt");
            return lines;
        }
    }
}
