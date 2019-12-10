using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day3 : Day
    {
        public override string Part1()
        {
            string[] lines = File.ReadAllLines("Input/3.txt");
            string[] wire1 = lines[0].Split(',');
            string[] wire2 = lines[1].Split(',');

            var path1 = GetWirePath(wire1);
            var path2 = GetWirePath(wire2);

            var intersect = path1.Keys.Intersect(path2.Keys);
            return intersect.Min(i => Math.Abs(i.x) + Math.Abs(i.y)).ToString();
        }

        public override string Part2()
        {
            string[] lines = File.ReadAllLines("Input/3.txt");
            string[] wire1 = lines[0].Split(',');
            string[] wire2 = lines[1].Split(',');

            var path1 = GetWirePath(wire1);
            var path2 = GetWirePath(wire2);

            var intersect = path1.Keys.Intersect(path2.Keys).Where(x => x.x != 0 && x.y != 0);
            return intersect.Min(p => path1[p] + path2[p]).ToString();
        }

        (int x, int y) GetDir(string direction)
        {
            if (direction == "U")
                return (0, 1);
            else if (direction == "L")
                return (-1, 0);
            else if (direction == "R")
                return (1, 0);
            else
                return (0, -1);
        }

        Dictionary<(int x, int y), int> GetWirePath(string[] wire)
        {
            Dictionary<(int, int), int> path = new Dictionary<(int, int), int>();
            (int x, int y) p = (0, 0);

            int steps = 0;
            foreach (string instruction in wire)
            {
                var dir = GetDir(instruction.Substring(0, 1));
                int amount = int.Parse(instruction.Substring(1));

                for (int i = 0; i < amount; i++)
                {
                    p = (p.x + dir.x, p.y + dir.y);
                    ++steps;

                    if (!path.ContainsKey(p))
                        path.Add(p, steps);
                }
            }

            return path;
        }
    }
}
