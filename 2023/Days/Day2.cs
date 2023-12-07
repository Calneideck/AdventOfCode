using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day2 : Day
    {
        string[] lines = File.ReadAllLines("Input/2.txt");
        int r = 12;
        int g = 13;
        int b = 14;

        public override object Part1()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(": ");

                int gameId = int.Parse(parts[0].Split(" ")[1]);
                IEnumerable<string[]> parts2 = parts[1].Split("; ").Select(a => a.Split(", "));

                bool allGood = true;

                foreach (string[] draw in parts2)
                    foreach (string cubes in draw)
                    {
                        string[] cube = cubes.Split(" ");
                        int count = int.Parse(cube[0].ToString());
                        string colour = cube[1];

                        if (count > GetColourMax(colour))
                        {
                            allGood = false;
                            break;
                        }
                    }

                if (allGood) sum += gameId;
            }

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(": ");

                IEnumerable<string[]> parts2 = parts[1].Split("; ").Select(a => a.Split(", "));

                Dictionary<string, int> maxCounts = new()
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };

                foreach (string[] draw in parts2)
                    foreach (string cubes in draw)
                    {
                        string[] cube = cubes.Split(" ");
                        int count = int.Parse(cube[0].ToString());
                        string colour = cube[1];

                        maxCounts[colour] = Math.Max(maxCounts[colour], count);
                    }

                sum += maxCounts.Values.Aggregate(1, (sum, x) => sum * x);
            }

            return sum;
        }

        int GetColourMax(string input)
        {
            return input switch
            {
                "red" => r,
                "green" => g,
                "blue" => b,
                _ => 0,
            };
        }
    }
}
