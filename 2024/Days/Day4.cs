using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day4 : Day
    {
        string[] lines = File.ReadAllLines("Input/4.txt");
        char[] cc = new char[] { 'M', 'A', 'S' };

        public override object Part1()
        {
            long sum = 0;

            Dictionary<V2, char> map = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map[new V2(x, y)] = lines[y][x];

            foreach (KeyValuePair<V2, char> kv in map)
            {
                if (kv.Value == 'X')
                {
                    foreach (V2 dir in V2.AllDirections)
                    {
                        bool good = true;

                        for (int i = 0; i < 3; i++)
                        {
                            if (map.TryGetValue(kv.Key + dir * (i + 1), out char newC))
                            {
                                if (newC != cc[i])
                                {
                                    good = false;
                                    break;
                                }
                            }
                            else
                            {
                                good = false;
                            }
                        }

                        if (good) sum++;
                    }
                }
            }


            return sum;
        }

        public override object Part2()
        {
            long sum = 0;
            Dictionary<V2, char> map = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map[new V2(x, y)] = lines[y][x];

            foreach (KeyValuePair<V2, char> kv in map)
                if (kv.Value == 'A')
                {
                    if (!map.TryGetValue(kv.Key + V2.UpLeft, out char c1)) continue;
                    if (!map.TryGetValue(kv.Key + V2.UpRight, out char c2)) continue;
                    if (!map.TryGetValue(kv.Key + V2.DownRight, out char c3)) continue;
                    if (!map.TryGetValue(kv.Key + V2.DownLeft, out char c4)) continue;

                    string s = c1.ToString() + c2.ToString() + c3.ToString() + c4.ToString();
                    if (s == "MMSS" || s == "MSSM" || s == "SSMM" || s == "SMMS")
                        sum++;
                }

            return sum;
        }
    }
}
