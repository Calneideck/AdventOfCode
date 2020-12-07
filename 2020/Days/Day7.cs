using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{

    struct Bag
    {
        public string colour;
        public string adj;

        public Bag(string adj, string colour)
        {
            this.colour = colour;
            this.adj = adj;
        }
    }

    class Day7 : Day
    {
        string[] lines = File.ReadAllLines("Input/7.txt");
        List<Bag> bags = new List<Bag>();
        Dictionary<Bag, List<(int, Bag)>> allBags = new Dictionary<Bag, List<(int, Bag)>>();

        Regex r = new Regex(@"(\d) (\w+) (\w+)", RegexOptions.Multiline);

        bool GoInBag(Bag bag)
        {
            var amounts = allBags[bag];
            foreach (var amount in amounts)
            {
                if (amount.Item2.colour == "gold" && amount.Item2.adj == "shiny")
                    return true;
                else if (GoInBag(amount.Item2))
                    return true;
            }

            return false;
        }

        int SumBag(Bag bag)
        {
            var amounts = allBags[bag];
            return amounts.Sum(b => b.Item1 + b.Item1 * SumBag(b.Item2));
        }

        public override object Part1()
        {
            foreach (var line in lines)
            {
                var temp = line.Split("contain ");

                var bagType = temp[0].Split(' ');
                bags.Add(new Bag(bagType[0], bagType[1]));

                List<(int, Bag)> amounts = new List<(int, Bag)>();
                var quants = temp[1].Split(',');
                foreach (var quant in quants)
                {
                    if (quant.StartsWith("no other"))
                        continue;

                    var match = r.Match(quant);
                    amounts.Add((int.Parse(match.Groups[1].ToString()), new Bag(match.Groups[2].ToString(), match.Groups[3].ToString())));
                }

                allBags.Add(new Bag(bagType[0], bagType[1]), amounts);
            }

            return bags.Count((b) => GoInBag(b));
        }

        public override object Part2()
        {
            var gold = new Bag("shiny", "gold");
            return SumBag(gold);
        }
    }
}
