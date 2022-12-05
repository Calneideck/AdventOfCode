using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day3 : Day
    {
        string[] lines = File.ReadAllLines("Input/3.txt");

        public override object Part1()
        {
            int count = 0;

            foreach (var line in lines)
            {
                string a = line.Substring(0, line.Length / 2);
                string b = line.Substring(line.Length / 2);
                char c = a.Intersect(b).First();

                int score = c.ToString().ToUpper()[0] - 64;
                if (c.ToString().ToUpper()[0] == c)
                    score += 26;

                count += score;
            }

            return count;
        }

        public override object Part2()
        {
            int count = 0;

            for (int i = 0; i < lines.Length; i += 3)
            {
                char c = lines[i].Intersect(lines[i + 1]).Intersect(lines[i + 2]).First();

                int score = c.ToString().ToUpper()[0] - 64;
                if (c.ToString().ToUpper()[0] == c)
                    score += 26;

                count += score;
            }

            return count;
        }
    }
}
