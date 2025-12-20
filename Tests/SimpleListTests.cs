using Xunit;
using Lab3.Collections;
using System;

namespace Lab3.Tests
{
    public class SimpleListTests
    {
        [Fact]
        public void Add_ShouldIncreaseCount()
        {
            var list = new SimpleList<int>();
            list.Add(10);
            Assert.Single(list);
        }

        [Fact]
        public void Remove_ShouldDecreaseCount()
        {
            var list = new SimpleList<int> { 1, 2, 3 };
            bool removed = list.Remove(2);
            Assert.True(removed);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void IndexOf_ShouldReturnCorrectIndex()
        {
            var list = new SimpleList<string> { "opa", "gangnam", "style" };
            int index = list.IndexOf("gangnam");
            Assert.Equal(1, index);
        }

        [Fact]
        public void IndexOf_ShouldReturnMinusOneForMissingItem()
        {
            var list = new SimpleList<int> { 1, 2, 3 };
            int index = list.IndexOf(99);
            Assert.Equal(-1, index);
        }

        [Fact]
        public void Insert_ShouldShiftElements()
        {
            var list = new SimpleList<int> { 1, 3, 4 };
            list.Insert(1, 2);
            Assert.Equal(4, list.Count);
            Assert.Equal(2, list[1]);
            Assert.Equal(3, list[2]);
        }

        [Fact]
        public void Insert_ShouldThrowWhenIndexOutOfRange()
        {
            var list = new SimpleList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(10, 99));
        }

        [Fact]
        public void RemoveAt_ShouldRemoveElement()
        {
            var list = new SimpleList<string> { "a", "b", "c", "d" };
            list.RemoveAt(2);
            Assert.Equal(3, list.Count);
            Assert.Equal("d", list[2]);
        }

        [Fact]
        public void Clear_ShouldResetCount()
        {
            var list = new SimpleList<int> { 1, 2, 3, 4, 5 };
            list.Clear();
            Assert.Empty(list);
        }

        [Fact]
        public void Contains_ShouldReturnTrueForExistingItem()
        {
            var list = new SimpleList<double> { 1.5, 2.7, 3.14 };
            Assert.Contains(2.7, list);
            Assert.DoesNotContain(99.9, list);
        }

        [Fact]
        public void CopyTo_ShouldCopyElements()
        {
            var list = new SimpleList<int> { 10, 20, 30 };
            var array = new int[5];
            list.CopyTo(array, 1);
            Assert.Equal(10, array[1]);
            Assert.Equal(20, array[2]);
            Assert.Equal(30, array[3]);
        }

        [Fact]
        public void Indexer_Get_ShouldThrowWhenIndexInvalid()
        {
            var list = new SimpleList<int> { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => list[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => list[5]);
        }

        [Fact]
        public void Indexer_Set_ShouldUpdateValue()
        {
            var list = new SimpleList<int> { 1, 2, 3 };
            list[1] = 99;
            Assert.Equal(99, list[1]);
        }

        [Fact]
        public void Enumeration_ShouldWorkWithForeach()
        {
            var list = new SimpleList<int> { 1, 2, 3, 4, 5 };
            var sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            Assert.Equal(15, sum);
        }

        [Fact]
        public void Add_MultipleItems_ShouldResizeArray()
        {
            var list = new SimpleList<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            Assert.Equal(10, list.Count);
        }
    }
}