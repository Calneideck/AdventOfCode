using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2 : Day
    {
        string[] lines = File.ReadAllLines("Input/2.txt");

        public override object Part1()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                int[] nums = line.Split(' ').Select(int.Parse).ToArray();

                if (DoLine(nums)) sum++;
            }

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                int[] nums = line.Split(' ').Select(int.Parse).ToArray();

                if (DoLine(nums)) sum++;
                else
                    for (int i = 0; i < nums.Length; i++)
                    {
                        var next = nums.ToList();
                        next.RemoveAt(i);
                        if (DoLine(next.ToArray()))
                        {
                            sum++;
                            break;
                        }
                    }
            }

            return sum;
        }

        bool DoLine(int[] nums)
        {
            bool asc = nums[1] > nums[0];

            for (int i = 1; i < nums.Length; i++)
                if (asc)
                {
                    if (nums[i] <= nums[i - 1] || nums[i] - nums[i - 1] > 3)
                        return false;
                }
                else if (nums[i] >= nums[i - 1] || nums[i - 1] - nums[i] > 3)
                    return false;

            return true;
        }
    }
}
