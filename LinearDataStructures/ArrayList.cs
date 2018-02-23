using System;

namespace LinearDataStructures
{
    public class ArrayList<T>
    {
        private T[] itemHolder;

        /// <summary>
        /// Initializes a new Linear ArrayList.
        /// </summary>
        /// <param name="size">Initial size of the array.</param>
        public ArrayList(int size = 16)
        {
            this.itemHolder = new T[size];
            this.Count = 0;
        }

        public int Count
        {
            get;
            private set;
        }

        public T this[int index]
        {
            get
            {
                if (this.IndexIsInvalid(index))
                {
                    throw new IndexOutOfRangeException();
                }
                return this.itemHolder[index];
            }
            set
            {
                if (this.IndexIsInvalid(index))
                {
                    throw new IndexOutOfRangeException();
                }
                this.itemHolder[index] = value;
            }
        }

        public void Add(T item)
        {
            if (this.Count >= this.itemHolder.Length)
            {
                this.Resize();
            }
            this.itemHolder[this.Count++] = item;
        }

        public T RemoveAt(int index)
        {
            if (this.IndexIsInvalid(index))
            {
                throw new IndexOutOfRangeException();
            }

            T elementToReturn = this.itemHolder[index];

            this.ShiftArrayLeftFrom(index);

            this.Count--;

            if (this.Count <= this.itemHolder.Length / 4)
            {
                this.Resize();
            }

            return elementToReturn;
        }

        private void ShiftArrayLeftFrom(int index)
        {
            for (int i = index + 1; i < this.Count; i++)
            {
                this.itemHolder[i - 1] = this.itemHolder[i];
            }
        }

        private void Resize()
        {
            T[] newArray = new T[this.Count * 2];
            Array.Copy(this.itemHolder, newArray, this.Count);
            this.itemHolder = newArray;
        }

        private bool IndexIsInvalid(int index) => index >= this.Count || index < 0;
    }
}
