using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day3 : Day
    {
        string[] lines = File.ReadAllLines("Input/3.txt");
        Dictionary<V2, bool> map = new Dictionary<V2, bool>();

        public override string Part1()
        {
            int len = lines[0].Length;
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < len; x++)
                    map.Add(new V2(x, y), lines[y][x] == '#');

            V2 pos = new V2(0, 0);
            int trees = 0;
            while (pos.y < lines.Length)
            {
                if (map[pos])
                    trees++;

                pos += new V2(3, 1);
                if (pos.x >= len)
                    pos.x -= len;
            }

            return trees.ToString();
        }

        int getSlope(V2 slope)
        {
            int len = lines[0].Length;

            var pos = new V2(0, 0);
            int trees = 0;
            while (pos.y < lines.Length)
            {
                if (map[pos])
                    trees++;

                pos += slope;
                if (pos.x >= len)
                    pos.x -= len;
            }

            return trees;
        }

        public override string Part2()
        {
            return (getSlope(new V2(1, 1)) *
             getSlope(new V2(3, 1)) *
             getSlope(new V2(5, 1)) *
             getSlope(new V2(7, 1)) *
             getSlope(new V2(1, 2))).ToString();
        }
    }
}
