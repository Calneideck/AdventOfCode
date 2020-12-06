using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day6 : Day
    {
        string[] lines = File.ReadAllLines("Input/6.txt");

        public override object Part1()
        {
            HashSet<char> letters = new HashSet<char>();
            int count = 0;

            foreach (var ln in lines)
                if (ln.Length == 0)
                {
                    count += letters.Distinct().Count();
                    letters.Clear();
                }
                else
                    foreach (var lt in ln)
                        letters.Add(lt);

            return count;
        }

        public override object Part2()
        {
            Dictionary<char, int> letters = new Dictionary<char, int>();
            int count = 0;
            int peeps = 0;

            foreach (var ln in lines)
                if (ln.Length == 0)
                {
                    count += letters.Count((KeyValuePair<char, int> kv) => kv.Value == peeps);
                    peeps = 0;
                    letters.Clear();
                }
                else
                {
                    foreach (var lt in ln)
                        if (!letters.ContainsKey(lt))
                            letters.Add(lt, 1);
                        else
                            letters[lt]++;

                    peeps++;
                }

            return count;
        }
    }
}
