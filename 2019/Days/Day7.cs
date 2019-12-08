using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day7 : Day
    {

        private IEnumerable<int> codeList;

        public override int Part1()
        {
            codeList = File.ReadAllText("Input/7.txt").Split(',').Select(s => int.Parse(s));
            int highestResult = 0;

            for (int a = 0; a < 5; a++)
            {
                for (int b = 0; b < 5; b++)
                {
                    for (int c = 0; c < 5; c++)
                    {
                        for (int d = 0; d < 5; d++)
                        {
                            for (int e = 0; e < 5; e++)
                            {
                                int[] inputs = new int[2];
                                int[] signals = new int[] { a, b, c, d, e };
                                if (signals.Distinct().Count() != 5)
                                    continue;

                                int result = 0;
                                for (int i = 0; i < 5; i++)
                                {
                                    inputs[0] = signals[i];
                                    inputs[1] = result;
                                    ICVM vm = new ICVM(codeList.ToArray());
                                    vm.AddInputs(inputs);
                                    result = vm.GetCodeResult(out bool stop);
                                }

                                if (result > highestResult)
                                    highestResult = result;
                            }
                        }
                    }
                }
            }

            return highestResult;
        }

        public override int Part2()
        {
            codeList = File.ReadAllText("Input/7.txt").Split(',').Select(s => int.Parse(s));
            int highestResult = 0;

            for (int a = 5; a < 10; a++)
            {
                for (int b = 5; b < 10; b++)
                {
                    for (int c = 5; c < 10; c++)
                    {
                        for (int d = 5; d < 10; d++)
                        {
                            for (int e = 5; e < 10; e++)
                            {
                                int[] signals = new int[] { a, b, c, d, e };
                                if (signals.Distinct().Count() != 5)
                                    continue;

                                int result = 0;
                                int lastE = 0;
                                int vmIndex = 0;
                                ICVM[] vms = new ICVM[5];
                                for (int i = 0; i < vms.Length; i++)
                                {
                                    vms[i] = new ICVM(codeList.ToArray());
                                    vms[i].AddInput(signals[i]);
                                }

                                bool stop = false;
                                while (!stop || vmIndex % 5 != 0)
                                {
                                    vms[vmIndex % 5].AddInput(result);
                                    result = vms[vmIndex % 5].GetCodeResult(out stop);

                                    // Get valid output from E amp
                                    if (result >= 0 && vmIndex % 5 == 4)
                                        lastE = result;

                                    vmIndex++;
                                }

                                if (lastE > highestResult)
                                    highestResult = lastE;
                            }
                        }
                    }
                }
            }

            return highestResult;
        }
    }
}
