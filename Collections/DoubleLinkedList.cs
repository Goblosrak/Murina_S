using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3.Collections
{
    public class DoubleLinkedList<T> : IList<T>
    {
        private class Node
        {
            public T Value;
            public Node? Next;
            public Node? Previous;

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node? _head;
        private Node? _tail;
        private int _count;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                
                var node = GetNodeAt(index);
                return node.Value;
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                
                var node = GetNodeAt(index);
                node.Value = value;
            }
        }

        public int Count => _count;
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            var newNode = new Node(item);
            
            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail!.Next = newNode;
                newNode.Previous = _tail;
                _tail = newNode;
            }
            
            _count++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < _count)
                throw new ArgumentException();

            var current = _head;
            int i = 0;
            while (current != null)
            {
                array[arrayIndex + i] = current.Value;
                current = current.Next;
                i++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int IndexOf(T item)
        {
            var current = _head;
            int index = 0;
            
            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Value, item))
                    return index;
                
                current = current.Next;
                index++;
            }
            
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == _count)
            {
                Add(item);
                return;
            }

            var newNode = new Node(item);
            
            if (index == 0)
            {
                newNode.Next = _head;
                _head!.Previous = newNode;
                _head = newNode;
            }
            else
            {
                var current = GetNodeAt(index);
                var previous = current.Previous;
                
                newNode.Next = current;
                newNode.Previous = previous;
                
                previous!.Next = newNode;
                current.Previous = newNode;
            }
            
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

            var nodeToRemove = GetNodeAt(index);
            
            if (nodeToRemove.Previous != null)
                nodeToRemove.Previous.Next = nodeToRemove.Next;
            else
                _head = nodeToRemove.Next;

            if (nodeToRemove.Next != null)
                nodeToRemove.Next.Previous = nodeToRemove.Previous;
            else
                _tail = nodeToRemove.Previous;
            
            _count--;
        }

        private Node GetNodeAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            Node current;
            if (index < _count / 2)
            {
                current = _head!;
                for (int i = 0; i < index; i++)
                    current = current.Next!;
            }
            else
            {
                current = _tail!;
                for (int i = _count - 1; i > index; i--)
                    current = current.Previous!;
            }
            
            return current;
        }
    }
}