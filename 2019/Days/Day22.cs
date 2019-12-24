using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

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
            /*
             * Couldn't complete part 2. Code taken from here:
             * https://github.com/mcpower/adventofcode/blob/master/2019/22/a-improved.py
             */

            var input = File.ReadAllLines("Input/22.txt").Select(s => s.Split(' '));

            BigInteger cards = 119315717514047;
            BigInteger interations = 101741582076661;

            BigInteger increment_mul = 1;
            BigInteger offset_diff = 0;

            foreach (var line in input)
            {
                if (line[0] == "cut")
                {
                    offset_diff += int.Parse(line[1]) * increment_mul;
                    offset_diff %= cards;
                }
                else if (line[2] == "increment")
                {
                    int diff = int.Parse(line[3]);
                    increment_mul *= Inverse(diff, cards);
                    increment_mul %= cards;
                }
                else if (line[2] == "new")
                {
                    increment_mul *= -1;
                    increment_mul %= cards;

                    offset_diff += increment_mul;
                    offset_diff %= cards;
                }
            }

            BigInteger increment = BigInteger.ModPow(increment_mul, interations, cards);
            BigInteger offset = offset_diff * (1 - increment) * Inverse((1 - increment_mul) % cards, cards);
            offset %= cards;

            return ((offset + 2020 * increment) % cards).ToString();
        }

        BigInteger Inverse(BigInteger n, BigInteger cards)
        {
            return BigInteger.ModPow(n, cards - 2, cards);
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
