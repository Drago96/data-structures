using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trees.Tasks
{
    public static class SubtreesWithGivenSum
    {
        public static void Solve(Tree<int> root)
        {
            int sum = int.Parse(Console.ReadLine());
            IList<Tree<int>> trees = new List<Tree<int>>();

            FindTreesWithSum(root, sum, trees);

            trees.ToList().ForEach(t => t.Print());

        }

        private static void FindTreesWithSum(Tree<int> root, int sum, IList<Tree<int>> trees)
        {
            int sumCurrent = FindSumOfTree(root);

            if (sumCurrent == sum)
            {
                trees.Add(root);
            }

            root.Children.ToList().ForEach(t => FindTreesWithSum(t, sum, trees));
        }

        private static int FindSumOfTree(Tree<int> root)
            => root.Root + root.Children.Sum(FindSumOfTree);

    }
}
