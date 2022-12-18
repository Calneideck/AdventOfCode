using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day17 : Day
    {
        string line = File.ReadAllText("Input/17.txt");
        HashSet<V2> blocked = new();

        List<V2[]> rocks = new()
        {
            new V2[] {
                V2.Zero, new V2(1, 0), new V2(2, 0), new V2(3, 0)
            },
            new V2[] {
                new V2(1, 0), new V2(0, 1), new V2(1, 1), new V2(2, 1), new V2(1, 2)
            },
            new V2[] {
                V2.Zero, new V2(1, 0), new V2(2, 0), new V2(2, 1), new V2(2, 2)
            },
            new V2[] {
                V2.Zero, new V2(0, 1), new V2(0, 2), new V2(0, 3)
            },
            new V2[] {
                V2.Zero, new V2(1, 0), new V2(0, 1), new V2(1, 1)
            },
        };

        void Print()
        {
            Console.WriteLine();

            for (int y = 3000; y >= 0; y--)
            {
                Console.Write('|');
                for (int x = 0; x < 7; x++)
                    Console.Write(blocked.Contains(new V2(x, y)) ? '#' : ' ');

                Console.Write('|');
                Console.WriteLine();
            }
        }

        public override object Part1()
        {
            int j = 0;
            for (int i = 0; i < 2022; i++)
            {
                V2 rockPos = new(2, blocked.Count > 1 ? blocked.Max(b => b.y) + 4 : 3);
                V2[] rock = rocks[i % 5];

                while (true)
                {
                    char dir = line[j];
                    j = (j + 1) % line.Length;
                    if (dir == '>')
                    {
                        rockPos.x++;
                        if (
                            rock.Any(p =>
                            {
                                V2 pos = rockPos + p;
                                if (blocked.Contains(pos)) return true;
                                if (pos.x > 6) return true;
                                return false;
                            })
                        )
                        {
                            rockPos.x--;
                        }
                    }
                    else
                    {
                        rockPos.x--;
                        if (
                            rock.Any(p =>
                            {
                                V2 pos = rockPos + p;
                                if (blocked.Contains(pos)) return true;
                                if (pos.x < 0) return true;
                                return false;
                            })
                        )
                        {
                            rockPos.x++;
                        }
                    }

                    rockPos.y--;
                    if (
                        rock.Any(p =>
                        {
                            V2 pos = rockPos + p;
                            if (blocked.Contains(pos)) return true;
                            if (pos.y == -1) return true;
                            return false;
                        })
                    )
                    {
                        rockPos.y++;
                        foreach (var r in rock)
                            blocked.Add(rockPos + r);

                        break;
                    }
                }
            }

            return blocked.Max(b => b.y) + 1;
        }

        public override object Part2()
        {
            HashSet<int> cache = new();
            Dictionary<int, (long, int)> map = new();
            blocked = new();

            int j = 0;

            for (long i = 0; i < 1e12; i++)
            {
                V2 rockPos = new(2, blocked.Count > 1 ? blocked.Max(b => b.y) + 4 : 3);
                V2[] rock = rocks[(int)(i % 5)];

                while (true)
                {
                    char dir = line[j];
                    j = (j + 1) % line.Length;
                    if (dir == '>')
                    {
                        rockPos.x++;
                        if (
                            rock.Any(p =>
                            {
                                V2 pos = rockPos + p;
                                if (blocked.Contains(pos)) return true;
                                if (pos.x < 0 || pos.x > 6) return true;
                                return false;
                            })
                        )
                            rockPos.x--;
                    }
                    else
                    {
                        rockPos.x--;
                        if (
                            rock.Any(p =>
                            {
                                V2 pos = rockPos + p;
                                if (blocked.Contains(pos)) return true;
                                if (pos.x < 0 || pos.x > 6) return true;
                                return false;
                            })
                        )
                            rockPos.x++;
                    }

                    rockPos.y--;
                    if (
                        rock.Any(p =>
                        {
                            V2 pos = rockPos + p;
                            if (blocked.Contains(pos)) return true;
                            if (pos.y == -1) return true;
                            return false;
                        })
                    )
                    {
                        rockPos.y++;
                        foreach (var r in rock)
                            blocked.Add(rockPos + r);

                        break;
                    }
                }

                int hash = HashCode.Combine(j, i % 5);
                if (cache.Contains(hash))
                {
                    long cycle = i - map[hash].Item1;
                    long cyclesLeft = (long)1e12 - i;
                    long cycleCount = cyclesLeft / cycle;

                    if (i + cycleCount * cycle == 1e12)
                    {
                        long diff = blocked.Max(b => b.y) + 1 - map[hash].Item2;
                        return blocked.Max(b => b.y) + cycleCount * diff;
                    }
                }
                else
                {
                    cache.Add(hash);
                    map.Add(hash, (i, blocked.Max(b => b.y) + 1));
                }

            }

            return 0;
        }
    }
}
