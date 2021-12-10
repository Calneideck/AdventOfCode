using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day7 : Day
    {
        string[] lines = File.ReadAllLines("Input/7.txt");
        List<int> numbers;

        public override object Part1()
        {
            numbers = lines[0].Split(',').Select(int.Parse).ToList();
            return Enumerable.Range(0, numbers.Last()).Min(SumFuel);
        }

        long SumFuel(int target)
        {
            return numbers.Sum((x) => Math.Abs(target - x));
        }

        public override object Part2()
        {
            numbers = lines[0].Split(',').Select(int.Parse).ToList();
            return Enumerable.Range(0, numbers.Last()).Min(SumFuelF);
        }

        long SumFuelF(int target)
        {
            return numbers.Sum((x) => {
                long dist = Math.Abs(target - x);
                return dist * (dist + 1) / 2;
            });
        }
    }
}
