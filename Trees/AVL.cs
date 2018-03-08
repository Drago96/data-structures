using System;

namespace Trees
{
    public class AVL<T> where T : IComparable<T>
    {
        private Node<T> root;

        public Node<T> Root => this.root;

        public bool Contains(T item)
        {
            var node = this.Search(this.root, item);
            return node != null;
        }

        public void Insert(T item)
        {
            this.root = this.Insert(this.root, item);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            this.UpdateHeight(node);

            node = this.BalanceTree(node);

            return node;
        }

        private Node<T> BalanceTree(Node<T> node)
        {
            int balance = this.Balance(node);

            if (balance > 1)
            {
                int leftChildBalance =
                    this.Balance(node?.Left);

                if (leftChildBalance < 0)
                {
                    node.Left = this.RotateLeft(node.Left);
                }

                node = this.RotateRight(node);
            }
            else if (balance < -1)
            {
                int rightChildBalance =
                    this.Balance(node?.Right);

                if (rightChildBalance > 0)
                {
                    node.Right = this.RotateRight(node.Right);
                }

                node = this.RotateLeft(node);
            }

            this.UpdateHeight(node);

            return node;
        }

        private int Balance(Node<T> node)
            => this.Height(node.Left) - this.Height(node.Right);

        private Node<T> RotateRight(Node<T> node)
        {
            Node<T> left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            this.UpdateHeight(node);

            return left;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            Node<T> right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            this.UpdateHeight(node);

            return right;
        }

        private void UpdateHeight(Node<T> node) =>
            node.Height = Math.Max(this.Height(node.Left), this.Height(node.Right)) + 1;

        private int Height(Node<T> node) => node?.Height ?? 0;

        private Node<T> Search(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Search(node.Left, item);
            }
            else if (cmp > 0)
            {
                return Search(node.Right, item);
            }

            return node;
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        public class Node<T> where T : IComparable<T>
        {
            public T Value;
            public Node<T> Left;
            public Node<T> Right;
            public int Height;

            public Node(T value)
            {
                this.Value = value;
                this.Height = 1;
            }
        }
    }
}