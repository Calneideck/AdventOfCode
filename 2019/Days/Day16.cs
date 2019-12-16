using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day16 : Day
    {
        private IEnumerable<int> input;

        public override string Part1()
        {
            input = File.ReadAllText("Input/16.txt").ToCharArray().Select(c => int.Parse(c.ToString()));

            var codes = input.ToArray();
            int count = codes.Length;

            for (int phase = 0; phase < 100; phase++)
            {
                int[] output = new int[count];
                for (int i = 0; i < count; i++)
                {
                    int sum = 0;
                    int j = i;
                    int step = i + 1;
                    while (j < count)
                    {
                        sum += codes.Skip(j).Take(step).Sum();
                        j += 2 * step;

                        sum -= codes.Skip(j).Take(step).Sum();
                        j += 2 * step;
                    }

                    output[i] = Math.Abs(sum) % 10;
                }

                codes = output;
            }

            string result = "";
            for (int i = 0; i < 8; i++)
                result += codes[i].ToString();

            return result;
        }

        public override string Part2()
        {
            List<int> newInput = new List<int>();
            for (int i = 0; i < 10000; i++)
                newInput.AddRange(input);

            int offset = 0;
            for (int i = 0; i < 7; i++)
                offset += (int)Math.Pow(10, 6 - i) * newInput[i];

            newInput = newInput.Skip(offset).ToList();
            int count = newInput.Count;

            for (int i = 0; i < 100; i++)
                for (int j = count - 2; j >= 0; j--)
                {
                    newInput[j] += newInput[j + 1];
                    newInput[j] %= 10;
                }

            string result = "";
            for (int i = 0; i < 8; i++)
                result += newInput[i].ToString();

            return result;
        }
    }
}
