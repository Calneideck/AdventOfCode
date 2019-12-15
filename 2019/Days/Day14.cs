using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day14 : Day
    {
        private Dictionary<string, (string, long)[]> reactions = new Dictionary<string, (string, long)[]>();
        private Dictionary<string, long> outputs = new Dictionary<string, long>();
        private Dictionary<string, long> requirements = new Dictionary<string, long>() { { "ORE", 0 } };
        private Dictionary<string, long> leftovers = new Dictionary<string, long>() { { "ORE", 0 } };

        public override string Part1()
        {
            var text = File.ReadAllLines("Input/14.txt");
            foreach (var line in text)
            {
                var eq = line.Split("=>").Select(s => s.Trim()).ToArray();
                var ins = eq[0].Split(',').Select(s => GetChem(s.Trim())).ToArray();
                var output = GetChem(eq[1]);

                reactions.Add(output.Item1, ins);
                outputs.Add(output.Item1, output.Item2);
                requirements.Add(output.Item1, 0);
                leftovers.Add(output.Item1, 0);
            }

            (string, long) currentChem = ("FUEL", 1);
            GetAmounts(currentChem, 1);

            return requirements["ORE"].ToString();
        }

        public override string Part2()
        {
            long minFuel = 1000;
            long maxFuel = (long)1e12;

            while (minFuel < maxFuel)
            {
                long fuel = (minFuel + maxFuel + 1) / 2;

                foreach (var key in requirements.Keys.ToList())
                    requirements[key] = 0;

                foreach (var key in outputs.Keys.ToList())
                    leftovers[key] = 0;

                (string, long) currentChem = ("FUEL", 1);
                GetAmounts(currentChem, fuel);

                long ore = requirements["ORE"];

                if (ore < (long)1e12)
                    minFuel = fuel;
                else
                    maxFuel = fuel - 1;
            }

            return minFuel.ToString();
        }

        (string, long) GetChem(string s)
        {
            long amount = long.Parse(s.Split(' ')[0]);
            string chem = s.Split(' ')[1];
            return (chem, amount);
        }

        void GetAmounts((string, long) chem, long req)
        {
            while (leftovers[chem.Item1] > 0 && req > 0)
            {
                req--;
                leftovers[chem.Item1]--;
            }

            long amount = (long)Math.Ceiling(req / (double)chem.Item2);
            leftovers[chem.Item1] += amount * chem.Item2 - req;

            if (reactions.TryGetValue(chem.Item1, out var inputs))
                foreach (var input in inputs)
                {
                    long newAmount = amount * input.Item2;
                    requirements[input.Item1] += newAmount;

                    if (input.Item1 != "ORE")
                    {
                        outputs.TryGetValue(input.Item1, out long output);
                        GetAmounts((input.Item1, output), newAmount);
                    }
                }
        }
    }
}
