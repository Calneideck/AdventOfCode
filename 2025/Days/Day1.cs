using System;
using System.IO;

namespace AdventOfCode
{
    class Day1 : Day
    {
        string[] lines = File.ReadAllLines("Input/1.txt");

        public override object Part1()
        {
            int dial = 50;
            int count = 0;

            foreach (string line in lines)
            {
                char dir = line[0];
                int num = line.GrabInts()[0];

                if (dir == 'L')
                {
                    dial -= num;
                    while (dial < 0) dial += 100;
                }
                else if (dir == 'R')
                {
                    dial += num;
                    while (dial > 99) dial -= 100;
                }

                if (dial == 0) count++;
            }

            return count;
        }

        public override object Part2()
        {
            int dial = 50;
            int count = 0;

            foreach (string line in lines)
            {
                char dir = line[0];
                int num = line.GrabInts()[0];

                if (dir == 'L')
                {
                    for (int i = 0; i < num; i++)
                    {
                        dial--;
                        if (dial < 0) dial = 99;
                        else if (dial == 0) count++;
                    }
                }
                else if (dir == 'R')
                {
                    for (int i = 0; i < num; i++)
                    {
                        dial++;
                        if (dial > 99) dial = 0;
                        if (dial == 0) count++;
                    }
                }
            }

            return count;
        }
    }
}
