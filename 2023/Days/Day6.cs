using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day6 : Day
    {
        string[] lines = File.ReadAllLines("Input/6.txt");
        int[] times = null;
        int[] distances = null;

        public override object Part1()
        {
            int count = 1;

            times = new Regex(@"(\d+)").Matches(lines[0]).Select(a => int.Parse(a.Value)).ToArray();
            distances = new Regex(@"(\d+)").Matches(lines[1]).Select(a => int.Parse(a.Value)).ToArray();
            V2[] races = times.Select((a, i) => new V2(a, distances[i])).ToArray();

            foreach (V2 race in races)
            {
                int wins = 0;
                for (int i = 1; i < race.x; i++) {
                    int maxDist = (race.x - i) * i;
                    if (maxDist > race.y) {
                        wins++;
                    }
                }
                count *= wins;
            }

            return count;
        }

        public override object Part2()
        {
            long time = long.Parse(string.Join("", times));
            long distance = long.Parse(string.Join("", distances));

            double left = (-time - Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / 2;
            double right = (-time + Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / 2;

            return Math.Floor(right) - Math.Ceiling(left) + 1;
        }
    }
}
