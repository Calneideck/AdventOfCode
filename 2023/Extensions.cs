using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
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

        public static void Draw(string[] lines, Func<V2, bool> fn)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < lines[0].Length; x++)
                    Console.Write(fn(new V2(x, y)) ? '#' : '.');
            }
            Console.WriteLine();
        }

        public static long LCM(long[] numbers)
        {
            return numbers.Aggregate(LCM);
        }

        public static long LCM(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }
}
