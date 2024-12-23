using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day14 : Day
    {
        string[] lines = File.ReadAllLines("Input/14.txt");

        class Robot
        {
            public V2 pos;
            public V2 vel;
        }

        public override object Part1()
        {
            List<Robot> robs = new();
            foreach (var line in lines)
            {
                int[] a = line.GrabInts();

                robs.Add(new Robot() { pos = new V2(a[0], a[1]), vel = new V2(a[2], a[3]) });
            }

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < robs.Count; j++)
                {
                    robs[j].pos = robs[j].pos + robs[j].vel;
                    if (robs[j].pos.x < 0) robs[j].pos = new V2(robs[j].pos.x + 101, robs[j].pos.y);
                    if (robs[j].pos.x >= 101) robs[j].pos = new V2(robs[j].pos.x - 101, robs[j].pos.y);
                    if (robs[j].pos.y < 0) robs[j].pos = new V2(robs[j].pos.x, robs[j].pos.y + 103);
                    if (robs[j].pos.y >= 103) robs[j].pos = new V2(robs[j].pos.x, robs[j].pos.y - 103);
                }
            }

            int[] q = new int[4];

            for (int i = 0; i < robs.Count; i++)
            {
                if (robs[i].pos.x < 101 / 2 && robs[i].pos.y < 103 / 2) q[0]++;
                if (robs[i].pos.x > 101 / 2 && robs[i].pos.y < 103 / 2) q[1]++;
                if (robs[i].pos.x > 101 / 2 && robs[i].pos.y > 103 / 2) q[2]++;
                if (robs[i].pos.x < 101 / 2 && robs[i].pos.y > 103 / 2) q[3]++;
            }

            return q.Aggregate(1, (x, y) => x * y);
        }

        public override object Part2()
        {
            List<Robot> robs = new();
            foreach (var line in lines)
            {
                int[] a = line.GrabInts();

                robs.Add(new Robot() { pos = new V2(a[0], a[1]), vel = new V2(a[2], a[3]) });
            }

            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < robs.Count; j++)
                {
                    robs[j].pos = robs[j].pos + robs[j].vel;
                    if (robs[j].pos.x < 0) robs[j].pos = new V2(robs[j].pos.x + 101, robs[j].pos.y);
                    if (robs[j].pos.x >= 101) robs[j].pos = new V2(robs[j].pos.x - 101, robs[j].pos.y);
                    if (robs[j].pos.y < 0) robs[j].pos = new V2(robs[j].pos.x, robs[j].pos.y + 103);
                    if (robs[j].pos.y >= 103) robs[j].pos = new V2(robs[j].pos.x, robs[j].pos.y - 103);
                }

                if (robs.All(robot => robs.Count(r => r.pos == robot.pos) == 1))
                {
                    MyExtensions.Draw(new V2(101, 103), (pos) => robs.Any(r => r.pos == pos));
                    return i + 1;
                }
            }

            return 0;
        }
    }
}
