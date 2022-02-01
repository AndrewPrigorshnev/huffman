![C#](https://github.com/AndrewPrigorshnev/huffman/workflows/C%2Dsharp/badge.svg)

# Huffman
[Huffman coding](https://en.wikipedia.org/wiki/Huffman_coding) is a famous algorithm for lossless data compression.

To use the C# implementation, you should have [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) installed. To use it:
1. Go to the `c-sharp` directory and build the project:

   ```bash
   cd c-sharp
   dotnet build --configuration Release
   ```
1. Use it:
   
   ```bash
   # compress a file
   dotnet build/net5.0/Huffman.Cli.dll compress path/to/file.txt path/to/archive.huffman
   
   # expand a compressed file
   dotnet build/net5.0/Huffman.Cli.dll expand path/to/archive.huffman path/to/expanded/file.txt
   ```
