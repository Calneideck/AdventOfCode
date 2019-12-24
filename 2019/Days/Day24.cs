using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day24 : Day
    {
        public override string Part1()
        {
            var input = File.ReadAllLines("Input/24.txt").Select(line => line.ToCharArray()).ToArray();
            var map = new Dictionary<V2, char>();
            var newMap = new Dictionary<V2, char>();
            var states = new HashSet<string>();

            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                    map[new V2(x, y)] = input[y][x];

            states.Add(GetMapString(map));

            while (true)
            {
                for (int y = 0; y < 5; y++)
                    for (int x = 0; x < 5; x++)
                    {
                        var pos = new V2(x, y);
                        int count = 0;
                        foreach (var dir in V2.Directions)
                            if (map.TryGetValue(pos + dir, out char c))
                                if (c == '#')
                                    count++;

                        newMap[pos] = (count == 1 || map[pos] == '.' && count == 2) ? '#' : '.';
                    }


                string state = GetMapString(newMap);

                if (states.Contains(state))
                {
                    long value = 0;
                    foreach (var kv in newMap)
                        if (kv.Value == '#')
                            value += (long)Math.Pow(2, kv.Key.y * 5 + kv.Key.x);

                    return value.ToString();
                }
                else
                    states.Add(state);

                foreach (var key in map.Keys.ToList())
                    map[key] = newMap[key];
            }
        }

        public override string Part2()
        {
            var input = File.ReadAllLines("Input/24.txt").Select(line => line.ToCharArray()).ToArray();
            var map = new Dictionary<(int level, V2 pos), bool>();
            var newMap = new Dictionary<(int level, V2 pos), bool>();

            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                    map[(0, new V2(x, y))] = input[y][x] == '#';

            int mins = 0;
            int depth = 1;
            while (mins < 200)
            {
                for (int d = -depth; d <= depth; d++)
                    for (int y = 0; y < 5; y++)
                        for (int x = 0; x < 5; x++)
                        {
                            if (x == 2 && y == 2)
                                continue;

                            var pos = new V2(x, y);
                            int count = 0;

                            map.TryAdd((d, pos), false);

                            foreach (var dir in V2.Directions)
                            {
                                int thisDepth = d;
                                bool bug;

                                if (x == 1 && y == 2 && dir.x == 1)
                                {
                                    thisDepth++;
                                    for (int newY = 0; newY < 5; newY++)
                                        if (map.TryGetValue((thisDepth, new V2(0, newY)), out bug))
                                            if (bug)
                                                count++;
                                }
                                else if (x == 3 && y == 2 && dir.x == -1)
                                {
                                    thisDepth++;
                                    for (int newY = 0; newY < 5; newY++)
                                        if (map.TryGetValue((thisDepth, new V2(4, newY)), out bug))
                                            if (bug)
                                                count++;
                                }
                                else if (x == 2 && y == 1 && dir.y == 1)
                                {
                                    thisDepth++;
                                    for (int newX = 0; newX < 5; newX++)
                                        if (map.TryGetValue((thisDepth, new V2(newX, 0)), out bug))
                                            if (bug)
                                                count++;
                                }
                                else if (x == 2 && y == 3 && dir.y == -1)
                                {
                                    thisDepth++;
                                    for (int newX = 0; newX < 5; newX++)
                                        if (map.TryGetValue((thisDepth, new V2(newX, 4)), out bug))
                                            if (bug)
                                                count++;
                                }
                                else if (x == 0 && dir.x == -1 || x == 4 && dir.x == 1 || y == 0 && dir.y == -1 || y == 4 && dir.y == 1)
                                {
                                    if (map.TryGetValue((thisDepth - 1, new V2(2, 2) + dir), out bug))
                                        if (bug)
                                            count++;
                                }
                                else if (map.TryGetValue((thisDepth, pos + dir), out bug))
                                    if (bug)
                                        count++;
                            }

                            newMap[(d, pos)] = count == 1 || !map[(d, pos)] && count == 2;
                        }

                foreach (var key in newMap.Keys.ToList())
                    map[key] = newMap[key];

                ++mins;
                ++depth;
            }

            return map.Values.Count(b => b == true).ToString();
        }

        void Print(Dictionary<V2, char> map)
        {
            Console.WriteLine();
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                    Console.Write(map[new V2(x, y)]);

                Console.WriteLine();
            }
        }

        string GetMapString(Dictionary<V2, char> map)
        {
            string s = "";
            foreach (var kv in map)
                s += kv.Value;

            return s;
        }
    }
}
