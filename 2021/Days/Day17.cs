using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day17 : Day
    {
        V2[] target = new V2[2]
        {
            new V2(117, -140),
            new V2(164, -89)
        };


        public override object Part1()
        {
            int max = 0;

            for (int x = 0; x < 50; x++)
                for (int y = 0; y < 500; y++)
                {
                    var result = Hit(new V2(x, y));
                    if (result.Item1)
                        max = Math.Max(max, result.Item2);
                }

            return max;
        }

        (bool, int) Hit(V2 v)
        {
            V2 pos = V2.Zero;
            V2 vel = v;
            int hightestY = 0;

            while (true)
            {
                pos += vel;
                vel.x -= Math.Sign(vel.x);
                vel.y--;

                if (pos.y > hightestY)
                    hightestY = pos.y;

                if (pos.x > target[1].x || pos.y < target[0].y)
                    return (false, 0);

                if (pos.x >= target[0].x && pos.y <= target[1].y)
                    return (true, hightestY);
            }
        }

        public override object Part2()
        {
            HashSet<V2> set = new();

            for (int x = 0; x < 500; x++)
                for (int y = -1000; y < 1000; y++)
                {
                    var result = Hit(new V2(x, y));
                    if (result.Item1)
                        set.Add(new V2(x, y));
                }

            return set.Count;
        }
    }
}
