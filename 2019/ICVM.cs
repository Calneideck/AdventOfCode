using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class ICVM
    {
        private enum Mode { Position, Immediate };

        private int[] codes;
        private int i;
        private Queue<int> inputs;

        public ICVM(int[] codes)
        {
            this.codes = codes;
            inputs = new Queue<int>();
        }

        public void AddInput(int input)
        {
            inputs.Enqueue(input);
        }

        public void AddInputs(IEnumerable<int> inputs)
        {
            foreach (var item in inputs)
                this.inputs.Enqueue(item);
        }

        public int GetCodeResult(out bool stop)
        {
            int length;

            while (true)
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
                        Write(inputs.Dequeue(), codes[i + 1]);
                        length = 2;
                        break;

                    case 4:
                        stop = false;
                        i += 2;
                        return num1;

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
                        {
                            stop = true;
                            return -1;
                        }

                    default:
                        throw new Exception("Error! Code: " + i);
                }

                i += length;
            }
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
