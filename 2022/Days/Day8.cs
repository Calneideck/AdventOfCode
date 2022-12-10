using System.IO;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day8 : Day
    {
        string[] lines = File.ReadAllLines("Input/8.txt");
        Dictionary<V2, int> map = null;
        int len;

        public override object Part1()
        {
            len = lines.Length;
            map = new Dictionary<V2, int>();
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                    map.Add(new V2(x, y), int.Parse(lines[y][x].ToString()));

            int count = 0;
            foreach (var m in map)
            {
                if (m.Key.x == 0 || m.Key.y == 0 || m.Key.x == len - 1 || m.Key.y == len - 1)
                {
                    count++;
                    continue;
                }

                int height = m.Value;
                foreach (var dir in V2.Directions) {
                    bool ban = false;
                    V2 pos = m.Key;

                    do {
                        pos += dir;
                        if (map[pos] >= height) {
                            ban = true;
                            break;
                        }
                    } while (pos.x != 0 && pos.y != 0 && pos.y != len - 1 && pos.x != len - 1);

                    if (!ban) {
                        count++;
                        break;
                    };
                }
            }

            return count;
        }

        public override object Part2()
        {
            int max = 0;
            foreach (var m in map)
            {
                if (m.Key.x == 0 || m.Key.y == 0 || m.Key.x == len - 1 || m.Key.y == len - 1)
                    continue;

                int height = m.Value;
                int sceanic = 1;

                foreach (var dir in V2.Directions) {
                    V2 pos = m.Key;

                    int scDir = 0;
                    do {
                        pos += dir;
                        scDir++;
                        if (map[pos] >= height) {
                            break;
                        }
                    } while (pos.x != 0 && pos.y != 0 && pos.y != len - 1 && pos.x != len - 1);

                    sceanic *= scDir;
                }

                if (sceanic > max)
                    max = sceanic;
            }
            
            return max;
        }
    }
}
