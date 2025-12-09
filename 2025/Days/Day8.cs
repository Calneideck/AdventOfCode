using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day8 : Day
    {
        string[] lines = File.ReadAllLines("Input/8.txt");

        class Dist : IComparable<Dist>
        {
            public int i1;
            public int i2;
            public V3 p1;
            public V3 p2;
            public double distance;

            public int CompareTo(Dist other)
            {
                return Math.Sign(distance - other.distance);
            }
        }

        List<V3> pts = new();
        List<Dist> distances = new();

        public override object Part1()
        {
            foreach (string line in lines)
            {
                int[] nums = line.GrabInts();
                pts.Add(new V3(nums[0], nums[1], nums[2]));
            }

            for (int i = 0; i < pts.Count - 1; i++)
                for (int j = i + 1; j < pts.Count; j++)
                {
                    double dist = Math.Pow(pts[j].x - pts[i].x, 2) +
                        Math.Pow(pts[j].y - pts[i].y, 2) +
                        Math.Pow(pts[j].z - pts[i].z, 2);

                    distances.Add(new Dist()
                    {
                        i1 = i,
                        i2 = j,
                        p1 = pts[i],
                        p2 = pts[j],
                        distance = dist
                    });
                }

            distances.Sort();
            List<List<V3>> circuits = new();

            for (int i = 0; i < 1000; i++)
            {
                Dist d = distances[i];

                List<V3> c1 = circuits.Find(c => c.Contains(d.p1));
                List<V3> c2 = circuits.Find(c => c.Contains(d.p2));

                if (c1 == null && c2 == null)
                    circuits.Add(new List<V3>() { d.p1, d.p2 });
                else if (c1 == c2)
                    continue;
                else if (c1 != null && c2 == null)
                    c1.Add(d.p2);
                else if (c1 == null && c2 != null)
                    c2.Add(d.p1);
                else
                {
                    c1.AddRange(c2);
                    circuits.Remove(c2);
                }
            }

            circuits.Sort((a, b) => b.Count - a.Count);

            return circuits[0].Count * circuits[1].Count * circuits[2].Count;
        }

        public override object Part2()
        {
            List<List<V3>> circuits = new();

            for (int i = 0; i < distances.Count; i++)
            {
                Dist d = distances[i];

                List<V3> c1 = circuits.Find(c => c.Contains(d.p1));
                List<V3> c2 = circuits.Find(c => c.Contains(d.p2));

                if (c1 == null && c2 == null)
                    circuits.Add(new List<V3>() { d.p1, d.p2 });
                else if (c1 == c2)
                    continue;
                else if (c1 != null && c2 == null)
                    c1.Add(d.p2);
                else if (c1 == null && c2 != null)
                    c2.Add(d.p1);
                else
                {
                    c1.AddRange(c2);
                    circuits.Remove(c2);
                }

                if (pts.All(pt => circuits.Any(c => c.Contains(pt))))
                    return (ulong)d.p1.x * (ulong)d.p2.x;
            }

            return 0;
        }
    }
}
