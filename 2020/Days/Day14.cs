using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day14 : Day
    {
        string[] lines = File.ReadAllLines("Input/14.txt").ToArray();

        public override object Part1()
        {
            checked
            {
                Regex memReg = new Regex(@"\d+");
                Dictionary<ulong, ulong> mem = new Dictionary<ulong, ulong>();
                string mask = "";

                for (int i = 0; i < lines.Length; i++)
                    if (lines[i].StartsWith("mask"))
                        mask = lines[i].Split(' ').Last();
                    else
                    {
                        ulong ptr = ulong.Parse(memReg.Match(lines[i]).Value);
                        ulong num = ulong.Parse(lines[i].Split(' ').Last());

                        for (int j = mask.Length - 1; j >= 0; j--)
                            if (mask[j] == '1')
                                num |= (ulong)1 << (mask.Length - j - 1);
                            else if (mask[j] == '0')
                                num &= ~((ulong)1 << (mask.Length - j - 1));

                        mem[ptr] = num;
                    }

                return mem.Values.Aggregate((x, y) => x + y);
            }
        }

        public override object Part2()
        {
            checked
            {
                Regex memReg = new Regex(@"\d+");
                Dictionary<ulong, ulong> mem = new Dictionary<ulong, ulong>();
                string mask = "";

                for (int i = 0; i < lines.Length; i++)
                    if (lines[i].StartsWith("mask"))
                        mask = lines[i].Split(' ').Last();
                    else
                    {
                        ulong ptr = ulong.Parse(memReg.Match(lines[i]).Value);
                        ulong num = ulong.Parse(lines[i].Split(' ').Last());

                        StringBuilder builder = new StringBuilder(mask);
                        for (int j = 0; j < mask.Length; j++)
                            if (mask[j] == '0')
                                if ((ptr & (ulong)1 << (mask.Length - j - 1)) > 0)
                                    builder[j] = '1';

                        Queue<string> open = new Queue<string>();
                        List<ulong> addresses = new List<ulong>();
                        open.Enqueue(builder.ToString());

                        while (open.Count > 0)
                        {
                            string m = open.Dequeue();
                            if (!m.Contains('X'))
                                addresses.Add(Convert.ToUInt64(m, 2));
                            else
                                for (int k = 0; k < m.Length; k++)
                                    if (m[k] == 'X')
                                    {
                                        var sb = new StringBuilder(m);
                                        sb[k] = '0';
                                        open.Enqueue(sb.ToString());
                                        sb[k] = '1';
                                        open.Enqueue(sb.ToString());
                                        break;
                                    }
                        }

                        foreach (var address in addresses)
                            mem[address] = num;
                    }

                return mem.Values.Aggregate((x, y) => x + y);
            }
        }
    }
}
