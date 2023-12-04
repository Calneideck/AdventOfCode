using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day4 : Day
    {
        string[] lines = File.ReadAllLines("Input/4.txt");

        public override object Part1()
        {
            int sum = 0;

            foreach (string line in lines)
            {
                string[] parts = line.Split(": ");
                string[] cards = parts[1].Split(" | ");
                string[] winning = cards[0].Split(" ");
                string[] myCards = cards[1].Split(" ");

                sum += (int)Math.Pow(
                    2,
                    myCards
                        .Intersect(winning.Where(a => a.Length > 0))
                        .Count() - 1
                );
            }

            return sum;
        }

        public override object Part2()
        {
            Dictionary<int, int> map = new(
                lines.Select((_, i) => new KeyValuePair<int, int>(i, 0))
            );

            for (int i = 0; i < lines.Length; i++)
            {
                map[i]++;
                string[] parts = lines[i].Split(": ");
                string[] cards = parts[1].Split(" | ");
                string[] winning = cards[0].Split(" ");
                string[] myCards = cards[1].Split(" ");

                int wins = myCards.Intersect(winning.Where(a => a.Length > 0)).Count();
                for (int j = 1; j <= wins; j++)
                    map[i + j] += map[i];
            }

            return map.Values.Sum();
        }

        // Original Part 2
        // public override object Part2()
        // {
        //     int sum = 0;
        //     Queue<(int, string)> q = new(lines.Select((l, i) => (i, l)));

        //     while (q.Count > 0)
        //     {
        //         sum++;
        //         (int, string) line = q.Dequeue();
        //         string[] parts = line.Item2.Split(": ");
        //         string[] cards = parts[1].Split(" | ");
        //         string[] winning = cards[0].Split(" ");
        //         string[] myCards = cards[1].Split(" ");

        //         int wins = myCards.Intersect(winning.Where(a => a.Length > 0)).Count();
        //         for (int i = 0; i < wins; i++)
        //             q.Enqueue((line.Item1 + i + 1, lines[line.Item1 + i + 1]));
        //     }

        //     return sum;
        // }
    }
}
