using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day18 : Day
    {
        string[] lines = File.ReadAllLines("Input/18.txt");
        List<V3> cubes = null;

        public override object Part1()
        {
            int count = 0;
            
            cubes = lines.Select(a =>
            {
                int[] t = a.Split(',').Select(int.Parse).ToArray();
                return new V3(t[0], t[1], t[2]);
            }).ToList();

            count = cubes.Count * 6;

            foreach (V3 c in cubes)
                foreach (V3 d in V3.AllDirections)
                    if (cubes.Contains(c + d))
                        count--;

            return count;
        }

        public override object Part2()
        {
            int count = 0;

            int minX = cubes.Min(a => a.x) - 1;
            int maxX = cubes.Max(a => a.x) + 1;
            int minY = cubes.Min(a => a.y) - 1;
            int maxY = cubes.Max(a => a.y) + 1;
            int minZ = cubes.Min(a => a.z) - 1;
            int maxZ = cubes.Max(a => a.z) + 1;

            Queue<V3> open = new();
            HashSet<V3> closed = new();

            open.Enqueue(new V3(minX, minY, minZ));

            while (open.Count > 0)
            {
                V3 pos = open.Dequeue();
                closed.Add(pos);

                foreach (V3 d in V3.AllDirections)
                {
                    V3 neighbour = pos + d;

                    if (neighbour.x < minX || neighbour.x > maxX ||
                        neighbour.y < minY || neighbour.y > maxY ||
                        neighbour.z < minZ || neighbour.z > maxZ)
                        continue;

                    if (cubes.Contains(neighbour))
                        count++;
                    else if (!closed.Contains(neighbour) && !open.Contains(neighbour))
                        open.Enqueue(neighbour);
                }
            }

            return count;
        }
    }
}
