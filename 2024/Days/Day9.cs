using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day9 : Day
    {
        string line = File.ReadAllText("Input/9.txt");

        class AmpFile
        {
            public long pos;
            public long length;
            public long id;
        }

        public override object Part1()
        {
            long sum = 0;

            List<long> list = new();

            long id = 0;
            long empty = 0;
            for (int i = 0; i < line.Length; i++)
            {
                int c = int.Parse(line[i].ToString());

                if (i % 2 == 0)
                {
                    // file
                    for (int x = 0; x < c; x++)
                        list.Add(id);

                    id++;
                }
                else
                    for (int x = 0; x < c; x++)
                    {
                        list.Add(-1);
                        empty++;
                    }
            }

            int pt = 0;
            int count = list.Count;
            for (int i = 0; i < empty; i++)
            {
                long c = list[count - 1 - i];
                if (c > 0)
                {
                    list[count - 1 - i] = -1;
                    while (pt < count - 1 && list[pt] >= 0)
                        pt++;

                    if (pt < count - 1)
                    {
                        list[pt] = c;
                        i--;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (list[i] == -1) break;
                sum += list[i] * i;
            }

            return sum;
        }

        public override object Part2()
        {
            long sum = 0;
            long id = 0;
            long pos = 0;
            List<AmpFile> files = new();

            for (int i = 0; i < line.Length; i++)
            {
                int c = int.Parse(line[i].ToString());

                if (i % 2 == 0)
                    files.Add(new AmpFile() { id = id++, pos = pos, length = c });

                pos += c;
            }

            for (int i = files.Count - 1; i > 0; i--)
                for (int x = 0; x < i; x++)
                {
                    long gap = files[x + 1].pos - files[x].pos - files[x].length;
                    if (gap >= files[i].length)
                    {
                        AmpFile f = files[i];
                        f.pos = files[x].pos + files[x].length;
                        files.RemoveAt(i);
                        files.Insert(x + 1, f);
                        i++;

                        break;
                    }
                }

            foreach (AmpFile file in files)
                for (long i = 0; i < file.length; i++)
                    sum += (file.pos + i) * file.id;

            return sum;
        }
    }
}
