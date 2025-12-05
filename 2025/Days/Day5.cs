using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Day5 : Day
    {
        string lines = File.ReadAllText("Input/5.txt");

        public override object Part1()
        {
            long count = 0;

            var blocks = lines.Split("\n\n");

            (long, long)[] fresh = blocks[0].Split('\n').Select(t =>
            {
                long[] nums = t.Split('-').Select(long.Parse).ToArray();
                return (nums[0], nums[1]);
            }).ToArray();

            long[] ings = blocks[1].Split('\n').Select(a => long.Parse(a)).ToArray();

            foreach (long i in ings)
                if (fresh.Any(f => i >= f.Item1 && i <= f.Item2))
                    count++;

            return count;
        }

        public override object Part2()
        {
            var blocks = lines.Split("\n\n");

            (long, long)[] fresh = blocks[0].Split('\n').Select(t =>
            {
                long[] nums = t.Split('-').Select(long.Parse).ToArray();
                return (nums[0], nums[1]);
            }).ToArray();

            List<(long, long)> values = [(fresh[0].Item1, fresh[0].Item2)];

            for (int i = 1; i < fresh.Length; i++)
                Test(values, fresh[i]);

            return values.Aggregate((long)0, (sum, val) => val.Item2 - val.Item1 + 1 + sum);
        }

        static void Test(List<(long, long)> values, (long, long) range)
        {
            foreach (var val in values)
            {
                long l = range.Item1, r = range.Item2;

                if (range.Item1 >= val.Item1 && range.Item1 <= val.Item2)
                    l = val.Item2 + 1;

                if (range.Item2 >= val.Item1 && range.Item2 <= val.Item2)
                    r = val.Item1 - 1;

                if (range.Item1 >= val.Item1 && range.Item2 <= val.Item2)
                    return; // wholly inside

                if (range.Item1 < val.Item1 && range.Item2 > val.Item2)
                {
                    // Completely covers existing val
                    Test(values, (range.Item1, val.Item1 - 1));
                    Test(values, (val.Item2 + 1, range.Item2));
                    return;
                }

                if (r >= l && l != range.Item1 || r != range.Item2)
                {
                    Test(values, (l, r));
                    return;
                }
            }

            values.Add(range);
        }
    }
}
