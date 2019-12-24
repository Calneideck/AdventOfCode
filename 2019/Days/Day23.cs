using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day23 : Day
    {
        private IEnumerable<long> codeList;

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/23.txt").Split(',').Select(s => long.Parse(s));

            var outputs = new Dictionary<long, List<long>>();
            var vms = new Dictionary<long, LCVM>();

            for (int i = 0; i < 50; i++)
            {
                vms[i] = new LCVM(codeList.ToArray());
                vms[i].AddInput(i);
                outputs[i] = new List<long>();
            }

            while (true)
                for (int i = 0; i < 50; i++)
                {
                    LCVM.StopCode result = vms[i].GetCodeResult();
                    if (result == LCVM.StopCode.Input)
                        vms[i].AddInput(-1);
                    else if (result == LCVM.StopCode.Output)
                        while (vms[i].HasOutput())
                        {
                            outputs[i].Add(vms[i].GetOutput());

                            if (outputs[i].Count == 3)
                            {
                                long addr = outputs[i][0];
                                long X = outputs[i][1];
                                long Y = outputs[i][2];
                                outputs[i].Clear();

                                if (addr == 255)
                                    return Y.ToString();

                                vms[addr].AddInput(X);
                                vms[addr].AddInput(Y);
                            }
                        }
                }
        }

        public override string Part2()
        {
            var outputs = new Dictionary<long, List<long>>();
            var vms = new Dictionary<long, LCVM>();

            for (int i = 0; i < 50; i++)
            {
                vms[i] = new LCVM(codeList.ToArray());
                vms[i].AddInput(i);
                outputs[i] = new List<long>();
            }

            (long X, long Y) NAT = (0, 0);
            (long X, long Y) oldNAT = (0, 0);

            while (true)
            {
                bool idle = true;
                for (int i = 0; i < 50; i++)
                {
                    LCVM.StopCode result = vms[i].GetCodeResult();
                    if (result == LCVM.StopCode.Input)
                        vms[i].AddInput(-1);
                    else if (result == LCVM.StopCode.Output)
                        while (vms[i].HasOutput())
                        {
                            idle = false;
                            outputs[i].Add(vms[i].GetOutput());

                            if (outputs[i].Count == 3)
                            {
                                long addr = outputs[i][0];
                                long X = outputs[i][1];
                                long Y = outputs[i][2];
                                outputs[i].Clear();

                                if (addr == 255)
                                {
                                    oldNAT = NAT;
                                    NAT = (X, Y);
                                }
                                else
                                {
                                    vms[addr].AddInput(X);
                                    vms[addr].AddInput(Y);
                                }
                            }
                        }
                }

                if (idle)
                {
                    if (NAT != (0, 0))
                    {
                        vms[0].AddInput(NAT.X);
                        vms[0].AddInput(NAT.Y);

                        if (oldNAT == NAT)
                            return NAT.Y.ToString();
                    }
                }
            }
        }
    }
}
