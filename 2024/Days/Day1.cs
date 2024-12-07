using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day1 : Day
    {
        string[] lines = File.ReadAllLines("Input/1.txt");

        public override object Part1()
        {
            int sum = 0;

            List<int> first = lines.Select(x => int.Parse(x.Split("   ")[0])).ToList();
            List<int> two = lines.Select(x => int.Parse(x.Split("   ")[1])).ToList();

            first.Sort();
            two.Sort();

            for (int i = 0; i < first.Count; i++)
                sum += Math.Abs(first[i] - two[i]);

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;

            List<int> first = lines.Select(x => int.Parse(x.Split("   ")[0])).ToList();
            List<int> two = lines.Select(x => int.Parse(x.Split("   ")[1])).ToList();

            first.ForEach(x =>
            {
                sum += x * two.Count(y => y == x);
            });

            return sum;
        }
    }
}
