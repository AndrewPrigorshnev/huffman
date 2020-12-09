using System;
using System.IO;

namespace Huffman.Codec.IO
{
    internal class BitWriter : IDisposable
    {
        private readonly Stream _stream;
        private readonly int _buffer; // we use 1 byte only
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

            _stream.Dispose();
            _disposed = true;
        }

        public void WriteBit(bool bit)
        {
            ThrowIfDisposed();

            if (BufferIsFull)
                Flush();

            // todo set next byte
            throw new NotImplementedException();
        }

        public void Flush()
        {
            ThrowIfDisposed();

            if (BufferIsEmpty)
                return;

            _stream.WriteByte((byte) _buffer);
            _position = 0;
        }




        private bool BufferIsEmpty => _position == 9;

        private bool BufferIsFull => _position == 9;

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}