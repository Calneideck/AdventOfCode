using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day19 : Day
    {
        string[] lines = File.ReadAllLines("Input/19.txt").ToArray();
        Dictionary<int, List<List<int>>> rules = new Dictionary<int, List<List<int>>>();
        Regex numReg = new Regex(@"[\d|]+");

        bool Match(string line, IEnumerable<int> seq)
        {
            if (line.Length == 0 || seq.Count() == 0)
                return line.Length == 0 && seq.Count() == 0;

            var ruleSet = rules[seq.First()];
            if (ruleSet.Count == 1)
            {
                var rule = ruleSet.First();
                if (rule[0] == -1)
                    return line[0] == 'a' && Match(line[1..], seq.Skip(1));
                else if (rule[0] == -2)
                    return line[0] == 'b' && Match(line[1..], seq.Skip(1));
                else
                {
                    var next = new List<int>(seq.Skip(1));
                    next.InsertRange(0, rule);
                    return Match(line, next);
                }
            }
            else
                return ruleSet.Any(r =>
                {
                    var next = new List<int>(seq.Skip(1));
                    next.InsertRange(0, r);
                    return Match(line, next);
                });
        }

        void GetRules(string line)
        {
            var matches = numReg.Matches(line);
            int ruleKey = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                if (int.TryParse(matches[i].ToString(), out int num))
                {
                    if (i == 0)
                    {
                        rules.Add(num, new List<List<int>>() { new List<int>() });
                        ruleKey = num;

                        if (line.Contains("a"))
                            rules[num].Last().Add(-1);
                        else if (line.Contains("b"))
                            rules[num].Last().Add(-2);
                    }
                    else
                        rules[ruleKey].Last().Add(num);
                }
                else
                    rules[ruleKey].Add(new List<int>());
            }
        }

        public override object Part1()
        {
            bool readingRules = true;
            int matches = 0;

            foreach (var line in lines)
                if (readingRules)
                {
                    if (line.Length == 0)
                        readingRules = false;
                    else
                        GetRules(line);
                }
                else if (Match(line, new int[] { 0 }))
                    matches++;

            return matches;
        }

        public override object Part2()
        {
            rules.Clear();

            for (int i = 0; i < lines.Length; i++)
                if (lines[i] == "8: 42")
                    lines[i] = "8: 42 | 42 8";
                else if (lines[i] == "11: 42 31")
                    lines[i] = "11: 42 31 | 42 11 31";

            bool readingRules = true;
            int matches = 0;

            foreach (var line in lines)
                if (readingRules)
                {
                    if (line.Length == 0)
                        readingRules = false;
                    else
                        GetRules(line);
                }
                else if (Match(line, new int[] { 0 }))
                    matches++;

            return matches;
        }
    }
}
