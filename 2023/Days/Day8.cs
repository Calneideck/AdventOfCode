using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day8 : Day
    {
        string[] lines = File.ReadAllLines("Input/8.txt");
        Dictionary<string, (string, string)> map = new();

        public override object Part1()
        {
            long count = 0;
            string dirs = lines[0];

            for (int i = 2; i < lines.Length; i++)
                map.Add(lines[i][0..3], (lines[i][7..10], lines[i][12..15]));

            string node = "AAA";
            int x = 0;

            while (node != "ZZZ")
            {
                bool left = dirs[x++ % dirs.Length] == 'L';
                node = left ? map[node].Item1 : map[node].Item2;
                count++;
            }

            return count;
        }

        public override object Part2()
        {
            string line = lines[0];
            string[] starts = map.Keys.Where(a => a.EndsWith('A')).ToArray();

            long[] counts = starts.Select(node => new Path(node, line, map).FindZ()).ToArray();

            return MyExtensions.LCM(counts);
        }

        class Path
        {
            string node;
            string dirs;
            Dictionary<string, (string, string)> map;
            int x = 0;
            long count = 0;

            public Path(string startNode, string dirs, Dictionary<string, (string, string)> map)
            {
                node = startNode;
                this.dirs = dirs;
                this.map = map;
            }

            public long FindZ()
            {
                while (!node.EndsWith('Z'))
                {
                    bool left = dirs[x++ % dirs.Length] == 'L';
                    node = left ? map[node].Item1 : map[node].Item2;
                    count++;
                }

                return count;
            }
        }
    }
}
