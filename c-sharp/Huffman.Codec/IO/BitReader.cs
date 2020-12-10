using System;
using System.IO;

namespace Huffman.Codec.IO
{
    internal sealed class BitReader : IDisposable
    {
        private readonly Stream _stream;
        private int _buffer; // we use 1 byte only
        private int _position = 0; // position == 0 means an empty buffer
        private bool _disposed;

        public BitReader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _stream = stream;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _stream.Dispose();
            _disposed = true;
        }

        public bool IsEndOfStream
        {
            get
            {
                ThrowIfDisposed();

                if (HasBufferedBits)
                    return false;

                return !TryReadNextByte();
            }
        }

        public bool ReadBit()
        {
            ThrowIfDisposed();

            if (IsEndOfStream)
                throw new EndOfStreamException();

            return ReadNextBit();
        }




        private bool BufferIsEmpty => _position == 0;

        private bool BufferIsFullyRead => _position == 9;

        private bool HasBufferedBits => !BufferIsEmpty && !BufferIsFullyRead;

        private bool ReadNextBit()
        {
            if (!HasBufferedBits)
                throw new InvalidOperationException("There aren't buffered bits.");

            var shift = 8 - _position;
            var mask = 0x01 << shift;
            var bit = (_buffer & mask) > 0;
            _position++;

            return bit;
        }

        private bool TryReadNextByte()
        {
            if (HasBufferedBits)
                throw new InvalidOperationException("There are buffered bits yet.");

            var nextByte = _stream.ReadByte();
            if (nextByte == -1)
                return false;

            _buffer = nextByte;
            _position = 1;
            return true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}
