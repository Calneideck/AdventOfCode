using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day9 : Day
    {
        string[] lines = File.ReadAllLines("Input/9.txt");
        Dictionary<char, V2> dirs = new() {
            { 'U', V2.Up },
            { 'D', V2.Down },
            { 'L', V2.Left },
            { 'R', V2.Right },
        };
        HashSet<V2> visited = new();

        public override object Part1()
        {
            V2 head = new();
            V2 tail = new();

            foreach (var line in lines)
            {
                string[] temp = line.Split();
                V2 dir = dirs[temp[0][0]];
                int count = int.Parse(temp[1]);

                for (int i = 0; i < count; i++)
                {
                    head += dir;
                    bool found = tail == head || V2.AllDirections.Any((d) => tail + d == head);

                    if (!found)
                        tail += new V2(Math.Sign(head.x - tail.x), Math.Sign(head.y - tail.y));

                    visited.Add(tail);
                }
            }

            return visited.Count;
        }

        public override object Part2()
        {
            visited.Clear();
            V2[] segments = new V2[10];
            for (int i = 0; i < 10; i++)
                segments[i] = new();

            foreach (var line in lines)
            {
                string[] temp = line.Split();
                V2 dir = dirs[temp[0][0]];
                int count = int.Parse(temp[1]);

                for (int i = 0; i < count; i++)
                {
                    segments[0] += dir;
                    for (int j = 1; j < 10; j++)
                    {
                        bool found = segments[j] == segments[j - 1] || V2.AllDirections.Any((d) => segments[j] + d == segments[j - 1]);

                        if (!found)
                            segments[j] += new V2(Math.Sign(segments[j - 1].x - segments[j].x), Math.Sign(segments[j - 1].y - segments[j].y));
                    }

                    visited.Add(segments[9]);
                }
            }

            return visited.Count;
        }
    }
}
