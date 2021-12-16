using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day16 : Day
    {
        string[] lines = File.ReadAllLines("Input/16.txt");
        int versions = 0;

        public override object Part1()
        {
            string line = hToB(lines[0]);

            ParsePackets(line);

            return versions;
        }

        public override object Part2()
        {
            string line = hToB(lines[0]);

            var res = ParsePackets(line);

            return res.results[0];
        }

        (int count, long[] results) ParsePackets(string line, int count = 0)
        {
            int i = 0;
            int numRead = 0;
            List<long> results = new();

            while (i < line.Length && (count == 0 || numRead < count))
            {
                if (!line.Substring(i).Contains('1'))
                    break;

                int v = Convert.ToInt32(line.Substring(i, 3), 2);
                i += 3;
                versions += v;

                int id = Convert.ToInt32(line.Substring(i, 3), 2);
                i += 3;

                if (id == 4)
                {
                    // literal
                    bool end = false;
                    string num = "";

                    while (!end)
                    {
                        if (line[i] == '0')
                            end = true;

                        num += line.Substring(i + 1, 4);
                        i += 5;
                    }

                    results.Add(Convert.ToInt64(num, 2));
                }
                else
                {
                    // operator
                    int lengthId = Convert.ToInt32(line.Substring(i, 1), 2);
                    i++;

                    (int count, long[] results) packets;
                    if (lengthId == 1)
                    {
                        int subCount = Convert.ToInt32(line.Substring(i, 11), 2);
                        i += 11;
                        packets = ParsePackets(line.Substring(i), subCount);
                        i += packets.count;
                    }
                    else
                    {
                        int subLength = Convert.ToInt32(line.Substring(i, 15), 2);
                        i += 15;
                        packets = ParsePackets(line.Substring(i, subLength));
                        i += subLength;
                    }

                    if (id == 0)
                        results.Add(packets.results.Sum());
                    else if (id == 1)
                        results.Add(packets.results.Aggregate((long)1, (x, sum) => x * sum));
                    else if (id == 2)
                        results.Add(packets.results.Min());
                    else if (id == 3)
                        results.Add(packets.results.Max());
                    else if (id == 5)
                        results.Add(packets.results[0] > packets.results[1] ? 1 : 0);
                    else if (id == 6)
                        results.Add(packets.results[0] < packets.results[1] ? 1 : 0);
                    else if (id == 7)
                        results.Add(packets.results[0] == packets.results[1] ? 1 : 0);
                }

                numRead++;
            }

            return (i, results.ToArray());
        }

        string hToB(string h)
        {
            return string.Join("",
                h.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );
        }
    }
}
