using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day10 : Day
    {
        private List<(int x, int y)> asteroids = new List<(int, int)>();
        (int x, int y) target = (0, 0);

        public override string Part1()
        {
            string[] lines = File.ReadAllLines("Input/10.txt");
            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[y].Length; x++)
                    if (lines[y][x] == '#')
                        asteroids.Add((x, y));

            int highest = 0;
            foreach (var a in asteroids)
            {
                var list = asteroids.Where(b => b != a).Select(b => Angle((b.x - a.x, b.y - a.y))).Distinct();

                if (list.Count() > highest)
                {
                    highest = list.Count();
                    target = a;
                }
            }

            Console.WriteLine(target);

            return highest.ToString();
        }

        public override string Part2()
        {
            var asteroidsWithAngles = asteroids.Where(b => b != target).Select(b => (b.x, b.y, Angle((b.x - target.x, b.y - target.y))));
            List<(int x, int y, double angle)> aliveAsteroids = asteroidsWithAngles.OrderBy(b => b.Item3).ThenBy(b => DistSq((b.x, b.y), target)).ToList();

            int c = 0;
            double lastAngle = -1;
            (int x, int y) last = (0, 0);
            List<(int x, int y, double a)> toRemove = new List<(int x, int y, double a)>();

            while (c < 200)
            {
                foreach (var asteroid in aliveAsteroids)
                    if (asteroid.angle != lastAngle)
                    {
                        lastAngle = asteroid.angle;
                        toRemove.Add(asteroid);
                        last = (asteroid.x, asteroid.y);
                        if (++c == 200)
                            break;
                    }

                foreach (var item in toRemove)
                    aliveAsteroids.Remove(item);

                toRemove.Clear();
            }

            return (last.x * 100 + last.y).ToString();
        }

        double Angle((int, int) dir)
        {
            double angle = Math.Atan2(dir.Item2, dir.Item1) + Math.PI * 0.5f;
            if (angle < 0)
                angle += Math.PI * 2;

            return angle;
        }

        double DistSq((int, int) a, (int, int) b)
        {
            return (Math.Pow(a.Item1 - b.Item1, 2) + Math.Pow(a.Item2 - b.Item2, 2));
        }
    }
}
