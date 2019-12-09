using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day9 : Day
    {
        private IEnumerable<long> codeList;

        public override int Part1()
        {
            codeList = File.ReadAllText("Input/9.txt").Split(',').Select(s => long.Parse(s));

            LCVM vm = new LCVM(codeList.ToArray());
            vm.AddInput(1);

            bool stop = false;
            while (!stop)
            {
                long result = vm.GetCodeResult(out stop);
                if (result >= 0)
                    Console.WriteLine(result);
            }

            return 0;
        }

        public override int Part2()
        {
            LCVM vm = new LCVM(codeList.ToArray());
            vm.AddInput(2);

            bool stop = false;
            while (!stop)
            {
                long result = vm.GetCodeResult(out stop);
                if (result >= 0)
                    Console.WriteLine(result);
            }

            return 0;
        }
    }
}
