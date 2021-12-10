using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day9 : Day
    {
        string[] lines = File.ReadAllLines("Input/9.txt");
        Dictionary<V2, int> nums = new();

        public override object Part1()
        {
            int count = 0;
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    nums.Add(new V2(x, y), int.Parse(lines[y][x].ToString()));

            foreach (var kv in nums)
            {
                bool low = true;
                foreach (var dir in V2.Directions)
                    if (nums.TryGetValue(kv.Key + dir, out int height))
                        if (height <= kv.Value)
                        {
                            low = false;
                            break;
                        }

                if (low)
                    count += kv.Value + 1;
            }

            return count;
        }


        public override object Part2()
        {
            List<V2> open = nums.Where(x => x.Value < 9).Select(x => x.Key).ToList();
            HashSet<V2> closed = nums.Where(x => x.Value == 9).Select(x => x.Key).ToHashSet();
            List<int> bCounts = new();

            while (closed.Count != nums.Count)
            {
                var next = open.First(x => !closed.Contains(x));
                int basinCount = 1;
                closed.Add(next);

                Queue<V2> n = new();
                n.Enqueue(next);

                while (n.Count > 0)
                {
                    next = n.Dequeue();
                    foreach (var dir in V2.Directions)
                        if (nums.ContainsKey(next + dir) && !closed.Contains(next + dir))
                        {
                            n.Enqueue(next + dir);
                            closed.Add(next + dir);
                            basinCount++;
                        }
                }

                bCounts.Add(basinCount);
            }

            bCounts.Sort();
            return bCounts.TakeLast(3).Aggregate((sum, x) => sum * x);
        }
    }
}
