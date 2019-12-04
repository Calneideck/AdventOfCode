using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Day today = new Day4();

            Console.WriteLine(today.GetType().ToString());
            Console.WriteLine("---------------------");
            Console.WriteLine();

            Console.Write("Part 1: ");
            Console.WriteLine(today.Part1());

            Console.WriteLine();
            Console.Write("Part 2: ");
            Console.WriteLine(today.Part2());

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
