using System;
using System.Collections.Generic;
using System.Text;

namespace LinearDataStructures
{
    public class LinkedList<T>
    {
        private Node<T> startNode;

        private Node<T> endNode;

        public LinkedList()
        {
            this.startNode = null;
            this.endNode = null;
        }

        public void Prepend(T value)
        {
            Node<T> newNode = new Node<T>(value);

            newNode.Next = this.startNode;
            this.startNode = newNode;

            if (this.endNode == null)
            {
                this.endNode = this.startNode;
            }
        }

        public void Append(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (this.endNode == null)
            {
                this.endNode = newNode;
                this.startNode = newNode;
                return;
            }

            this.endNode.Next = newNode;
            this.endNode = newNode;
        }

        public T Remove(T value)
        {
            if (this.startNode == null)
            {
                throw new ArgumentException();
            }

            if (this.startNode.Value.Equals(value))
            {
                this.startNode = this.startNode.Next;
                return this.startNode.Value;
            }

            Node<T> previousNode = this.startNode;
            Node<T> currentNode = this.startNode.Next;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    previousNode.Next = currentNode.Next;
                    if (currentNode == this.endNode)
                    {
                        this.endNode = previousNode;
                    }
                    return currentNode.Value;
                }
                Node<T> tempNode = currentNode.Next;
                previousNode = currentNode;
                currentNode = tempNode;       
            }

            throw new ArgumentException();
        }

        public bool Contains(T value)
        {
            Node<T> currentNode = this.startNode;

            while (currentNode!= null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }
    }
}
