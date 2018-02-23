using System;

namespace LinearDataStructures
{
    public class Queue<T>
    {
        private T[] items;
        private int head;
        private int tail;

        public Queue()
        {
            this.items = new T[16];
            this.Count = 0;
            this.head = 0;
            this.tail = 0;
        }

        public int Count { get; private set; }

        public void Enqueue(T value)
        {
            this.items[tail] = value;

            this.Count++;
            this.tail++;

            if (this.tail >= this.items.Length)
            {
                this.tail = 0;
            }

            if (this.tail == this.head)
            {
                this.Resize();
            }
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = this.items[this.head];
            this.head++;

            if (head >= this.items.Length)
            {
                this.head = 0;
            }

            this.Count--;
            return result;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.items[this.head];
        }

        private void Resize()
        {
            T[] newItemsArray = new T[this.Count * 2];

            for (int i = this.head; i < this.items.Length; i++)
            {
                newItemsArray[i - this.head] = this.items[i];
            }

            for (int i = 0; i < this.tail; i++)
            {
                newItemsArray[this.Count - this.tail + i] = this.items[i];
            }

            this.items = newItemsArray;

            this.head = 0;
            this.tail = this.Count;
        }
    }
}