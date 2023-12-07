using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Card : IComparable<Card>
    {
        public string cards;
        public int bid;
        public int rank;
        private bool joker;

        public Card(string cards, int bid, bool joker)
        {
            this.cards = cards;
            this.bid = bid;
            this.joker = joker;

            Dictionary<char, int> map = new();
            for (int i = 0; i < 5; i++)
                if (!map.TryAdd(cards[i], 1))
                    map[cards[i]]++;


            int[] counts = map.Values.ToArray();

            if (joker && counts[0] != 5)
            {
                List<KeyValuePair<char, int>> a = map.ToList();
                a.Sort((a, b) => b.Value - a.Value);

                if (map.TryGetValue('J', out int J))
                {
                    for (int i = 0; i < a.Count; i++)
                        if (a[i].Key != 'J')
                        {
                            a[i] = new(a[i].Key, a[i].Value + J);
                            break;
                        }

                    counts = a
                        .Where(x => x.Key != 'J')
                        .Select(x => x.Value)
                        .ToArray();
                }
            }

            Array.Sort(counts);
            counts = counts.Reverse().ToArray();

            if (counts[0] == 5) rank = 7;
            else if (counts[0] == 4) rank = 6;
            else if (counts[0] == 3 && counts[1] == 2) rank = 5;
            else if (counts[0] == 3) rank = 4;
            else if (counts[0] == 2 && counts[1] == 2) rank = 3;
            else if (counts[0] == 2) rank = 2;
            else rank = 1;
        }

        public int CompareTo(Card other)
        {
            if (rank != other.rank)
                return rank - other.rank;
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    int a = GetValue(cards[i], joker);
                    int b = GetValue(other.cards[i], joker);

                    if (a != b)
                        return a - b;
                }
            }

            return 0;
        }

        static int GetValue(char c, bool joker)
        {
            if (int.TryParse(c.ToString(), out int result)) return result;
            else if (c == 'T') return 10;
            else if (c == 'J') return joker ? 1 : 11;
            else if (c == 'Q') return 12;
            else if (c == 'K') return 13;
            else if (c == 'A') return 14;

            return 0;
        }
    }

    class Day7 : Day
    {
        string[] lines = File.ReadAllLines("Input/7.txt");

        public override object Part1()
        {
            return Run(false);
        }

        public override object Part2()
        {
            return Run(true);
        }

        long Run(bool joker)
        {
            long count = 0;
            List<Card> cards = new();

            foreach (string line in lines)
            {
                string[] parts = line.Split(" ");
                cards.Add(new Card(parts[0], int.Parse(parts[1]), joker));
            }

            cards.Sort();

            for (int i = 0; i < cards.Count; i++)
                count += (i + 1) * (long)cards[i].bid;

            return count;
        }
    }
}
