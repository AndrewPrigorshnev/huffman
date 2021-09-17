using System;
using System.IO;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Huffman.Codec.Coding;
using Huffman.Codec.Collections;
using Huffman.Codec.IO;
using Xunit;

namespace Huffman.IntegrationTests
{
    public class TrieTests
    {
        private readonly IFixture _fixture = new Fixture();

        [Fact]
        public void Should_generate_a_coding_trie_for_an_arbitrary_binary_input()
        {
            var stream = GenerateBinaryStream();
            var reader = new Reader(stream);

            Func<TrieNode> func = () => Trie.Build(reader);

            func.Should()
                .NotThrow()
                .Which.Should()
                .Match<TrieNode>(trieNode =>
                    trieNode.Left != null
                    && trieNode.Right != null
                    && trieNode.Frequency > 0);
        }

        [Fact]
        public void Should_write_and_read_a_trie()
        {
            var stream = new MemoryStream();
            var writer = new Writer(stream);
            var reader = new Reader(stream);

            var trie = GenerateTrie();
            trie.WriteTo(writer);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);

            var readTrie = Trie.Read(reader);

            var triesAreEqual = TriesAreEqual(trie, readTrie);
            triesAreEqual
                .Should()
                .BeTrue();
        }




        private MemoryStream GenerateBinaryStream()
        {
            var bytes = _fixture
                .CreateMany<byte>()
                .ToArray();

            return new MemoryStream(bytes);
        }

        private TrieNode GenerateTrie()
        {
            var stream = GenerateBinaryStream();
            var reader = new Reader(stream);
            var trie = Trie.Build(reader);
            return trie;
        }

        private bool TriesAreEqual(TrieNode one, TrieNode other)
        {
            if (one == null && other == null)
                return true;

            if ((one == null) != (other == null))
                return false;

            if (one.Byte != other.Byte)
                return false;

            return TriesAreEqual(one.Left, other.Left)
                   && TriesAreEqual(one.Right, other.Right);
        }
    }
}
