using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3.Collections
{
    public class SimpleList<T> : IList<T>
    {
        private T[] _items;
        private int _count;

        public SimpleList()
        {
            _items = new T[4];
            _count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                _items[index] = value;
            }
        }

        public int Count => _count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (_count == _items.Length)
                Array.Resize(ref _items, _items.Length * 2);
            _items[_count] = item;
            _count++;
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _count);
            _count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_items, 0, array, arrayIndex, _count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(_items[i], item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_count == _items.Length)
                Array.Resize(ref _items, _items.Length * 2);

            if (index < _count)
                Array.Copy(_items, index, _items, index + 1, _count - index);

            _items[index] = item;
            _count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index < _count - 1)
                Array.Copy(_items, index + 1, _items, index, _count - index - 1);

            _count--;
            _items[_count] = default!; 
        }
    }
}