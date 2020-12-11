namespace Huffman.Cli
{
    internal static class Resources
    {
        public static string UnknownCommand(string command) =>
            $"Unknown command - `{command}`. {SeeHelp}";

        public static string Help =>
            "usage: dotnet Huffman.Cli.dll <command> [<args>]\n\n" +
            "Commands:\n\n" +
            $"\t{FormatCommand(Commands.Compress)}\n" +
            $"\t{FormatCommand(Commands.Expand)}\n" +
            $"\t{FormatCommand(Commands.Help)}";

        public static string MissingParameter(string name) =>
            $"Missing parameter '{name}'. {SeeHelp}";




        private static string FormatCommand(Command command)
            => $"{command.Syntax,-30}{command.Description}";

        private static string SeeHelp => "See 'dotnet Huffman.Cli.dll help'.";
    }
}
