using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day17 : Day
    {
        string[] lines = File.ReadAllLines("Input/17.txt");


        public override object Part1()
        {
            int[] reg = new int[3];
            reg[0] = int.Parse(lines[0].Split(' ')[2]);
            reg[1] = int.Parse(lines[1].Split(' ')[2]);
            reg[2] = int.Parse(lines[2].Split(' ')[2]);

            return Run(reg);
        }

        public override object Part2()
        {
            string program = lines[4].Split(' ')[1];

            for (int i = 1; i < int.MaxValue; i++)
                if (Run(new int[] { i, 0, 0 }) == program)
                    return i;

            return 0;
        }

        int GetCombo(int i, int[] registers)
        {
            switch (i)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4: return registers[0];
                case 5: return registers[1];
                case 6: return registers[2];
                default: throw new Exception("ERROR 7");
            }
        }

        string Run(int[] reg)
        {
            int ptr = 0;
            int[] program = lines[4].Split(' ')[1].Split(',').Select(int.Parse).ToArray();
            List<int> nums = new();

            while (ptr < program.Length)
            {
                bool jumped = false;
                switch (program[ptr])
                {
                    case 0:
                        {  //adv
                            long num = reg[0];
                            double denom = Math.Pow(2, GetCombo(program[ptr + 1], reg));
                            reg[0] = (int)Math.Floor(num / denom);
                            break;
                        }

                    case 1: //bxl
                        reg[1] = reg[1] ^ program[ptr + 1];
                        break;

                    case 2: //bst
                        reg[1] = GetCombo(program[ptr + 1], reg) % 8;
                        break;

                    case 3: //jnz
                        if (reg[0] != 0)
                        {
                            ptr = program[ptr + 1];
                            jumped = true;
                        }
                        break;

                    case 4: //bxc
                        reg[1] = reg[1] ^ reg[2];
                        break;

                    case 5: //out
                        nums.Add(GetCombo(program[ptr + 1], reg) % 8);
                        break;

                    case 6:
                        { //bdv
                            long num = reg[0];
                            double denom = Math.Pow(2, GetCombo(program[ptr + 1], reg));
                            reg[1] = (int)Math.Floor(num / denom);
                            break;
                        }
                    case 7:
                        { //cdv
                            long num = reg[0];
                            double denom = Math.Pow(2, GetCombo(program[ptr + 1], reg));
                            reg[2] = (int)Math.Floor(num / denom);
                            break;
                        }
                }

                if (!jumped) ptr += 2;
            }

            return string.Join(",", nums.Select(i => i.ToString()));
        }
    }
}
