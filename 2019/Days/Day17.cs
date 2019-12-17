using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day17 : Day
    {
        private IEnumerable<long> codeList;
        private List<long> area = new List<long>();
        private Dictionary<(int x, int y), char> map = new Dictionary<(int x, int y), char>();
        private (int, int)[] dirs = new (int, int)[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        public override string Part1()
        {
            codeList = File.ReadAllText("Input/17.txt").Split(',').Select(s => long.Parse(s));
            LCVM vm = new LCVM(codeList.ToArray());

            var result = LCVM.StopCode.None;
            int x = 0, y = 0;
            Console.WriteLine();

            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                {
                    long o = vm.GetOutput();
                    area.Add(o);
                    Console.Write((char)o);

                    if (o == 10)
                    {
                        y++;
                        x = 0;
                    }
                    else
                    {
                        map.Add((x, y), (char)o);
                        x++;
                    }
                }
            }

            int sum = 0;
            foreach (var tile in map)
                if (tile.Value == '#' || tile.Value == '^')
                {
                    bool good = true;
                    foreach (var dir in dirs)
                        if (!IsTile((tile.Key.x + dir.Item1, tile.Key.y + dir.Item2)))
                        {
                            good = false;
                            break;
                        }

                    if (good)
                        sum += tile.Key.x * tile.Key.y;
                }

            return sum.ToString();
        }

        public override string Part2()
        {
            // Determine path
            List<string> path = new List<string>();
            (int x, int y) pos = map.First(p => p.Value == '^').Key;
            (int x, int y) currentDir = (0, -1);

            var left = TurnLeft(currentDir);
            if (IsTile((pos.x + left.x, pos.y + left.y)))
            {
                path.Add("L");
                currentDir = left;
            }
            else
            {
                path.Add("R");
                currentDir = TurnRight(currentDir);
            }

            int count = 0;
            (int x, int y) nextTile = (pos.x + currentDir.x, pos.y + currentDir.y);
            while (true)
            {
                if (IsTile(nextTile))
                {
                    count++;
                    pos = nextTile;
                }
                else
                {
                    path.Add(count.ToString());
                    count = 0;

                    left = TurnLeft(currentDir);
                    if (IsTile((pos.x + left.x, pos.y + left.y)))
                    {
                        path.Add("L");
                        currentDir = left;
                    }
                    else
                    {
                        var right = TurnRight(currentDir);
                        if (IsTile((pos.x + right.x, pos.y + right.y)))
                        {
                            path.Add("R");
                            currentDir = TurnRight(currentDir);
                        }
                        else
                            break;
                    }
                }

                nextTile = (pos.x + currentDir.x, pos.y + currentDir.y);
            }

            Console.WriteLine();

            string pathString = string.Join("", path);
            Console.WriteLine(pathString);

            // I worked out these sections manually!
            string A = "L,9,3,R,4,R,4,L,6\n";
            string B = "L,9,3,R,4,R,4,R,9,3\n";
            string C = "L,9,1,L,6,R,4\n";
            string Route = "A,B,A,C,A,B,C,B,C,A\n";

            var codes = codeList.ToArray();
            codes[0] = 2;

            var aInput = A.ToCharArray();
            var bInput = B.ToCharArray();
            var cInput = C.ToCharArray();
            var routeInput = Route.ToCharArray();

            LCVM vm = new LCVM(codes);
            long lastOutput = 0;

            foreach (var r in routeInput)
                vm.AddInput(r);

            foreach (var a in aInput)
                vm.AddInput(a);

            foreach (var b in bInput)
                vm.AddInput(b);

            foreach (var c in cInput)
                vm.AddInput(c);

            vm.AddInput('n');
            vm.AddInput('\n');

            var result = LCVM.StopCode.None;
            while (result != LCVM.StopCode.Halt)
            {
                result = vm.GetCodeResult();
                if (result == LCVM.StopCode.Output)
                {
                    long o = vm.GetOutput();
                    lastOutput = o;
                    Console.Write((char)o);
                }
            }

            return lastOutput.ToString();
        }

        bool IsTile((int x, int y) pos)
        {
            return map.TryGetValue(pos, out char c) && c == '#';
        }

        (int x, int y) TurnRight((int x, int y) dir)
        {
            return dir switch
            {
                (0, -1) => (1, 0),
                (1, 0) => (0, 1),
                (0, 1) => (-1, 0),
                (-1, 0) => (0, -1)
            };
        }

        (int x, int y) TurnLeft((int x, int y) dir)
        {
            return dir switch
            {
                (0, -1) => (-1, 0),
                (1, 0) => (0, -1),
                (0, 1) => (1, 0),
                (-1, 0) => (0, 1)
            };
        }
    }
}
