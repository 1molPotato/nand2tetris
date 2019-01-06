using System;
using System.IO;

namespace VMTranslator
{
    class Program
    {
        static string inputFile;
        static void Main(string[] args)
        {
            if (args.Length == 1)
                inputFile = args[0];
            else
                throw new ArgumentException("Usage: dotnet run <inputFile>");
            Console.WriteLine("VMTranslator starts to run...");
            Run();            
        }

        static void Run()
        {
            if (!File.Exists(inputFile) || Path.GetExtension(inputFile) != ".vm") 
                throw new ArgumentException("Invalid input file");
            string outputFile = Path.GetDirectoryName(inputFile) + "/" +  Path.GetFileNameWithoutExtension(inputFile) + ".asm";
            using (var parser = new Parser(inputFile))
            using (var codeWriter = new CodeWriter(outputFile))
            {
                while (parser.HasMoreCommands())
                {
                    parser.Advance();   
                    var type = parser.CommandType();                 
                    if (type == CommandTypeEnum.C_ARITHMETIC)
                        codeWriter.WriteArithmetric(parser.Arg1());
                    else if (type == CommandTypeEnum.C_PUSH || type == CommandTypeEnum.C_POP)
                        codeWriter.WritePushPop(type, parser.Arg1(), parser.Arg2());
                    else
                        throw new NotImplementedException();
                }
            }
        }
    }
}
