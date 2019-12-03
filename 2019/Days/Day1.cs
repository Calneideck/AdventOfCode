using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day1 : Day
    {
        public override int Part1()
        {
            string[] lines = File.ReadAllLines("Input/1.txt");
            int sum = 0;

            IEnumerable<int> numbers = lines.Select(line => int.Parse(line));
            foreach (int num in numbers)
                sum += (int)Math.Floor(num / 3f) - 2;

            return sum;
        }

        public override int Part2()
        {
            string[] lines = File.ReadAllLines("Input/1.txt");
            int sum = 0;

            IEnumerable<int> numbers = lines.Select(line => int.Parse(line));
            int thisNum;
            foreach (int num in numbers)
            {
                thisNum = num;

                while (thisNum > 0)
                {
                    thisNum = Math.Max((int)Math.Floor(thisNum / 3f) - 2, 0);
                    sum += thisNum;
                }
            }

            return sum;
        }
    }
}
