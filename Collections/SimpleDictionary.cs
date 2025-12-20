using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3.Collections
{
    public class SimpleDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
    {
        private class Node
        {
            public TKey Key = default!;
            public TValue Value = default!;
            public Node? Next;
        }

        private Node?[] _buckets;
        private int _count;

        public SimpleDictionary()
        {
            _buckets = new Node[8];
            _count = 0;
        }

        public TValue this[TKey key]
        {
            get
            {
                var node = FindNode(key);
                if (node != null) return node.Value;
                throw new KeyNotFoundException();
            }
            set => AddOrUpdate(key, value);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new List<TKey>();
                foreach (var kvp in this)
                    keys.Add(kvp.Key);
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>();
                foreach (var kvp in this)
                    values.Add(kvp.Value);
                return values;
            }
        }

        public int Count => _count;
        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException("Элемент с таким ключом уже существует");
            AddInternal(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            _buckets = new Node[8];
            _count = 0;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var node = FindNode(item.Key);
            return node != null && EqualityComparer<TValue>.Default.Equals(node.Value, item.Value);
        }

        public bool ContainsKey(TKey key) => FindNode(key) != null;

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length) 
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _count)
                throw new ArgumentException();

            int i = 0;
            foreach (var kvp in this)
                array[arrayIndex + i++] = kvp;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in _buckets)
            {
                var current = bucket;
                while (current != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Remove(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);
            Node? current = _buckets[bucketIndex];
            Node? previous = null;

            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                {
                    if (previous == null)
                        _buckets[bucketIndex] = current.Next;
                    else
                        previous.Next = current.Next;
                    
                    _count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (Contains(item))
                return Remove(item.Key);
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var node = FindNode(key);
            if (node != null)
            {
                value = node.Value;
                return true;
            }
            value = default!;
            return false;
        }

        private Node? FindNode(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            int bucketIndex = GetBucketIndex(key);
            var current = _buckets[bucketIndex];
            
            while (current != null)
            {
                if (EqualityComparer<TKey>.Default.Equals(current.Key, key))
                    return current;
                current = current.Next;
            }
            return null;
        }

        private void AddOrUpdate(TKey key, TValue value)
        {
            var node = FindNode(key);
            if (node != null)
            {
                node.Value = value;
                return;
            }
            AddInternal(key, value);
        }

        private void AddInternal(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            int bucketIndex = GetBucketIndex(key);
            var newNode = new Node
            {
                Key = key,
                Value = value,
                Next = _buckets[bucketIndex]
            };
            _buckets[bucketIndex] = newNode;
            _count++;

            if (_count > _buckets.Length * 2)
                Rehash();
        }

        private int GetBucketIndex(TKey key) => Math.Abs(key.GetHashCode()) % _buckets.Length;

        private void Rehash()
        {
            var oldBuckets = _buckets;
            _buckets = new Node[oldBuckets.Length * 2];
            _count = 0;

            foreach (var bucket in oldBuckets)
            {
                var current = bucket;
                while (current != null)
                {
                    AddInternal(current.Key, current.Value);
                    current = current.Next;
                }
            }
        }
    }
}