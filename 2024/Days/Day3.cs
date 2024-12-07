using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day3 : Day
    {
        string lines = File.ReadAllText("Input/3.txt");

        public override object Part1()
        {
            long sum = 0;

            Regex r = new(@"mul\((\d{1,3}),(\d{1,3})\)");

            var matches = r.Matches(lines);

            foreach (Match a in matches)
                sum += int.Parse(a.Groups[1].Value) * int.Parse(a.Groups[2].Value);

            return sum;
        }

        public override object Part2()
        {
            long sum = 0;
            bool e = true;

            Regex r = new(@"(mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\))");

            var matches = r.Matches(lines);

            foreach (Match a in matches)
            {
                if (a.Value == "do()") e = true;
                else if (a.Value == "don't()") e = false;
                else if (e)
                    sum += int.Parse(a.Groups[2].Value) * int.Parse(a.Groups[3].Value);
            }

            return sum;
        }
    }
}
