using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day11 : Day
    {
        string[] lines = File.ReadAllLines("Input/11.txt");

        Dictionary<V2, bool> map = new Dictionary<V2, bool>();

        private readonly List<V2> dirs = new List<V2>()
        {
            new V2(1,0),
            new V2(1,1),
            new V2(0,1),
            new V2(-1,1),
            new V2(-1,0),
            new V2(-1,-1),
            new V2(0,-1),
            new V2(1,-1)
        };

        public override object Part1()
        {
            int len = lines[0].Length;

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < len; x++)
                    if (lines[y][x] != '.')
                        map.Add(new V2(x, y), false);

            while (true)
            {
                bool changed = false;
                Dictionary<V2, bool> newMap = new Dictionary<V2, bool>();

                foreach (var seat in map)
                {
                    int neighbourCount = 0;
                    foreach (var dir in dirs)
                    {
                        var n = seat.Key + dir;
                        if (!map.ContainsKey(n)) continue;
                        if (map[n])
                            ++neighbourCount;
                    }

                    if (!seat.Value && neighbourCount == 0)
                    {
                        newMap[seat.Key] = true;
                        changed = true;
                    }
                    else if (seat.Value && neighbourCount >= 4)
                    {
                        newMap[seat.Key] = false;
                        changed = true;
                    }
                    else
                        newMap[seat.Key] = seat.Value;
                }

                map = new Dictionary<V2, bool>(newMap);

                if (!changed)
                    return map.Count((kv) => kv.Value);
            }
        }

        public override object Part2()
        {
            int len = lines[0].Length;
            map.Clear();

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < len; x++)
                    if (lines[y][x] != '.')
                        map.Add(new V2(x, y), false);

            while (true)
            {
                bool changed = false;
                Dictionary<V2, bool> newMap = new Dictionary<V2, bool>();

                foreach (var seat in map)
                {
                    int neighbourCount = 0;
                    foreach (var dir in dirs)
                    {
                        var thisDir = dir;
                        while (true)
                        {
                            var n = seat.Key + thisDir;
                            if (n.x < 0 || n.y < 0 || n.x >= len || n.y >= lines.Length)
                                break;

                            if (map.ContainsKey(n))
                            {
                                if (map[n])
                                    ++neighbourCount;

                                break;
                            }
                            else
                                thisDir += dir;
                        }
                    }

                    if (!seat.Value && neighbourCount == 0)
                    {
                        newMap[seat.Key] = true;
                        changed = true;
                    }
                    else if (seat.Value && neighbourCount >= 5)
                    {
                        newMap[seat.Key] = false;
                        changed = true;
                    }
                    else
                        newMap[seat.Key] = seat.Value;
                }

                map = new Dictionary<V2, bool>(newMap);

                if (!changed)
                    return map.Count((kv) => kv.Value);
            }
        }
    }
}
