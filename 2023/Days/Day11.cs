using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{

    class Day11 : Day
    {
        string[] lines = File.ReadAllLines("Input/11.txt");
        Dictionary<V2, char> map;
        HashSet<int> emptyRows = new();
        HashSet<int> emptyCols = new();
        KeyValuePair<V2, char>[] galaxies;

        public override object Part1()
        {
            map = lines.GetMap();

            for (int y = 0; y < lines.Length; y++)
                if (map.Where(kv => kv.Key.y == y).All(kv => kv.Value == '.'))
                    emptyRows.Add(y);

            for (int x = 0; x < lines[0].Length; x++)
                if (map.Where(kv => kv.Key.x == x).All(kv => kv.Value == '.'))
                    emptyCols.Add(x);

            galaxies = map.Where(x => x.Value == '#').ToArray();

            return FindLengths(true);
        }

        public override object Part2()
        {
            return FindLengths(false);
        }

        long FindLengths(bool part1)
        {
            long count = 0;

            for (int i = 0; i < galaxies.Length - 1; i++)
                for (int j = i + 1; j < galaxies.Length; j++)
                {
                    V2 dir = galaxies[i].Key - galaxies[j].Key;
                    long dist = dir.Cost;

                    int xMin = Math.Min(galaxies[i].Key.x, galaxies[j].Key.x);
                    int xMax = Math.Max(galaxies[i].Key.x, galaxies[j].Key.x);
                    int yMin = Math.Min(galaxies[i].Key.y, galaxies[j].Key.y);
                    int yMax = Math.Max(galaxies[i].Key.y, galaxies[j].Key.y);

                    for (int x = xMin; x < xMax; x++)
                        if (emptyCols.Contains(x))
                            dist += part1 ? 1 : 1000000 - 1;

                    for (int y = yMin; y < yMax; y++)
                        if (emptyRows.Contains(y))
                            dist += part1 ? 1 : 1000000 - 1;

                    count += dist;
                }

            return count;
        }
    }
}
