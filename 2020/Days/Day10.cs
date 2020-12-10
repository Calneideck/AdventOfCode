using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day10 : Day
    {
        string[] lines = File.ReadAllLines("Input/10.txt");

        List<int> nums;

        public override object Part1()
        {
            nums = lines.Select(int.Parse).ToList();
            nums.Sort();

            int[] diffs = new int[] { 0, 0, 0 };

            int jolt = 0;

            while (true)
            {
                var next = nums.FindAll(j => j - jolt <= 3 && j - jolt > 0);
                if (next.Count == 0)
                {
                    diffs[2]++;
                    break;
                }

                int adapter = next[0];
                diffs[adapter - jolt - 1]++;
                jolt = adapter;
            }


            return diffs[0] * diffs[2];
        }

        Dictionary<string, long> map = new Dictionary<string, long>();

        long GetDistincts(int jolt, int[] adapters)
        {
            string key = string.Join(',', adapters.Select(i => i.ToString()));
            if (map.ContainsKey(key))
                return map[key];
            
            int[] next = adapters.Where(j => j - jolt <= 3 && j - jolt > 0).ToArray();
            long sum = 0;

            for (int i = 0; i < next.Length; i++)
                sum += GetDistincts(next[i], adapters.Skip(i + 1).ToArray());

            map[key] = sum + Math.Max(0, next.Length - 1);

            return sum + Math.Max(0, next.Length - 1);
        }

        public override object Part2()
        {
            return GetDistincts(0, nums.ToArray()) + 1;
        }
    }
}
