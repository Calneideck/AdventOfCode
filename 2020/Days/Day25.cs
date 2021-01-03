using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day25 : Day
    {
        string[] lines = File.ReadAllLines("Input/25.txt").ToArray();

        long Transform(long input, long subject = 7)
        {
            input *= subject;
            input %= 20201227;
            return input;
        }

        public override object Part1()
        {
            long key1 = long.Parse(lines[0]);
            long key2 = long.Parse(lines[1]);

            int loop1 = 0;

            long num = 1;
            while (num != key1)
            {
                num = Transform(num);
                loop1++;
            }

            num = 1;
            for (int i = 0; i < loop1; i++)
                num = Transform(num, key2);

            return num;
        }
    }
}
