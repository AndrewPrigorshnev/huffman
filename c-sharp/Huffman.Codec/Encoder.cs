using System;
using System.IO;
using Huffman.Codec.Coding;
using Huffman.Codec.Collections;
using Huffman.Codec.IO;

namespace Huffman.Codec
{
    public static class Encoder
    {
        public static void Encode(Stream input, Stream output)
        {
            if (!input.CanSeek)
                throw new InvalidOperationException("The input stream should be seekable.");

            using var reader = new Reader(input);
            using var writer = new Writer(output);

            var trie = Trie.Build(reader);
            Trie.Write(writer, trie);
            writer.WriteLong(trie.Frequency);

            input.Seek(0, SeekOrigin.Begin);
            EncodeAndWriteData(reader, writer, trie);
        }

        private static void EncodeAndWriteData(Reader reader, Writer writer, TrieNode trie)
        {
            var codeTable = CodeTable.FromTrie(trie);
            while (!reader.IsEndOfStream)
            {
                var @byte = reader.ReadByte();
                var code = codeTable[@byte];
                WriteCode(code, writer);
            }
        }

        private static void WriteCode(string code, Writer writer)
        {
            foreach (var character in code)
            {
                if (character == '1')
                    writer.WriteBit(true);
                else
                    writer.WriteBit(false);
            }
        }
    }
}
