using System;
using System.IO;

namespace Huffman.Codec.IO
{
    internal class BitWriter : IDisposable
    {
        private readonly Stream _stream;
        private int _buffer; // we use 1 byte only
        private int _position = 0; // position == 0 means empty buffer
        private bool _disposed;

        public BitWriter(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _stream = stream;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            Flush();
            _stream.Dispose();

            _disposed = true;
        }

        public void WriteBit(bool bit)
        {
            ThrowIfDisposed();

            if (BufferIsFull)
                WriteBuffer();

            if (BufferIsEmpty)
                InitBuffer();

            BufferBit(bit);
        }

        public void Flush()
        {
            ThrowIfDisposed();

            if (!BufferIsEmpty)
                WriteBuffer();

            _stream.Flush();
        }




        private bool BufferIsEmpty => _position == 0;

        private bool BufferIsFull => _position == 9;

        private void BufferBit(bool bit)
        {
            if (bit)
            {
                var shift = 8 - _position;
                _buffer |= 0x01 << shift;
            }

            _position++;
        }

        private void InitBuffer()
        {
            _buffer = 0;
            _position = 1;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        private void WriteBuffer()
        {
            if (BufferIsEmpty)
                throw new InvalidOperationException("Buffer is empty.");

            _stream.WriteByte((byte) _buffer);
            _position = 0;
        }
    }
}
