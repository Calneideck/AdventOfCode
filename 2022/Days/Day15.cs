using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day15 : Day
    {
        string[] lines = File.ReadAllLines("Input/15.txt");
        List<V2> sensors = new();
        List<V2> beacons = new();
        List<int> dists = new();

        public override object Part1()
        {
            foreach (var line in lines)
            {
                int[] m = new Regex(@"(-?\d+)")
                    .Matches(line)
                    .Select(g => int.Parse(g.Value))
                    .ToArray();
                sensors.Add(new V2(m[0], m[1]));
                beacons.Add(new V2(m[2], m[3]));
            }

            HashSet<int> row = new();
            int targetRow = 2_000_000;

            for (int i = 0; i < beacons.Count; i++)
            {
                V2 sensor = sensors[i];
                V2 dir = beacons[i] - sensor;
                int dist = Math.Abs(dir.x) + Math.Abs(dir.y);
                dists.Add(dist);

                int yDist = Math.Abs(targetRow - sensor.y);
                dist -= yDist;

                if (dist >= 0)
                    for (int x = -dist; x <= dist; x++)
                        if (!beacons.Contains(new V2(sensor.x + x, targetRow)))
                            row.Add(sensor.x + x);
            }

            return row.Count;
        }

        public override object Part2()
        {
            HashSet<int> indices = new();

            for (int i = 0; i < sensors.Count; i++)
                for (int j = 0; j < sensors.Count; j++)
                    if (i != j)
                    {
                        V2 dir = sensors[i] - sensors[j];
                        int distToSensor = Math.Abs(dir.x) + Math.Abs(dir.y);

                        if (dists[i] + dists[j] + 2 == distToSensor)
                        {
                            indices.Add(i);
                            indices.Add(j);
                        }
                    }

            V2 pos = new V2(sensors[1].x + 1, sensors[1].y - dists[1]);
            while (true)
            {
                pos += V2.DownRight;
                V2 dir = pos - sensors[20];
                int dist = Math.Abs(dir.x) + Math.Abs(dir.y);
                if (dist <= dists[20])
                {
                    if (dist == dists[20])
                        pos.y--;
                    else
                        pos += V2.UpLeft;
                    return (long)pos.x * 4000000 + pos.y;
                }
            }
        }
    }
}
