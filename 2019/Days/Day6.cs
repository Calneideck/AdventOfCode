using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day6 : Day
    {
        private Dictionary<string, string> orbits = new Dictionary<string, string>();

        public override int Part1()
        {
            string[] lines = File.ReadAllLines("Input/6.txt");

            foreach (var line in lines)
            {
                if (line.StartsWith('#'))
                    break;

                var parts = line.Split(')');
                orbits.Add(parts[1], parts[0]);
            }

            int orbitCount = 0;
            foreach (KeyValuePair<string, string> item in orbits)
            {
                string target = item.Value;
                orbitCount++;
                while (target != "COM")
                {
                    target = orbits[target];
                    orbitCount++;
                }
            }

            return orbitCount;
        }

        public override int Part2()
        {
            List<string> me = new List<string>();
            List<string> san = new List<string>();

            string target = "YOU";
            while (target != "COM")
            {
                target = orbits[target];
                me.Add(target);
            }

            target = "SAN";
            while (target != "COM")
            {
                target = orbits[target];
                san.Add(target);
            }

            List<string> both = me.Intersect(san).ToList();
            int count = me.FindIndex(s => s == both[0]) + san.FindIndex(s => s == both[0]);

            return count;
        }
    }
}
