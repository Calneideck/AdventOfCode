using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day13 : Day
    {
        string[] lines = File.ReadAllLines("Input/13.txt");

        public override object Part1()
        {
            int timestamp = int.Parse(lines[0]);
            var times = lines[1].Split(',').Where((a) => a != "x").Select(int.Parse);

            int minId = 0;
            int minWait = 10000;
            foreach (var t in times)
            {
                int time = (int)Math.Ceiling(timestamp / (float)t) * t;
                int wait = time - timestamp;
                if (wait < minWait)
                {
                    minId = t;
                    minWait = wait;
                }
            }

            return minWait * minId;
        }

        public override object Part2()
        {
            var times = lines[1].Split(',').Select((t) =>
            {
                if (t == "x")
                    return (ulong)1;
                else
                    return ulong.Parse(t);
            }).ToArray();

            ulong t = 0;

            while (true)
            {
                t += times[0];
                ulong sum = t;

                bool pass = true;

                for (uint i = 1; i < times.Length; i++)
                {
                    if (times[i] == 1)
                        continue;

                    while ((t + i) % times[i] != 0)
                        t += sum;

                    sum = times.Take((int)(i + 1)).Aggregate((x, y) => x * y);
                }

                if (pass)
                    return t;
            }
        }
    }
}
