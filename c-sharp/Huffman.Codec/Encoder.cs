using System;
using System.IO;
using Huffman.Codec.Coding;
using Huffman.Codec.Collections;
using Huffman.Codec.IO;

namespace Huffman.Codec
{
    public static class Encoder
    {
        public static void Encode(Func<Stream> getInputStream, Stream output)
        {
            TrieNode trie;
            using (var reader = new Reader(getInputStream()))
                trie = Trie.Build(reader);

            using (var writer = new Writer(output))
            {
                Trie.Write(writer, trie);
                writer.WriteLong(trie.Frequency);

                using (var reader = new Reader(getInputStream()))
                    EncodeAndWriteData(reader, writer, trie);
            }
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
