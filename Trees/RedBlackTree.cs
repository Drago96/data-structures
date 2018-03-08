﻿using System;
using System.Collections.Generic;

namespace Trees
{
    public class RedBlackTree<T> where T : IComparable
    {
        private const bool Red = true;
        private const bool Black = false;
        private Node root;

        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            if (this.IsRed(node.Right) && !this.IsRed(node.Left))
            {
                node = this.RotateLeft(node);
            }

            if (this.IsRed(node.Left) && this.IsRed(node.Left.Left))
            {
                node = this.RotateRight(node);
            }

            if (this.IsRed(node.Left) && this.IsRed(node.Right))
            {
                this.FlipColors(node);
            }

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
            return node;
        }

        private void Range(Node node, Queue<T> queue, T startRange, T endRange)
        {
            if (node == null)
            {
                return;
            }

            int nodeInLowerRange = startRange.CompareTo(node.Value);
            int nodeInHigherRange = endRange.CompareTo(node.Value);

            if (nodeInLowerRange < 0)
            {
                this.Range(node.Left, queue, startRange, endRange);
            }
            if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
            {
                queue.Enqueue(node.Value);
            }
            if (nodeInHigherRange > 0)
            {
                this.Range(node.Right, queue, startRange, endRange);
            }
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private RedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public RedBlackTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
            this.root.Color = Black;
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new RedBlackTree<T>(current);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            Queue<T> queue = new Queue<T>();

            this.Range(this.root, queue, startRange, endRange);

            return queue;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindMin(node.Left);
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        public int Rank(T element)
        {
            return this.Rank(element, this.root);
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            int compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                return this.Rank(element, node.Left);
            }

            if (compare > 0)
            {
                return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
            }

            return this.Count(node.Left);
        }

        public T Select(int rank)
        {
            Node node = this.Select(rank, this.root);
            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int leftCount = this.Count(node.Left);
            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return this.Select(rank, node.Left);
            }
            else
            {
                return this.Select(rank - (leftCount + 1), node.Right);
            }
        }

        public T Ceiling(T element)
        {
            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public bool Color { get; set; }

            public int Count { get; set; }
        }

        private bool IsRed(Node node) => node?.Color == Red;

        private void FlipColors(Node node)
        {
            node.Color = Red;
            node.Left.Color = Black;
            node.Right.Color = Black;
        }

        private Node RotateLeft(Node node)
        {
            Node subTreeRoot = node.Right;
            node.Right = subTreeRoot.Left;
            subTreeRoot.Left = node;

            subTreeRoot.Color = node.Color;
            node.Color = Red;
            subTreeRoot.Count = node.Count;

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return subTreeRoot;
        }

        private Node RotateRight(Node node)
        {
            Node subTreeRoot = node.Left;
            node.Left = subTreeRoot.Right;
            subTreeRoot.Right = node;

            subTreeRoot.Color = node.Color;
            node.Color = Red;
            subTreeRoot.Count = node.Count;

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return subTreeRoot;
        }
    }
}