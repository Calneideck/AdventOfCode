using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day23 : Day
    {
        string[] lines = File.ReadAllLines("Input/23.txt").ToArray();

        public override object Part1()
        {
            Dictionary<int, LinkedListNode<int>> positions = new Dictionary<int, LinkedListNode<int>>();
            LinkedList<int> circle = new LinkedList<int>(lines[0].ToCharArray().Select(c => int.Parse(c.ToString())));

            LinkedListNode<int> node = circle.First;
            while (node != null)
            {
                positions.Add(node.Value, node);
                node = node.Next;
            }

            var currentCup = circle.First;
            Console.WriteLine();

            for (int i = 0; i < 100; i++)
            {
                List<int> hand = new List<int>();

                for (int x = 1; x <= 3; x++)
                {
                    hand.Add((currentCup.Next ?? circle.First).Value);
                    circle.Remove(currentCup.Next ?? circle.First);
                }

                int nextLabel = currentCup.Value - 1;
                if (nextLabel == 0)
                    nextLabel = 9;

                while (hand.Contains(nextLabel))
                {
                    nextLabel--;
                    if (nextLabel == 0)
                        nextLabel = 9;
                }

                var nextNode = positions[nextLabel].Next ?? circle.First;
                for (int x = 0; x < 3; x++)
                {
                    circle.AddBefore(nextNode, hand[x]);
                    positions[hand[x]] = nextNode.Previous;
                }

                currentCup = currentCup.Next ?? circle.First;
            }

            node = positions[1];
            StringBuilder sb = new StringBuilder();
            for (int j = 1; j < circle.Count; j++)
            {
                node = node.Next ?? circle.First;
                sb.Append(node.Value.ToString());
            }

            return sb.ToString();
        }

        public override object Part2()
        {
            Dictionary<int, LinkedListNode<int>> positions = new Dictionary<int, LinkedListNode<int>>();
            LinkedList<int> circle = new LinkedList<int>(lines[0].ToCharArray().Select(c => int.Parse(c.ToString())));
            for (int i = 10; i <= 1000000; i++)
                circle.AddLast(i);

            LinkedListNode<int> node = circle.First;
            while (node != null)
            {
                positions.Add(node.Value, node);
                node = node.Next;
            }

            var currentCup = circle.First;
            Console.WriteLine();

            for (int i = 0; i < 10000000; i++)
            {
                List<int> hand = new List<int>();

                for (int x = 1; x <= 3; x++)
                {
                    hand.Add((currentCup.Next ?? circle.First).Value);
                    circle.Remove(currentCup.Next ?? circle.First);
                }

                int nextLabel = currentCup.Value - 1;
                if (nextLabel == 0)
                    nextLabel = 1000000;

                while (hand.Contains(nextLabel))
                {
                    nextLabel--;
                    if (nextLabel == 0)
                        nextLabel = 1000000;
                }

                var nextNode = positions[nextLabel].Next ?? circle.First;
                for (int x = 0; x < 3; x++)
                {
                    circle.AddBefore(nextNode, hand[x]);
                    positions[hand[x]] = nextNode.Previous;
                }

                currentCup = currentCup.Next ?? circle.First;
            }

            node = positions[1];
            var n1 = node.Next ?? circle.First;
            var n2 = n1.Next ?? circle.First;
            return (long)n1.Value * n2.Value;
        }
    }
}
