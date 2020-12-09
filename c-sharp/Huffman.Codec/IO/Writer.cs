using System;
using System.IO;

namespace Huffman.Codec.IO
{
    internal sealed class Writer : IDisposable
    {
        private readonly BitWriter _bitWriter;
        private bool _disposed;

        public Writer(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _bitWriter = new BitWriter(stream);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _bitWriter.Dispose();
            _disposed = true;
        }

        public void WriteBit(bool value)
        {
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public void WriteByte(byte value)
        {
            ThrowIfDisposed();
            throw new NotImplementedException();
        }

        public void WriteLong(long value)
        {
            ThrowIfDisposed();
            throw new NotImplementedException();
        }




        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}
