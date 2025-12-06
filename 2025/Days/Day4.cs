using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Day4 : Day
    {
        string[] lines = File.ReadAllLines("Input/4.txt");

        public override object Part1()
        {
            long count = 0;

            Dictionary<V2, char> map = lines.GetMap();

            foreach (KeyValuePair<V2, char> kv in map)
                if (kv.Value == '@')
                {
                    int num = V2.AllDirections.Count(
                        (dir) => map.TryGetValue(kv.Key + dir, out char c) && c == '@'
                    );

                    if (num < 4) count++;
                }

            return count;
        }

        public override object Part2()
        {
            long count = 0;

            Dictionary<V2, char> map = lines.GetMap();
            bool removed;

            do
            {
                removed = false;

                foreach (KeyValuePair<V2, char> kv in map)
                    if (kv.Value == '@')
                    {
                        int num = V2.AllDirections.Count(
                            (dir) => map.TryGetValue(kv.Key + dir, out char c) && c == '@'
                        );

                        if (num < 4)
                        {
                            count++;
                            removed = true;
                            map[kv.Key] = '.';
                        }
                    }
            } while (removed);

            return count;
        }
    }
}
