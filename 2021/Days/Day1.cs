using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day1 : Day
    {
        string[] lines = File.ReadAllLines("Input/1.txt");
        List<int> numbers;

        public override object Part1()
        {
            numbers = lines.Select(int.Parse).ToList();
            int count = 0;

            for (int i = 1; i < numbers.Count; i++)
                if (numbers[i] > numbers[i - 1])
                    count++;

            return count;
        }

        public override object Part2()
        {
            int count = 0;

            for (int i = 1; i < numbers.Count - 2; i++)
            {
                int a = numbers[i - 1] + numbers[i] + numbers[i + 1];
                int b = numbers[i] + numbers[i + 1] + numbers[i + 2];
                if (b > a)
                    count++;
            }

            return count;
        }
    }
}
