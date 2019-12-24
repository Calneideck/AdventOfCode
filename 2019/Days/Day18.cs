using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day18 : Day
    {
        public class Node
        {
            public V2 pos;
            public char tile;
            public bool isKey;
            public bool isDoor;
            public List<Node> path;
        }

        struct State
        {
            public V2[] positions;
            public char[] keys;

            public State(V2[] positions, char[] keys)
            {
                this.positions = positions;
                this.keys = keys;
            }

            public override bool Equals(object obj)
            {
                var other = (State)obj;
                return positions.SequenceEqual(other.positions) && keys.SequenceEqual(other.keys);
            }
        }

        private List<Node> nodes = new List<Node>();
        private List<Node> keyNodes = new List<Node>();
        private List<char> allKeys = new List<char>();
        private Dictionary<(int robot, V2 start, V2 end), (int steps, char[] reqKeys, char[] passesKeys)> paths = new Dictionary<(int robot, V2 start, V2 end), (int steps, char[] reqKeys, char[] passesKeys)>();
        private Dictionary<State, int> seenPaths = new Dictionary<State, int>();

        public override string Part1()
        {
            var input = File.ReadAllLines("Input/18.txt");
            Node startNode = null;
            int y = 0;

            foreach (var line in input)
            {
                int x = 0;
                var c = line.ToCharArray();

                foreach (var tile in c)
                {
                    Node n = new Node()
                    {
                        pos = new V2(x, y),
                        tile = tile,
                        isDoor = tile >= 'A' && tile <= 'Z',
                        isKey = tile >= 'a' && tile <= 'z',
                        path = new List<Node>()
                    };

                    nodes.Add(n);

                    if (tile == '@')
                        startNode = n;

                    if (n.isKey)
                        allKeys.Add(n.tile);

                    x++;
                }

                y++;
            }

            Console.WriteLine();

            keyNodes = nodes.Where(n => n.isKey).ToList();
            FindPaths(0, startNode);
            Console.WriteLine("Paths found.");

            int steps = ProcessPathSingle(startNode.pos, new char[0], 0);

            return steps.ToString();
        }

        public override string Part2()
        {
            nodes.Clear();
            paths.Clear();
            seenPaths.Clear();

            var input = File.ReadAllLines("Input/18b.txt");
            var robots = new List<Node>();

            int y = 0;
            foreach (var line in input)
            {
                int x = 0;
                var c = line.ToCharArray();

                foreach (var tile in c)
                {
                    Node n = new Node()
                    {
                        pos = new V2(x, y),
                        tile = tile,
                        isDoor = tile >= 'A' && tile <= 'Z',
                        isKey = tile >= 'a' && tile <= 'z',
                        path = new List<Node>()
                    };

                    nodes.Add(n);

                    if (tile == '@')
                        robots.Add(n);

                    if (n.isKey)
                        allKeys.Add(n.tile);

                    x++;
                }

                y++;
            }

            Console.WriteLine();

            keyNodes = nodes.Where(n => n.isKey).ToList();
            for (int i = 0; i < robots.Count; i++)
                FindPaths(i, robots[i]);

            Console.WriteLine("Paths found.");

            int steps = ProcessPath(robots.Select(r => r.pos).ToArray(), new char[0], 0);

            return steps.ToString();
        }


        int ProcessPathSingle(V2 pos, char[] keys, int steps)
        {
            var remainingKeyNodes = GetReachableKeys(0, pos, keys);
            if (remainingKeyNodes.Any())
            {
                var state = new State(new V2[] { pos }, keys);
                if (seenPaths.TryGetValue(state, out int newSteps))
                {
                }
                else
                {
                    newSteps = remainingKeyNodes.Min(keyNode => ProcessPathSingle(keyNode.pos, keys.Append(keyNode.tile).OrderBy(c => c).ToArray(), paths[(0, pos, keyNode.pos)].steps));
                    seenPaths.Add(state, newSteps);
                }

                return steps + newSteps;
            }

            return steps;
        }

        int ProcessPath(V2[] positions, char[] keys, int steps)
        {
            int bestSteps = int.MaxValue;
            bool found = false;

            for (int i = 0; i < positions.Length; i++)
            {
                var remainingKeyNodes = GetReachableKeys(i, positions[i], keys);
                if (remainingKeyNodes.Any())
                {
                    if (seenPaths.TryGetValue(new State(positions, keys), out int newSteps))
                    { }
                    else
                    {
                        newSteps = remainingKeyNodes.Min(keyNode => ProcessPath(SetPositions(positions, i, keyNode.pos), keys.Append(keyNode.tile).OrderBy(c => c).ToArray(), paths[(i, positions[i], keyNode.pos)].steps));
                        seenPaths.Add(new State(positions, keys), newSteps);
                    }

                    if (newSteps < bestSteps)
                        bestSteps = newSteps;

                    found = true;
                }
            }

            return steps + (found ? bestSteps : 0);
        }

        V2[] SetPositions(V2[] array, int index, V2 value)
        {
            V2[] newArray = (V2[])array.Clone();
            newArray[index] = value;
            return newArray;
        }

        void FindPaths(int robot, Node startNode)
        {
            HashSet<char> doneKeys = new HashSet<char>();
            var dirs = V2.Directions;

            keyNodes.Insert(0, startNode);

            foreach (Node n in keyNodes)
            {
                HashSet<Node> closed = new HashSet<Node>();
                Queue<Node> open = new Queue<Node>();
                open.Enqueue(n);

                if (n.isKey)
                    doneKeys.Add(n.tile);

                foreach (var node in nodes)
                    node.path.Clear();

                while (open.Any())
                {
                    var currentNode = open.Dequeue();
                    closed.Add(currentNode);

                    foreach (var dir in dirs)
                    {
                        Node nextNode = nodes.Find(n => n.pos == currentNode.pos + dir);
                        if (nextNode != null && nextNode.tile != '#' && !closed.Contains(nextNode) && !open.Contains(nextNode))
                        {
                            nextNode.path.AddRange(currentNode.path);
                            nextNode.path.Add(currentNode);

                            if (nextNode.isKey && !doneKeys.Contains(nextNode.tile))
                            {
                                int steps = nextNode.path.Count;
                                char[] reqKeys = nextNode.path.Where(n => n.isDoor).Select(n => n.tile.ToString().ToLower()[0]).ToArray();
                                char[] passesKeys = nextNode.path.Where(node => node.isKey && n != node).Select(node => node.tile).ToArray();
                                paths.Add((robot, n.pos, nextNode.pos), (steps, reqKeys, passesKeys));
                                paths.Add((robot, nextNode.pos, n.pos), (steps, reqKeys, passesKeys));
                            }

                            open.Enqueue(nextNode);
                        }
                    }
                }
            }

            keyNodes.RemoveAt(0);
        }

        List<Node> GetReachableKeys(int robot, V2 pos, char[] keys)
        {
            List<Node> reachableKeys = new List<Node>();

            foreach (var n in keyNodes)
            {
                if (n.pos == pos || keys.Contains(n.tile))
                    continue;

                if (!paths.TryGetValue((robot, pos, n.pos), out var path))
                    continue;

                if (path.passesKeys.Except(keys).Any())
                    continue;

                bool hasKeys = true;
                foreach (var reqKey in path.reqKeys)
                    if (!keys.Contains(reqKey))
                    {
                        hasKeys = false;
                        break;
                    }

                if (hasKeys)
                    reachableKeys.Add(n);
            }

            return reachableKeys;
        }
    }
}
