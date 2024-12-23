using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day13 : Day
    {
        string[] lines = File.ReadAllLines("Input/13.txt");
        List<Machine> machines = new();

        class Machine
        {
            public V2 a;
            public V2 b;
            public V2 prize;
        }

        public override object Part1()
        {
            int sum = 0;

            for (int i = 0; i < lines.Length; i += 4)
            {
                var a = lines[i].GrabInts();
                V2 a1 = new(a[0], a[1]);

                var b = lines[i + 1].GrabInts();
                V2 b1 = new(b[0], b[1]);

                var p = lines[i + 2].GrabInts();
                V2 p1 = new(p[0], p[1]);

                machines.Add(new Machine() { a = a1, b = b1, prize = p1 });
            }

            foreach (Machine machine in machines)
            {
                int minCost = int.MaxValue;

                for (int a = 0; a < 100; a++)
                    for (int b = 0; b < 100; b++)
                    {
                        int x = machine.a.x * a + machine.b.x * b;
                        int y = machine.a.y * a + machine.b.y * b;
                        if (x == machine.prize.x && y == machine.prize.y)
                        {
                            int cost = a * 3 + b;
                            if (cost < minCost)
                                minCost = cost;
                        }
                    }

                if (minCost != int.MaxValue)
                    sum += minCost;
            }

            return sum;
        }

        public override object Part2()
        {
            long sum = 0;

            foreach (Machine machine in machines)
            {
                long prizeX = machine.prize.x + 10000000000000;
                long prizeY = machine.prize.y + 10000000000000;

                long ax = machine.a.x * machine.a.y;
                long bx = machine.b.x * machine.a.y;
                long prizex = prizeX * machine.a.y;

                long ay = machine.a.y * machine.a.x;
                long by = machine.b.y * machine.a.x;
                long prizey = prizeY * machine.a.x;

                long x3 = ax - ay;
                long y3 = bx - by;
                long prize3 = prizex - prizey;

                if (prize3 < 0)
                {
                    x3 *= -1;
                    y3 *= -1;
                    prize3 *= -1;
                }

                if (y3 < 0) continue;

                double a = 0;
                double b = 0;

                b = prize3 / y3;
                a = (prizeX - b * machine.b.x) / machine.a.x;

                if (a <= 0 || b <= 0 || Math.Floor(a) != a || Math.Floor(b) != b)
                    continue;

                if (a * machine.a.y + b * machine.b.y != prizeY)
                    continue;

                sum += (long)a * 3 + (long)b;
            }

            return sum;
        }
    }
}
