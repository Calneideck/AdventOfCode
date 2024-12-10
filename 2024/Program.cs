using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Day today = new Day9();

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
}
