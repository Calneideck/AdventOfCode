using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day5 : Day
    {
        string[] lines = File.ReadAllLines("Input/5.txt");
        List<(int, int)> rules = new();

        public override object Part1()
        {
            int sum = 0;

            bool readingRules = true;
            for (int x = 0; x < lines.Length; x++)
                if (readingRules)
                {
                    if (lines[x].Length == 0)
                        readingRules = false;
                    else
                    {
                        int[] a = lines[x].Split('|').Select(int.Parse).ToArray();
                        rules.Add((a[0], a[1]));
                    }
                }
                else
                {
                    int[] nums = lines[x].Split(",").Select(int.Parse).ToArray();

                    if (IsGood(nums))
                        sum += nums[nums.Length / 2];
                }

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;

            bool rr = true;
            for (int x = 0; x < lines.Length; x++)
                if (rr)
                {
                    if (lines[x].Length == 0)
                        rr = false;
                }
                else
                {
                    int[] nums = lines[x].Split(",").Select(int.Parse).ToArray();

                    if (!IsGood(nums))
                        sum += MakeGood(nums);
                }

            return sum;
        }

        bool IsGood(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
                foreach (var rule in rules)
                    if (nums[i] == rule.Item2)
                        if (nums.Skip(i).Contains(rule.Item1))
                            return false;

            return true;
        }

        int MakeGood(int[] nums)
        {
            if (IsGood(nums))
                return nums[nums.Length / 2];

            for (int i = 0; i < nums.Length; i++)
                foreach (var rule in rules)
                    if (nums[i] == rule.Item2)
                    {
                        int index = nums.Skip(i + 1).ToList().IndexOf(rule.Item1);
                        if (index >= 0)
                        {
                            List<int> newArr = nums.ToList();
                            newArr.RemoveAt(index + i + 1);
                            newArr.Insert(i, rule.Item1);
                            return MakeGood(newArr.ToArray());
                        }
                    }

            return 0;
        }
    }
}
