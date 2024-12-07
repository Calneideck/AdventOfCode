using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{

    class Day17 : Day
    {
        class Node : IComparable<Node>
        {
            public V2 pos;
            public int cost;
            public int straight;
            public V2 lastDir = V2.Zero;
            public Node parent;

            public Node(V2 pos, int cost, int straight, V2 lastDir, Node parent)
            {
                this.pos = pos;
                this.cost = cost;
                this.straight = straight;
                this.lastDir = lastDir;
                this.parent = parent;
            }

            public int CompareTo(Node other)
            {
                return cost - other.cost;
            }
        }

        string[] lines = File.ReadAllLines("Input/17.txt");
        Dictionary<V2, int> map = new();

        public override object Part1()
        {
            long min = long.MaxValue;

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    map.Add(new V2(x, y), int.Parse(lines[y][x].ToString()));

            V2 start = V2.Zero;
            //V2 end = new(lines[0].Length - 1, lines.Length - 1);
            V2 end = new(5, 0);

            BTSortedList<Node> open = new();
            HashSet<V2> closed = new();
            HashSet<V2> path = new();

            open.Add(new Node(start, 0, 0, V2.Zero, null));

            while (open.Any())
            {
                Node n = open.RemoveFirst();
                closed.Add(n.pos);
                if (n.pos == end)
                {
                    min = Math.Min(min, n.cost);
                    Node p = n;
                    while (p != null)
                    {
                        path.Add(p.pos);
                        p = p.parent;
                    }
                    continue;
                }

                foreach (V2 dir in V2.Directions)
                {
                    V2 next = n.pos + dir;
                    if (
                        map.TryGetValue(next, out int val) &&
                        !V2.Opposite(dir, n.lastDir) &&
                        !closed.Contains(next)
                    )
                    {
                        if (n.straight == 3 && n.lastDir == dir) continue;

                        Node ext = open.FirstOrDefault(x => x.pos == next);

                        if (ext != null)
                        {
                            if (n.cost + val < ext.cost)
                                ext.cost = n.cost + val;
                        }
                        else
                            open.Add(new Node(next, n.cost + val, n.lastDir == dir ? n.straight + 1 : 1, dir, n));
                    }
                }
            }

            MyExtensions.Draw(lines, path.Contains);

            return min;
        }


        public override object Part2()
        {
            long count = 0;

            return count;
        }
    }
}
