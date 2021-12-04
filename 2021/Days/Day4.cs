using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day4 : Day
    {
        string[] lines = File.ReadAllLines("Input/4.txt");
        int[] inps;
        List<int[,]> boards;

        public override object Part1()
        {
            inps = lines[0].Split(',').Select(int.Parse).ToArray();
            boards = new List<int[,]>();

            for (int i = 2; i < lines.Length; i += 6)
            {
                int[,] b = new int[5, 5];

                for (int y = 0; y < 5; y++)
                {
                    var ln = new Regex(@"(\d+)").Matches(lines[i + y]);
                    for (int x = 0; x < 5; x++)
                        b[x, y] = int.Parse(ln[x].Value);
                }

                boards.Add(b);
            }

            for (int i = 0; i < inps.Length; i++)
            {
                var nums = inps.Take(i + 1);
                foreach (var b in boards)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        bool win = true;

                        for (int x = 0; x < 5; x++)
                            if (!nums.Contains(b[x, y]))
                            {
                                win = false;
                                break;
                            }

                        if (win)
                            return Win(b, nums.ToArray()) * nums.Last();
                    }

                    for (int x = 0; x < 5; x++)
                    {
                        bool win = true;

                        for (int y = 0; y < 5; y++)
                            if (!nums.Contains(b[x, y]))
                            {
                                win = false;
                                break;
                            }

                        if (win)
                            return Win(b, nums.ToArray()) * nums.Last();
                    }
                }
            }

            return "";
        }

        int Win(int[,] b, int[] nums)
        {
            int sum = 0;
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                    if (!nums.Contains(b[x, y]))
                        sum += b[x, y];

            return sum;
        }

        public override object Part2()
        {
            HashSet<int[,]> winners = new HashSet<int[,]>();

            for (int i = 0; i < inps.Length; i++)
            {
                var nums = inps.Take(i + 1);
                foreach (var b in boards)
                {
                    if (winners.Contains(b))
                        continue;
                    
                    for (int y = 0; y < 5; y++)
                    {
                        bool win = true;

                        for (int x = 0; x < 5; x++)
                            if (!nums.Contains(b[x, y]))
                            {
                                win = false;
                                break;
                            }

                        if (win)
                        {
                            winners.Add(b);
                            if (winners.Count == boards.Count)
                                return Win(b, nums.ToArray()) * nums.Last();

                            break;
                        }
                    }

                    for (int x = 0; x < 5; x++)
                    {
                        bool win = true;

                        for (int y = 0; y < 5; y++)
                            if (!nums.Contains(b[x, y]))
                            {
                                win = false;
                                break;
                            }

                        if (win)
                        {
                            winners.Add(b);
                            if (winners.Count == boards.Count)
                                return Win(b, nums.ToArray()) * nums.Last();

                            break;
                        }
                    }
                }
            }

            return "";
        }
    }
}
