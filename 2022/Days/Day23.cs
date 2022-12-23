using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day23 : Day
    {
        string[] lines = File.ReadAllLines("Input/23.txt");

        V2[] GetDirs(int index)
        {
            switch (index)
            {
                case 0: return new V2[] { V2.UpLeft, V2.Up, V2.UpRight };
                case 1: return new V2[] { V2.DownLeft, V2.Down, V2.DownRight };
                case 2: return new V2[] { V2.UpLeft, V2.Left, V2.DownLeft };
                case 3: return new V2[] { V2.UpRight, V2.Right, V2.DownRight };
            }
            return null;
        }

        int Simulate(bool part2)
        {
            V2[] dirs = new V2[] { V2.Up, V2.Down, V2.Left, V2.Right };

            HashSet<V2> elves = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    if (lines[y][x] == '#')
                        elves.Add(new V2(x, y));

            int dirIndex = 0;
            int roundCount = part2 ? int.MaxValue : 10;
            for (int i = 0; i < roundCount; i++)
            {
                HashSet<V2> newElves = new();
                List<(V2, V2)> transforms = new();
                bool anyMoved = false;

                foreach (V2 elf in elves)
                {
                    bool moved = false;
                    if (V2.AllDirections.Any(d => elves.Contains(elf + d)))
                        for (int j = 0; j < 4; j++)
                        {
                            int thisIndex = (dirIndex + j) % 4;
                            if (GetDirs(thisIndex).All(d => !elves.Contains(elf + d)))
                            {
                                transforms.Add((elf, elf + dirs[thisIndex]));
                                moved = true;
                                anyMoved = true;
                                break;
                            }
                        }

                    if (!moved)
                        transforms.Add((elf, elf));
                }

                foreach (var t in transforms)
                    if (transforms.Count(kv => kv.Item2 == t.Item2) > 1)
                        newElves.Add(t.Item1);
                    else
                        newElves.Add(t.Item2);

                dirIndex++;
                elves = newElves;

                if (!anyMoved && part2)
                    return i + 1;
            }

            int minX = elves.Min(e => e.x);
            int minY = elves.Min(e => e.y);
            int maxX = elves.Max(e => e.x);
            int maxY = elves.Max(e => e.y);

            return (maxX - minX + 1) * (maxY - minY + 1) - elves.Count;
        }

        public override object Part1()
        {
            return Simulate(false);
        }

        public override object Part2()
        {
            return Simulate(true);
        }
    }
}
