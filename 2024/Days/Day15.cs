using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Intrinsics;
using System.Threading;

namespace AdventOfCode
{
    class Day15 : Day
    {
        string text = File.ReadAllText("Input/15.txt");
        List<char> indices = new(new char[] { '^', '>', 'v', '<' });

        public override object Part1()
        {
            string[] lines = text.Split("\n\n")[0].Split('\n');
            string instructions = text.Split("\n\n")[1];

            Dictionary<V2, char> map = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map[new V2(x, y)] = lines[y][x];

            int max = Math.Max(lines[0].Length - 1, lines.Length);

            V2 robot = map.First(x => x.Value == '@').Key;
            map[robot] = '.';

            for (int i = 0; i < instructions.Length; i++)
            {
                V2 dir = V2.Directions[indices.IndexOf(instructions[i])];

                char c = map[robot + dir];
                if (c == '.')
                    robot += dir;
                else if (c == 'O')
                {
                    V2 end = robot + dir;
                    for (int x = 0; x < max; x++)
                    {
                        end += dir;

                        if (map[end] == '#')
                            break;
                        else if (map[end] == '.')
                        {
                            map[end] = 'O';
                            map[robot + dir] = '.';
                            robot += dir;
                            break;
                        }
                    }
                }
            }

            return map.Sum(x => x.Value == 'O' ? x.Key.x + x.Key.y * 100 : 0);
        }

        public override object Part2()
        {
            string[] lines = text.Split("\n\n")[0].Split('\n');
            string instructions = text.Split("\n\n")[1];

            Dictionary<V2, char> map = new();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                {
                    map[new V2(x * 2, y)] = lines[y][x];
                    if (map[new V2(x * 2, y)] == '.' || map[new V2(x * 2, y)] == '@')
                        map[new V2(x * 2 + 1, y)] = '.';
                    else if (map[new V2(x * 2, y)] == '#')
                        map[new V2(x * 2 + 1, y)] = '#';
                    else if (map[new V2(x * 2, y)] == 'O')
                    {
                        map[new V2(x * 2, y)] = '[';
                        map[new V2(x * 2 + 1, y)] = ']';
                    }
                }

            int max = Math.Max(lines[0].Length - 1, lines.Length);

            V2 robot = map.First(x => x.Value == '@').Key;
            map[robot] = '.';

            for (int i = 0; i < instructions.Length; i++)
            {
                V2 dir = V2.Directions[indices.IndexOf(instructions[i])];

                char c = map[robot + dir];
                if (c == '.')
                    robot += dir;
                else if (c == '[' || c == ']')
                {
                    V2 end = robot + dir;

                    if (dir == V2.Left || dir == V2.Right)
                    {
                        // Horizontal
                        List<V2> boxes = new() { end };
                        for (int x = 0; x < max; x++)
                        {
                            end += dir;

                            if (map[end] == '#')
                                break;
                            else if (map[end] == '[' || map[end] == ']')
                                boxes.Add(end);
                            else if (map[end] == '.')
                            {
                                map[end] = dir == V2.Left ? '[' : ']';
                                foreach (V2 v in boxes)
                                    map[v] = map[v] == '[' ? ']' : '[';
                                map[robot + dir] = '.';
                                robot += dir;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Vertical
                        List<V2> boxes = new()
                        {
                            end,
                            map[end] == '[' ? end + V2.Right : end + V2.Left
                        };
                        Queue<V2> q = new();
                        q.Enqueue(end);
                        q.Enqueue(map[end] == '[' ? end + V2.Right : end + V2.Left);

                        for (int z = 0; z < max; z++)
                        {
                            Queue<V2> nextQ = new();

                            bool good = true;
                            bool wall = false;
                            while (q.Count > 0)
                            {
                                V2 pos = q.Dequeue();

                                if (map[pos + dir] == '#')
                                {
                                    good = false;
                                    wall = true;
                                    break;
                                }
                                else if (map[pos + dir] == '.')
                                {
                                    // good
                                }
                                else if (map[pos + dir] == '[')
                                {
                                    nextQ.Enqueue(pos + dir);
                                    nextQ.Enqueue(pos + dir + V2.Right);
                                    boxes.Add(pos + dir);
                                    boxes.Add(pos + dir + V2.Right);
                                    good = false;
                                }
                                else if (map[pos + dir] == ']')
                                {
                                    nextQ.Enqueue(pos + dir);
                                    nextQ.Enqueue(pos + dir + V2.Left);
                                    boxes.Add(pos + dir);
                                    boxes.Add(pos + dir + V2.Left);
                                    good = false;
                                }
                            }

                            if (wall) break;

                            if (good)
                            {
                                for (int x = boxes.Count - 1; x >= 0; x--)
                                {
                                    map[boxes[x] + dir] = map[boxes[x]];
                                    map[boxes[x]] = '.';
                                }

                                robot += dir;
                                break;
                            }
                            else
                            {
                                q = nextQ;
                            }
                        }
                    }
                }
            }

            return map.Sum(x => x.Value == '[' ? x.Key.x + x.Key.y * 100 : 0);
        }
    }
}
