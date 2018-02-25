using System;
using System.Collections.Generic;
using System.Linq;

namespace Trees.Tasks
{
    public class StartUp
    {
        private static readonly IDictionary<int, Tree<int>> nodes = new Dictionary<int, Tree<int>>();

        public static void Main(string[] args)
        {
            // ReadTree();
            // Tree<int> rootNode = nodes.Values.First();

            // DeepestNode.Solve(rootNode);
            // SubtreesWithGivenSum.Solve(rootNode);
            // PathsWithGivenSum.Solve(rootNode);

            BinarySearchTree<int> bst = ReadBinarySearchTree();
            bst.Range(5,15).ToList().ForEach(Console.WriteLine);
        }

        private static BinarySearchTree<int> ReadBinarySearchTree()
        {
            int[] nodes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            BinarySearchTree<int> result = new BinarySearchTree<int>();

            foreach (int t in nodes)
            {
                result.Insert(t);
            }

            return result;
        }


        /// <summary>
        /// Builds a Tree from console input.
        /// <para/>First line takes the node count of the tree.
        /// <para/>Each of the next n lines takes two parameters -
        /// the parent and the child of the given edge and
        /// builds a tree accordingly. 
        /// </summary>
        /// <example>
        /// This example input:
        /// 
        /// 3
        /// 1 2
        /// 1 3
        /// 2 4
        /// 
        /// will build the following tree:
        /// 
        ///     1
        ///    / \
        ///   2   3
        ///  /
        /// 4
        /// </example>
        private static void ReadTree()
        {
            int nodeCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < nodeCount - 1; i++)
            {
                int[] edge = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                if (!nodes.ContainsKey(edge[0]))
                {
                    nodes[edge[0]] = new Tree<int>(edge[0]);
                }

                if (!nodes.ContainsKey(edge[1]))
                {
                    nodes[edge[1]] = new Tree<int>(edge[1]);
                }

                nodes[edge[0]].Children.Add(nodes[edge[1]]);
            }
        }
    }
}
