using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day9 : Day
    {
        string[] lines = File.ReadAllLines("Input/9.txt");
        List<V2> points = new();

        public override object Part1()
        {
            foreach (string line in lines)
            {
                int[] nums = line.GrabInts();
                points.Add(new V2(nums[0], nums[1]));
            }

            long largest = 0;

            for (int i = 0; i < points.Count - 1; i++)
                for (int j = i + 1; j < points.Count; j++)
                {
                    long w = Math.Abs(points[i].x - points[j].x) + 1;
                    long h = Math.Abs(points[i].y - points[j].y) + 1;
                    largest = Math.Max(largest, w * h);
                }

            return largest;
        }

        public override object Part2()
        {
            ulong count = 0;

            for (int i = 0; i < points.Count - 1; i++)
                for (int j = i + 1; j < points.Count; j++)
                {
                    V2 pt3 = new V2(points[i].x, points[j].y);
                    V2 pt4 = new V2(points[j].x, points[i].y);


                    

                    long w = Math.Abs(points[i].x - points[j].x) + 1;
                    long h = Math.Abs(points[i].y - points[j].y) + 1;
                }

            return count;
        }

        bool PointInside(V2 pt)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                V2 dir = points[i + 1] - points[i];
                
            }
        }
    }
}
