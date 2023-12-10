using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Day today = new Day8();

            Console.WriteLine(today.GetType().ToString());
            Console.WriteLine("---------------------");
            Console.WriteLine();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Console.Write("Part 1: ");
            Console.WriteLine(today.Part1());

            Console.WriteLine();
            Console.Write("Part 2: ");
            Console.WriteLine(today.Part2());

            sw.Stop();
            Console.WriteLine("\nTime (ms): " + sw.ElapsedMilliseconds);
        }
    }

    public static class MyExtensions
    {
        public static int[] GetInts(this string[] s)
        {
            return s.Select(int.Parse).ToArray();
        }

        public static long[] GetLongs(this string[] s)
        {
            return s.Select(long.Parse).ToArray();
        }

        public static int[] GrabInts(this string s)
        {
            return new Regex(@"(\d+)")
                .Matches(s).Select(a => int.Parse(a.Value))
                .ToArray();
        }

        public static long[] GrabLongs(this string s)
        {
            return new Regex(@"(\d+)")
                .Matches(s).Select(a => long.Parse(a.Value))
                .ToArray();
        }

        public static Dictionary<V2, char> GetMap(this string[] s)
        {
            Dictionary<V2, char> map = new();

            for (int y = 0; y < s.Length; y++)
                for (int x = 0; x < s[y].Length; x++)
                    map.Add(new V2(x, y), s[y][x]);

            return map;
        }
    }
}
