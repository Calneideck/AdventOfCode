using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day19 : Day
    {
        string[] lines = File.ReadAllLines("Input/19.txt");
        List<V3[]> scanners = new();

        public override object Part1()
        {
            List<V3> beacons = new();
            foreach (var line in lines)
                if (line.Contains("scanner"))
                    beacons = new();
                else if (line.Length == 0)
                    scanners.Add(beacons.ToArray());
                else
                {
                    int[] temp = line.Split(",").Select(int.Parse).ToArray();
                    beacons.Add(new V3(temp[0], temp[1], temp[2]));
                }

            scanners.Add(beacons.ToArray());
            Dictionary<V3, bool> map = new();

            foreach (var b in scanners[0])
                map.Add(b, true);

            scanners.RemoveAt(0);

            List<V3> mapRels = new();
            foreach (var v1 in map.Keys)
                foreach (var v2 in map.Keys)
                    if (v1 != v2)
                        mapRels.Add(v2 - v1);

            foreach (var scanner in scanners)
            {
                List<V3> rels = new();
                foreach (var v1 in scanner)
                    foreach (var v2 in scanner)
                        if (v1 != v2)
                            rels.Add(v2 - v1);

                var matches = mapRels.Intersect(rels);
            }


            return map.Count;
        }

        public override object Part2()
        {
            return "";
        }
    }
}
