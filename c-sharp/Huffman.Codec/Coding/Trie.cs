using Huffman.Codec.Collections;
using Huffman.Codec.IO;

namespace Huffman.Codec.Coding
{
    internal static class Trie
    {
        public static TrieNode Build(Reader reader)
        {
            var frequencyTable = FrequencyTable.Build(reader);
            var triesQueue = TriesQueue.FromFrequencyTable(frequencyTable);

            while (triesQueue.Size > 1)
            {
                var left = triesQueue.RemoveMin();
                var right = triesQueue.RemoveMin();
                var parent = TrieNode.CreateInnerNode(left, right);
                triesQueue.Insert(parent);
            }

            return triesQueue.RemoveMin();
        }

        public static TrieNode Read(Reader reader)
        {
            if (reader.ReadBit())
            {
                var @byte = reader.ReadByte();
                return TrieNode.CreateLeaf(@byte, 0);
            }

            return TrieNode.CreateInnerNode(Read(reader), Read(reader));
        }
    }
}
