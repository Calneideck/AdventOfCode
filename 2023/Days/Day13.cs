using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{

    class Day13 : Day
    {
        string[] lines = File.ReadAllText("Input/13.txt").Split("\n\n");

        public override object Part1()
        {
            long count = 0;

            var groups = lines.Select(a => a.Split("\n"));
            foreach (string[] group in groups)
            {
                for (int y = 1; y < group.Length; y++)
                {
                    if (group[y] == group[y - 1])
                    {
                        bool match = true;

                        for (int i = 1; i < y; i++)
                            if (
                                y + i < group.Length &&
                                y - 1 - i >= 0 &&
                                group[y + i] != group[y - 1 - i]
                            )
                            {
                                match = false;
                                break;
                            }

                        if (match)
                            count += y * 100;
                    }
                }

                for (int x = 1; x < group[0].Length; x++)
                {
                    if (
                        string.Join("", group.Select(g => g[x])) ==
                        string.Join("", group.Select(g => g[x - 1]))
                    )
                    {
                        bool match = true;

                        for (int i = 1; i < x; i++)
                            if (
                                x + i < group[0].Length &&
                                x - 1 - i >= 0 &&
                                string.Join("", group.Select(g => g[x + i])) !=
                                string.Join("", group.Select(g => g[x - 1 - i]))
                            )
                            {
                                match = false;
                                break;
                            }

                        if (match)
                            count += x;
                    }
                }
            }

            return count;
        }


        public override object Part2()
        {
            long count = 0;

            return count;
        }
    }
}
