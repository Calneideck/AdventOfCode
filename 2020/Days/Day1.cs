using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day1 : Day
    {
        string[] lines = File.ReadAllLines("Input/1.txt");

        public override object Part1()
        {
            IEnumerable<int> numbers = lines.Select(line => int.Parse(line));
            foreach (int num in numbers)
                foreach (int num2 in numbers)
                    if (num + num2 == 2020)
                        return (num * num2).ToString();

            return "";
        }

        public override object Part2()
        {
            IEnumerable<int> numbers = lines.Select(line => int.Parse(line));
            foreach (int num in numbers)
                foreach (int num2 in numbers)
                    foreach (int num3 in numbers)
                        if (num + num2 + num3 == 2020)
                            return (num * num2 * num3).ToString();

            return "";
        }
    }
}
