using System;
using System.Collections.Generic;
using System.Text;

namespace Trees.Tasks
{
    public static class DeepestNode
    {
        public static void Solve<T>(Tree<T> root)
        {
            int depth = 0;
            int maxDepth = 0;
            T deepestNode = root.Root;

            FindDeepestNode(root,ref deepestNode, depth, ref maxDepth);

            Console.WriteLine(deepestNode);
        }

        private static void FindDeepestNode<T>(Tree<T> currentNode,ref T deepestNode, int currentDepth, ref int maxDepth)
        {
            if (currentNode == null)
            {
                return;
            }

            if (currentDepth > maxDepth)
            {
                deepestNode = currentNode.Root;
                maxDepth = currentDepth;
            }

            foreach (Tree<T> child in currentNode.Children)
            {
                FindDeepestNode(child, ref deepestNode, currentDepth + 1, ref maxDepth);
            }

        }
    }
}
