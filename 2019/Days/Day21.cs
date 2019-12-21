using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day21 : Day
    {
        private IEnumerable<long> codeList;


        public override string Part1()
        {
            codeList = File.ReadAllText("Input/21.txt").Split(',').Select(s => long.Parse(s));
            LCVM vm = new LCVM(codeList.ToArray());

            string[] instructions = new string[]
            {
                "NOT A J",
                "NOT B T",
                "OR T J",
                "NOT C T",
                "OR T J",
                "AND D J",
                "WALK"
            };

            foreach (string s in instructions)
            {
                var chars = s.ToCharArray();
                foreach (var c in chars)
                    vm.AddInput(c);

                vm.AddInput('\n');
            }

            long lastOutput = 0;
            var result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                {
                    lastOutput = vm.GetOutput();
                    if (lastOutput < 256)
                        Console.Write((char)lastOutput);
                }
            }

            return lastOutput.ToString();
        }

        public override string Part2()
        {
            LCVM vm = new LCVM(codeList.ToArray());

            string[] instructions = new string[]
            {
                "NOT A J",
                "NOT B T",
                "OR T J",
                "NOT C T",
                "OR T J",
                "AND D J",

                "NOT D T",
                "OR E T",
                "OR H T",
                "AND T J",
                "RUN"
            };

            foreach (string s in instructions)
            {
                var chars = s.ToCharArray();
                foreach (var c in chars)
                    vm.AddInput(c);

                vm.AddInput('\n');
            }

            long lastOutput = 0;
            var result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                {
                    lastOutput = vm.GetOutput();
                    if (lastOutput < 256)
                        Console.Write((char)lastOutput);
                }
            }

            return lastOutput.ToString();
        }
    }
}
