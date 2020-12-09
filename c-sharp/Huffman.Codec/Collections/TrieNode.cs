using System;

namespace Huffman.Codec.Collections
{
    internal sealed class TrieNode : IComparable<TrieNode>
    {
        private TrieNode()
        {
        }

        public TrieNode? Left { get; set; }
        public TrieNode? Right { get; set; }
        public long Frequency { get; set; }
        public byte Byte { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public int CompareTo(TrieNode? that)
        {
            if (ReferenceEquals(this, that))
                return 0;

            if (ReferenceEquals(null, that))
                return 1;

            return Frequency.CompareTo(that.Frequency);
        }

        public static TrieNode CreateInnerNode(TrieNode left, TrieNode right)
        {
            var frequency = left.Frequency + right.Frequency;
            return new TrieNode
            {
                Left = left,
                Right = right,
                Frequency = frequency
            };
        }

        public static TrieNode CreateLeaf(byte @byte, long frequency)
        {
            return new TrieNode
            {
                Byte = @byte,
                Frequency = frequency
            };
        }
    }
}