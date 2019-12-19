using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day19 : Day
    {
        private IEnumerable<long> codeList;

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/19.txt").Split(',').Select(s => long.Parse(s));

            long count = 0;

            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    LCVM vm = new LCVM(codeList.ToArray());
                    vm.AddInput(x);
                    vm.AddInput(y);
                    vm.GetCodeResult();
                    count += vm.GetOutput();
                }
            }

            return count.ToString();
        }

        public override string Part2()
        {
            var map = new Dictionary<(int x, int y), bool>();

            int y = 1;
            int x = 1;
            int minX = 0;
            int rowLength = 1;
            bool foundThisRow = false;
            bool foundStart = false;

            while (true)
            {
                LCVM vm = new LCVM(codeList.ToArray());
                vm.AddInput(x);
                vm.AddInput(y);
                vm.GetCodeResult();
                bool isBeam = vm.GetOutput() == 1;
                map.Add((x, y), isBeam);

                if (isBeam)
                {
                    if (foundThisRow)
                        x++;
                    else
                    {
                        if (rowLength >= 100)
                            if (map.TryGetValue((x + 99, y - 99), out bool beam) && beam)
                                return (x * 10000 + (y - 99)).ToString();

                        minX = x;
                        x += rowLength;
                    }

                    foundThisRow = true;
                    foundStart = true;
                }
                else if (foundThisRow)
                {
                    // After beam
                    rowLength = x - minX;
                    x = minX;
                    y++;
                    foundThisRow = false;
                }
                else
                {
                    // Before beam
                    x++;

                    if (!foundStart && x > 10)
                    {
                        y++;
                        x = 0;
                    }
                }
            }
        }
    }
}
