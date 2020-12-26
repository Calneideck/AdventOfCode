using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day24 : Day
    {
        string[] lines = File.ReadAllLines("Input/24.txt").ToArray();
        HashSet<V2> blackTiles = new HashSet<V2>();

        bool EvenTile(V2 tile) => tile.y % 2 == 0;

        public override object Part1()
        {
            foreach (var line in lines)
            {
                int i = 0;
                V2 tile = new V2();
                while (i < line.Length)
                    if (line[i] == 'w')
                    {
                        tile.x--;
                        i++;
                    }
                    else if (line[i] == 'e')
                    {
                        tile.x++;
                        i++;
                    }
                    else
                    {
                        string dir = line.Substring(i, 2);
                        if (dir == "ne")
                        {
                            tile.x += EvenTile(tile) ? 1 : 0;
                            tile.y++;
                        }
                        else if (dir == "se")
                        {
                            tile.x += EvenTile(tile) ? 1 : 0;
                            tile.y--;
                        }
                        else if (dir == "sw")
                        {
                            tile.x -= EvenTile(tile) ? 0 : 1;
                            tile.y--;
                        }
                        else if (dir == "nw")
                        {
                            tile.x -= EvenTile(tile) ? 0 : 1;
                            tile.y++;
                        }

                        i += 2;
                    }

                if (blackTiles.Contains(tile))
                    blackTiles.Remove(tile);
                else
                    blackTiles.Add(tile);
            }

            return blackTiles.Count;
        }

        V2[] evenDirs = {
            new V2(1, 0),
            new V2(1, 1),
            new V2(0, 1),
            new V2(-1, 0),
            new V2(0, -1),
            new V2(1, -1)
        };

        V2[] oddDirs = {
            new V2(1, 0),
            new V2(0, 1),
            new V2(-1, 1),
            new V2(-1, 0),
            new V2(-1, -1),
            new V2(0, -1)
        };

        public override object Part2()
        {
            for (int i = 0; i < 100; i++)
            {
                List<V2> list = blackTiles.ToList();
                int listLen = list.Count;

                for (int j = 0; j < listLen; j++)
                {
                    V2 tile = list[j];
                    V2[] dirs = EvenTile(tile) ? evenDirs : oddDirs;
                    foreach (V2 dir in dirs)
                        list.Add(tile + dir);
                }

                HashSet<V2> newTiles = new HashSet<V2>();
                HashSet<V2> toCheck = new HashSet<V2>(list);

                foreach (V2 tile in toCheck)
                {
                    int count = 0;
                    if (EvenTile(tile))
                        count = evenDirs.Count(dir => blackTiles.Contains(tile + dir));
                    else
                        count = oddDirs.Count(dir => blackTiles.Contains(tile + dir));

                    if (blackTiles.Contains(tile) && count > 0 && count <= 2)
                        newTiles.Add(tile);
                    else if (!blackTiles.Contains(tile) && count == 2)
                        newTiles.Add(tile);
                }

                blackTiles = new HashSet<V2>(newTiles);
            }

            return blackTiles.Count;
        }
    }
}
