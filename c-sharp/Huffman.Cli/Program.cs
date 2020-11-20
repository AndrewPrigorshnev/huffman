using System;
using System.IO;
using Huffman.Codec;

namespace Huffman.Cli
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == string.Empty)
                PrintAndQuit(Resources.Help);

            string command = args[0]!;
            var @in = args.Length > 1 ? args[1] : null;
            var @out = args.Length > 2 ? args[2] : null;

            if (command == Commands.Compress.Name)
            {
                Compress(@in, @out);
                return;
            }

            if (command == Commands.Expand.Name)
            {
                Expand(@in, @out);
                return;
            }

            if (command == Commands.Help.Name)
                PrintAndQuit(Resources.Help);

            PrintAndQuit(Resources.UnknownCommand(command));
        }

        private static void Expand(string? @in, string? @out)
        {
            var input = GetInputStream(@in);
            var output = GetOutputStream(@out);
            Decoder.Decode(input, output);
        }

        private static void Compress(string? @in, string? @out)
        {
            var output = GetOutputStream(@out);
            Encoder.Encode(() => GetInputStream(@in), output);
        }

        private static Stream GetInputStream(string? @in)
        {
            if (string.IsNullOrEmpty(@in))
                PrintAndQuit(Resources.MissingParameter(nameof(@in)));

            return File.OpenRead(@in!);
        }

        private static Stream GetOutputStream(string? @out)
        {
            if (string.IsNullOrEmpty(@out))
                PrintAndQuit(Resources.MissingParameter(nameof(@out)));

            return File.Create(@out!);
        }

        private static void Print(string message) => Console.WriteLine(message);

        private static void PrintAndQuit(string message)
        {
            Print(message);
            Environment.Exit(1);
        }
    }
}
