using System;
using System.Collections.Generic;
using System.IO;
using AutoFixture;
using FluentAssertions;
using Huffman.Codec.IO;
using Xunit;

namespace Huffman.IntegrationTests
{
    public class IOTests
    {
        private readonly IFixture _fixture = new Fixture();
        private readonly MemoryStream _stream;
        private readonly Writer _writer;
        private readonly Reader _reader;

        public IOTests()
        {
            _stream = new MemoryStream();
            _writer = new Writer(_stream);
            _reader = new Reader(_stream);
        }

        [Fact]
        public void Should_write_and_read_long_integers()
        {
            var value1 = _fixture.Create<long>();
            var value2 = _fixture.Create<long>();
            var value3 = _fixture.Create<long>();

            _writer.WriteLong(value1);
            _writer.WriteLong(value2);
            _writer.WriteLong(value3);
            _writer.WriteLong(value1);
            _writer.Flush();

            _stream.Seek(0, SeekOrigin.Begin);

            var read = new[]
            {
                _reader.ReadLong(),
                _reader.ReadLong(),
                _reader.ReadLong(),
                _reader.ReadLong(),
            };

            read
                .Should()
                .HaveCount(4)
                .And.ContainInOrder(new[] {value1, value2, value3, value1});
        }

        [Fact]
        public void Should_write_and_read_bit_sequences()
        {
            var bitSequence = GenerateUnalignedBitSequence();
            WriteBitSequence(bitSequence);
            _writer.Flush();

            _stream.Seek(0, SeekOrigin.Begin);

            var read = new List<bool>();
            while (!_reader.IsEndOfStream)
                read.Add(_reader.ReadBit());

            var alignedSequence = Align(bitSequence);

            read.Should()
                .HaveCount(alignedSequence.Count)
                .And.ContainInOrder(alignedSequence);
        }

        [Fact]
        public void Should_write_and_read_bit_sequences_mixed_with_long_values()
        {
            var sequence1 = GenerateUnalignedBitSequence();
            var longValue1 = _fixture.Create<long>();
            var sequence2 = GenerateUnalignedBitSequence();
            var longValue2 = _fixture.Create<long>();
            var sequence3 = GenerateUnalignedBitSequence();

            WriteBitSequence(sequence1);
            WriteLong(longValue1);
            WriteBitSequence(sequence2);
            WriteLong(longValue2);
            WriteBitSequence(sequence3);
            _writer.Flush();

            _stream.Seek(0, SeekOrigin.Begin);

            ReadBitSequence(sequence1.Count)
                .Should()
                .HaveCount(sequence1.Count)
                .And.ContainInOrder(sequence1);

            ReadLong()
                .Should()
                .Be(longValue1);

            ReadBitSequence(sequence2.Count)
                .Should()
                .HaveCount(sequence2.Count)
                .And.ContainInOrder(sequence2);

            ReadLong()
                .Should()
                .Be(longValue2);

            ReadBitSequence(sequence3.Count)
                .Should()
                .HaveCount(sequence3.Count)
                .And.ContainInOrder(sequence3);

            // the rest is zeros
            while (!_reader.IsEndOfStream)
                _reader.ReadBit().Should().BeFalse();
        }




        private List<bool> GenerateUnalignedBitSequence()
        {
            var sequence = new List<bool>();
            var random = new Random();
            var count = _fixture.Create<int>();
            for (var i = 1; i <= count; i++)
                sequence.Add(RandomBool(random));

            return sequence;
        }

        private List<bool> ReadBitSequence(int length)
        {
            var readSequence = new List<bool>();
            for (var i = 0; i < length; i++)
                readSequence.Add(_reader.ReadBit());
            return readSequence;
        }

        private long ReadLong() => _reader.ReadLong();

        private void WriteBitSequence(List<bool> bitSequence)
        {
            foreach (var bit in bitSequence)
                _writer.WriteBit(bit);
        }

        private void WriteLong(long value) => _writer.WriteLong(value);

        private static List<bool> Align(List<bool> bitSequence)
        {
            var aligned = new List<bool>(bitSequence);
            if (aligned.Count % 8 != 0)
            {
                var offset = 8 - aligned.Count % 8;
                for (var i = 1; i <= offset; i++)
                    aligned.Add(false);
            }

            return aligned;
        }

        private static bool RandomBool(Random random) => random.NextDouble() >= 0.5;
    }
}
