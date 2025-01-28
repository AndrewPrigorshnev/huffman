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

            // todo what to do if there aren't enough bits for a byte value?
            // don't throw an exception? restore the pointer then?
            // fix it even though it's not a problem in this application
            var result = 0;
            for (var shift = 7; shift >= 0; shift--)
            {
                var bit = ReadBit() ? 0x01 : 0x00;
                result |= bit << shift;
            }

            return (byte) result;
        }

        public long ReadLong()
        {
            ThrowIfDisposed();

            // todo what to do if there aren't enough bits for a byte value?
            // don't throw an exception? restore the pointer then?
            // fix it even though it's not a problem in this application
            long result = 0;
            for (var shift = 0; shift <= 56; shift += 8)
            {
                long @byte = ReadByte() << shift;
                result |= @byte;
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