namespace advent_of_code_2018
{
    class InputReader
    {
        public static string[] GetInput(int dayNo)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\resources\input" + dayNo + ".txt");
            return lines;
        }
    }
}
