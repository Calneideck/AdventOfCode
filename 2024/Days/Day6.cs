using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Intrinsics;

namespace AdventOfCode
{
    class Day6 : Day
    {
        string[] lines = File.ReadAllLines("Input/6.txt");

        HashSet<V2> walls = new HashSet<V2>();
        HashSet<V2> distinct = new HashSet<V2>();
        V2 start;

        public override object Part1()
        {
            V2 node = V2.Zero;
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    if (lines[y][x] == '#')
                        walls.Add(new V2(x, y));
                    else if (lines[y][x] == '^')
                        node = new V2(x, y);

            int dir = 0;
            start = node;

            while (node.x >= 0 && node.y >= 0 && node.x < lines[0].Length && node.y < lines.Length)
            {
                if (!distinct.Contains(node))
                    distinct.Add(node);

                if (walls.Contains(node + V2.Directions[dir]))
                    dir = (dir + 1) % V2.Directions.Length;

                node += V2.Directions[dir];
            }

            return distinct.Count;
        }

        public override object Part2()
        {
            int sum = 0;

            distinct.Remove(start);

            foreach (V2 v in distinct)
            {
                HashSet<V2> newWalls = new(walls) { v };
                HashSet<(V2, int)> saved = new();
                int dir = 0;
                V2 node = start;

                while (node.x >= 0 && node.y >= 0 && node.x < lines[0].Length && node.y < lines.Length)
                    if (newWalls.Contains(node + V2.Directions[dir]))
                    {
                        if (saved.Contains((node, dir)))
                        {
                            sum++;
                            break;
                        }
                        else
                            saved.Add((node, dir));

                        dir = (dir + 1) % V2.Directions.Length;
                    }
                    else
                        node += V2.Directions[dir];
            }

            return sum;
        }
    }
}
