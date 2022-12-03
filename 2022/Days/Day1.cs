using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day1 : Day
    {
        string[] lines = File.ReadAllLines("Input/1.txt");
        List<int> numbers = new List<int>();

        public override object Part1()
        {
            int i = 0;
            numbers.Add(0);

            foreach (var line in lines)
                if (line.Length == 0)
                {
                    i++;
                    numbers.Add(0);
                }
                else
                    numbers[i] += int.Parse(line);

            return numbers.Max();
        }

        public override object Part2()
        {
            numbers.Sort();
            return numbers[^1] + numbers[^2] + numbers[^3];
        }
    }
}
