using Huffman.Codec.IO;

namespace Huffman.Codec.Coding
{
    internal static class FrequencyTable
    {
        public static long[] Build(Reader reader)
        {
            var table = new long[CodeTable.Size];
            while (!reader.IsEndOfStream)
            {
                var @byte = reader.ReadByte();
                table[@byte]++;
            }

            return table;
        }
    }
}
