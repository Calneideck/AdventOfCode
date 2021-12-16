using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day14 : Day
    {
        string[] lines = File.ReadAllLines("Input/14.txt");

        public override object Part1()
        {
            string inp = lines[0];
            Dictionary<string, char> map = new();

            for (int i = 2; i < lines.Length; i++)
            {
                var temp = lines[i].Split(" -> ");
                map.Add(temp[0], temp[1][0]);
            }

            for (int x = 0; x < 10; x++)
                for (int i = 0; i < inp.Length - 1; i += 2)
                {
                    string c = inp.Substring(i, 2);
                    char r = map[c];
                    inp = inp.Insert(i + 1, r.ToString());
                }

            Dictionary<char, long> d = new();
            foreach (var c in inp)
            {
                d.TryAdd(c, 0);
                d[c]++;
            }

            return d.Values.Max() - d.Values.Min();
        }

        public override object Part2()
        {
            string inp = lines[0];
            Dictionary<string, char> map = new();
            Dictionary<string, long> counts = new();
            Dictionary<char, long> charCounts = new();

            for (int i = 2; i < lines.Length; i++)
            {
                var temp = lines[i].Split(" -> ");
                map.Add(temp[0], temp[1][0]);
                counts.Add(temp[0], 0);
                charCounts.TryAdd(temp[0][0], 0);
            }

            for (int i = 0; i < inp.Length - 1; i++)
            {
                string c = inp.Substring(i, 2);
                counts[c]++;
                charCounts[c[0]]++;
            }

            charCounts[inp.Last()]++;

            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> newCount = new(counts);

                foreach (var key in counts.Keys)
                    if (counts[key] > 0)
                    {
                        char n = map[key];
                        charCounts[n] += counts[key];
                        newCount[key[0].ToString() + n.ToString()] += counts[key];
                        newCount[n.ToString() + key[1].ToString()] += counts[key];
                        newCount[key] -= counts[key];
                    }

                counts = newCount;
            }

            return charCounts.Values.Max() - charCounts.Values.Min();
        }
    }
}
