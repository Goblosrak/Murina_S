using Xunit;
using Lab3.Collections;
using System;
using System.Collections.Generic;

namespace Lab3.Tests
{
    public class DoubleLinkedListTests
    {
        [Fact]
        public void Add_ShouldIncreaseCount()
        {
            var list = new DoubleLinkedList<int>();
            list.Add(10);
            Assert.Single(list);
        }

        [Fact]
        public void Add_MultipleItems_ShouldWork()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3, 4, 5 };
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void Remove_ShouldDecreaseCount()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            bool removed = list.Remove(2);
            Assert.True(removed);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void IndexOf_ShouldReturnCorrectIndex()
        {
            var list = new DoubleLinkedList<string> { "opa", "gangnam", "style" };
            int index = list.IndexOf("gangnam");
            Assert.Equal(1, index);
        }

        [Fact]
        public void IndexOf_ShouldReturnMinusOneForMissingItem()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            int index = list.IndexOf(99);
            Assert.Equal(-1, index);
        }

        [Fact]
        public void Insert_AtBeginning_ShouldWork()
        {
            var list = new DoubleLinkedList<int> { 2, 3 };
            list.Insert(0, 1);
            Assert.Equal(3, list.Count);
            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
        }

        [Fact]
        public void Insert_AtMiddle_ShouldWork()
        {
            var list = new DoubleLinkedList<int> { 1, 3, 4 };
            list.Insert(1, 2);
            Assert.Equal(4, list.Count);
            Assert.Equal(1, list[0]);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
            Assert.Equal(4, list[3]);
        }

        [Fact]
        public void Insert_AtEnd_ShouldWork()
        {
            var list = new DoubleLinkedList<int> { 1, 2 };
            list.Insert(2, 3);
            Assert.Equal(3, list.Count);
            Assert.Equal(3, list[2]);
        }

        [Fact]
        public void Insert_ShouldThrowWhenIndexOutOfRange()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(10, 99));
        }

        [Fact]
        public void RemoveAt_FromBeginning_ShouldWork()
        {
            var list = new DoubleLinkedList<string> { "a", "b", "c", "d" };
            list.RemoveAt(0);
            Assert.Equal(3, list.Count);
            Assert.Equal("b", list[0]);
        }

        [Fact]
        public void RemoveAt_FromMiddle_ShouldWork()
        {
            var list = new DoubleLinkedList<string> { "a", "b", "c", "d" };
            list.RemoveAt(2);
            Assert.Equal(3, list.Count);
            Assert.Equal("d", list[2]);
        }

        [Fact]
        public void RemoveAt_FromEnd_ShouldWork()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3, 4, 5 };
            list.RemoveAt(4);
            Assert.Equal(4, list.Count);
            Assert.Equal(4, list[3]);
        }

        [Fact]
        public void RemoveAt_ShouldThrowWhenIndexInvalid()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(5));
        }

        [Fact]
        public void Clear_ShouldResetCount()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3, 4, 5 };
            list.Clear();
            Assert.Empty(list);
        }

        [Fact]
        public void Contains_ShouldReturnTrueForExistingItem()
        {
            var list = new DoubleLinkedList<double> { 1.5, 2.7, 3.14 };
            Assert.Contains(2.7, list);
            Assert.DoesNotContain(99.9, list);
        }

        [Fact]
        public void CopyTo_ShouldCopyElements()
        {
            var list = new DoubleLinkedList<int> { 10, 20, 30 };
            var array = new int[5];
            list.CopyTo(array, 1);
            Assert.Equal(10, array[1]);
            Assert.Equal(20, array[2]);
            Assert.Equal(30, array[3]);
        }

        [Fact]
        public void Indexer_Get_ShouldThrowWhenIndexInvalid()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[5]);
        }

        [Fact]
        public void Indexer_Set_ShouldUpdateValue()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            list[1] = 99;
            Assert.Equal(99, list[1]);
        }

        [Fact]
        public void Enumeration_ShouldWorkWithForeach()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3, 4, 5 };
            var sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            Assert.Equal(15, sum);
        }

        [Fact]
        public void ReverseEnumeration_ShouldWork()
        {
            var list = new DoubleLinkedList<int> { 1, 2, 3 };
            var values = new List<int>();
  
            var tailValue = list[2];
            Assert.Equal(3, list[2]);
            Assert.Equal(1, list[0]);
        }
    }
}