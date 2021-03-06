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

        public void Flush()
        {
            ThrowIfDisposed();
            _bitWriter.Flush();
        }

        public void WriteBit(bool value)
        {
            ThrowIfDisposed();
            _bitWriter.WriteBit(value);
        }

        public void WriteByte(byte value)
        {
            ThrowIfDisposed();

            for (var shift = 7; shift >= 0; shift--)
            {
                var mask = 0x01 << shift;
                var bit = (value & mask) > 0;
                WriteBit(bit);
            }
        }

        public void WriteLong(long value)
        {
            ThrowIfDisposed();

            var bytes = BitConverter.GetBytes(value);
            foreach (var @byte in bytes)
                WriteByte(@byte);
        }




        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}
