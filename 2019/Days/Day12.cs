using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Moon
    {
        public (int x, int y, int z) pos;
        public (int x, int y, int z) vel;

        public int Kinetic => (Math.Abs(pos.x) + Math.Abs(pos.y) + Math.Abs(pos.z)) *
            (Math.Abs(vel.x) + Math.Abs(vel.y) + Math.Abs(vel.z));
    }

    class Day12 : Day
    {
        List<Moon> Moons = new List<Moon>()
        {
            new Moon() { pos = (-3, 10, -1), vel = (0, 0, 0) },
            new Moon() { pos = (-12, -10, -5), vel = (0, 0, 0) },
            new Moon() { pos = (-9, 0, 10), vel = (0, 0, 0) },
            new Moon() { pos = (7, -5, -3), vel = (0, 0, 0) }
        };

        // TEST
        /*List<Moon> Moons = new List<Moon>()
        {
            new Moon() { pos = (-1, 0, 2), vel = (0, 0, 0) },
            new Moon() { pos = (2, -10, -7), vel = (0, 0, 0) },
            new Moon() { pos = (4, -8, 8), vel = (0, 0, 0) },
            new Moon() { pos = (3, 5, -1), vel = (0, 0, 0) }
        };*/

        public override string Part1()
        {
            Moon[] moons = Moons.ToArray();
            
            List<(Moon, Moon)> pairs = new List<(Moon, Moon)>();
            for (int i = 0; i < moons.Length - 1; i++)
                for (int j = i + 1; j < moons.Length; j++)
                    pairs.Add((moons[i], moons[j]));

            ulong time = 0;
            while (time < 1000)
            {
                // Get velocity between each pair
                foreach (var pair in pairs)
                {
                    var v1 = pair.Item1.vel;
                    var v2 = pair.Item2.vel;

                    v1.x += Math.Sign(pair.Item2.pos.x - pair.Item1.pos.x);
                    v1.y += Math.Sign(pair.Item2.pos.y - pair.Item1.pos.y);
                    v1.z += Math.Sign(pair.Item2.pos.z - pair.Item1.pos.z);

                    v2.x += Math.Sign(pair.Item1.pos.x - pair.Item2.pos.x);
                    v2.y += Math.Sign(pair.Item1.pos.y - pair.Item2.pos.y);
                    v2.z += Math.Sign(pair.Item1.pos.z - pair.Item2.pos.z);

                    pair.Item1.vel = v1;
                    pair.Item2.vel = v2;
                }

                foreach (var moon in moons)
                    moon.pos = (moon.pos.x + moon.vel.x,
                                moon.pos.y + moon.vel.y,
                                moon.pos.z + moon.vel.z);

                time++;
            }

            int energy = moons.Sum(m => m.Kinetic);
            return energy.ToString();
        }

        public override string Part2()
        {
            Moon[] moons = Moons.ToArray();

            List<(Moon, Moon)> pairs = new List<(Moon, Moon)>();
            for (int i = 0; i < moons.Length - 1; i++)
                for (int j = i + 1; j < moons.Length; j++)
                    pairs.Add((moons[i], moons[j]));

            var xStates = new HashSet<(int p1, int v1, int p2, int v2, int p3, int v3, int p4, int v4)>();
            var yStates = new HashSet<(int p1, int v1, int p2, int v2, int p3, int v3, int p4, int v4)>();
            var zStates = new HashSet<(int p1, int v1, int p2, int v2, int p3, int v3, int p4, int v4)>();

            uint time = 0;
            uint xTime = 0;
            uint yTime = 0;
            uint zTime = 0;

            while (true)
            {
                var xState = (moons[0].pos.x, moons[0].vel.x,
                            moons[1].pos.x, moons[1].vel.x,
                            moons[2].pos.x, moons[2].vel.x,
                            moons[3].pos.x, moons[3].vel.x);

                var yState = (moons[0].pos.y, moons[0].vel.y,
                            moons[1].pos.y, moons[1].vel.y,
                            moons[2].pos.y, moons[2].vel.y,
                            moons[3].pos.y, moons[3].vel.y);

                var zState = (moons[0].pos.z, moons[0].vel.z,
                            moons[1].pos.z, moons[1].vel.z,
                            moons[2].pos.z, moons[2].vel.z,
                            moons[3].pos.z, moons[3].vel.z);

                if (xTime == 0)
                    if (xStates.Contains(xState))
                        xTime = time;
                    else
                        xStates.Add(xState);

                if (yTime == 0)
                    if (yStates.Contains(yState))
                        yTime = time;
                    else
                        yStates.Add(yState);

                if (zTime == 0)
                    if (zStates.Contains(zState))
                        zTime = time;
                    else
                        zStates.Add(zState);

                if (xTime > 0 && yTime > 0 && zTime > 0)
                    break;

                // Get velocity between each pair
                foreach (var pair in pairs)
                {
                    var v1 = pair.Item1.vel;
                    var v2 = pair.Item2.vel;

                    v1.x += Math.Sign(pair.Item2.pos.x - pair.Item1.pos.x);
                    v1.y += Math.Sign(pair.Item2.pos.y - pair.Item1.pos.y);
                    v1.z += Math.Sign(pair.Item2.pos.z - pair.Item1.pos.z);

                    v2.x += Math.Sign(pair.Item1.pos.x - pair.Item2.pos.x);
                    v2.y += Math.Sign(pair.Item1.pos.y - pair.Item2.pos.y);
                    v2.z += Math.Sign(pair.Item1.pos.z - pair.Item2.pos.z);

                    pair.Item1.vel = v1;
                    pair.Item2.vel = v2;
                }

                foreach (var moon in moons)
                    moon.pos = (moon.pos.x + moon.vel.x,
                                moon.pos.y + moon.vel.y,
                                moon.pos.z + moon.vel.z);

                ++time;
            }

            long lcm = GetGCF(xTime, GetGCF(yTime, zTime));
            return lcm.ToString();
        }

        long GetGCF(long a, long b)
        {
            return a * b / GetGCD(a, b);
        }

        long GetGCD(long a, long b)
        {
            while (a != b)
                if (a < b)
                    b -= a;
                else
                    a -= b;

            return a;
        }
    }
}
