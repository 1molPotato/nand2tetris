using System;
using System.IO;

namespace HackAssembler
{
    class Program
    {
        static string[] _args;
        static void Main(string[] args)
        {            
            _args = args;
            Console.WriteLine("HackAssembler starts to run...");
            Run();
        }

        static void Run()
        {
            string inputFile = "", outputFile = "";
            if (_args.Length != 1)
                throw new ArgumentException("Use the assembler like: dotnet run <inputFile>");
            inputFile = _args[0];
            Console.WriteLine(Directory.GetCurrentDirectory());
            outputFile = Path.GetDirectoryName(inputFile) + "/" +  Path.GetFileNameWithoutExtension(inputFile) + ".hack";
            Console.WriteLine(outputFile);
            using (var writer = new StreamWriter(outputFile))
            {
                var symbolTable = new SymbolTable();
                //FirstPass(inputFile, symbolTable);
                MainProcess(writer, inputFile, symbolTable);                
            }
        }

        static void FirstPass(string inputFile, SymbolTable symbolTable)
        {
            using (var parser = new Parser(inputFile))
            {
                int counter = 16;
                while (parser.HasMoreCommands())
                {
                    parser.Advance();
                    if (parser.CommandType() == 1)
                        continue; // skip when come across C_COMMAND
                    int address = 0;
                    string symbol = parser.GetSymbol();
                    if (parser.CommandType() == 0)                                            
                        if (Int32.TryParse(symbol, out address))
                            continue; // skip if it is a number
                    if (!symbolTable.Contains(symbol))
                        symbolTable.AddEntry(symbol, counter++);                    
                }
            }
        }

        static void MainProcess(StreamWriter writer, string inputFile, SymbolTable symbolTable)
        {
            using (var parser = new Parser(inputFile))
            {
                string line = "";
                while (parser.HasMoreCommands())
                {
                    parser.Advance();
                    if (parser.CommandType() == 0)
                    {
                        int address = 0;
                        if (!Int32.TryParse(parser.GetSymbol(), out address))                        
                            address = symbolTable.GetAddress(parser.GetSymbol());                            
                        line = Convert.ToString(address, 2).PadLeft(16, '0');
                    }
                    else if (parser.CommandType() == 1)
                    {
                        var dest = Code.Dest(parser.GetDest());
                        var comp = Code.Comp(parser.GetComp());
                        var jump = Code.Jump(parser.GetJump());
                        line = string.Format($"111{comp}{dest}{jump}");
                    }
                    else
                        continue;
                    writer.WriteLine(line);
                }               
            }
        }
    }
}
