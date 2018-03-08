using System;
using System.Collections.Generic;
using System.Linq;

namespace Trees.Tasks
{
    public static class PathsWithGivenSum
    {
        public static void Solve(Tree<int> root)
        {
            int sum = int.Parse(Console.ReadLine());
            IList<IList<int>> paths = new List<IList<int>>();
            Stack<int> currentPath = new Stack<int>();
            FindPaths(root, sum, paths, currentPath);
        }

        private static void FindPaths(Tree<int> root, int sum, IList<IList<int>> paths, Stack<int> currentPath)
        {
            if (root == null)
            {
                return;
            }

            currentPath.Push(root.Root);

            if (currentPath.Sum() == sum)
            {
                paths.Add(currentPath.Reverse().ToList());
            }

            foreach (Tree<int> child in root.Children)
            {
                FindPaths(child, sum, paths, currentPath);
            }

            currentPath.Pop();
        }
    }
}