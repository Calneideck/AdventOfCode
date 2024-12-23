using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day16 : Day
    {
        string[] lines = File.ReadAllLines("Input/16.txt");

        class Point : IComparable<Point>
        {
            public V2 pos;
            public V2 dir;
            public int cost;
            public Point parent;

            public int CompareTo(Point other)
            {
                return cost - other.cost;
            }
        }

        public override object Part1()
        {
            V2 start = V2.Zero;
            V2 end = V2.Zero;
            long min = long.MaxValue;

            Dictionary<V2, bool> map = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                {
                    map[new V2(x, y)] = lines[y][x] == '#';
                    if (lines[y][x] == 'S') start = new V2(x, y);
                    if (lines[y][x] == 'E') end = new V2(x, y);
                }

            BTSortedList<Point> points = new();
            HashSet<V2> close = new();
            HashSet<V2> path = new() { start, end };

            points.Add(new Point() { pos = start, cost = 0, dir = V2.Right });

            while (points.Count > 0)
            {
                Point node = points.RemoveFirst();

                if (node.pos == end)
                {
                    if (node.cost <= min)
                    {
                        min = node.cost;

                        Point p = node;
                        while (p.pos != start)
                        {
                            p = p.parent;
                            path.Add(p.pos);
                        }
                    }

                    continue;
                }

                close.Add(node.pos);

                foreach (V2 dir in V2.Directions)
                {
                    V2 next = node.pos + dir;
                    int nextCost = node.cost + 1 + (dir == node.dir ? 0 : 1000);

                    if (!map[next] && !points.Any(x => x.pos == next) && !close.Contains(next))
                        points.Add(new Point()
                        {
                            pos = next,
                            cost = nextCost,
                            dir = dir,
                            parent = node
                        });
                    else if (points.Any(x => x.pos == next))
                    {
                        Point p = points.First(x => x.pos == next);
                        if (nextCost < p.cost)
                        {
                            p.cost = nextCost;
                            p.parent = node;
                        }
                    }
                }
            }

            return min + " - " + path.Count;
        }

        public override object Part2()
        {
            long sum = 0;

            return sum;
        }
    }
}
