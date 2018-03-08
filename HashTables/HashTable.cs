using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTables
{
    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DefaultCapacity = 16;
        private const float LoadFactor = 0.75f;

        private LinkedList<KeyValue<TKey, TValue>>[] hashTable;
        private int maxElements => (int)(this.Capacity * LoadFactor);

        public int Count { get; private set; }

        public int Capacity => this.hashTable.Length;

        public HashTable(int capacity = DefaultCapacity)
        {
            this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            this.CheckGrowth();

            int hash = this.GetHash(key);
            if (this.hashTable[hash] == null)
            {
                this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var keyValue in this.hashTable[hash])
            {
                if (keyValue.Key.Equals(key))
                {
                    throw new ArgumentException();
                }
            }

            KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
            this.hashTable[hash].AddLast(kvp);
            this.Count++;
        }

        private void CheckGrowth()
        {
            if (this.Count >= this.maxElements)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            HashTable<TKey, TValue> newTable = new HashTable<TKey, TValue>(this.Capacity * 2);

            foreach (var linkedList in this.hashTable)
            {
                if (linkedList != null)
                {
                    foreach (var keyValue in linkedList)
                    {
                        newTable.Add(keyValue.Key, keyValue.Value);
                    }
                }
            }

            this.hashTable = newTable.hashTable;
            this.Count = newTable.Count;

        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            this.CheckGrowth();

            int hash = this.GetHash(key);
            if (this.hashTable[hash] == null)
            {
                this.hashTable[hash] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var keyValue in this.hashTable[hash])
            {
                if (keyValue.Key.Equals(key))
                {
                    keyValue.Value = value;
                    return true;
                }
            }

            KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
            this.hashTable[hash].AddLast(kvp);
            this.Count++;
            return false;
        }

        public TValue Get(TKey key)
        {
            var kvp = this.Find(key);

            if (kvp == null)
            {
                throw new KeyNotFoundException();
            }

            return kvp.Value;
        }

        public TValue this[TKey key]
        {
            get => this.Get(key);
            set => this.AddOrReplace(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var kvp = this.Find(key);

            if (key == null)
            {
                value = default(TValue);
                return false;
            }

            value = kvp.Value;
            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int hash = this.GetHash(key);

            var linkedList = this.hashTable[hash];
            if (linkedList != null)
            {
                foreach (var keyValue in linkedList)
                {
                    if (keyValue.Key.Equals(key))
                    {
                        return keyValue;
                    }
                }
            }

            return null;
        }

        private int GetHash(TKey key)
            => Math.Abs(key.GetHashCode()) % this.Capacity;

        public bool ContainsKey(TKey key)
            => this.Find(key) != null;

        public bool Remove(TKey key)
        {
            int hash = this.GetHash(key);

            var linkedList = this.hashTable[hash];
            if (linkedList != null)
            {
                foreach (var keyValue in linkedList)
                {
                    if (keyValue.Key.Equals(key))
                    {
                        linkedList.Remove(keyValue);
                        this.Count--;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Clear()
        {
            this.hashTable = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys
            => this.Select(kvp => kvp.Key);

        public IEnumerable<TValue> Values
            => this.Select(kvp => kvp.Value);


        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in this.hashTable)
            {
                if (linkedList != null)
                {
                    foreach (var kvp in linkedList)
                    {
                        yield return kvp;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
