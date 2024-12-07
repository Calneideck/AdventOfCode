using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day7 : Day
    {
        string[] lines = File.ReadAllLines("Input/7.txt");

        public override object Part1()
        {
            long sum = 0;

            foreach (string line in lines)
            {
                long t = long.Parse(line.Split(":")[0]);
                long[] nums = line.Split(": ")[1].Split(" ").Select(long.Parse).ToArray();

                if (Check(nums, Array.Empty<long>(), t, false))
                    sum += t;
            }

            return sum;
        }

        public override object Part2()
        {
            long sum = 0;

            foreach (string line in lines)
            {
                long t = long.Parse(line.Split(":")[0]);
                long[] nums = line.Split(": ")[1].Split(" ").Select(long.Parse).ToArray();

                if (Check(nums, Array.Empty<long>(), t, true))
                    sum += t;
            }

            return sum;
        }

        bool Check(long[] nums, long[] ops, long target, bool v2)
        {
            if (ops.Length < nums.Length - 1)
            {
                int[] next = v2 ? new int[] { 0, 1, 2 } : new int[] { 0, 1 };
                return next.Any(op => Check(nums, ops.Append(op).ToArray(), target, v2));
            }
            else
            {
                long sum = nums[0];
                for (int i = 1; i < nums.Length; i++)
                {
                    if (ops[i - 1] == 0) sum += nums[i];
                    else if (ops[i - 1] == 1) sum *= nums[i];
                    else if (ops[i - 1] == 2) sum = long.Parse(sum.ToString() + nums[i].ToString());
                }
                return sum == target;
            }
        }
    }
}
