using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day21 : Day
    {
        string[] lines = File.ReadAllLines("Input/21.txt");

        long Run(long human)
        {
            Dictionary<string, long> nums = new();
            List<(string, string, string, string)> inputs = new();
            if (human > 0)
                nums.Add("humn", human);

            for (int i = 0; i < lines.Length; i++)
            {
                Match m = new Regex(@"(-?\d+)").Match(lines[i]);
                if (m.Success)
                {
                    string monkey = lines[i].Split()[0].Substring(0, 4);
                    nums.TryAdd(monkey, long.Parse(m.Value));
                }
                else
                {
                    string[] temp = lines[i].Split();
                    inputs.Add((temp[0].Substring(0, 4), temp[1], temp[2], temp[3]));
                }
            }

            while (inputs.Count > 0)
            {
                List<(string, string, string, string)> toRemove = new();
                foreach (var inp in inputs)
                {
                    if (
                        (human == 0 || inp.Item1 != "root") &&
                        nums.TryGetValue(inp.Item2, out long num1) && 
                        nums.TryGetValue(inp.Item4, out long num2)
                    )
                    {
                        long result = 0;
                        if (inp.Item3 == "+") result = num1 + num2;
                        if (inp.Item3 == "*") result = num1 * num2;
                        if (inp.Item3 == "-") result = num1 - num2;
                        if (inp.Item3 == "/") result = num1 / num2;

                        nums.Add(inp.Item1, result);
                        toRemove.Add(inp);
                    }
                }

                foreach (var inp in toRemove)
                    inputs.Remove(inp);

                if (human > 0 && inputs.Count == 1)
                {
                    long left = nums[inputs[0].Item2];
                    long right = nums[inputs[0].Item4];
                    return left - right;
                }
            }

            return nums["root"];
        }

        public override object Part1()
        {
            return Run(0);
        }

        public override object Part2()
        {
            long left = 0;
            long right = 10000000000000;
            long pivot = right / 2;
            while (true)
            {
                long result = Run(pivot);
                if (result == 0)
                    return pivot;

                if (result > 0)
                    left = pivot;
                else
                    right = pivot;

                pivot = (right - left) / 2 + left;
            }
        }
    }
}
