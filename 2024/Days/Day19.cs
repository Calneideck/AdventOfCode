using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day19 : Day
    {
        string[] lines = File.ReadAllLines("Input/19.txt");

        Dictionary<(string, string), bool> cache = new();
        Dictionary<(string, string), long> cache2 = new();

        public override object Part1()
        {
            int sum = 0;

            string[] patterns = lines[0].Split(", ");

            List<string> designs = new();
            for (int i = 2; i < lines.Length; i++)
                designs.Add(lines[i]);

            foreach (string d in designs)
                if (MakeDesign(patterns, d, "")) sum++;

            return sum;
        }

        public override object Part2()
        {
            long sum = 0;
            string[] patterns = lines[0].Split(", ");

            List<string> designs = new();
            for (int i = 2; i < lines.Length; i++)
                designs.Add(lines[i]);

            foreach (string d in designs)
                sum += MakeDesign2(patterns, d, "");

            return sum;
        }

        bool MakeDesign(string[] patterns, string target, string current)
        {
            if (cache.ContainsKey((target, current)))
                return cache[(target, current)];

            bool result = patterns.Any((p) =>
            {
                if (current.Length + p.Length <= target.Length && target.Substring(current.Length, p.Length) == p)
                {
                    if (current + p == target) return true;

                    return MakeDesign(patterns, target, current + p);
                }

                return false;
            });

            cache.TryAdd((target, current), result);

            return result;
        }

        long MakeDesign2(string[] patterns, string target, string current)
        {
            if (cache2.ContainsKey((target, current)))
                return cache2[(target, current)];

            long count = patterns.Sum((p) =>
            {
                if (current.Length + p.Length <= target.Length && target.Substring(current.Length, p.Length) == p)
                {
                    if (current + p == target) return 1;

                    return MakeDesign2(patterns, target, current + p);
                }

                return 0;
            });

            cache2.TryAdd((target, current), count);

            return count;
        }
    }
}
