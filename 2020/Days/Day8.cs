using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    class Day8 : Day
    {
        string[] lines = File.ReadAllLines("Input/8.txt");

        public override object Part1()
        {
            int i = 0;
            int jmp = 0;
            int acc = 0;
            HashSet<int> prev = new HashSet<int>();

            while (true)
            {
                if (prev.Contains(jmp))
                    return acc;
                else
                    prev.Add(jmp);

                string line = lines[jmp];
                var inp = line.Split(' ');
                if (inp[0] == "nop")
                    jmp++;
                else if (inp[0] == "acc")
                {
                    acc += int.Parse(inp[1]);
                    jmp++;
                }
                else if (inp[0] == "jmp")
                    jmp += int.Parse(inp[1]);

                if (++i == int.MaxValue)
                    return "ERR";
            }
        }

        int Run(string[] lines)
        {
            int i = 0;
            int jmp = 0;
            int acc = 0;
            HashSet<int> prev = new HashSet<int>();

            while (true)
            {
                if (prev.Contains(jmp))
                    return -1;
                else
                    prev.Add(jmp);

                if (jmp == lines.Length)
                    return acc;
                else if (jmp > lines.Length)
                    return -1;

                string line = lines[jmp];
                var inp = line.Split(' ');
                if (inp[0] == "nop")
                    jmp++;
                else if (inp[0] == "acc")
                {
                    acc += int.Parse(inp[1]);
                    jmp++;
                }
                else if (inp[0] == "jmp")
                    jmp += int.Parse(inp[1]);

                if (++i == int.MaxValue)
                    return -1;
            }
        }

        public override object Part2()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                var newLines = new List<string>(lines);

                if (newLines[i].Contains("nop"))
                    newLines[i] = newLines[i].Replace("nop", "jmp");
                else if (newLines[i].Contains("jmp"))
                    newLines[i] = newLines[i].Replace("jmp", "nop");
                else
                    continue;

                int run = Run(newLines.ToArray());
                if (run >= 0) return run;
            }

            return -1;
        }
    }
}
