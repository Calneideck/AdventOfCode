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
        Dictionary<V2, char> map = new();
        V2 start;
        V2 end;

        int GetLength(V2 start)
        {
            map[start] = 'a';
            map[end] = 'z';

            Queue<V2> open = new();
            HashSet<V2> closed = new();
            Dictionary<V2, V2> parents = new();

            V2 pos = start;
            open.Enqueue(pos);

            while (open.Count > 0)
            {
                pos = open.Dequeue();
                closed.Add(pos);

                foreach (var dir in V2.Directions)
                    if (
                        map.TryGetValue(pos + dir, out char c) &&
                        !closed.Contains(pos + dir) &&
                        !open.Contains(pos + dir)
                    )
                        if (c <= map[pos] || c - map[pos] == 1)
                        {
                            open.Enqueue(pos + dir);
                            parents.Add(pos + dir, pos);
                        }
            }

            pos = end;
            int count = 0;
            while (pos != start)
                if (parents.TryGetValue(pos, out V2 parent))
                {
                    pos = parent;
                    count++;
                }
                else
                    return int.MaxValue;

            return count;
        }

        public override object Part1()
        {
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map.Add(new V2(x, y), lines[y][x]);

            start = map.First((kv) => kv.Value == 'S').Key;
            end = map.First((kv) => kv.Value == 'E').Key;

            return GetLength(start);
        }

        public override object Part2()
        {
            return map
                .Where(kv => kv.Value == 'a')
                .Select(kv => kv.Key)
                .Min(GetLength);
        }
    }
}
