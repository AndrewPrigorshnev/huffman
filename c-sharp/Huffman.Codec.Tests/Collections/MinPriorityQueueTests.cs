using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Huffman.Codec.Collections;
using Xunit;

namespace Huffman.Codec.Tests.Collections
{
    public class MinPriorityQueueTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Theory]
        [InlineData(new [] {3, 2, 1, 4}, new [] {1, 2, 3, 4})]
        [InlineData(new [] {2, 1, 2, 3, 2}, new [] {1, 2, 2, 2, 3})]
        [InlineData(new [] {1}, new [] {1})]
        [InlineData(new [] {1, 1, 1, 1}, new [] {1, 1, 1, 1})]
        [InlineData(new [] {1, 2, 3}, new [] {1, 2, 3})]
        [InlineData(new [] {3, 2, 1}, new [] {1, 2, 3})]
        public void Queue_returns_ints_in_sorted_order(int[] input, int[] sorted)
        {
            var result = PutIntoQueueAndTakeBack(input);

            result.Should()
                .HaveCount(input.Length)
                .And.ContainInOrder(sorted);
        }

        [Fact]
        public void Queue_returns_comparable_objects_in_sorted_order()
        {
            var input = ItemsArray.FromIntArray(new [] {4, 2, 1, 3});
            var result = PutIntoQueueAndTakeBack(input);

            result.Should()
                .BeInAscendingOrder()
                .And.BeEquivalentTo<Item>(ItemsArray.FromIntArray(new[] {1, 2, 3, 4}));
        }

        [Fact]
        public void Insert_into_a_full_queue_leads_to_an_exception()
        {
            var queue = new MinPriorityQueue<int>(3);
            queue.Insert(_fixture.Create<int>());
            queue.Insert(_fixture.Create<int>());
            queue.Insert(_fixture.Create<int>());

            Action act = () => queue.Insert(_fixture.Create<int>());

            act
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Queue is full.");
        }

        [Fact]
        public void DeleteMin_on_an_empty_queue_leads_to_an_exception()
        {
            var queue = new MinPriorityQueue<int>(3);
            Action act = () => queue.RemoveMin();

            act
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Queue is empty.");
        }




        private IEnumerable<T> PutIntoQueueAndTakeBack<T>(T[] input) where T : IComparable<T>
        {
            var queue = new MinPriorityQueue<T>(input.Length);
            foreach (var item in input)
                queue.Insert(item);

            var result = new List<T>();
            while (!queue.IsEmpty)
                result.Add(queue.RemoveMin());

            return result;
        }

        private static class ItemsArray
        {
            public static Item[] FromIntArray(int[] arr)
            {
                return arr
                    .Select(x => new Item(x))
                    .ToArray();
            }
        }

        private class Item
            : IComparable<Item>,
              IComparable // for assertions only
        {
            public Item(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public int CompareTo(Item? other)
            {
                if (ReferenceEquals(this, other))
                    return 0;

                if (ReferenceEquals(null, other))
                    return 1;

                return Value.CompareTo(other.Value);
            }

            public int CompareTo(object? obj) =>  CompareTo(obj as Item);
        }
    }
}
