using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day9 : Day
    {
        string[] lines = File.ReadAllLines("Input/9.txt");

        long target = 0;

        public override object Part1()
        {
            List<long> nums = lines.Select(l => long.Parse(l)).ToList();

            for (int i = 25; i < nums.Count; i++)
            {
                long thisNum = nums[i];
                var past = nums.GetRange(i - 25, 25);

                bool good = false;

                for (int x = 0; x < 25; x++)
                {
                    for (int y = 0; y < 25; y++)
                        if (past[x] + past[y] == thisNum)
                        {
                            good = true;
                            break;
                        }


                    if (good)
                        break;
                }

                if (!good)
                {
                    target = thisNum;
                    return thisNum;
                }
            }

            return -1;
        }

        public override object Part2()
        {
            List<long> nums = lines.Select(l => long.Parse(l)).ToList();
            int index = nums.FindIndex(num => num == target);

            for (int i = 0; i < index; i++)
            {
                int length = 0;
                long sum = 0;
                long min = long.MaxValue;
                long max = 0;
                while (sum < target)
                {
                    long thisNum = nums[i + length++];
                    min = Math.Min(min, thisNum);
                    max = Math.Max(max, thisNum);
                    sum += thisNum;
                }

                if (sum == target)
                    return min + max;
            }

            return -1;
        }
    }
}
