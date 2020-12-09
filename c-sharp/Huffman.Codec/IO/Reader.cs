using System;
using System.IO;

namespace Huffman.Codec.IO
{
    internal sealed class Reader : IDisposable
    {
        private readonly BitReader _bitReader;
        private bool _disposed;

        public Reader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _bitReader = new BitReader(stream);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _bitReader.Dispose();
            _disposed = true;
        }

        public bool IsEndOfStream
        {
            get
            {
                ThrowIfDisposed();
                return _bitReader.IsEndOfStream;
            }
        }

        public bool ReadBit()
        {
            ThrowIfDisposed();
            return _bitReader.ReadBit();
        }

        public byte ReadByte()
        {
            ThrowIfDisposed();

            // todo restore pointer if there is not enough bits for a byte value
            // todo extract method
            var result = 0;
            for (var i = 1; i <= 8; i++)
            {
                var bit = ReadBit() ? 0x01 : 0x00;
                var mask = bit << (8 - i);
                result |= mask;
            }

            return (byte) result;
        }

        public long ReadLong()
        {
            ThrowIfDisposed();

            // todo restore pointer if there is not enough bits for a long value
            // todo extract method
            var result = 0L;
            for (var i = 1; i <= 64; i++)
            {
                var bit = ReadBit() ? 0x01 : 0x00;
                var mask = bit << (64 - i);
                result |= mask;
            }

            return result;
        }




        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}