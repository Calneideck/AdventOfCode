using System.IO;

namespace AdventOfCode
{
    class Day2 : Day
    {
        string[] lines = File.ReadAllLines("Input/2.txt");

        public override object Part1()
        {
            int count = 0;
            foreach (var line in lines)
            {
                var s = line.Split(' ');

                int score = 0;
                if (s[1] == "X")
                {
                    score = 1;
                    if (s[0] == "C") score += 6;
                    else if (s[0] == "A") score += 3;
                }
                else if (s[1] == "Y")
                {
                    score = 2;
                    if (s[0] == "A") score += 6;
                    else if (s[0] == "B") score += 3;
                }
                else if (s[1] == "Z")
                {
                    score = 3;
                    if (s[0] == "B") score += 6;
                    else if (s[0] == "C") score += 3;
                }

                count += score;
            }

            return count;
        }

        public override object Part2()
        {
            int count = 0;
            foreach (var line in lines)
            {
                var s = line.Split(' ');

                int score = 0;

                if (s[1] == "X")
                {
                    if (s[0] == "A") score += 3;
                    else if (s[0] == "B") score += 1;
                    else score += 2;
                }
                else if (s[1] == "Y")
                {
                    score = 3;
                    if (s[0] == "A") score += 1;
                    else if (s[0] == "B") score += 2;
                    else score += 3;
                }
                else if (s[1] == "Z")
                {
                    score = 6;
                    if (s[0] == "A") score += 2;
                    else if (s[0] == "B") score += 3;
                    else score += 1;
                }

                count += score;
            }

            return count;
        }
    }
}
