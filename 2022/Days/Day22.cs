using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day22 : Day
    {
        string lines = File.ReadAllText("Input/22.txt");
        Dictionary<V2, char> map = new();
        V2 mainStart = V2.Zero;
        string[] directions = null;

        public override object Part1()
        {
            var temp = lines.Split("\n\n");

            string[] _lines = temp[0].Split('\n');

            V2 start = new();
            for (int y = 0; y < _lines.Length; y++)
                for (int x = 0; x < _lines[y].Length; x++)
                {
                    if (_lines[y][x] != ' ')
                        map.Add(new V2(x, y), _lines[y][x]);

                    if (start == V2.Zero && _lines[y][x] == '.')
                        start = new V2(x, y);
                }

            mainStart = start;
            directions = new Regex(@"(\d+)|([LR])").Matches(temp[1]).Select(x => x.Value).ToArray();

            int dirIndex = 1; // Right
            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == "L")
                    dirIndex = (dirIndex + 3) % 4;
                else if (directions[i] == "R")
                    dirIndex = (dirIndex + 1) % 4;
                else
                {
                    int num = int.Parse(directions[i]);
                    for (int j = 0; j < num; j++)
                    {
                        V2 newPos = start + V2.Directions[dirIndex];
                        if (map.TryGetValue(newPos, out char c))
                        {
                            if (c == '.')
                            {
                                start = newPos;
                                continue;
                            }
                            else if (c == '#')
                                break;
                        }

                        // off map
                        V2 oldStart = start;
                        V2 backDir = V2.Directions[(dirIndex + 2) % 4];
                        while (true)
                        {
                            V2 newPos2 = start + backDir;
                            if (!map.TryGetValue(newPos2, out char c2) || c == ' ')
                                break;
                            else
                                start = newPos2;
                        }

                        if (map[start] == '#')
                        {
                            start = oldStart;
                            break;
                        }
                    }
                }
            }

            return 1000 * (start.y + 1) + 4 * (start.x + 1) + dirIndex - 1;
        }

        int GetIndex(V2 pos)
        {
            if (pos.x >= 100)
                return 5;
            if (pos.x >= 50)
            {
                if (pos.y >= 100)
                    return 2;
                else if (pos.y >= 50)
                    return 3;
                return 4;
            }
            else
            {
                if (pos.y >= 150)
                    return 0;
                else
                    return 1;
            }
        }

        V2 GetPosInCube(V2 pos)
        {
            int index = GetIndex(pos);
            switch (index)
            {
                case 0:
                    return pos - new V2(0, 150);
                case 1:
                    return pos - new V2(0, 100);
                case 2:
                    return pos - new V2(50, 100);
                case 3:
                    return pos - new V2(50, 50);
                case 4:
                    return pos - new V2(50, 0);
                case 5:
                    return pos - new V2(100, 0);
            }
            return V2.Zero;
        }

        V2 GetAbsPos(V2 posInCube, int index)
        {
            switch (index)
            {
                case 0:
                    return posInCube + new V2(0, 150);
                case 1:
                    return posInCube + new V2(0, 100);
                case 2:
                    return posInCube + new V2(50, 100);
                case 3:
                    return posInCube + new V2(50, 50);
                case 4:
                    return posInCube + new V2(50, 0);
                case 5:
                    return posInCube + new V2(100, 0);
            }
            return V2.Zero;
        }

        Dictionary<(int, int), (int, int, Func<V2, V2>)> transforms = new() {
            { (0, 0), (1, 0, (p) => new V2(p.x, 49)) },
            { (0, 1), (2, 0, (p) => new V2(p.y, 49)) },
            { (0, 2), (5, 2, (p) => new V2(p.x, 0)) },
            { (0, 3), (4, 2, (p) => new V2(p.y, 0)) },

            { (1, 0), (3, 1, (p) => new V2(0, p.x)) },
            { (1, 1), (2, 1, (p) => new V2(0, p.y)) },
            { (1, 2), (0, 2, (p) => new V2(p.x, 0)) },
            { (1, 3), (4, 1, (p) => new V2(0, 49 - p.y)) },

            { (2, 0), (3, 0, (p) => new V2(p.x, 49)) },
            { (2, 1), (5, 3, (p) => new V2(49, 49 - p.y)) },
            { (2, 2), (0, 3, (p) => new V2(49, p.x)) },
            { (2, 3), (1, 3, (p) => new V2(49, p.y)) },
            
            { (3, 0), (4, 0, (p) => new V2(p.x, 49)) },
            { (3, 1), (5, 0, (p) => new V2(p.y, 49)) },
            { (3, 2), (2, 2, (p) => new V2(p.x, 0)) },
            { (3, 3), (1, 2, (p) => new V2(p.y, 0)) },

            { (4, 0), (0, 1, (p) => new V2(0, p.x)) },
            { (4, 1), (5, 1, (p) => new V2(0, p.y)) },
            { (4, 2), (3, 2, (p) => new V2(p.x, 0)) },
            { (4, 3), (1, 1, (p) => new V2(0, 49 - p.y)) },

            { (5, 0), (0, 0, (p) => new V2(p.x, 0)) },
            { (5, 1), (2, 3, (p) => new V2(49, 49 - p.y)) },
            { (5, 2), (3, 3, (p) => new V2(49, p.x)) },
            { (5, 3), (4, 3, (p) => new V2(49, p.y)) },
        };

        public override object Part2()
        {
            int dirIndex = 1; // Right
            V2 start = mainStart;

            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == "L")
                    dirIndex = (dirIndex + 3) % 4;
                else if (directions[i] == "R")
                    dirIndex = (dirIndex + 1) % 4;
                else
                {
                    int num = int.Parse(directions[i]);
                    for (int j = 0; j < num; j++)
                    {
                        V2 newPos = start + V2.Directions[dirIndex];
                        if (map.TryGetValue(newPos, out char c))
                        {
                            if (c == '.')
                            {
                                start = newPos;
                                continue;
                            }
                            else if (c == '#')
                                break;
                        }

                        // off map
                        V2 oldStart = start;

                        int cubeIndex = GetIndex(start);
                        var result = transforms[(cubeIndex, dirIndex)];
                        V2 posInCube = GetPosInCube(start);
                        posInCube = result.Item3(posInCube);
                        start = GetAbsPos(posInCube, result.Item1);

                        if (map[start] == '#')
                        {
                            start = oldStart;
                            break;
                        }
                        else
                            dirIndex = result.Item2;
                    }
                }
            }

            return 1000 * (start.y + 1) + 4 * (start.x + 1) + dirIndex - 1;
        }
    }
}
