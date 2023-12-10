using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{

    class Day9 : Day
    {
        string[] lines = File.ReadAllLines("Input/9.txt");

        public override object Part1()
        {
            long count = 0;

            foreach (string line in lines)
            {
                List<List<int>> diffs = new()
                {
                    new List<int>(line.Split(" ").Select(int.Parse).ToArray())
                };

                while (!diffs.Last().All(x => x == 0))
                {
                    List<int> toCheck = diffs.Last();

                    diffs.Add(new List<int>());
                    for (int x = 1; x < toCheck.Count; x++)
                        diffs.Last().Add(toCheck[x] - toCheck[x - 1]);
                }

                count += diffs.Aggregate(0, (sum, diffs) => sum + diffs.Last());
            }

            return count;
        }

        public override object Part2()
        {
            long count = 0;

            foreach (string line in lines)
            {
                List<List<int>> diffs = new()
                {
                    new List<int>(line.Split(" ").Select(int.Parse).ToArray())
                };

                while (!diffs.Last().All(x => x == 0))
                {
                    List<int> toCheck = diffs.Last();

                    diffs.Add(new List<int>());
                    for (int x = 1; x < toCheck.Count; x++)
                        diffs.Last().Add(toCheck[x] - toCheck[x - 1]);
                }

                diffs.Reverse();
                count += diffs.Aggregate(0, (sum, diffs) => diffs[0] - sum);
            }

            return count;
        }
    }
}
