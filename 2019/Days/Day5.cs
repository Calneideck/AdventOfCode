using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day5 : Day
    {
        enum Mode { Position, Immediate };

        private int[] codes;

        public override int Part1()
        {
            codes = File.ReadAllText("Input/5.txt").Split(',').Select(s => int.Parse(s)).ToArray();
            return GetCodeResult(codes, 1);
        }

        public override int Part2()
        {
            codes = File.ReadAllText("Input/5.txt").Split(',').Select(s => int.Parse(s)).ToArray();
            return GetCodeResult(codes, 5);
        }

        private int GetCodeResult(int[] codes, int input)
        {
            int length;
            int diagCode = -1;

            for (int i = 0; i < codes.Length; i += length)
            {
                string instruction = codes[i].ToString("0000");

                int opcode = PlaceValue(instruction, 0) + PlaceValue(instruction, 1) * 10;
                Mode mode1 = (Mode)PlaceValue(instruction, 2);
                Mode mode2 = (Mode)PlaceValue(instruction, 3);

                int num1 = Num(i + 1, mode1);
                int num2 = Num(i + 2, mode2);
                int num3 = Num(i + 3, Mode.Immediate);

                switch (opcode)
                {
                    case 1:
                        codes[num3] = num1 + num2;
                        length = 4;
                        break;

                    case 2:
                        codes[codes[i + 3]] = num1 * num2;
                        length = 4;
                        break;

                    case 3:
                        Write(input, codes[i + 1]);
                        length = 2;
                        break;

                    case 4:
                        int output = num1;
                        if (output != 0 && codes[i + 2] != 99)
                            throw new Exception("Error! Output: " + output + ", Code: " + i);
                        else
                            diagCode = output;

                        length = 2;
                        break;

                    case 5:
                        if (num1 != 0)
                        {
                            i = num2;
                            length = 0;
                        }
                        else
                            length = 3;
                        break;

                    case 6:
                        if (num1 == 0)
                        {
                            i = num2;
                            length = 0;
                        }
                        else
                            length = 3;
                        break;

                    case 7:
                        Write(num1 < num2 ? 1 : 0, num3);
                        length = 4;
                        break;

                    case 8:
                        Write(num1 == num2 ? 1 : 0, num3);
                        length = 4;
                        break;

                    case 99:
                        return diagCode;

                    default:
                        throw new Exception("Error! Code: " + i);
                }
            }

            return -1;
        }

        int PlaceValue(string s, int place)
        {
            return int.Parse(s[s.Length - 1 - place].ToString());
        }

        int Num(int address, Mode mode)
        {
            if (mode == Mode.Position && address >= 0 && address < codes.Length)
                address = codes[address];

            if (address >= 0 && address < codes.Length)
                return codes[address];
            else
                return 0;
        }

        void Write(int num, int target)
        {
            codes[target] = num;
        }
    }
}
