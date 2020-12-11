namespace Huffman.Cli
{
    internal static class Commands
    {
        public static readonly Command Compress = new Command
        {
            Name = "compress",
            Syntax = "compress <source> <target>",
            Description = "Compress a 'source' file into an archive with the name 'target'",
        };

        public static readonly Command Expand = new Command
        {
            Name = "expand",
            Syntax = "expand <source> <target>",
            Description = "Expand an archive 'source' into a file with the name 'target'",
        };

        public static readonly Command Help = new Command
        {
            Name = "help",
            Syntax = "help",
            Description = "Show help"
        };
    }
}
