using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Module
    {
        public char type;
        public string inp;
        public string[] dest;

        public bool lastSignal;
        public Dictionary<string, bool> lastSignals = new();

        public bool? Run(bool signal, string input)
        {
            if (type == '%')
            {
                if (signal)
                    return null;
                else
                {
                    lastSignal = !lastSignal;
                    return lastSignal;
                }
            }
            else if (type == '&')
            {
                // if (!lastSignals.ContainsKey(input))
                //     lastSignals.Add(input, signal);
                // else
                lastSignals[input] = signal;

                return !lastSignals.Values.All(s => s);
            }
            else
                return signal;
        }
    }

    class Day20 : Day
    {
        string[] lines = File.ReadAllLines("Input/20.txt");

        public override object Part1()
        {
            long low = 0;
            long high = 0;

            Dictionary<string, Module> map = new();


            foreach (string line in lines)
            {
                char type = '\0';
                var temp = line.Split(" -> ");
                var dests = temp[1].Split(", ");
                string input = temp[0];
                if (input[0] == '%' || input[0] == '&')
                {
                    type = input[0];
                    input = temp[0][1..];
                }

                map.Add(input, new Module()
                {
                    type = type,
                    inp = input,
                    dest = dests
                });
            }

            foreach (var module in map.Values.Where(m => m.type == '&'))
                foreach (var mod2 in map.Values.Where(m2 => m2.dest.Contains(module.inp)))
                    module.lastSignals.Add(mod2.inp, false);

            for (int i = 0; i < int.MaxValue; i++)
            {
                bool signal = false;

                Queue<(string input, bool signal, Module m)> signals = new();

                signals.Enqueue(("broadcaster", signal, map["broadcaster"]));

                while (signals.Count > 0)
                {
                    var ss = signals.Dequeue();
                    //if (ss.signal) high++;
                    //else low++;

                    if (!ss.signal && ss.m.inp == "rx") return i;

                    bool? result = ss.m.Run(ss.signal, ss.input);
                    if (result != null)
                        foreach (var dest in ss.m.dest)
                            if (map.ContainsKey(dest))
                                signals.Enqueue((ss.m.inp, (bool)result, map[dest]));
                            // else if (result == true) high++;
                            // else if (result == false) low++;

                }
            }

            return low * high;
        }


        public override object Part2()
        {
            return 0;
        }
    }
}
