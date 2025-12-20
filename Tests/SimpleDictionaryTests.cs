using Xunit;
using Lab3.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3.Tests
{
    public class SimpleDictionaryTests
    {
        [Fact]
        public void Add_ShouldStoreKeyValue()
        {
            var dict = new SimpleDictionary<string, int>();
            dict.Add("one", 1);
            Assert.Equal(1, dict["one"]);
            Assert.Single(dict);
        }

        [Fact]
        public void Add_ShouldThrowWhenKeyExists()
        {
            var dict = new SimpleDictionary<string, int>();
            dict.Add("key", 100);
            Assert.Throws<ArgumentException>(() => dict.Add("key", 200));
        }

        [Fact]
        public void Remove_ShouldDeleteKey()
        {
            var dict = new SimpleDictionary<string, int>
            {
                { "a", 1 },
                { "b", 2 },
                { "c", 3 }
            };
            bool removed = dict.Remove("b");
            Assert.True(removed);
            Assert.Equal(2, dict.Count);
            Assert.DoesNotContain("b", dict.Keys);
        }

        [Fact]
        public void Remove_ShouldReturnFalseWhenKeyMissing()
        {
            var dict = new SimpleDictionary<int, string> { { 1, "one" } };
            bool removed = dict.Remove(99);
            Assert.False(removed);
            Assert.Single(dict);
        }

        [Fact]
        public void ContainsKey_ShouldReturnTrueForExistingKey()
        {
            var dict = new SimpleDictionary<int, string> { { 5, "five" }, { 10, "ten" } };
            Assert.Contains(5, dict.Keys);
            Assert.DoesNotContain(99, dict.Keys);
        }

        [Fact]
        public void TryGetValue_ShouldReturnTrueAndValue()
        {
            var dict = new SimpleDictionary<string, string> { { "key", "value" } };
            bool found = dict.TryGetValue("key", out string value);
            Assert.True(found);
            Assert.Equal("value", value);
        }

        [Fact]
        public void TryGetValue_ShouldReturnFalseForMissingKey()
        {
            var dict = new SimpleDictionary<int, int> { { 1, 100 } };
            bool found = dict.TryGetValue(99, out int value);
            Assert.False(found);
            Assert.Equal(0, value);
        }

        [Fact]
        public void Clear_ShouldRemoveAllItems()
        {
            var dict = new SimpleDictionary<int, int>
            {
                { 1, 100 },
                { 2, 200 },
                { 3, 300 }
            };
            dict.Clear();
            Assert.Empty(dict);
        }

        [Fact]
        public void Keys_ShouldReturnAllKeys()
        {
            var dict = new SimpleDictionary<string, int>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 }
            };
            var keys = dict.Keys.ToList();
            Assert.Equal(3, keys.Count);
            Assert.Contains("x", keys);
        }

        [Fact]
        public void Values_ShouldReturnAllValues()
        {
            var dict = new SimpleDictionary<int, string>
            {
                { 1, "a" },
                { 2, "b" },
                { 3, "c" }
            };
            var values = dict.Values.ToList();
            Assert.Equal(3, values.Count);
            Assert.Contains("a", values);
        }

        [Fact]
        public void Enumeration_ShouldIterateAllPairs()
        {
            var dict = new SimpleDictionary<int, int>
            {
                { 1, 10 },
                { 2, 20 },
                { 3, 30 }
            };
            int sum = 0;
            foreach (var kvp in dict)
            {
                sum += kvp.Value;
            }
            Assert.Equal(60, sum);
        }

        [Fact]
        public void Indexer_Get_ShouldThrowWhenKeyNotFound()
        {
            var dict = new SimpleDictionary<string, int>();
            Assert.Throws<KeyNotFoundException>(() => dict["missing"]);
        }

        [Fact]
        public void Indexer_Set_ShouldAddOrUpdateValue()
        {
            var dict = new SimpleDictionary<string, string>();
            dict["key1"] = "value1";
            Assert.Equal("value1", dict["key1"]);
            dict["key1"] = "updated";
            Assert.Equal("updated", dict["key1"]);
            Assert.Single(dict);
        }

        [Fact]
        public void Contains_ShouldCheckKeyValuePair()
        {
            var dict = new SimpleDictionary<int, string> { { 1, "one" }, { 2, "two" } };
            Assert.Contains(new KeyValuePair<int, string>(1, "one"), dict);
            Assert.DoesNotContain(new KeyValuePair<int, string>(1, "wrong"), dict);
        }

        [Fact]
        public void CopyTo_ShouldCopyKeyValuePairs()
        {
            var dict = new SimpleDictionary<int, string>
            {
                { 1, "a" },
                { 2, "b" },
                { 3, "c" }
            };
            var array = new KeyValuePair<int, string>[5];
            dict.CopyTo(array, 1);
            Assert.Contains(new KeyValuePair<int, string>(1, "a"), array);
        }

        [Fact]
        public void Add_ManyItems_ShouldWork()
        {
            var dict = new SimpleDictionary<int, string>();
            for (int i = 0; i < 20; i++)
            {
                dict.Add(i, $"value{i}");
            }
            Assert.Equal(20, dict.Count);
            Assert.Contains(10, dict.Keys);
        }

        [Fact]
        public void Remove_KeyValuePair_ShouldWork()
        {
            var dict = new SimpleDictionary<string, int> { { "a", 1 }, { "b", 2 } };
            var pair = new KeyValuePair<string, int>("a", 1);
            bool removed = dict.Remove(pair);
            Assert.True(removed);
            Assert.Single(dict);
            Assert.DoesNotContain("a", dict.Keys);
        }
    }
}