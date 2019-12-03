using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Day3 : Day
    {
        struct V2
        {
            public int x, y;

            public V2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public override int Part1()
        {
            string[] lines = File.ReadAllLines("Input/3.txt");
            string[] wire1 = lines[0].Split(',');
            string[] wire2 = lines[1].Split(',');

            HashSet<V2> points = new HashSet<V2>();

            var p = new V2(0, 0);

            int shortestDist = int.MaxValue;

            foreach (string instruction in wire1)
            {
                V2 dir = GetDir(instruction.Substring(0, 1));
                int amount = int.Parse(instruction.Substring(1));

                for (int i = 0; i < amount; i++)
                {
                    p = new V2(p.x + dir.x, p.y + dir.y);
                    if (!points.Contains(p))
                        points.Add(p);
                }
            }

            // Reset for wire 2
            p = new V2(0, 0);

            foreach (string instruction in wire2)
            {
                V2 dir = GetDir(instruction.Substring(0, 1));
                int amount = int.Parse(instruction.Substring(1));

                for (int i = 0; i < amount; i++)
                {
                    p = new V2(p.x + dir.x, p.y + dir.y);
                    if ((p.x != 0 || p.y != 0) && points.Contains(p))
                    {
                        // Intersection
                        int dist = Math.Abs(p.x) + Math.Abs(p.y);
                        if (dist < shortestDist)
                            shortestDist = dist;
                    }
                }
            }


            return shortestDist;
        }

        public override int Part2()
        {
            string[] lines = File.ReadAllLines("Input/3.txt");
            string[] wire1 = lines[0].Split(',');
            string[] wire2 = lines[1].Split(',');

            Dictionary<V2, int> points = new Dictionary<V2, int>();

            V2 p = new V2(0, 0);
            int steps = 0;
            int shortestDist = int.MaxValue;

            foreach (string instruction in wire1)
            {
                V2 dir = GetDir(instruction.Substring(0, 1));
                int amount = int.Parse(instruction.Substring(1));

                for (int i = 0; i < amount; i++)
                {
                    p = new V2(p.x + dir.x, p.y + dir.y);
                    ++steps;

                    if (!points.ContainsKey(p))
                        points.Add(p, steps);
                }
            }

            // Reset for wire 2
            p = new V2(0, 0);
            steps = 0;

            foreach (string instruction in wire2)
            {
                V2 dir = GetDir(instruction.Substring(0, 1));
                int amount = int.Parse(instruction.Substring(1));

                for (int i = 0; i < amount; i++)
                {
                    p = new V2(p.x + dir.x, p.y + dir.y);
                    ++steps;

                    if ((p.x != 0 || p.y != 0) && points.ContainsKey(p))
                    {
                        // Intersection
                        if (steps + points[p] < shortestDist)
                            shortestDist = steps + points[p];
                    }
                }
            }

            return shortestDist;
        }

        V2 GetDir(string direction)
        {
            if (direction == "U")
                return new V2(0, 1);
            else if (direction == "L")
                return new V2(-1, 0);
            else if (direction == "R")
                return new V2(1, 0);
            else
                return new V2(0, -1);
        }
    }
}
