using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day3 : Day
    {
        string[] lines = File.ReadAllLines("Input/3.txt");

        public override object Part1()
        {
            int count = 0;

            foreach (string line in lines)
            {
                int[] nums = [.. line.ToCharArray().Select(c => int.Parse(c.ToString()))];

                int max = nums[0..^1].Max();
                int index = nums.IndexOf(max) + 1;
                int max2 = nums[index..].Max();

                count += int.Parse(max + "" + max2);
            }

            return count;
        }

        public override object Part2()
        {
            ulong count = 0;

            foreach (string line in lines)
            {
                int[] nums = [.. line.ToCharArray().Select(c => int.Parse(c.ToString()))];

                int index = 0;
                string num = "";

                for (int i = 11; i >= 0; i--)
                {
                    int max = nums[index..^i].Max();
                    index += nums[index..^i].IndexOf(max) + 1;
                    num += max;
                }

                count += ulong.Parse(num);
            }

            return count;
        }
    }
}
