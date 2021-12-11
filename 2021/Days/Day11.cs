using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day11 : Day
    {
        string[] lines = File.ReadAllLines("Input/11.txt");

        public override object Part1()
        {
            int count = 0;
            Dictionary<V2, int> nums = new();

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    nums.Add(new V2(x, y), int.Parse(lines[y][x].ToString()));

            for (int i = 0; i < 100; i++)
            {
                foreach (var item in nums.Keys)
                    nums[item]++;

                HashSet<V2> flashed = new(nums.Where(x => x.Value > 9).Select(x => x.Key));
                Queue<V2> toFlash = new(nums.Where(x => x.Value > 9).Select(x => x.Key));
                count += toFlash.Count;

                while (toFlash.Count > 0)
                {
                    var item = toFlash.Dequeue();
                    nums[item] = 0;

                    foreach (var dir in V2.AllDirections)
                        if (nums.TryGetValue(item + dir, out var _))
                            if (!flashed.Contains(item + dir) && ++nums[item + dir] > 9)
                            {
                                toFlash.Enqueue(item + dir);
                                flashed.Add(item + dir);
                                count++;
                            }
                }

            }

            return count;
        }


        public override object Part2()
        {
            Dictionary<V2, int> nums = new();

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    nums.Add(new V2(x, y), int.Parse(lines[y][x].ToString()));

            int i = 0;
            while (true)
            {
                foreach (var item in nums.Keys)
                    nums[item]++;

                HashSet<V2> flashed = new(nums.Where(x => x.Value > 9).Select(x => x.Key));
                Queue<V2> toFlash = new(nums.Where(x => x.Value > 9).Select(x => x.Key));

                while (toFlash.Count > 0)
                {
                    var item = toFlash.Dequeue();

                    nums[item] = 0;
                    flashed.Add(item);

                    foreach (var dir in V2.AllDirections)
                        if (nums.TryGetValue(item + dir, out var _))
                            if (!flashed.Contains(item + dir) && ++nums[item + dir] > 9)
                            {
                                toFlash.Enqueue(item + dir);
                                flashed.Add(item + dir);
                            }
                }

                i++;
                if (flashed.Count == 100)
                    return i;
            }
        }
    }
}
