using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day24 : Day
    {
        string[] lines = File.ReadAllLines("Input/24.txt");
        List<(V2, V2)> blizzards = new();
        Dictionary<int, List<V2>> blizzMap = new();
        int first;

        List<V2> GenerateBlizzards(int time)
        {
            int w = lines[0].Length - 2;
            int h = lines.Length - 2;
            time = time % (w * h);

            if (blizzMap.TryGetValue(time, out List<V2> value)) return value;

            List<V2> bliz = new();

            foreach (var b in blizzards)
            {
                V2 pos = b.Item1;
                pos.x--;
                pos.y--;

                pos += b.Item2 * time;

                pos.x = (pos.x + (w * h) * w) % w;
                pos.y = (pos.y + (w * h) * h) % h;
                pos.x++;
                pos.y++;
                bliz.Add(pos);
            }

            blizzMap.Add(time, bliz);
            return bliz;
        }

        int FindPath(V2 startPos, V2 endPos, int offset)
        {
            HashSet<(V2, int)> closed = new();
            Queue<(V2 pos, int time)> open = new();

            open.Enqueue((startPos, 0));

            while (open.Any())
            {
                var state = open.Dequeue();
                closed.Add(state);
                state.time++;

                List<V2> _blizzards = GenerateBlizzards(state.time + offset);

                List<V2> validDirs = V2.Directions.Where(dir =>
                {
                    V2 newPos = state.pos + dir;
                    return
                        (
                            newPos.x > 0 &&
                            newPos.y > 0 &&
                            newPos.x < lines[0].Length - 1 &&
                            newPos.y < lines.Length - 1 &&
                            _blizzards.Find(b => b == newPos) == V2.Zero
                        ) || newPos == endPos;
                }).ToList();
                if (!_blizzards.Contains(state.pos))
                    validDirs.Add(V2.Zero);

                foreach (V2 dir in validDirs)
                {
                    V2 newPos = state.pos + dir;
                    if (!closed.Contains((newPos, state.time)) && !open.Contains((newPos, state.time)))
                        if (newPos == endPos)
                            return state.time;
                        else
                            open.Enqueue((newPos, state.time));
                }
            }

            return 0;
        }

        public override object Part1()
        {
            char[] chars = new char[] { '^', '>', 'v', '<' };
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    if (lines[y][x] != '#' && lines[y][x] != '.')
                    {
                        V2 bPos = new V2(x, y);
                        blizzards.Add((bPos, V2.Directions[chars.ToList().FindIndex(z => z == lines[y][x])]));
                    }

            V2 start = new V2(1, 0);
            V2 end = new V2(lines[0].Length - 2, lines.Length - 1);
            first = FindPath(start, end, 0);
            return first;
        }

        public override object Part2()
        {
            V2 start = new V2(1, 0);
            V2 end = new V2(lines[0].Length - 2, lines.Length - 1);
            int second = FindPath(end, start, first);
            int third = FindPath(start, end, first + second);

            return first + second + third;
        }
    }
}
