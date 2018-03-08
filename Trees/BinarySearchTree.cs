using System;
using System.Collections.Generic;

namespace Trees
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Right = null;
                this.Left = null;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }
        }

        private Node Root { get; set; }

        public void Insert(T value)
        {
            if (this.Root == null)
            {
                this.Root = new Node(value);
                return;
            }
            this.Insert(value, this.Root);
        }

        private void Insert(T value, Node node)
        {
            if (node.Value.CompareTo(value) > 0)
            {
                if (node.Left == null)
                {
                    node.Left = new Node(value);
                }
                this.Insert(value, node.Left);
            }
            else if (node.Value.CompareTo(value) < 0)
            {
                if (node.Right == null)
                {
                    node.Right = new Node(value);
                }
                this.Insert(value, node.Right);
            }
        }

        public bool Search(T value) => Search(value, this.Root);

        private bool Search(T value, Node node)
        {
            if (node == null)
            {
                return false;
            }

            if (node.Value.Equals(value))
            {
                return true;
            }

            if (node.Value.CompareTo(value) > 0)
            {
                return this.Search(value, node.Left);
            }

            return this.Search(value, node.Right);
        }

        public void EachInOrder(Action<T> action) => this.EachInOrder(action, this.Root);

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(action, node.Left);
            action(node.Value);
            this.EachInOrder(action, node.Right);
        }

        public T DeleteMin()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
            if (this.Root.Left == null)
            {
                T result = this.Root.Value;
                this.Root = this.Root.Right;
                return result;
            }

            return this.DeleteMin(this.Root);
        }

        private T DeleteMin(Node node)
        {
            if (node.Left.Left == null)
            {
                T result = node.Value;
                node.Left = node.Left.Right;
                return result;
            }

            return this.DeleteMin(node.Left);
        }

        public T DeleteMax()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
            if (this.Root.Right == null)
            {
                T result = this.Root.Value;
                this.Root = this.Root.Left;
                return result;
            }

            return this.DeleteMax(this.Root);
        }

        private T DeleteMax(Node node)
        {
            if (node.Right.Right == null)
            {
                T result = node.Value;
                node.Right = node.Right.Left;
                return result;
            }

            return this.DeleteMax(node.Right);
        }

        public IList<T> Range(T min, T max)
        {
            IList<T> result = new List<T>();
            this.Range(min, max, this.Root, result);
            return result;
        }

        private void Range(T min, T max, Node node, IList<T> result)
        {
            if (node == null)
            {
                return;
            }

            int compareMin = node.Value.CompareTo(min);
            int compareMax = node.Value.CompareTo(max);

            if (compareMin >= 0)
            {
                this.Range(min, max, node.Left, result);
            }

            if (compareMin >= 0 && compareMax <= 0)
            {
                result.Add(node.Value);
            }

            if (compareMax <= 0)
            {
                this.Range(min, max, node.Right, result);
            }
        }
    }
}