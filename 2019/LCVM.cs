using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class LCVM
    {
        private enum Mode { Position, Immediate, Relative };

        private List<long> codes;
        private int i;
        private Queue<long> inputs;
        private int relBase;

        public LCVM(long[] codes)
        {
            this.codes = codes.ToList();
            inputs = new Queue<long>();
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

        public long GetCodeResult(out bool stop)
        {
            int length;

            while (true)
            {
                string instruction = codes[i].ToString("00000");

                int opcode = PlaceValue(instruction, 0) + PlaceValue(instruction, 1) * 10;
                Mode mode1 = (Mode)PlaceValue(instruction, 2);
                Mode mode2 = (Mode)PlaceValue(instruction, 3);
                Mode mode3 = (Mode)PlaceValue(instruction, 4);

                long p1 = GetParam(1, mode1);
                long p3 = GetParam(3, mode3);

                long num1 = Num(i + 1, mode1);
                long num2 = Num(i + 2, mode2);

                switch (opcode)
                {
                    case 1:
                        Write(num1 + num2, (int)p3);
                        length = 4;
                        break;

                    case 2:
                        Write(num1 * num2, (int)p3);
                        length = 4;
                        break;

                    case 3:
                        if (inputs.Count == 0)
                        {
                            stop = false;
                            return -2;
                        }

                        Write(inputs.Dequeue(), (int)p1);
                        length = 2;
                        break;

                    case 4:
                        stop = false;
                        i += 2;
                        return num1;

                    case 5:
                        if (num1 != 0)
                        {
                            i = (int)num2;
                            length = 0;
                        }
                        else
                            length = 3;
                        break;

                    case 6:
                        if (num1 == 0)
                        {
                            i = (int)num2;
                            length = 0;
                        }
                        else
                            length = 3;
                        break;

                    case 7:
                        Write(num1 < num2 ? 1 : 0, (int)p3);
                        length = 4;
                        break;

                    case 8:
                        Write(num1 == num2 ? 1 : 0, (int)p3);
                        length = 4;
                        break;

                    case 9:
                        relBase += (int)num1;
                        length = 2;
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

        long Num(int address, Mode mode)
        {
            if (address >= codes.Count)
                codes.AddRange(Enumerable.Range(0, address - codes.Count + 1).Select(x => (long)0));

            if ((mode == Mode.Position || mode == Mode.Relative) && address >= 0)
                address = (int)codes[address];

            if (address >= codes.Count)
                codes.AddRange(Enumerable.Range(0, address - codes.Count + 1).Select(x => (long)0));

            if (mode == Mode.Relative)
                address += relBase;
            
            if (address >= 0 && address < codes.Count)
                return codes[address];
            else
                return 0;
        }

        long GetParam(int offset, Mode mode)
        {
            long value = codes[i + offset];
            if (mode == Mode.Relative)
                value += relBase;

            return value;
        }

        void Write(long num, int target)
        {
            if (target >= codes.Count)
                codes.AddRange(Enumerable.Range(0, target - codes.Count + 1).Select(x => (long)0));

            codes[target] = num;
        }
    }
}
