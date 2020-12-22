using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day22 : Day
    {
        string[] lines = File.ReadAllLines("Input/22.txt").ToArray();

        void GetCards(Queue<int> player1, Queue<int> player2)
        {
            bool player1sCards = true;
            foreach (var line in lines)
                if (line.Contains("Player 1") || line.Length == 0)
                    continue;
                else if (line.Contains("Player 2"))
                    player1sCards = false;
                else if (player1sCards)
                    player1.Enqueue(int.Parse(line));
                else
                    player2.Enqueue(int.Parse(line));
        }

        public override object Part1()
        {
            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();

            GetCards(player1, player2);

            while (player1.Count > 0 && player2.Count > 0)
            {
                int p1Card = player1.Dequeue();
                int p2Card = player2.Dequeue();
                if (p1Card > p2Card)
                {
                    player1.Enqueue(p1Card);
                    player1.Enqueue(p2Card);
                }
                else
                {
                    player2.Enqueue(p2Card);
                    player2.Enqueue(p1Card);
                }
            }

            Queue<int> q = player1.Count > 0 ? player1 : player2;
            return Enumerable.Range(1, q.Count).Reverse().Zip(q).Sum(pair => pair.First * pair.Second);
        }


        bool PlayRound(Queue<int> player1, Queue<int> player2, bool top)
        {
            HashSet<string> prevStates = new HashSet<string>();

            while (player1.Count > 0 && player2.Count > 0)
            {
                string state = string.Join(',', player1.ToArray()) + '-' + string.Join(',', player2.ToArray());
                if (prevStates.Contains(state))
                    return true;
                else
                    prevStates.Add(state);

                int p1Card = player1.Dequeue();
                int p2Card = player2.Dequeue();

                bool p1Wins;
                if (player1.Count >= p1Card && player2.Count >= p2Card)
                    p1Wins = PlayRound(new Queue<int>(player1.Take(p1Card)), new Queue<int>(player2.Take(p2Card)), false);
                else
                    p1Wins = p1Card > p2Card;

                if (p1Wins)
                {
                    player1.Enqueue(p1Card);
                    player1.Enqueue(p2Card);
                }
                else
                {
                    player2.Enqueue(p2Card);
                    player2.Enqueue(p1Card);
                }
            }

            if (top)
            {
                Queue<int> q = player1.Count > 0 ? player1 : player2;
                long sum = Enumerable.Range(1, q.Count).Reverse().Zip(q).Sum(pair => pair.First * pair.Second);
                Console.WriteLine(sum);
            }

            return player1.Count > 0;
        }

        public override object Part2()
        {
            Queue<int> player1 = new Queue<int>();
            Queue<int> player2 = new Queue<int>();

            GetCards(player1, player2);
            PlayRound(player1, player2, true);

            return 0;
        }
    }
}
