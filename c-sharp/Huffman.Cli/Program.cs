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
                EnsureArgumentIsSpecified(nameof(@in), @in);
                EnsureArgumentIsSpecified(nameof(@out), @out);
                Compress(@in!, @out!);
                return;
            }

            if (command == Commands.Expand.Name)
            {
                EnsureArgumentIsSpecified(nameof(@in), @in);
                EnsureArgumentIsSpecified(nameof(@out), @out);
                Expand(@in!, @out!);
                return;
            }

            if (command == Commands.Help.Name)
                PrintAndQuit(Resources.Help);

            PrintAndQuit(Resources.UnknownCommand(command));
        }




        private static void EnsureArgumentIsSpecified(string name, string? value)
        {
            if (string.IsNullOrEmpty(value))
                PrintAndQuit(Resources.MissingParameter(name));
        }

        private static void Expand(string @in, string @out)
        {
            var input = GetInputStream(@in);
            var output = GetOutputStream(@out);
            Decoder.Decode(input, output);
        }

        private static void Compress(string @in, string @out)
        {
            var input = GetInputStream(@in);
            var output = GetOutputStream(@out);
            Encoder.Encode(input, output);
        }

        private static Stream GetInputStream(string @in) => File.OpenRead(@in);

        private static Stream GetOutputStream(string @out) => File.Create(@out);

        private static void Print(string message) => Console.WriteLine(message);

        private static void PrintAndQuit(string message)
        {
            Print(message);
            Environment.Exit(1);
        }
    }
}
