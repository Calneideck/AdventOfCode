using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day5 : Day
    {
        string[] lines = File.ReadAllLines("Input/5.txt");


        public override object Part1()
        {
            return FindOverlaps(false);
        }

        public override object Part2()
        {
            return FindOverlaps(true);
        }

        int FindOverlaps(bool diagonals)
        {
            Dictionary<V2, int> map = new();
            Regex r = new Regex(@"(\d+)");

            foreach (var line in lines)
            {
                var matches = r.Matches(line);
                V2 v1 = new V2(int.Parse(matches[0].Value), int.Parse(matches[1].Value));
                V2 v2 = new V2(int.Parse(matches[2].Value), int.Parse(matches[3].Value));

                if (diagonals || v1.x == v2.x || v1.y == v2.y)
                {
                    V2 dir = new V2(Math.Sign(v2.x - v1.x), Math.Sign(v2.y - v1.y));
                    V2 target = v1;

                    while (target != v2)
                    {
                        if (!map.ContainsKey(target))
                            map.Add(target, 0);

                        map[target]++;
                        target += dir;
                    }

                    if (!map.ContainsKey(v2))
                        map.Add(v2, 0);

                    map[v2]++;
                }
            }


            return map.Values.Count(x => x > 1);
        }
    }
}
