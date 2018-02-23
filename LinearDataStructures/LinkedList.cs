using System;
using System.Collections;
using System.Collections.Generic;

namespace LinearDataStructures
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private Node<T> head;

        private Node<T> tail;

        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            this.Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public void InsertFirst(T value)
        {
            Node<T> newNode = new Node<T>(value);

            newNode.Next = this.head;
            this.head = newNode;

            if (this.tail == null)
            {
                this.tail = this.head;
            }

            this.Count++;
        }

        public void InsertLast(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (this.tail == null)
            {
                this.tail = newNode;
                this.head = newNode;
                return;
            }

            this.tail.Next = newNode;
            this.tail = newNode;

            this.Count++;
        }

        public T Remove(T value)
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }

            if (this.head.Value.Equals(value))
            {
                this.head = this.head.Next;
                this.Count--;
                return this.head.Value;
            }

            Node<T> previousNode = this.head;
            Node<T> currentNode = this.head.Next;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    previousNode.Next = currentNode.Next;
                    if (currentNode == this.tail)
                    {
                        this.tail = previousNode;
                    }
                    this.Count--;
                    return currentNode.Value;
                }
                Node<T> tempNode = currentNode.Next;
                previousNode = currentNode;
                currentNode = tempNode;
            }

            throw new InvalidOperationException();
        }

        public bool Contains(T value)
        {
            Node<T> currentNode = this.head;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> currentNode = this.head;

            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}