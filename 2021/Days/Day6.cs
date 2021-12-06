using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day6 : Day
    {
        string[] lines = File.ReadAllLines("Input/6.txt");
        List<int> numbers;

        public override object Part1()
        {
            numbers = lines[0].Split(',').Select(int.Parse).ToList();

            for (int i = 0; i < 80; i++)
            {
                int c = numbers.Count;
                for (int x = 0; x < c; x++)
                    if (--numbers[x] == -1)
                    {
                        numbers.Add(8);
                        numbers[x] = 6;
                    }
            }

            return numbers.Count;
        }

        public override object Part2()
        {
            numbers = lines[0].Split(',').Select(int.Parse).ToList();

            List<long> fish = new();
            for (int i = 0; i < 9; i++)
                fish.Add(0);

            numbers.ForEach(x => ++fish[x]);

            for (int i = 0; i < 256; i++)
            {
                long newCount = fish[0];
                fish.Add(newCount);
                fish.RemoveAt(0);
                fish[6] += newCount;
            }

            return fish.Sum();
        }
    }
}
