using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day14 : Day
    {
        string[] lines = File.ReadAllLines("Input/14.txt");

        HashSet<V2> GetBlocked()
        {
            HashSet<V2> blocked = new();

            foreach (var line in lines)
            {
                string[] temp = line.Split(" -> ");
                V2[] path = temp.Select(x =>
                {
                    string[] parts = x.Split(',');
                    return new V2(int.Parse(parts[0]), int.Parse(parts[1]));
                }).ToArray();

                for (int i = 0; i < path.Length - 1; i++)
                {
                    V2 dir = path[i + 1] - path[i];
                    dir.x = Math.Sign(dir.x);
                    dir.y = Math.Sign(dir.y);

                    V2 pos = path[i];
                    blocked.Add(pos);
                    while (pos != path[i + 1])
                    {
                        pos += dir;
                        blocked.Add(pos);
                    }
                }
            }

            return blocked;
        }

        public override object Part1()
        {
            HashSet<V2> blocked = GetBlocked();

            V2 start = new V2(500, 0);
            int count = 0;

            V2 sand = start;
            while (true)
            {
                if (!blocked.Contains(sand + V2.Down))
                    sand += V2.Down;
                else if (!blocked.Contains(sand + V2.DownLeft))
                    sand += V2.DownLeft;
                else if (!blocked.Contains(sand + V2.DownRight))
                    sand += V2.DownRight;
                else
                {
                    // At rest
                    count++;
                    blocked.Add(sand);
                    sand = start;
                }

                if (sand.y > 1000)
                    break;
            }

            return count;
        }

        public override object Part2()
        {
            HashSet<V2> blocked = GetBlocked();

            V2 start = new V2(500, 0);
            int count = 0;
            int maxY = blocked.Max(x => x.y) + 2;

            V2 sand = start;
            while (true)
            {
                if (!blocked.Contains(sand + V2.Down) && sand.y + 1 < maxY)
                    sand += V2.Down;
                else if (!blocked.Contains(sand + V2.DownLeft) && sand.y + 1 < maxY)
                    sand += V2.DownLeft;
                else if (!blocked.Contains(sand + V2.DownRight) && sand.y + 1 < maxY)
                    sand += V2.DownRight;
                else
                {
                    // At rest
                    count++;
                    if (sand == start)
                        break;

                    blocked.Add(sand);
                    sand = start;
                }
            }

            return count;
        }
    }
}
