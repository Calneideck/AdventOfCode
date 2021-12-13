using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day13 : Day
    {
        string[] lines = File.ReadAllLines("Input/13.txt");

        public override object Part1()
        {
            HashSet<V2> set = new();
            bool mapping = true;
            bool part1 = true;

            foreach (var line in lines)
            {
                if (mapping)
                {
                    if (line.Length == 0)
                    {
                        mapping = false;
                        continue;
                    }

                    int[] temp = line.Split(",").Select(int.Parse).ToArray();
                    set.Add(new V2(temp[0], temp[1]));
                }
                else
                {
                    string[] ins = line.Split(' ').Last().Split('=');
                    int num = int.Parse(ins[1]);

                    if (ins[0] == "x")
                    {
                        V2[] toFold = set.Where(n => n.x > num).ToArray();
                        foreach (V2 v in toFold)
                        {
                            if (!set.Contains(new V2(num + num - v.x, v.y)))
                                set.Add(new V2(num + num - v.x, v.y));

                            set.Remove(v);
                        }
                    }
                    else
                    {
                        V2[] toFold = set.Where(n => n.y > num).ToArray();
                        foreach (V2 v in toFold)
                        {
                            if (!set.Contains(new V2(v.x, num + num - v.y)))
                                set.Add(new V2(v.x, num + num - v.y));

                            set.Remove(v);
                        }
                    }

                    if (part1)
                    {
                        Console.WriteLine(set.Count);
                        part1 = false;
                    }
                }
            }

            int maxX = set.Max(v => v.x);
            int maxY = set.Max(v => v.y);

            Console.WriteLine("");
            Console.WriteLine("Part 2:");

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                    Console.Write(set.Contains(new V2(x, y)) ? '█' : ' ');

                Console.WriteLine();
            }

            return "";
        }
    }
}
