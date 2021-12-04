using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;

namespace AdventOfCode
{
    class Day3 : Day
    {
        string[] lines = File.ReadAllLines("Input/3.txt");

        public override object Part1()
        {
            int len = lines[0].Length;

            int gamma = 0;
            int ep = 0;

            for (int i = 0; i < len; i++)
            {
                int z = lines.Select(l => l[i]).Count(x => x == '0');
                int one = lines.Select(l => l[i]).Count(x => x == '1');

                gamma += one > z ? (int)Math.Pow(2, len - 1 - i) : 0;
                ep += one < z ? (int)Math.Pow(2, len - 1 - i) : 0;
            }

            return gamma * ep;
        }

        public override object Part2()
        {
            int len = lines[0].Length;

            int ox = 0;
            int co = 0;

            List<string> nums = lines.ToList();

            // O2
            for (int i = 0; i < len; i++)
            {
                int z = nums.Select(l => l[i]).Count(x => x == '0');
                int one = nums.Select(l => l[i]).Count(x => x == '1');

                nums = nums.Where(n => one >= z ? n[i] == '1' : n[i] == '0').ToList();
                if (nums.Count == 1)
                    break;
            }

            ox = Convert.ToInt32(nums[0], 2);
            nums = lines.ToList();

            // CO2
            for (int i = 0; i < len; i++)
            {
                int z = nums.Select(l => l[i]).Count(x => x == '0');
                int one = nums.Select(l => l[i]).Count(x => x == '1');

                nums = nums.Where(n => z <= one ? n[i] == '0' : n[i] == '1').ToList();
                if (nums.Count == 1)
                    break;
            }

            co = Convert.ToInt32(nums[0], 2);

            return ox * co;
        }
    }
}
