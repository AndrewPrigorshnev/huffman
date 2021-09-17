using Huffman.Codec.Collections;
using Huffman.Codec.IO;

namespace Huffman.Codec.Coding
{
    internal static class TrieNodeExtensions
    {
        public static void WriteTo(this TrieNode node, Writer writer)
        {
            if (node.IsLeaf)
            {
                writer.WriteBit(true);
                writer.WriteByte(node.Byte);
                return;
            }

            writer.WriteBit(false);
            node.Left!.WriteTo(writer);
            node.Right!.WriteTo(writer);
        }
    }
}
