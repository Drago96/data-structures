using System;
using System.Collections.Generic;

namespace Trees
{
    public class Tree<T>
    {
        public T Root { get; set; }
        public IList<Tree<T>> Children { get; set; }

        public Tree(T root, params Tree<T>[] children)
        {
            this.Root = root;
            this.Children = new List<Tree<T>>(children);
        }

        public void Print(int identation = 0)
        {
            Console.WriteLine(new string(' ', identation) + this.Root);

            foreach (Tree<T> child in this.Children)
            {
                child.Print(identation + 2);
            }
        }

        public IList<T> RecursiveDfs()
        {
            IList<T> result = new List<T>();
            this.RecursiveDfs(this, result);
            return result;
        }

        private void RecursiveDfs(Tree<T> currentNode, IList<T> result)
        {
            foreach (Tree<T> child in currentNode.Children)
            {
                this.RecursiveDfs(child, result);
            }

            result.Add(currentNode.Root);
        }

        public IList<T> IterativeDfs()
        {
            Stack<Tree<T>> nodes = new Stack<Tree<T>>();
            Stack<T> result = new Stack<T>();

            nodes.Push(this);

            while (nodes.Count > 0)
            {
                Tree<T> currentNode = nodes.Pop();
                result.Push(currentNode.Root);

                foreach (Tree<T> child in currentNode.Children)
                {
                    nodes.Push(child);
                }
            }

            return result.ToArray();
        }

        public IList<T> Bfs()
        {
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();
            IList<T> result = new List<T>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                Tree<T> currentNode = nodes.Dequeue();
                result.Add(currentNode.Root);

                foreach (Tree<T> child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }

            return result;
        }
    }
}