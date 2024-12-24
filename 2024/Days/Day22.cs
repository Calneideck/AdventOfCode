using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day22 : Day
    {
        string[] lines = File.ReadAllLines("Input/22.txt");

        public override object Part1()
        {
            long sum = 0;

            foreach (string line in lines)
            {
                long num = int.Parse(line);

                for (int i = 0; i < 2000; i++)
                {
                    num = Prune(Mix(num, num * 64));
                    num = Prune(Mix(num, num / 32));
                    num = Prune(Mix(num, num * 2048));
                }

                sum += num;
            }

            return sum;
        }

        public override object Part2()
        {
            Dictionary<string, long> map = new();

            foreach (string line in lines)
            {
                long num = int.Parse(line);
                int[] changes = new int[2000];
                HashSet<string> seen = new();

                for (int i = 0; i < 2000; i++)
                {
                    long next = Prune(Mix(num, num * 64));
                    next = Prune(Mix(next, next / 32));
                    next = Prune(Mix(next, next * 2048));

                    int price = int.Parse(next.ToString()[^1].ToString());
                    changes[i] = price - int.Parse(num.ToString()[^1].ToString());

                    if (i >= 3)
                    {
                        int start = i - 3;
                        int end = i + 1;
                        string key = string.Join(",", changes[start..end]);

                        if (!seen.Contains(key))
                        {
                            seen.Add(key);

                            if (map.ContainsKey(key))
                                map[key] += price;
                            else
                                map.Add(key, price);
                        }
                    }

                    num = next;
                }
            }

            return map.Max(kv => kv.Value);
        }

        long Mix(long a, long b)
        {
            return a ^ b;
        }

        long Prune(long a)
        {
            return a % 16777216;
        }
    }
}
