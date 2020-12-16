using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day16 : Day
    {
        string[] lines = File.ReadAllLines("Input/16.txt").ToArray();

        Dictionary<string, List<V2>> rules = new Dictionary<string, List<V2>>();
        List<int[]> validTickets = new List<int[]>();
        int[] myTicket = new int[0];

        bool validForRule(int val, string ruleName)
        {
            var rule = rules[ruleName];

            bool valid = false;
            foreach (var edge in rule)
                if (val >= edge.x && val <= edge.y)
                {
                    valid = true;
                    break;
                }

            return valid;
        }

        public override object Part1()
        {
            Regex nReg = new Regex(@"\d+");

            bool readingRules = true;
            bool readingMine = false;
            bool readingOthers = false;

            int invalid = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (readingOthers)
                {
                    var ticket = lines[i].Split(',').Select(int.Parse).ToArray();
                    bool validTicket = true;

                    foreach (var val in ticket)
                    {
                        bool valid = false;
                        foreach (var rule in rules)
                            if (valid = validForRule(val, rule.Key))
                                break;

                        if (!valid)
                        {
                            invalid += val;
                            validTicket = false;
                        }
                    }

                    if (validTicket)
                        validTickets.Add(ticket);
                }

                if (readingRules)
                    if (lines[i].Length == 0)
                    {
                        readingRules = false;
                        readingMine = true;
                    }
                    else
                    {
                        string rName = lines[i].Split(':')[0];
                        var nums = nReg.Matches(lines[i]);

                        int num = 0;
                        for (int j = 0; j < nums.Count; j++)
                            if (j % 2 == 0)
                                num = int.Parse(nums[j].ToString());
                            else
                            {
                                rules.TryAdd(rName, new List<V2>());
                                rules[rName].Add(new V2(num, int.Parse(nums[j].ToString())));
                            }
                    }

                if (readingMine)
                    if (lines[i].StartsWith("your") || lines[i].Length == 0)
                        continue;
                    else if (lines[i].StartsWith("nearby"))
                    {
                        readingMine = false;
                        readingOthers = true;
                    }
                    else
                        myTicket = lines[i].Split(',').Select(int.Parse).ToArray();
            }

            return invalid;
        }

        public override object Part2()
        {
            List<List<string>> goodRules = new List<List<string>>();

            for (int i = 0; i < myTicket.Length; i++)
            {
                var values = validTickets.Select((ticket) => ticket[i]).ToList();
                values.Add(myTicket[i]);
                goodRules.Add(new List<string>());

                foreach (var ruleName in rules.Keys)
                {
                    bool goodRule = true;

                    foreach (var val in values)
                        if (!validForRule(val, ruleName))
                        {
                            goodRule = false;
                            break;
                        }

                    if (goodRule)
                        goodRules[i].Add(ruleName);
                }
            }

            Dictionary<string, int> ruleSlots = new Dictionary<string, int>();

            for (int i = 0; i < goodRules.Count; i++)
            {
                int nextIndex = goodRules.FindIndex(r => r.Count == 1);
                string ruleName = goodRules[nextIndex][0];
                ruleSlots.Add(ruleName, nextIndex);

                foreach (var goodRule in goodRules)
                    goodRule.Remove(ruleName);
            }

            long result = 1;
            var deps = rules.Where((kv) => kv.Key.StartsWith("dep")).ToList();
            for (int i = 0; i < deps.Count; i++)
            {
                int colIndex = ruleSlots[deps[i].Key];
                result *= myTicket[colIndex];
            }

            return result;
        }
    }
}
