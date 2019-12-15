using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day9 : Day
    {
        private IEnumerable<long> codeList;

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/9.txt").Split(',').Select(s => long.Parse(s));

            LCVM vm = new LCVM(codeList.ToArray());
            vm.AddInput(1);

            LCVM.StopCode result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                    Console.WriteLine(vm.GetOutput());
            }

            return base.Part1();
        }

        public override string Part2()
        {
            LCVM vm = new LCVM(codeList.ToArray());
            vm.AddInput(2);

            LCVM.StopCode result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                    Console.WriteLine(vm.GetOutput());
            }

            return base.Part2();
        }
    }
}
