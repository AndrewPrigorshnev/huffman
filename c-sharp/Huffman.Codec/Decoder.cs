using System.IO;
using Huffman.Codec.Coding;
using Huffman.Codec.Collections;
using Huffman.Codec.IO;

namespace Huffman.Codec
{
    public static class Decoder
    {
        public static void Decode(Stream input, Stream output)
        {
            using var reader = new Reader(input);
            using var writer = new Writer(output);

            var trie = Trie.Read(reader);
            var encodedBytesCount = reader.ReadLong();
            DecodeAndWriteData(reader, writer, trie, encodedBytesCount);
        }

        private static void DecodeAndWriteData(
            Reader reader,
            Writer writer,
            TrieNode trie,
            long count)
        {
            for (var i = 1; i <= count; i++)
            {
                var node = trie;
                while (!node!.IsLeaf)
                {
                    if (reader.ReadBit())
                        node = node.Right;
                    else
                        node = node.Left;
                }

                writer.WriteByte(node.Byte);
            }
        }
    }
}
