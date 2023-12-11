using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day10 : Day
    {
        string[] lines = File.ReadAllLines("Input/10.txt");
        Dictionary<V2, char> map;

        public override object Part1()
        {
            map = lines.GetMap();
            V2 start = map.First(kv => kv.Value == 'S').Key;

            Queue<(V2, int)> open = new();
            open.Enqueue((start, 0));
            HashSet<V2> closed = new();

            long maxLength = 0;

            while (open.Count > 0)
            {
                var (node, count) = open.Dequeue();
                maxLength = Math.Max(count, maxLength);

                foreach (var v in GetDirs(map[node]))
                {
                    V2 n = node + v;
                    if (!closed.Contains(n))
                    {
                        open.Enqueue((n, count + 1));
                        closed.Add(n);
                    }
                }
            }

            return maxLength;
        }

        public override object Part2()
        {
            long count = 0;
            V2 node = map.First(kv => kv.Value == 'S').Key;
            V2 dir = V2.Right;

            HashSet<V2> path = new();

            do
            {
                V2 oldPos = node;
                node += dir;

                path.Add(node);

                V2[] dirs = GetDirs(map[node]);
                dir = node + dirs[0] != oldPos ? dirs[0] : dirs[1];
            } while (map[node] != 'S');

            for (int y = 0; y < lines.Length; y++)
            {
                bool inside = false;
                for (int x = 0; x < lines[0].Length; x++)
                {
                    char c = lines[y][x];
                    if (path.Contains(new V2(x, y)))
                    {
                        if (c == '|' || c == 'L' || c == 'J')
                            inside = !inside;
                    }
                    else if (inside)
                        count++;
                }
            }

            return count;
        }

        V2[] GetDirs(char c)
        {
            return c switch
            {
                '|' => new V2[] { V2.Up, V2.Down },
                '-' => new V2[] { V2.Left, V2.Right },
                'L' => new V2[] { V2.Up, V2.Right },
                'J' => new V2[] { V2.Up, V2.Left },
                '7' => new V2[] { V2.Down, V2.Left },
                'F' => new V2[] { V2.Down, V2.Right },
                'S' => new V2[] { V2.Left, V2.Right },
                _ => null
            };
        }
    }
}
