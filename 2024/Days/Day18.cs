using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day18 : Day
    {
        string[] lines = File.ReadAllLines("Input/18.txt");
        V2 start = V2.Zero;
        V2 end = new(70, 70);

        public override object Part1()
        {
            HashSet<V2> closed = new();

            for (int i = 0; i < 1024; i++)
            {
                int[] a = MyExtensions.GrabInts(lines[i]);
                closed.Add(new V2(a[0], a[1]));
            }

            return Simulate(closed);
        }

        public override object Part2()
        {
            HashSet<V2> closed = new();
            for (int i = 0; i < 1024; i++)
            {
                int[] a = MyExtensions.GrabInts(lines[i]);
                closed.Add(new V2(a[0], a[1]));
            }

            for (int j = 1024; j < int.MaxValue; j++)
            {
                int[] a = MyExtensions.GrabInts(lines[j - 1]);
                closed.Add(new V2(a[0], a[1]));

                if (Simulate(new HashSet<V2>(closed)) == 0)
                    return a[0] + "," + a[1];
            }

            return 0;
        }

        int Simulate(HashSet<V2> closed)
        {
            Queue<(V2, int)> open = new();
            open.Enqueue((start, 0));

            while (open.Any())
            {
                var n = open.Dequeue();
                closed.Add(n.Item1);

                foreach (var dir in V2.Directions)
                {
                    V2 pos = n.Item1 + dir;

                    if (pos == end)
                        return n.Item2 + 1;

                    if (pos.x >= 0 && pos.y >= 0 && pos.x <= 70 && pos.y <= 70)
                        if (!open.Any(x => x.Item1 == pos) && !closed.Contains(pos))
                            open.Enqueue((pos, n.Item2 + 1));
                }
            }

            return 0;
        }
    }
}
