using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day25 : Day
    {
        string[] lines = File.ReadAllLines("Input/25.txt");

        long Mult(char a) => a switch
        {
            '1' => 1,
            '2' => 2,
            '-' => -1,
            '=' => -2,
            _ => 0
        };

        char MultInv(int a) => a switch
        {
            1 => '1',
            2 => '2',
            -1 => '-',
            -2 => '=',
            _ => '0'
        };

        public override object Part1()
        {
            long sum = 0;

            foreach (var line in lines.Select(x => x.Reverse().ToArray()))
                for (int i = 0; i < line.Length; i++)
                    sum += (long)Math.Pow(5, i) * Mult(line[i]);

            string output = "";
            for (int i = 20; i >= 0; i--)
            {
                int thisSign = Math.Sign(sum);
                int diff = (int)Math.Round(Math.Abs(sum) / Math.Pow(5, i));
                sum -= thisSign * diff * (long)Math.Pow(5, i);
                output += MultInv(thisSign * diff);
            }

            return output;
        }

        public override object Part2()
        {
            return 0;
        }
    }
}
