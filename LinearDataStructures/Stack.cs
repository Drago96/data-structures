using System;

namespace LinearDataStructures
{
    public class Stack<T>
    {
        private Node<T> top;

        public Stack()
        {
            this.top = null;
            this.Count = 0;
        }

        public int Count { get; private set; }

        public T Peek()
        {
            if (this.top == null)
            {
                throw new InvalidOperationException();
            }

            return this.top.Value;
        }

        public void Push(T value)
        {
            Node<T> newNode = new Node<T>(value);
            newNode.Next = this.top;
            this.top = newNode;
            this.Count++;
        }

        public T Pop()
        {
            if (this.top == null)
            {
                throw new InvalidOperationException();
            }

            T result = this.top.Value;
            this.top = this.top.Next;
            this.Count--;
            return result;
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];

            Node<T> currentNode = this.top;
            int currentIndex = 0;

            while (currentNode != null)
            {
                result[currentIndex] = currentNode.Value;
                currentIndex++;
                currentNode = currentNode.Next;
            }

            return result;
        }
    }
}
