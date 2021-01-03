# How to build and run
To build and use the C# implementation, you should have [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) installed. 

1. Go to `c-sharp` directory:
   ```bash
   cd c-sharp
   ```
1. Build the project:
   ```bash
    dotnet build --configuration Release  
   ```
1. Use it:
   ```bash
   # compress a file
   dotnet build/net5.0/Huffman.Cli.dll compress path/to/file.txt path/to/archive.huffman
   
   # expand a compressed file
   dotnet build/net5.0/Huffman.Cli.dll expand path/to/archive.huffman path/to/expanded/file.txt
   ```
