using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day8 : Day
    {
        string[] lines = File.ReadAllLines("Input/8.txt");
        Dictionary<V2, char> map = new();

        public override object Part1()
        {
            HashSet<V2> nodes = new();

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map[new V2(x, y)] = lines[y][x];

            foreach (var kv in map)
                if (kv.Value != '.')
                    foreach (var kv2 in map)
                        if (kv2.Value == kv.Value && kv.Key != kv2.Key)
                        {
                            V2 dir = kv2.Key - kv.Key;

                            V2 n1 = kv2.Key + dir;
                            V2 n2 = kv.Key - dir;

                            if (map.ContainsKey(n1) && !nodes.Contains(n1)) nodes.Add(n1);
                            if (map.ContainsKey(n2) && !nodes.Contains(n2)) nodes.Add(n2);
                        }

            return nodes.Count;
        }

        public override object Part2()
        {
            HashSet<V2> nodes = new();

            foreach (var kv in map)
                if (kv.Value != '.')
                    foreach (var kv2 in map)
                        if (kv2.Value == kv.Value && kv.Key != kv2.Key)
                        {
                            V2 dir = kv2.Key - kv.Key;
                            V2 p = kv.Key + dir;

                            while (map.ContainsKey(p))
                            {
                                if (!nodes.Contains(p)) nodes.Add(p);
                                p += dir;
                            }

                            p = kv.Key - dir;

                            while (map.ContainsKey(p))
                            {
                                if (!nodes.Contains(p)) nodes.Add(p);
                                p -= dir;
                            }
                        }

            return nodes.Count;
        }
    }
}
