using System.Collections.Generic;

namespace Trees
{
    public class BinaryTree<T>
    {
        public T Root { get; set; }
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }

        public BinaryTree()
        {
        }

        public BinaryTree(T value, BinaryTree<T> left = null,
            BinaryTree<T> right = null)
        {
            this.Root = value;
            this.Left = left;
            this.Right = right;
        }

        public IList<T> InOrder()
        {
            IList<T> result = new List<T>();
            this.InOrder(this, result);
            return result;
        }

        private void InOrder(BinaryTree<T> currentNode, IList<T> result)
        {
            if (currentNode == null)
            {
                return;
            }

            this.InOrder(currentNode.Left, result);
            result.Add(currentNode.Root);
            this.InOrder(currentNode.Right, result);
        }

        public IList<T> PreOrder()
        {
            IList<T> result = new List<T>();
            this.PreOrder(this, result);
            return result;
        }

        private void PreOrder(BinaryTree<T> currentNode, IList<T> result)
        {
            if (currentNode == null)
            {
                return;
            }

            result.Add(currentNode.Root);
            this.PreOrder(currentNode.Left, result);
            this.PreOrder(currentNode.Right, result);
        }

        public IList<T> PostOrder()
        {
            IList<T> result = new List<T>();
            this.PostOrder(this, result);
            return result;
        }

        private void PostOrder(BinaryTree<T> currentNode, IList<T> result)
        {
            if (currentNode == null)
            {
                return;
            }

            this.PostOrder(currentNode.Left, result);
            this.PostOrder(currentNode.Right, result);
            result.Add(currentNode.Root);
        }
    }
}