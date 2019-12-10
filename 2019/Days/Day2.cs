using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day2 : Day
    {
        public override string Part1()
        {
            int[] codes = File.ReadAllText("Input/2.txt").Split(',').Select(s => int.Parse(s)).ToArray();

            return GetCodeResult(codes, 12, 2).ToString();
        }

        public override string Part2()
        {
            var codes = File.ReadAllText("Input/2.txt").Split(',').Select(s => int.Parse(s));

            for (int noun = 0; noun <= 99; noun++)
                for (int verb = 0; verb <= 99; verb++)
                {
                    int result = GetCodeResult(codes.ToArray(), noun, verb);
                    if (result == 19690720)
                        return (100 * noun + verb).ToString();
                }

            return "";
        }

        private int GetCodeResult(int[] codes, int noun, int verb)
        {
            codes[1] = noun;
            codes[2] = verb;

            for (int i = 0; i < codes.Length; i += 4)
            {
                if (codes[i] == 1)
                {
                    codes[codes[i + 3]] = codes[codes[i + 1]] + codes[codes[i + 2]];
                }
                else if (codes[i] == 2)
                {
                    codes[codes[i + 3]] = codes[codes[i + 1]] * codes[codes[i + 2]];
                }
                else if (codes[i] == 99)
                    break;
                else
                    throw new Exception("Error! Code: " + i);
            }

            return codes[0];
        }
    }
}
