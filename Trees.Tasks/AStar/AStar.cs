using AStar;
using System;
using System.Collections.Generic;

namespace Trees.Tasks.AStar
{
    public class AStar
    {
        private readonly char[,] map;

        public AStar(char[,] map)
        {
            this.map = map;
        }

        public IEnumerable<Node> GetPath(Node start, Node goal)
        {
            var cost = new Dictionary<Node, int>();
            var parents = new Dictionary<Node, Node>();
            var visited = new HashSet<Node>();

            var queue = new MinPriorityQueue<Node>();

            cost[start] = 0;
            parents[start] = null;
            queue.Enqueue(start);

            bool foundPath = false;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visited.Add(current);

                if (current.Equals(goal))
                {
                    foundPath = true;
                    break;
                }

                var neighbours = this.GetNeighbours(current);
                foreach (var neighbour in neighbours)
                {
                    if (visited.Contains(neighbour) || this.IsWall(neighbour))
                    {
                        continue;
                    }

                    var newCost = cost[current] + 1;

                    if (!cost.ContainsKey(neighbour) || newCost < cost[neighbour])
                    {
                        cost[neighbour] = newCost;
                        neighbour.F = newCost + this.GetH(neighbour, goal);
                        queue.Enqueue(neighbour);
                        parents[neighbour] = current;
                    }
                }
            }

            if (!foundPath)
            {
                throw new InvalidOperationException("No path found.");
            }

            return this.GetPath(parents, goal);
        }

        private bool IsWall(Node neighbour) => this.map[neighbour.Row, neighbour.Col] == 'W';

        private IEnumerable<Node> GetNeighbours(Node current)
        {
            var directions = new int[] { -1, 1 };
            var neighbours = new List<Node>();

            foreach (int direction in directions)
            {
                int newRow = current.Row + direction;
                int newCol = current.Col;
                if (this.InBounds(newRow, newCol))
                {
                    var node = new Node(newRow, newCol);
                    neighbours.Add(node);
                }

                newRow = current.Row;
                newCol = current.Col + direction;
                if (this.InBounds(newRow, newCol))
                {
                    var node = new Node(newRow, newCol);
                    neighbours.Add(node);
                }
            }

            return neighbours;
        }

        private bool InBounds(int row, int col)
        {
            var rowsCount = this.map.GetLength(0);
            var colsCount = this.map.GetLength(1);

            return row >= 0 && row < rowsCount && col >= 0 && col < colsCount;
        }

        private IEnumerable<Node> GetPath(Dictionary<Node, Node> parents, Node goal)
        {
            Stack<Node> result = new Stack<Node>();

            Node current = goal;

            while (current != null)
            {
                result.Push(current);
                current = parents[current];
            }

            return result.ToArray();
        }

        private int GetH(Node current, Node goal)
        {
            var deltaY = Math.Abs(current.Row - goal.Row);
            var deltaX = Math.Abs(current.Col - goal.Col);

            return deltaX + deltaY;
        }
    }
}