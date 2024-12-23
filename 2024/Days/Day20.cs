using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode
{
    class Day20 : Day
    {
        string[] lines = File.ReadAllLines("Input/20.txt");
        Dictionary<string, List<string>> links = new();
        List<List<string>> parties = new();

        public override object Part1()
        {
            foreach (string line in lines)
            {
                string[] a = line.Split("-");

                for (int i = 0; i < 2; i++)
                    if (links.ContainsKey(a[i]))
                        links[a[i]].Add(a[1 - i]);
                    else
                        links.Add(a[i], new() { a[1 - i] });
            }

            HashSet<string> matches = new();

            foreach (var kv in links)
            {
                string first = kv.Key;
                foreach (string second in kv.Value)
                    foreach (string third in links[second])
                        if (kv.Value.Contains(third))
                        {
                            List<string> temp = new() { kv.Key, second, third };
                            temp.Sort();

                            if (kv.Key.StartsWith("t") || second.StartsWith("t") || third.StartsWith("t"))
                                matches.Add(string.Join(",", temp));

                            parties.Add(temp);
                        }
            }

            return matches.Count;
        }

        public override object Part2()
        {
            foreach (string pc in links.Keys)
                Find(pc);

            int maxIndex = -1;
            int maxLength = 0;
            for (int i = 0; i < parties.Count; i++)
                if (parties[i].Count > maxLength)
                {
                    maxIndex = i;
                    maxLength = parties[i].Count;
                }

            parties[maxIndex].Sort();
            return string.Join(",", parties[maxIndex]);
        }

        public void Find(string pc)
        {
            foreach (List<string> party in parties)
                if (party.All(other => links[pc].Contains(other)))
                    party.Add(pc);
        }
    }
}
