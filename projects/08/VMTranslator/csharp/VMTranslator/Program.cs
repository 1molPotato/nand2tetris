using System;
using System.IO;

namespace VMTranslator
{
    public class Program
    {
        static string inputPath;
        public static void Main(string[] args)
        {
            if (args.Length == 1)
                inputPath = args[0];
            else
                throw new ArgumentException("Usage: dotnet run <inputFile/Directory>");
            Console.WriteLine("VMTranslator starts to run...");
            Run();  
            Console.WriteLine("VMTranslator has finished its work.");          
        }

        static void Run()
        {
            string outputFile = "";
            var attr = File.GetAttributes(inputPath);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                // handle directory
                if (!Directory.Exists(inputPath))
                    throw new ArgumentException("Invalid input path");
                var dirName = new DirectoryInfo(inputPath).Name;
                outputFile = $"{inputPath}/{dirName}.asm";
                var vmFiles = Directory.GetFiles(inputPath, "*.vm", SearchOption.TopDirectoryOnly);
                using (var codeWriter = new CodeWriter(outputFile))
                {
                    var flag = false;
                    foreach (var vmFile in vmFiles)
                    {
                        if (vmFile.EndsWith("Sys.vm"))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) codeWriter.WriteInit();
                    foreach (var vmFile in vmFiles)
                    {                        
                        using (var parser = new Parser(vmFile))
                        {
                            codeWriter.SetFileName(Path.GetFileNameWithoutExtension(vmFile));
                            Process(parser, codeWriter);
                        }
                    }
                }
            }
            else
            {
                // handle a single source file
                if (!File.Exists(inputPath) || Path.GetExtension(inputPath) != ".vm")
                    throw new ArgumentException("Invalid input path");
                outputFile = Path.ChangeExtension(inputPath, ".asm");
                using (var parser = new Parser(inputPath))
                using (var codeWriter = new CodeWriter(outputFile))
                {                    
                    Process(parser, codeWriter);
                }
            }
        }

        static void Process(Parser parser, CodeWriter codeWriter)
        {
            while (parser.HasMoreCommands())
            {
                parser.Advance();
                var type = parser.CommandType();
                if (type == CommandTypeEnum.C_ARITHMETIC)
                    codeWriter.WriteArithmetric(parser.Arg1());
                else if (type == CommandTypeEnum.C_PUSH || type == CommandTypeEnum.C_POP)
                    codeWriter.WritePushPop(type, parser.Arg1(), parser.Arg2());
                else if (type == CommandTypeEnum.C_LABEL)
                    codeWriter.WriteLabel(parser.Arg1());
                else if (type == CommandTypeEnum.C_GOTO)
                    codeWriter.WriteGoto(parser.Arg1());
                else if (type == CommandTypeEnum.C_IF)
                    codeWriter.WriteIf(parser.Arg1());
                else if (type == CommandTypeEnum.C_FUNCTION)
                    codeWriter.WriteFunction(parser.Arg1(), parser.Arg2());                
                else if (type == CommandTypeEnum.C_CALL)
                    codeWriter.WriteCall(parser.Arg1(), parser.Arg2());
                else
                    codeWriter.WriteReturn();
            }
        }
    }
}
