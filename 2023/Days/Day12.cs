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
        Regex hashes = new(@"#+");
        List<long> uniqueCounts = new();

        public override object Part1()
        {
            foreach (string line in lines)
            {
                unique = 0;
                string[] parts = line.Split(' ');
                string spring = parts[0];
                int[] seq = parts[1].Split(',').Select(int.Parse).ToArray();

                Solve(spring, seq);

                uniqueCounts.Add(unique);
            }

            return uniqueCounts.Sum();
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

            var matches = hashes.Matches(s);
            int len = match.Length;
            if (matches.Count != len) return;

            for (int i = 0; i < len; i++)
                if (matches[i].Value.Length != match[i])
                    return;

            unique++;
        }

        public override object Part2()
        {
            long part2 = 0;
            
            List<long> unique2 = new();

            int c = 0;

            foreach (string line in lines)
            {
                unique = 0;
                string[] parts = line.Split(' ');
                string spring = parts[0] + "?" + parts[0];
                int[] seq = (parts[1] + "," + parts[1]).Split(',').Select(int.Parse).ToArray();

                Solve(spring, seq);

                unique2.Add(unique);

                Console.WriteLine(++c);
            }

            for (int i = 0; i < lines.Length; i++) {
                long factor = unique2[i] / uniqueCounts[i];
                part2 += uniqueCounts[i] * (long)Math.Pow(factor, 4);
            }

            return part2;
        }
    }
}
