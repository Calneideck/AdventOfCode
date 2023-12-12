using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{

    class Day12 : Day
    {
        string[] lines = File.ReadAllLines("Input/12.txt");
        long unique = 0;

        public override object Part1()
        {
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                string spring = parts[0];
                int[] seq = parts[1].Split(',').Select(int.Parse).ToArray();

                Solve(line, seq);
            }

            return unique;
        }

        void Solve(string s, int[] match)
        {
            for (int x = 0; x < s.Length; x++)
                if (s[x] == '?')
                {
                    Solve(s[..x] + '.' + s[(x + 1)..], match);
                    Solve(s[..x] + '#' + s[(x + 1)..], match);
                    return;
                }

            var matches = new Regex(@"#+").Matches(s);
            if (matches.Count != match.Length) return;

            for (int i = 0; i < match.Length; i++)
                if (matches[i].Value.Length != match[i])
                    return;

            unique++;
        }

        public override object Part2()
        {
            long count = 0;

            return count;
        }

    }
}
