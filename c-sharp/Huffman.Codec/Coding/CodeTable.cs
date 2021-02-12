using Huffman.Codec.Collections;

namespace Huffman.Codec.Coding
{
    internal static class CodeTable
    {
        public const int Size = 256;

        public static string[] FromTrie(TrieNode root)
        {
            var table = new string[Size];
            Build(table, root, string.Empty);
            return table;
        }

        private static void Build(string[] table, TrieNode node, string code)
        {
            if (node.IsLeaf)
            {
                table[node.Byte] = code;
                return;
            }

            Build(table, node.Left!, $"{code}0");
            Build(table, node.Right!, $"{code}1");
        }
    }
}
