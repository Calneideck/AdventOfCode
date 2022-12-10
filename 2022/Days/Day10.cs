using System;
using System.IO;

namespace AdventOfCode
{
    class Day10 : Day
    {
        string[] lines = File.ReadAllLines("Input/10.txt");

        public override object Part1()
        {
            int X = 1;
            int c = 0;
            int ss = 0;

            foreach (var line in lines)
            {
                c++;
                int V = int.MaxValue;
                if (line.StartsWith("addx"))
                    V = int.Parse(line.Split()[1]);

                if (c == 20 || (c - 20) % 40 == 0)
                    ss += X * c;

                if (V < int.MaxValue)
                {
                    c++;
                    if (c == 20 || (c - 20) % 40 == 0)
                        ss += X * c;

                    X += V;
                }
            }

            return ss;
        }

        public override object Part2()
        {
            int X = 1;
            int c = 0;

            Console.WriteLine();
            foreach (var line in lines)
            {
                c++;
                int V = int.MaxValue;
                if (line.StartsWith("addx"))
                    V = int.Parse(line.Split()[1]);

                Console.Write(Math.Abs((c % 40) - 1 - X) < 2 ? '█' : ' ');
                bool newline = false;
                if (c % 40 == 0)
                {
                    Console.WriteLine();
                    newline = true;
                }

                if (V < int.MaxValue)
                {
                    c++;
                    Console.Write(Math.Abs((c % 40) - 1 - X) < 2 ? '█' : ' ');

                    X += V;
                }

                if (c % 40 == 0 && !newline)
                    Console.WriteLine();
            }

            return 0;
        }
    }
}
