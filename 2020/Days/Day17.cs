using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;
using System;

namespace AdventOfCode
{
    class Day17 : Day
    {
        string[] lines = File.ReadAllLines("Input/17.txt").ToArray();

        public override object Part1()
        {
            List<(int x, int y, int z)> Dirs = new List<(int x, int y, int z)>();
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    for (int z = -1; z <= 1; z++)
                        Dirs.Add((x, y, z));

            Dirs.Remove((0, 0, 0));

            List<(int x, int y, int z)> actives = new List<(int x, int y, int z)>();

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    if (lines[y][x] == '#')
                        actives.Add((x - 1, y - 1, 0));

            int xyBounds = lines.Length + 2;
            int zBounds = 3;

            for (int i = 0; i < 6; i++)
            {
                List<(int x, int y, int z)> nextActives = new List<(int x, int y, int z)>();

                for (int x = -xyBounds; x <= xyBounds; x++)
                    for (int y = -xyBounds; y <= xyBounds; y++)
                        for (int z = -zBounds; z <= zBounds; z++)
                        {
                            int count = 0;
                            foreach (var dir in Dirs)
                                if (actives.Contains((x + dir.x, y + dir.y, z + dir.z)))
                                    count++;

                            bool thisActive = actives.Contains((x, y, z));
                            if (thisActive && (count == 2 || count == 3))
                                nextActives.Add((x, y, z));
                            else if (!thisActive && count == 3)
                                nextActives.Add((x, y, z));
                        }

                actives = nextActives;
                xyBounds += 2;
                zBounds += 2;
            }

            return actives.Count;
        }

        public override object Part2()
        {
            List<(int x, int y, int z, int w)> Dirs = new List<(int x, int y, int z, int w)>();
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                    for (int z = -1; z <= 1; z++)
                        for (int w = -1; w <= 1; w++)
                            Dirs.Add((x, y, z, w));

            Dirs.Remove((0, 0, 0, 0));

            HashSet<(int x, int y, int z, int w)> actives = new HashSet<(int x, int y, int z, int w)>();

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    if (lines[y][x] == '#')
                        actives.Add((x - 1, y - 1, 0, 0));

            int xyBounds = lines.Length + 2;
            int zwBounds = 3;

            for (int i = 0; i < 6; i++)
            {
                HashSet<(int x, int y, int z, int w)> nextActives = new HashSet<(int x, int y, int z, int w)>();

                for (int x = -xyBounds; x <= xyBounds; x++)
                    for (int y = -xyBounds; y <= xyBounds; y++)
                        for (int z = -zwBounds; z <= zwBounds; z++)
                            for (int w = -zwBounds; w <= zwBounds; w++)
                            {
                                int count = 0;
                                foreach (var dir in Dirs)
                                    if (actives.Contains((x + dir.x, y + dir.y, z + dir.z, w + dir.w)))
                                        count++;

                                bool thisActive = actives.Contains((x, y, z, w));
                                if (thisActive && (count == 2 || count == 3))
                                    nextActives.Add((x, y, z, w));
                                else if (!thisActive && count == 3)
                                    nextActives.Add((x, y, z, w));
                            }

                actives = nextActives;
                xyBounds += 2;
                zwBounds += 2;
            }

            return actives.Count;
        }
    }
}
