using System.IO;

namespace AdventOfCode
{
    class Day6 : Day
    {
        string[] lines = File.ReadAllLines("Input/6.txt");

        public override object Part1()
        {
            return FindUnique(4);
        }

        public override object Part2()
        {
            return FindUnique(14);
        }

        int FindUnique(int len)
        {
            for (int i = len - 1; i < lines[0].Length; i++)
            {
                bool dupe = false;

                for (int j = len - 1; j >= 0; j--)
                {
                    char c = lines[0][i - j];
                    for (int x = len - 1; x >= 0; x--)
                    {
                        char b = lines[0][i - x];
                        if (c == b && j != x)
                            dupe = true;
                    }
                }


                if (!dupe)
                    return i + 1;
            }

            return 0;
        }
    }
}
