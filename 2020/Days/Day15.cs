using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AdventOfCode
{
    class Day15 : Day
    {
        int[] input = File.ReadAllLines("Input/14.txt")[0].Split(',').Select(int.Parse).ToArray();

        Dictionary<int, int> ages = new Dictionary<int, int>();

        public override object Part1()
        {
            for (int i = 0; i < input.Length; i++)
                ages.Add(input[i], i + 1);

            int last = input.Last();
            for (int i = input.Length + 1; i <= 2020; i++)
            {
                int num = 0;
                if (ages.TryGetValue(last, out var prev))
                    num = i - 1 - prev;

                if (ages.ContainsKey(last))
                    ages[last] = i - 1;
                else
                    ages.Add(last, i - 1);

                last = num;
            }

            return last;
        }

        public override object Part2()
        {
            ages.Clear();
            for (int i = 0; i < input.Length; i++)
                ages.Add(input[i], i + 1);

            int last = input.Last();
            for (int i = input.Length + 1; i <= 30000000; i++)
            {
                int num = 0;
                if (ages.TryGetValue(last, out var prev))
                    num = i - 1 - prev;

                if (ages.ContainsKey(last))
                    ages[last] = i - 1;
                else
                    ages.Add(last, i - 1);

                last = num;
            }

            return last;
        }
    }
}
