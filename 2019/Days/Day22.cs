using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Day22 : Day
    {
        public override string Part1()
        {
            var input = File.ReadAllLines("Input/22.txt").Select(s => s.Split(' '));

            List<long> deck = new List<long>();
            for (long i = 0; i < 10007; i++)
                deck.Add(i);

            foreach (var line in input)
            {
                if (line[0] == "cut")
                    deck = CutDeck(deck, long.Parse(line[1]));
                else if (line[2] == "increment")
                    deck = DealIncrement(deck, long.Parse(line[3]));
                else if (line[2] == "new")
                    deck.Reverse();
            }

            return deck.FindIndex(c => c == 2019).ToString();
        }

        public override string Part2()
        {
            var input = File.ReadAllLines("Input/22.txt").Select(s => s.Split(' '));

            List<long> deck = new List<long>();
            for (long i = 0; i < 5000; i++)
                deck.Add(i);

            foreach (var line in input)
            {
                if (line[0] == "cut")
                    deck = CutDeck(deck, long.Parse(line[1]));
                else if (line[2] == "increment")
                    deck = DealIncrement(deck, long.Parse(line[3]));
                else if (line[2] == "new")
                    deck.Reverse();
            }

            return deck[2020].ToString();
        }

        List<long> CutDeck(List<long> deck, long count)
        {
            List<long> newDeck;
            if (count > 0)
            {
                newDeck = new List<long>(deck.Skip((int)count));
                var cut = deck.Take((int)count);
                newDeck.AddRange(cut);
            }
            else
            {
                count = Math.Abs(count);
                newDeck = new List<long>(deck.Take(deck.Count() - (int)count));
                var cut = deck.TakeLast((int)count);
                newDeck.InsertRange(0, cut);
            }

            return newDeck;
        }

        List<long> DealIncrement(List<long> deck, long n)
        {
            long count = deck.LongCount();
            List<long> newDeck = new List<long>(deck);

            long index = 0;
            long i = 0;

            while (i < count)
            {
                newDeck[(int)index] = deck[(int)i++];
                index = (index + n) % count;
            }

            return newDeck;
        }
    }
}
