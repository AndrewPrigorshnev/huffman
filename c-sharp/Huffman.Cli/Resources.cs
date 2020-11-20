namespace Huffman.Cli
{
    internal static class Resources
    {
        public static string UnknownCommand(string command) =>
            $"Unknown command - `{command}`. {SeeHelp}";

        public static string Help =>
            "usage: dotnet Huffman.Cli.dll <command> [<args>]\n\n" +
            "Commands:\n\n" +
            $"\t{Commands.Compress}\n" +
            $"\t{Commands.Expand}\n" +
            $"\t{Commands.Help}";

        public static string MissingParameter(string name) =>
            $"Missing parameter '{name}'. {SeeHelp}";




        private static string SeeHelp => "See 'dotnet Huffman.Cli.dll help'.";
    }
}