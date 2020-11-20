namespace Huffman.Cli
{
    internal static class Commands
    {
        public static Command Compress = new Command
        {
            Name = "compress",
            Syntax = "compress <source> <target>",
            Description = "Compress a 'source' file into an archive with the name 'target'",
        };

        public static Command Expand = new Command
        {
            Name = "expand",
            Syntax = "expand <source> <target>",
            Description = "Expand an archive 'source' into a file with the name 'target'",
        };

        public static Command Help = new Command
        {
            Name = "help",
            Syntax = "help",
            Description = "Show help"
        };

        public sealed class Command
        {
            public string? Name { get; set; }
            public string? Syntax { get; set; }
            public string? Description { get; set; }

            public override string ToString() => $"{Syntax,-30}{Description}";
        }
    }
}
