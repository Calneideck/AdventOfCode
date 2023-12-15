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

        public override object Part1()
        {
            var map = lines.GetMap();

            foreach (var kv in map)
            {
                V2 pos = kv.Key;
                if (pos.y == 0 || kv.Value != 'O') continue;

                V2 newPos = pos;

                for (int y = 1; y <= pos.y; y++)
                    if (map[pos + V2.Up * y] == '.')
                        newPos = pos + V2.Up * y;
                    else
                        break;

                if (newPos != pos)
                {
                    map[newPos] = 'O';
                    map[pos] = '.';
                }
            }

            // for (int y = 0; y < lines.Length; y++)
            // {
            //     Console.WriteLine();
            //     for (int x = 0; x < lines[0].Length; x++)
            //         Console.Write(map[new V2(x, y)]);
            // }

            return map
                .Where(kv => kv.Value == 'O')
                .Aggregate(0, (sum, kv) => sum + lines.Length - kv.Key.y);
        }


        public override object Part2()
        {
            long count = 0;

            return count;
        }
    }
}
