using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day7 : Day
    {
        string[] lines = File.ReadAllLines("Input/7.txt");
        Dictionary<V2, char> map = new();

        public override object Part1()
        {
            long count = 0;
            map = lines.GetMap();
            V2 start = map.First(x => x.Value == 'S').Key;
            List<V2> points = new() { start };

            while (points.Count > 0)
            {
                List<V2> nextList = new();

                foreach (V2 point in points)
                {
                    V2 next = point + V2.Down;
                    if (map.TryGetValue(next, out char c))
                        if (c == '.')
                        {
                            if (!nextList.Contains(next))
                                nextList.Add(next);
                        }
                        else if (c == '^')
                        {
                            if (!nextList.Contains(next + V2.Left))
                                nextList.Add(next + V2.Left);

                            if (!nextList.Contains(next + V2.Right))
                                nextList.Add(next + V2.Right);

                            count++;
                        }
                }

                points = nextList;
            }

            return count;
        }

        Dictionary<V2, ulong> counts = new();

        public override object Part2()
        {
            V2 start = map.First(x => x.Value == 'S').Key;

            return Solve(start);
        }

        ulong Solve(V2 point)
        {
            while (true)
            {
                V2 next = point + V2.Down;
                
                if (map.TryGetValue(next, out char c))
                {
                    if (c == '.') point = next;
                    else if (c == '^')
                        if (counts.TryGetValue(next, out ulong value))
                            return value;
                        else
                        {
                            ulong left = Solve(point + V2.Left);
                            ulong right = Solve(point + V2.Right);
                            counts[next] = left + right;
                            return left + right;
                        }
                }
                else
                    return 1;
            }
        }
    }
}
