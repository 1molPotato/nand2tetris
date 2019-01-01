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
            outputFile = Path.GetDirectoryName(inputFile) + "/" +  Path.GetFileNameWithoutExtension(inputFile) + ".hack";
            Console.WriteLine(outputFile);
            using (var writer = new StreamWriter(outputFile))
            {
                var symbolTable = new SymbolTable();
                FirstPass(inputFile, symbolTable);
                MainProcess(writer, inputFile, symbolTable);                
            }
        }

        static void FirstPass(string inputFile, SymbolTable symbolTable)
        {
            using (var parser = new Parser(inputFile))
            {
                int counter = 0;
                while (parser.HasMoreCommands())
                {
                    parser.Advance();
                    if (parser.CommandType() == Parser.L_COMMAND)
                        symbolTable.AddEntry(parser.GetSymbol(), counter);
                    else
                        counter++;                  
                }
            }
        }

        static void MainProcess(StreamWriter writer, string inputFile, SymbolTable symbolTable)
        {
            using (var parser = new Parser(inputFile))
            {
                string line = "";
                int counter = 16;
                while (parser.HasMoreCommands())
                {
                    parser.Advance();
                    if (parser.CommandType() == Parser.A_COMMAND)
                    {
                        int address = 0;
                        string symbol = parser.GetSymbol();
                        if (!Int32.TryParse(symbol, out address))
                        {
                            if (symbolTable.Contains(symbol))
                                address = symbolTable.GetAddress(parser.GetSymbol()); 
                            else
                            {
                                address = counter++;
                                symbolTable.AddEntry(symbol, address);
                            }                                
                        }                                                                               
                        line = Convert.ToString(address, 2).PadLeft(16, '0');
                    }
                    else if (parser.CommandType() == Parser.C_COMMAND)
                    {
                        var dest = Code.Dest(parser.GetDest());
                        var comp = Code.Comp(parser.GetComp());
                        var jump = Code.Jump(parser.GetJump());
                        line = "111" + comp + dest + jump;
                    }
                    else
                        continue; // skip labels
                    writer.WriteLine(line);
                }               
            }
        }
    }
}
