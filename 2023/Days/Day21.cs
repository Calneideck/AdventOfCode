using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day21 : Day
    {
        string[] lines = File.ReadAllLines("Input/21.txt");
        Dictionary<V2, char> map;

        public override object Part1()
        {
            map = lines.GetMap();

            V2 node = map.First(x => x.Value == 'S').Key;

            List<V2> frontier = new() { node };
            HashSet<V2> closed = new() { node };

            long count = 1;

            for (int i = 1; i <= 64; i++)
            {
                List<V2> nexts = new();

                foreach (var v2 in frontier)
                    foreach (V2 dir in V2.Directions)
                    {
                        V2 next = v2 + dir;
                        if (map.TryGetValue(next, out char c) && c == '.' && !nexts.Contains(next) && !closed.Contains(next))
                        {
                            nexts.Add(next);
                            closed.Add(next);
                            if (i % 2 == 0)
                                count++;
                        }
                    }

                frontier = nexts;
            }

            return count;
        }


        public override object Part2()
        {
            V2 node = map.First(x => x.Value == 'S').Key;

            List<V2> frontier = new() { node };
            HashSet<V2> closed = new() { node };
            HashSet<V2> path = new() { };

            long count = 1;
            int i = 0;

            for (i = 1; i <= int.MaxValue; i++)
            {
                if (!frontier.Any()) break;
                
                List<V2> nexts = new();

                foreach (var v2 in frontier)
                    foreach (V2 dir in V2.Directions)
                    {
                        V2 next = v2 + dir;
                        if (map.TryGetValue(next, out char c) && c == '.' && !nexts.Contains(next) && !closed.Contains(next))
                        {
                            nexts.Add(next);
                            closed.Add(next);
                            if (i % 2 == 0)
                                path.Add(next);
                        }
                    }

                frontier = nexts;
            }

            MyExtensions.Draw(lines, path.Contains);

            return path.Count;
        }
    }
}
