using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day24 : Day
    {
        string[] lines = File.ReadAllLines("Input/24.txt");

        class Gate
        {
            public string[] inputs;
            public int op;
            public string output;
        }

        public override object Part1()
        {
            long sum = 0;
            bool top = true;
            Dictionary<string, bool> map = new();
            List<Gate> gates = new();
            HashSet<Gate> seen = new();

            foreach (string s in lines)
                if (s.Length == 0)
                    top = false;
                else if (top)
                {
                    string[] a = s.Split(": ");
                    map.Add(a[0], a[1] == "1");
                }
                else
                {
                    string[] a = s.Split(" ");
                    string[] inps = new string[] { a[0], a[2] };
                    int op = 0;
                    if (a[1] == "OR") op = 1;
                    else if (a[1] == "XOR") op = 2;

                    gates.Add(new Gate() { inputs = inps, op = op, output = a[4] });
                }

            while (seen.Count != gates.Count)
                foreach (Gate g in gates)
                    if (
                        !seen.Contains(g) &&
                        map.TryGetValue(g.inputs[0], out bool left) &&
                        map.TryGetValue(g.inputs[1], out bool right)
                    )
                    {
                        if (g.op == 0)
                            map[g.output] = left & right;
                        else if (g.op == 1)
                            map[g.output] = left | right;
                        else if (g.op == 2)
                            map[g.output] = left ^ right;

                        seen.Add(g);
                    }

            foreach (var kv in map.Where(kv => kv.Key.StartsWith("z") && kv.Value))
            {
                int i = int.Parse(kv.Key[1..]);
                sum += (long)Math.Pow(2, i);
            }

            return sum;
        }

        public override object Part2()
        {
            int sum = 0;


            return sum;
        }
    }
}
