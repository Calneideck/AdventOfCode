using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day10 : Day
    {
        string[] lines = File.ReadAllLines("Input/10.txt");
        Dictionary<V2, int> map = new();

        public override object Part1()
        {
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map[new V2(x, y)] = int.Parse(lines[y][x].ToString());


            return map.Where(kv => kv.Value == 0).Sum(kv => FindTrails(kv.Key, true));
        }

        public override object Part2()
        {
            return map.Where(kv => kv.Value == 0).Sum(kv => FindTrails(kv.Key, false));
        }

        int FindTrails(V2 start, bool distinct)
        {
            int trails = 0;
            HashSet<V2> distinctTrails = new();
            Queue<(V2, int)> q = new();

            q.Enqueue((start, 0));

            while (q.Any())
            {
                var n = q.Dequeue();

                foreach (V2 dir in V2.Directions)
                    if (map.ContainsKey(n.Item1 + dir))
                    {
                        int c = map[n.Item1 + dir];
                        if (n.Item2 + 1 == c)
                        {
                            if (c == 9)
                            {
                                distinctTrails.Add(n.Item1 + dir);
                                trails++;
                            }
                            else
                                q.Enqueue((n.Item1 + dir, c));
                        }
                    }
            }

            return distinct ? distinctTrails.Count : trails;
        }
    }
}
