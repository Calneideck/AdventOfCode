using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day10 : Day
    {
        string[] lines = File.ReadAllLines("Input/10.txt");
        List<string> incompletes = new();

        public override object Part1()
        {
            int count = 0;
            Dictionary<char, int> map = new()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };
            Dictionary<char, char> match = new()
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' },
                { '>', '<' },
            };

            foreach (var line in lines)
            {
                Stack<char> s = new();
                bool corrupt = false;

                for (int i = 0; i < line.Length; i++)
                    if (line[i] == '[' || line[i] == '(' || line[i] == '{' || line[i] == '<')
                        s.Push(line[i]);
                    else if (match[line[i]] != s.Pop())
                    {
                        count += map[line[i]];
                        corrupt = true;
                        break;
                    }

                if (!corrupt)
                    incompletes.Add(line);
            }

            return count;
        }


        public override object Part2()
        {
            Dictionary<char, int> map = new()
            {
                { '(', 1 },
                { '[', 2 },
                { '{', 3 },
                { '<', 4 },
            };
            List<long> scores = new();

            foreach (var line in incompletes)
            {
                Stack<char> s = new();

                for (int i = 0; i < line.Length; i++)
                    if (line[i] == '[' || line[i] == '(' || line[i] == '{' || line[i] == '<')
                        s.Push(line[i]);
                    else
                        s.Pop();

                long score = 0;
                while (s.Count > 0)
                {
                    score *= 5;
                    score += map[s.Pop()];
                }

                scores.Add(score);
            }

            scores.Sort();
            return scores[(int)Math.Floor(scores.Count / 2f)];
        }
    }
}
