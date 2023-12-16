using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{

    class Day16 : Day
    {
        string[] lines = File.ReadAllLines("Input/16.txt");
        HashSet<V2> path = new();
        HashSet<(V2, V2)> seen = new();
        Dictionary<V2, char> map = null;

        public override object Part1()
        {
            map = lines.GetMap();
            V2 node = new(-1, 0);
            V2 dir = V2.Right;

            DoPath(node, dir);

            //MyExtensions.Draw(lines, pos => path.Contains(pos));

            return path.Count;
        }

        void DoPath(V2 start, V2 dir)
        {
            V2 node = start;
            if (seen.Contains((start, dir))) return;
            else seen.Add((start, dir));

            while (true)
            {
                node += dir;
                if (!map.ContainsKey(node)) return;

                if (!path.Contains(node))
                    path.Add(node);

                char c = map[node];

                if (c == '/')
                {
                    if (dir == V2.Right) dir = V2.Up;
                    else if (dir == V2.Down) dir = V2.Left;
                    else if (dir == V2.Up) dir = V2.Right;
                    else if (dir == V2.Left) dir = V2.Down;
                }
                else if (c == '\\')
                {
                    if (dir == V2.Right) dir = V2.Down;
                    else if (dir == V2.Down) dir = V2.Right;
                    else if (dir == V2.Up) dir = V2.Left;
                    else if (dir == V2.Left) dir = V2.Up;
                }
                else if (c == '-')
                {
                    if (dir == V2.Up || dir == V2.Down)
                    {
                        DoPath(node, V2.Left);
                        DoPath(node, V2.Right);
                        return;
                    }
                }
                else if (c == '|')
                {
                    if (dir == V2.Left || dir == V2.Right)
                    {
                        DoPath(node, V2.Up);
                        DoPath(node, V2.Down);
                        return;
                    }
                }
            }
        }


        public override object Part2()
        {
            long count = 0;

            for (int y = 0; y < lines.Length; y++)
            {
                path.Clear();
                seen.Clear();
                DoPath(new V2(-1, y), V2.Right);

                count = Math.Max(count, path.Count);

                path.Clear();
                seen.Clear();
                DoPath(new V2(lines[0].Length, y), V2.Left);

                count = Math.Max(count, path.Count);
            }

            for (int x = 0; x < lines[0].Length; x++)
            {
                path.Clear();
                seen.Clear();
                DoPath(new V2(x, -1), V2.Down);

                count = Math.Max(count, path.Count);

                path.Clear();
                seen.Clear();
                DoPath(new V2(x, lines.Length), V2.Up);

                count = Math.Max(count, path.Count);
            }

            return count;
        }
    }
}
