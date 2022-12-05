using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day4 : Day
    {
        string[] lines = File.ReadAllLines("Input/4.txt");

        public override object Part1()
        {
            int count = 0;

            foreach (var line in lines)
            {
                int[][] a = line.Split(',').Select(x => x.Split('-').Select(int.Parse).ToArray()).ToArray();

                for (int i = 0; i < 2; i++)
                    if (a[i][0] >= a[1 - i][0] && a[i][1] <= a[1 - i][1])
                    {
                        count++;
                        break;
                    }
            }

            return count;
        }

        public override object Part2()
        {
            int count = 0;

            foreach (var line in lines)
            {
                int[][] a = line.Split(',').Select(x => x.Split('-').Select(int.Parse).ToArray()).ToArray();

                for (int i = 0; i < 2; i++)
                    if ((a[i][0] >= a[1 - i][0] && a[i][0] <= a[1 - i][1]) || 
                        (a[i][1] >= a[1 - i][0] && a[i][1] <= a[1 - i][1]))
                    {
                        count++;
                        break;
                    }
            }

            return count;
        }
    }
}
