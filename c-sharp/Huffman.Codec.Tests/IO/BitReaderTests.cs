using System;
using System.IO;
using System.Text;
using FluentAssertions;
using Huffman.Codec.IO;
using Xunit;

namespace Huffman.Codec.Tests.IO
{
    public class BitReaderTests
    {
        [Theory]
        [InlineData(new byte[] {1},                "00000001")]
        [InlineData(new byte[] {3},                "00000011")]
        [InlineData(new byte[] {128},              "10000000")]
        [InlineData(new byte[] {255},              "11111111")]
        [InlineData(new byte[] {0, 0, 0, 0},       "00000000" + "00000000" + "00000000" + "00000000")]
        [InlineData(new byte[] {187, 23, 13, 105}, "10111011" + "00010111" + "00001101" + "01101001")]
        public void Should_read_bits_from_input_stream(byte[] input, string result)
        {
            var stream = new MemoryStream(input);
            var bitReader = new BitReader(stream);

            ReadAllBits(bitReader)
                .Should()
                .Be(result);
        }

        [Fact]
        public void IsEndOfStream_should_return_true_for_an_empty_stream()
        {
            var stream = new MemoryStream();
            var bitReader = new BitReader(stream);
            bitReader.IsEndOfStream.Should().BeTrue();
        }

        [Fact]
        public void IsEndOfStream_should_return_true_for_a_fully_read_stream()
        {
            var stream = new MemoryStream(new byte[] {1});
            var bitReader = new BitReader(stream);
            for (var i = 1; i <= 8; i++)
                bitReader.ReadBit();

            bitReader.IsEndOfStream.Should().BeTrue();
        }

        [Fact]
        public void Should_throw_an_exception_on_a_reading_attempt_when_a_stream_is_empty()
        {
            var stream = new MemoryStream();
            var bitReader = new BitReader(stream);

            Action action = () => bitReader.ReadBit();

            action
                .Should()
                .Throw<EndOfStreamException>();
        }

        [Fact]
        public void Should_throw_an_exception_on_a_reading_attempt_when_a_stream_is_fully_read()
        {
            var stream = new MemoryStream(new byte[] {1});
            var bitReader = new BitReader(stream);
            for (var i = 1; i <= 8; i++)
                bitReader.ReadBit();

            Action action = () => bitReader.ReadBit();

            action
                .Should()
                .Throw<EndOfStreamException>();
        }

        [Fact]
        public void Should_throw_an_exception_on_attempt_of_using_a_disposed_object()
        {
            var stream = new MemoryStream();
            var bitReader = new BitReader(stream);
            bitReader.Dispose();

            Action action = () => bitReader.ReadBit();

            action
                .Should()
                .Throw<ObjectDisposedException>()
                .WithMessage($"Cannot access a disposed object.\nObject name: '{nameof(BitReader)}'.");
        }




        private static string ReadAllBits(BitReader reader)
        {
            var stringBuilder = new StringBuilder();
            while (!reader.IsEndOfStream)
            {
                var bit = reader.ReadBit() ? '1' : '0';
                stringBuilder.Append(bit);
            }

            return stringBuilder.ToString();
        }
    }
}
