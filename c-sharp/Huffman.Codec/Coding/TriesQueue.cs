using Huffman.Codec.Collections;

namespace Huffman.Codec.Coding
{
    internal static class TriesQueue
    {
        public static MinPriorityQueue<TrieNode> FromFrequencyTable(long[] frequencyTable)
        {
            var minQueue = new MinPriorityQueue<TrieNode>(2 * CodeTable.Size - 1);
            for (var i = 0; i < frequencyTable.Length; i++)
            {
                if (frequencyTable[i] > 0)
                {
                    var node = TrieNode.CreateLeaf((byte)i, frequencyTable[i]);
                    minQueue.Insert(node);
                }
            }

            return minQueue;
        }
    }
}
