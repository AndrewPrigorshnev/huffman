namespace Huffman.Cli
{
    internal sealed class Command
    {
        private Command(string name, string syntax, string description)
        {
            Name = name;
            Syntax = syntax;
            Description = description;
        }

        public string Name { get; }
        public string Syntax { get; }
        public string Description { get; }

        public static readonly Command Compress = new(
            name: "compress",
            syntax: "compress <source> <target>",
            description: "Compress a 'source' file into an archive with the name 'target'"
        );

        public static readonly Command Expand = new(
            name: "expand",
            syntax: "expand <source> <target>",
            description: "Expand an archive 'source' into a file with the name 'target'"
        );

        public static readonly Command Help = new(
            name: "help",
            syntax: "help",
            description: "Show help"
        );
    }
}
