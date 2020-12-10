using System;
using System.IO;
using FluentAssertions;
using Huffman.Codec.IO;
using Xunit;

namespace Huffman.Codec.Tests.IO
{
    public class BitWriterTests
    {
        [Theory]
        [InlineData("00000001", new byte[] {1})]
        [InlineData("00000011", new byte[] {3})]
        [InlineData("10000000", new byte[] {128})]
        [InlineData("11111111", new byte[] {255})]
        [InlineData("00000000" + "00000000" + "00000000" + "00000000", new byte[] {0, 0, 0, 0})]
        [InlineData("10111011" + "00010111" + "00001101" + "01101001", new byte[] {187, 23, 13, 105})]
        public void Should_write_bits_to_a_stream(string input, byte[] result)
        {
            var stream = new MemoryStream();
            var bitWriter = new BitWriter(stream);
            WriteAllBits(bitWriter, input);
            bitWriter.Flush();

            stream.ToArray()
                .Should()
                .HaveCount(result.Length)
                .And.ContainInOrder(result);
        }

        [Fact]
        public void Should_throw_an_exception_on_attempt_of_using_a_disposed_object()
        {
            var stream = new MemoryStream();
            var bitWriter = new BitWriter(stream);
            bitWriter.Dispose();

            Action action = () => bitWriter.WriteBit(true);

            action
                .Should()
                .Throw<ObjectDisposedException>()
                .WithMessage($"Cannot access a disposed object.\nObject name: '{nameof(BitWriter)}'.");
        }




        private static void WriteAllBits(BitWriter writer, string bits)
        {
            foreach (var character in bits)
            {
                var bit = character == '1';
                writer.WriteBit(bit);
            }
        }
    }
}
