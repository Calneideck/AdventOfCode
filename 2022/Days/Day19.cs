using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day19 : Day
    {
        string[] lines = File.ReadAllLines("Input/19.txt");

        class BP
        {
            public int id;
            public int ore;
            public int clay;
            public V2 obsidian;
            public V2 geode;

            public BP(int id, int ore, int clay, V2 obsidian, V2 geode)
            {
                this.id = id;
                this.ore = ore;
                this.clay = clay;
                this.obsidian = obsidian;
                this.geode = geode;
            }
        }

        List<BP> blueprints = new();
        Dictionary<State, int> cache = new();

        struct State
        {
            public int timeLeft;
            public int ore;
            public int clay;
            public int obsidian;
            public int oreRobo;
            public int clayRobo;
            public int obRobo;

            public State(int timeLeft, int ore, int clay, int obsidian, int oreRobo, int clayRobo, int obRobo)
            {
                this.timeLeft = timeLeft;
                this.ore = ore;
                this.clay = clay;
                this.obsidian = obsidian;
                this.oreRobo = oreRobo;
                this.clayRobo = clayRobo;
                this.obRobo = obRobo;
            }

            public override bool Equals(object obj)
            {
                return obj is State state &&
                       timeLeft == state.timeLeft &&
                       ore == state.ore &&
                       clay == state.clay &&
                       obsidian == state.obsidian &&
                       oreRobo == state.oreRobo &&
                       clayRobo == state.clayRobo &&
                       obRobo == state.obRobo;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(timeLeft, ore, clay, obsidian, oreRobo, clayRobo, obRobo);
            }
        }

        int Attempt(BP b, State s)
        {
            if (cache.ContainsKey(s))
                return cache[s];

            if (s.timeLeft <= 1)
                return 0;

            bool canBuildGeode = s.ore / b.geode.x >= 1 && s.obsidian / b.geode.y >= 1;
            bool canBuildOb = s.ore / b.obsidian.x >= 1 && s.clay / b.obsidian.y >= 1 && s.obRobo < b.geode.y;
            bool canBuildClay = s.ore / b.clay >= 1 && s.clayRobo < b.obsidian.y;
            bool canBuildOre = s.ore / b.ore >= 1 && s.oreRobo < Math.Max(b.clay, Math.Max(b.obsidian.x, b.geode.x));

            int newGeodes = 0;

            if (canBuildGeode)
            {
                newGeodes = Math.Max(
                    newGeodes,
                    Attempt(
                        b, new State(s.timeLeft - 1, s.ore + s.oreRobo - b.geode.x, s.clay + s.clayRobo, s.obsidian + s.obRobo - b.geode.y, s.oreRobo, s.clayRobo, s.obRobo)
                    ) + s.timeLeft - 1
                );
            }
            else
            {
                if (canBuildOb)
                {
                    newGeodes = Math.Max(
                        newGeodes,
                        Attempt(
                            b, new State(s.timeLeft - 1, s.ore + s.oreRobo - b.obsidian.x, s.clay + s.clayRobo - b.obsidian.y, s.obsidian + s.obRobo, s.oreRobo, s.clayRobo, s.obRobo + 1)
                        )
                    );
                }
                if (canBuildClay)
                {
                    newGeodes = Math.Max(
                        newGeodes,
                        Attempt(
                            b, new State(s.timeLeft - 1, s.ore + s.oreRobo - b.clay, s.clay + s.clayRobo, s.obsidian + s.obRobo, s.oreRobo, s.clayRobo + 1, s.obRobo)
                        )
                    );
                }
                if (canBuildOre)
                {
                    newGeodes = Math.Max(
                        newGeodes,
                        Attempt(
                            b, new State(s.timeLeft - 1, s.ore + s.oreRobo - b.ore, s.clay + s.clayRobo, s.obsidian + s.obRobo, s.oreRobo + 1, s.clayRobo, s.obRobo)
                        )
                    );
                }
                newGeodes = Math.Max(
                    newGeodes,
                    Attempt(
                        b, new State(s.timeLeft - 1, s.ore + s.oreRobo, s.clay + s.clayRobo, s.obsidian + s.obRobo, s.oreRobo, s.clayRobo, s.obRobo)
                    )
                );
            }


            cache.Add(s, newGeodes);
            return newGeodes;
        }

        public override object Part1()
        {
            int quality = 0;
            foreach (string line in lines)
            {
                int[] m = new Regex(@"(\d+)").Matches(line).Select(a => int.Parse(a.Value)).ToArray();
                blueprints.Add(new(m[0], m[1], m[2], new V2(m[3], m[4]), new V2(m[5], m[6])));
            }

            foreach (BP b in blueprints)
            {
                int amount = Attempt(b, new State(24, 0, 0, 0, 1, 0, 0));
                cache.Clear();
                quality += b.id * amount;
            }

            return quality;
        }

        public override object Part2()
        {
            int quality = 1;
            foreach (string line in lines)
            {
                int[] m = new Regex(@"(\d+)").Matches(line).Select(a => int.Parse(a.Value)).ToArray();
                blueprints.Add(new(m[0], m[1], m[2], new V2(m[3], m[4]), new V2(m[5], m[6])));
            }

            foreach (BP b in blueprints.Take(3))
            {
                int amount = Attempt(b, new State(32, 0, 0, 0, 1, 0, 0));
                cache.Clear();
                quality *= amount;
            }

            return quality;
        }
    }
}
