using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Rule
    {
        public int inp;
        public string op;
        public int cmp;
        public string dest;

        public static string[] inputs = new string[] { "x", "m", "a", "s" };

        public Rule(string inp, string op, string cmp, string dest)
        {
            this.inp = Array.FindIndex(inputs, i => i == inp);

            this.op = op;
            this.cmp = cmp.Length > 0 ? int.Parse(cmp) : 0;
            this.dest = dest;
        }

        public string Run(int[] values)
        {
            if (inp == -1) return dest;

            int val = values[inp];

            if (op == ">")
            {
                if (val > cmp) return dest;
            }
            else if (op == "<")
            {
                if (val < cmp) return dest;
            }

            return null;
        }
    }

    class Day19 : Day
    {
        string[] lines = File.ReadAllText("Input/19.txt").Split("\n\n");

        public override object Part1()
        {
            string[] rules = lines[0].Split("\n");
            string[] parts = lines[1].Split("\n");

            Dictionary<string, Rule[]> ruleMap = new();

            long count = 0;

            foreach (string line in rules)
            {
                string[] t1 = line.Split("{");
                string name = t1[0];
                string[] ruleStrings = t1[1][0..^1].Split(",");

                ruleMap.Add(name, new Rule[ruleStrings.Length]);

                int i = 0;
                foreach (string rule in ruleStrings)
                {
                    string[] t2 = rule.Split(":");
                    if (t2.Length == 1)
                        ruleMap[name][i++] = new Rule("", "", "", rule);
                    else
                    {
                        char inp = t2[0][0];
                        char cmp = t2[0][1];
                        string val = t2[0][2..];
                        ruleMap[name][i++] = new Rule(inp.ToString(), cmp.ToString(), val, t2[1]);
                    }
                }
            }

            foreach (string p in parts)
            {
                int[] nums = p.GrabInts();

                string node = "in";

                while (node != "A" && node != "R")
                {
                    Rule[] nextRuleset = ruleMap[node];

                    for (int i = 0; i < nextRuleset.Length; i++)
                    {
                        string result = nextRuleset[i].Run(nums);
                        if (result != null)
                        {
                            node = result;
                            break;
                        }
                    }
                }

                if (node == "A")
                    count += nums.Sum();
            }

            return count;
        }


        public override object Part2()
        {
            return 0;
        }
    }
}
