using System;

namespace Huffman.Codec.Collections
{
    internal sealed class MinPriorityQueue<T> where T : IComparable<T>
    {
        private readonly T[] _heap;
        private int _tail = 0; // _heap[0] is unused; if _tail == 0, then queue is empty

        public MinPriorityQueue(int capacity)
        {
            _heap = new T[capacity + 1];
        }

        public bool IsEmpty => _tail == 0;

        public bool IsFull => _tail == _heap.Length - 1;

        public int Size => _tail;

        public void Insert(T item)
        {
            if (IsFull)
                throw new InvalidOperationException("Queue is full.");

            _tail++;
            _heap[_tail] = item;
            SiftUp(_tail);
        }

        public T RemoveMin()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Queue is empty.");

            var min = _heap[1];
            _heap[1] = _heap[_tail];
            _heap[_tail] = default!; // avoid loitering
            _tail--;
            SiftDown(1);
            return min;
        }




        private void SiftDown(int i)
        {
            while (HasChildren(i))
            {
                var minChild = MinChild(i);
                if (!Less(minChild, i))
                    break;

                Swap(minChild, i);
                i = minChild;
            }
        }

        private void SiftUp(int i)
        {
            while (i > 1 && Less(i, Parent(i)))
            {
                var parent = Parent(i);
                Swap(i, parent);
                i = parent;
            }
        }

        private bool Less(int left, int right) => _heap[left].CompareTo(_heap[right]) < 0;

        private void Swap(int i, int j)
        {
            var temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        private bool HasChildren(int i) => Left(i) <= _tail;

        private int MinChild(int i)
        {
            var left = Left(i);
            var right = Right(i);

            if (right <= _tail && Less(right, left))
                return right;
            else
                return left;
        }

        private static int Left(int i) => 2 * i;

        private static int Right(int i) => 2 * i + 1;

        private static int Parent(int i) => i / 2;
    }
}
