namespace advent_of_code_2018
{
    class InputReader
    {
        public static string[] GetInput(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\resources\" + fileName + ".txt");
            return lines;
        }
    }
}
