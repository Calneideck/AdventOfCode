using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day12 : Day
    {
        string[] lines = File.ReadAllLines("Input/12.txt");
        Dictionary<V2, char> map = new();

        public override object Part1()
        {
            int sum = 0;

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map[new V2(x, y)] = lines[y][x];

            Queue<V2> openList = new();
            HashSet<V2> closedList = new();

            foreach (var node in map.Keys)
            {
                if (closedList.Contains(node)) continue;

                char c = map[node];

                openList.Enqueue(node);
                int p = 0;
                int a = 0;

                while (openList.Any())
                {
                    V2 n = openList.Dequeue();
                    closedList.Add(n);

                    a++;
                    p += 4;

                    foreach (V2 d in V2.Directions)
                        if (map.ContainsKey(n + d) && map[n + d] == c)
                        {
                            p--;
                            if (!openList.Contains(n + d) && !closedList.Contains(n + d))
                                openList.Enqueue(n + d);
                        }
                }

                sum += p * a;
            }

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;


            return sum;
        }
    }
}
