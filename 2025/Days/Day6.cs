using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace AdventOfCode
{
    class Day6 : Day
    {
        string[] lines = File.ReadAllLines("Input/6.txt");

        public override object Part1()
        {
            ulong count = 0;
            int rows = lines.Length - 1;

            List<int[]> cols = new();

            for (int y = 0; y < rows; y++)
            {
                int[] numbersByRow = lines[y].GrabInts();

                for (int x = 0; x < numbersByRow.Length; x++)
                {
                    if (y == 0)
                        cols.Add(new int[rows]);

                    cols[x][y] = numbersByRow[x];
                }
            }

            string[] ops = new Regex(@"([*+])")
                .Matches(lines[rows]).Select(a => a.Value)
                .ToArray();

            for (int i = 0; i < cols.Count; i++)
            {
                if (ops[i] == "+")
                    count += cols[i].Aggregate((ulong)0, (sum, val) => (ulong)val + sum);
                else
                    count += cols[i].Aggregate((ulong)1, (sum, val) => (ulong)val * sum);
            }

            return count;
        }

        public override object Part2()
        {
            ulong count = 0;
            int rows = lines.Length - 1;

            string[] ops = new Regex(@"([*+])")
                .Matches(lines[rows]).Select(a => a.Value)
                .ToArray();

            int index = 0;
            for (int i = 0; i < ops.Length; i++)
            {
                int endIndex = NextEndIndex(index + 1) - 1;
                int numCount = endIndex - index + 1;
                int[] nums = new int[numCount];

                for (int x = endIndex; x >= index; x--)
                    nums[x - index] = int.Parse(
                        lines[0..^1].Aggregate("", (text, line) => text + line[x]).Trim()
                    );

                if (ops[i] == "+")
                    count += nums.Aggregate((ulong)0, (sum, val) => (ulong)val + sum);
                else
                    count += nums.Aggregate((ulong)1, (sum, val) => (ulong)val * sum);

                index = endIndex + 2;
            }


            return count;
        }

        int NextEndIndex(int startIndex)
        {
            for (int i = startIndex; i < lines[0].Length; i++)
                if (lines[0..^1].All(line => line[i] == ' '))
                    return i;

            return lines[0].Length;
        }
    }
}
