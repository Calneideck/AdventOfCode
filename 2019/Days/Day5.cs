using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day5 : Day
    {
        enum Mode { Position, Immediate };

        public override int Part1()
        {
            int[] codes = File.ReadAllText("Input/5.txt").Split(',').Select(s => int.Parse(s)).ToArray();
            return GetCodeResult(codes, 1);
        }

        public override int Part2()
        {
            int[] codes = File.ReadAllText("Input/5.txt").Split(',').Select(s => int.Parse(s)).ToArray();
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

                int num1, num2;
                
                switch (opcode)
                {
                    case 1:
                        num1 = Num(codes, i, 1, mode1);
                        num2 = Num(codes, i, 2, mode2);
                        codes = Add(codes, num1, num2, codes[i + 3]);
                        length = 4;
                        break;

                    case 2:
                        num1 = Num(codes, i, 1, mode1);
                        num2 = Num(codes, i, 2, mode2);
                        codes = Multiply(codes, num1, num2, codes[i + 3]);
                        length = 4;
                        break;

                    case 3:
                        codes = Write(codes, input, codes[i + 1]);
                        length = 2;
                        break;

                    case 4:
                        int output = Num(codes, i, 1, mode1);
                        if (output != 0 && codes[i + 2] != 99)
                            throw new Exception("Error! Output: " + output + ", Code: " + i);
                        else
                            diagCode = output;

                        length = 2;
                        break;

                    case 5:
                        if (Num(codes, i, 1, mode1) != 0)
                        {
                            i = Num(codes, i, 2, mode2);
                            length = 0;
                        }
                        else
                            length = 3;
                        break;

                    case 6:
                        if (Num(codes, i, 1, mode1) == 0)
                        {
                            i = Num(codes, i, 2, mode2);
                            length = 0;
                        }
                        else
                            length = 3;
                        break;

                    case 7:
                        bool lessThan = Num(codes, i, 1, mode1) < Num(codes, i, 2, mode2);
                        codes = Write(codes, lessThan ? 1 : 0, codes[i + 3]);

                        length = 4;
                        break;

                    case 8:
                        bool equals = Num(codes, i, 1, mode1) == Num(codes, i, 2, mode2);
                        codes = Write(codes, equals ? 1 : 0, codes[i + 3]);

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

        int Num(int[] codes, int index, int offset, Mode mode)
        {
            return codes[mode == Mode.Position ? codes[index + offset] : index + offset];
        }

        int[] Add(int[] codes, int num1, int num2, int target)
        {
            codes[target] = num1 + num2;
            return codes;
        }

        int[] Multiply(int[] codes, int num1, int num2, int target)
        {
            codes[target] = num1 * num2;
            return codes;
        }

        int[] Write(int[] codes, int num, int target)
        {
            codes[target] = num;
            return codes;
        }
    }
}
