using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trees
{
    public class BinaryHeap<T> where T:IComparable
    {
        private readonly IList<T> heap;

        public BinaryHeap()
        {
            this.heap = new List<T>();
        }

        public int Count => this.heap.Count;

        public T Pull()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = this.heap[0];
            this.Swap(0, this.Count-1);
            this.heap.RemoveAt(this.Count-1);
            this.HeapifyDown(0);

            return result;
        }

        private void HeapifyDown(int index)
        {
            if (index >= this.Count / 2)
            {
                return;
            }

            int left = index * 2 + 1;
            int right = index * 2 + 2;

            int greaterChild;

            if (right == this.Count)
            {
                greaterChild = left;
            }
            else
            {
                greaterChild =
                    this.IsGreater(left, right) ? left : right;
            }

            if (this.IsGreater(greaterChild, index))
            {
                this.Swap(greaterChild,index);
                this.HeapifyDown(greaterChild);
            }

        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.heap.First();
        }

        public void Insert(T item)
        {
            this.heap.Add(item);
            this.HeapifyUp(this.heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            if (index == 0)
            {
                return;
            }

            int parentIndex = (index - 1) / 2;

            if (this.IsGreater(index,parentIndex))
            {
                Swap(index, parentIndex);
                HeapifyUp(parentIndex);
            }
        }

        private void Swap(int firstElementIndex, int secondElementIndex)
        {
            T temp = this.heap[firstElementIndex];
            this.heap[firstElementIndex] = this.heap[secondElementIndex];
            this.heap[secondElementIndex] = temp;
        }

        private bool IsGreater(int firstElementIndex, int secondElementIndex)
            => this.heap[firstElementIndex].CompareTo(this.heap[secondElementIndex]) > 0;
    }
}
