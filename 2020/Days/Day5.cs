using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day5 : Day
    {
        string[] lines = File.ReadAllLines("Input/5.txt");

        public override object Part1()
        {
            int max = 0;
            
            foreach (var line in lines)
            {
                int lo = 0;
                int hi = 127;
                for (int i = 0; i < 7; i++)
                {
                    char c = line[i];
                    if (c == 'F')
                        hi = (int)Math.Floor((hi - lo) / 2f + lo);
                    else
                        lo = (int)Math.Ceiling((hi - lo) / 2f + lo);
                }

                int row = lo;

                lo = 0;
                hi = 7;

                for (int i = 7; i < line.Length; i++)
                {
                    char c = line[i];
                    if (c == 'L')
                        hi = (int)Math.Floor((hi - lo) / 2f + lo);
                    else
                        lo = (int)Math.Ceiling((hi - lo) / 2f + lo);
                }

                int col = lo;

                int seat = row * 8 + col;

                max = Math.Max(max, seat);
            }
            
            return max;
        }

        public override object Part2()
        {
            List<int> seats = new List<int>();

            foreach (var line in lines)
            {
                int lo = 0;
                int hi = 127;
                for (int i = 0; i < 7; i++)
                {
                    char c = line[i];
                    if (c == 'F')
                        hi = (int)Math.Floor((hi - lo) / 2f + lo);
                    else
                        lo = (int)Math.Ceiling((hi - lo) / 2f + lo);
                }

                int row = lo;

                lo = 0;
                hi = 7;

                for (int i = 7; i < line.Length; i++)
                {
                    char c = line[i];
                    if (c == 'L')
                        hi = (int)Math.Floor((hi - lo) / 2f + lo);
                    else
                        lo = (int)Math.Ceiling((hi - lo) / 2f + lo);
                }

                int col = lo;

                int seat = row * 8 + col;

                seats.Add(seat);
            }

            for (int i = 1; i < 1023; i++)
            {
                if (!seats.Contains(i) && seats.Contains(i - 1) && seats.Contains(i + 1))
                    return i;
            }

            return -1;
        }
    }
}
