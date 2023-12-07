using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace AdventOfCode
{
    class Day5 : Day
    {
        string[] lines = File.ReadAllLines("Input/5.txt");
        long[] seeds = null;
        List<List<long[]>> map = new();

        public override object Part1()
        {
            foreach (var line in lines)
            {
                if (line.StartsWith("seeds"))
                {
                    seeds = new Regex(@"(\d+)")
                        .Matches(line).Select(a => long.Parse(a.Value))
                        .ToArray();
                }
                else if (line.Contains("-to-"))
                    map.Add(new List<long[]>());
                else if (line.Length > 1)
                {
                    // numbers
                    long[] nums = new Regex(@"(\d+)")
                        .Matches(line).Select(a => long.Parse(a.Value))
                        .ToArray();
                    map.Last().Add(new long[] { nums[0], nums[1], nums[2] });
                }
            }

            return seeds.Min((seed) => FindMin(seed, 1));
        }

        public override object Part2()
        {
            Task[] tasks = new Task[seeds.Length / 2];
            ConcurrentBag<long> bag = new();

            for (int i = 0; i < seeds.Length; i += 2)
            {
                int index = i;
                tasks[index / 2] = Task.Factory.StartNew(() =>
                {
                    bag.Add(FindMin(seeds[index], seeds[index + 1]));
                });
            }

            Task.WaitAll(tasks.ToArray());

            return bag.Min();
        }

        long FindMin(long start, long length)
        {
            long min = long.MaxValue;
            long end = start + length;

            for (long x = start; x < end; x++)
            {
                long seed = x;

                int mapLen = map.Count;
                for (int i = 0; i < mapLen; i++)
                {
                    int map2Len = map[i].Count;
                    for (int j = 0; j < map2Len; j++)
                    {
                        long s = map[i][j][1];
                        if (seed >= s && seed <= s + map[i][j][2])
                        {
                            seed = map[i][j][0] + (seed - s);
                            break;
                        }
                    }
                }

                min = Math.Min(min, seed);
            }

            return min;
        }
    }
}
