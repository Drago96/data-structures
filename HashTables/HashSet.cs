using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTables
{
    public class HashSet<T> : IEnumerable<T>
    {
        private readonly HashTable<T, T> hashTable;

        public HashSet()
        {
            this.hashTable = new HashTable<T, T>();
        }

        public void Add(T item)
            => this.hashTable.AddOrReplace(item, item);

        public bool Remove(T item)
            => this.hashTable.Remove(item);

        public bool Contains(T item)
            => this.hashTable.Find(item) != null;

        public IEnumerable<T> UnionWith(IEnumerable<T> other)
        {
            HashSet<T> result = new HashSet<T>();

            foreach (var item in this)
            {
                result.Add(item);
            }

            foreach (var item in other)
            {
                result.Add(item);
            }

            return result;
        }

        public IEnumerable<T> IntersectWith(IEnumerable<T> other)
        {
            HashSet<T> result = new HashSet<T>();

            foreach (var item in this)
            {
                if (other.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public IEnumerable<T> Except(IEnumerable<T> other)
        {
            HashSet<T> result = new HashSet<T>();

            foreach (var item in this)
            {
                if (!other.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public IEnumerable<T> SymetricExcept(IEnumerable<T> other)
            => this.UnionWith(other).Except(this.IntersectWith(other));


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var hashTableKey in this.hashTable.Keys)
            {
                yield return hashTableKey;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
