using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Day20 : Day
    {
        class Node
        {
            public V2 pos;
            public char c;
            public string name;
            public Node[] neighbours;
            public Node other;
            public Node exit;
            public bool outSide;
        }

        List<Node> nodes = new List<Node>();
        Node startNode;

        public override string Part1()
        {
            // Get map
            var lines = File.ReadAllLines("Input/20.txt");
            for (int y = 0; y < lines.Length; y++)
            {
                var chars = lines[y].ToCharArray();
                for (int x = 0; x < chars.Length; x++)
                    if (chars[x] == '.' || chars[x] >= 'A' && chars[x] <= 'Z')
                        nodes.Add(new Node()
                        {
                            pos = new V2(x, y),
                            c = chars[x]
                        });
            }

            foreach (var node in nodes)
            {
                // Get list of neighbours
                List<Node> neighbours = new List<Node>();
                foreach (var dir in V2.Directions)
                {
                    Node neighbour = nodes.Find(n => n.pos == node.pos + dir);
                    if (neighbour != null)
                        neighbours.Add(neighbour);
                }
                node.neighbours = neighbours.ToArray();

                // Get list of portals
                if (node.c >= 'A' && node.c <= 'Z')
                {
                    bool start = false;
                    char other = 'x';
                    V2 otherDir = V2.Zero;
                    Node exitNode = null;

                    foreach (var neighbour in neighbours)
                        if (neighbour.c == '.')
                        {
                            start = true;
                            exitNode = neighbour;
                        }
                        else
                        {
                            other = neighbour.c;
                            otherDir = neighbour.pos - node.pos;
                        }

                    if (start)
                    {
                        node.exit = exitNode;
                        node.name = (otherDir == V2.Up || otherDir == V2.Left) ? other.ToString() + node.c : node.c.ToString() + other;
                        if (node.pos.x == 1 || node.pos.x == lines[0].Length - 2)
                            node.outSide = true;
                        else if (node.pos.y == 1 || node.pos.y == lines.Length - 2)
                            node.outSide = true;
                    }
                }
            }

            startNode = nodes.Find(n => n.name == "AA").exit;

            // Connect portals
            foreach (var node in nodes)
                if (node.name != null)
                {
                    Node other = nodes.Find(n => n.name == node.name && n != node);
                    if (other != null)
                        node.other = other;
                }

            int steps = FindPath(startNode);

            return steps.ToString();
        }

        public override string Part2()
        {
            int steps = FindPathPart2(startNode);
            return steps.ToString();
        }

        int FindPath(Node startNode)
        {
            var open = new Queue<(int steps, Node node)>();
            var closed = new HashSet<Node>();

            open.Enqueue((0, startNode));

            while (open.Any())
            {
                var currentNode = open.Dequeue();
                closed.Add(currentNode.node);

                foreach (var nextNode in currentNode.node.neighbours)
                {
                    if (closed.Contains(nextNode))
                        continue;

                    if (nextNode.c == '.')
                        open.Enqueue((currentNode.steps + 1, nextNode));
                    else if (nextNode.name == "ZZ")
                        return currentNode.steps;
                    else if (nextNode.other != null && !closed.Contains(nextNode.other.exit))
                        open.Enqueue((currentNode.steps + 1, nextNode.other.exit));
                }
            }

            return -1;
        }

        int FindPathPart2(Node startNode)
        {
            var open = new Queue<(int steps, int depth, Node node)>();
            var closed = new HashSet<(int depth, Node node)>();

            open.Enqueue((0, 0, startNode));

            while (open.Any())
            {
                var currentNode = open.Dequeue();
                closed.Add((currentNode.depth, currentNode.node));

                foreach (var nextNode in currentNode.node.neighbours)
                    if (nextNode.c == '.')
                    {
                        if (!closed.Contains((currentNode.depth, nextNode)))
                            open.Enqueue((currentNode.steps + 1, currentNode.depth, nextNode));
                    }
                    else if (nextNode.name == "ZZ" && currentNode.depth == 0)
                        return currentNode.steps;
                    else
                    {
                        if (nextNode.outSide && currentNode.depth == 0 || nextNode.other == null)
                            continue;

                        int nextDepth = currentNode.depth + (nextNode.outSide ? -1 : 1);
                        if (!closed.Contains((nextDepth, nextNode.other.exit)))
                            open.Enqueue((currentNode.steps + 1, nextDepth, nextNode.other.exit));
                    }
            }

            return -1;
        }
    }
}
