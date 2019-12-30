using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day25 : Day
    {
        private IEnumerable<long> codeList;
        private string[] items = {
            "mouse",
            "mutex",
            "pointer",
            "monolith",
            "asterisk",
            "food ration",
            "space law space brochure"
        };

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/25.txt").Split(',').Select(s => long.Parse(s));
            LCVM vm = new LCVM(codeList.ToArray());

            var result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                {
                    long o = vm.GetOutput();
                    Console.Write((char)o);
                }
                else if (result == LCVM.StopCode.Input)
                {
                    var input = Console.ReadLine();
                    if (input == "collect")
                    {
                        GetToSecurityPanel(vm);
                    }
                    else if (input == "hack")
                    {
                        for (int i = 0; i <= 127; i++)
                        {
                            DropAll(vm);

                            string command = "";
                            for (int j = 0; j < 7; j++)
                                if ((i & (1 << j)) != 0)
                                {
                                    command = "take " + items[j] + '\n';
                                    vm.AddInputs(command.ToCharArray().Select(c => (long)c));
                                    while (vm.GetCodeResult() == LCVM.StopCode.Output) { }
                                    vm.GetTextOutput();
                                }
                            
                            command = "east\n";
                            vm.AddInputs(command.ToCharArray().Select(c => (long)c));
                            while (vm.GetCodeResult() == LCVM.StopCode.Output) { }

                            string text = vm.GetTextOutput();
                            if (!text.Contains("eject"))
                                Console.WriteLine(text);
                        }

                        Console.WriteLine("Finished Hacking!: ");
                    }
                    else
                    {
                        input += '\n';
                        vm.AddInputs(input.ToCharArray().Select(c => (long)c));
                    }
                }
            }


            return base.Part1();

        }

        void GetToSecurityPanel(LCVM vm)
        {
            string[] commands =
            {
                "north",
                "take mouse",
                "north",
                "take pointer",
                "south",
                "south",
                "west",
                "take monolith",
                "north",
                "west",
                "take food ration",
                "south",
                "take space law space brochure",
                "north",
                "east",
                "south",
                "south",
                "south",
                "west",
                "take asterisk",
                "south",
                "take mutex",
                "north",
                "east",
                "north",
                "north",
                "east",
                "south",
                "south",
                "west",
                "south",
            };

            foreach (string command in commands)
            {
                vm.AddInputs(command.ToCharArray().Select(c => (long)c).Append('\n'));
                while (vm.GetCodeResult() != LCVM.StopCode.Input) { }
                string text = vm.GetTextOutput();
                Console.WriteLine(text);
            }
        }

        void DropAll(LCVM vm)
        {
            foreach (var item in items)
            {
                var command = "drop " + item + '\n';
                vm.AddInputs(command.ToCharArray().Select(c => (long)c));
                while (vm.GetCodeResult() != LCVM.StopCode.Input) { }
                string text = vm.GetTextOutput();
                //Console.WriteLine(text);
            }
        }
    }
}
