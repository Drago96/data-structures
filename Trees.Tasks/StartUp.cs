﻿using System;
using System.Collections.Generic;
using System.Linq;
using Trees.Tasks.AStar;

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

            // BinarySearchTree<int> bst = ReadBinarySearchTree();
            // bst.EachInOrder(Console.WriteLine);

            // MaxBinaryHeap<int> heap = ReadBinaryHeap();
            // heap.Pull();

            // HeapSort<int>.Sort(new int[] { 1, 5, 3, 4, 15, -5, 2, 40, 7, -3, 8 });

            // AStarStartUp.Solve();

            RedBlackTree<int> rbt = new RedBlackTree<int>();
            rbt.Insert(5);
            rbt.Insert(12);
            rbt.Insert(18);
            rbt.Insert(37);
            rbt.Insert(48);
            rbt.Insert(60);
            rbt.Insert(80);

            Console.WriteLine(rbt.Search(12).Count());
            Console.WriteLine(rbt.Search(60).Count());
        }

        private static MaxBinaryHeap<int> ReadBinaryHeap()
        {
            int[] nodes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            MaxBinaryHeap<int> result = new MaxBinaryHeap<int>();

            foreach (int t in nodes)
            {
                result.Insert(t);
            }

            return result;
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
