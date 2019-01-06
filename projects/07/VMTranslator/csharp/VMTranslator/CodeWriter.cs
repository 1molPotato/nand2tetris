using System;
using System.IO;
using System.Collections.Generic;

namespace VMTranslator
{
    public class CodeWriter : IDisposable
    {
        // Fields
        private readonly StreamWriter _writer;
        private readonly string _fileName;
        private int _labelCounter;
        
        // Constructor
        public CodeWriter(string path)
        {
            _writer = new StreamWriter(path);
            _fileName = Path.GetFileNameWithoutExtension(path);
            _labelCounter = 0;
        }

        // Implement IDisposable
        public void Dispose()
        {
            _writer.Close();
        }

        // Interfaces
        public void WriteArithmetric(string command)
        {
            Write(string.Format($"// {command}"), false);
            switch (command)
            {
                case "add": WriteAdd(); break;
                case "sub": WriteSub(); break;
                case "neg": WriteNeg(); break;
                case "eq": WriteEq(); break;
                case "gt": WriteGt(); break;
                case "lt": WriteLt(); break;
                case "and": WriteAnd(); break;
                case "or": WriteOr(); break;
                case "not": WriteNot(); break;
                default: break;
            }
        }

        public void WritePushPop(CommandTypeEnum commandType, string segment, int index)
        {
            // string comment = "//";            
            // string assembly = "";
            if (commandType == CommandTypeEnum.C_PUSH)
                WritePush(segment, index);
            else if (commandType == CommandTypeEnum.C_POP)
                WritePop(segment, index);                        
            else
                throw new InvalidOperationException("WritePushPop() should be called only when CommandType is C_PUSH or C_POP");
        }

        // Utilities  
        private void Write(string code, bool indent = true)    
        {
            if (indent)
                code = "\t" + code;
            _writer.WriteLine(code);
        }  

        private void WritePush(string segment, int i)
        {
            Write($"// push {segment} {i}", false);
            string register = RegisterName(segment, i);
            if (segment == "constant")
            {
                Write($"@{i}");
                Write("D=A"); // D = i
            }
            else if (segment == "temp" || segment == "pointer")
            {
                Write($"@{register}");
                Write("D=M"); // D = RAM[register]
            }
            else
            {
                Write($"@{i}");
                Write("D=A");
                Write($"@{register}");
                Write("A=D+M");
                Write("D=M"); // D = RAM[*segment + index]
            }
            Write("@SP");
            Write("A=M");
            Write("M=D"); // RAM[*SP] = D
            Write("@SP");
            Write("M=M+1"); // SP++
        }

        private void WritePop(string segment, int i)
        {
            Write($"// pop {segment} {i}", false);
            string register = RegisterName(segment, i);
            if (segment == "temp" || segment == "pointer")
            {
                Write("@SP");
                Write("AM=M-1");
                Write("D=M");
                Write($"@{register}");
                Write("M=D");
            }
            else if (segment == "constant")
                throw new InvalidOperationException("Can't pop constant i");
            else
            {
                Write($"@{i}");
                Write("D=A");
                Write($"@{register}");
                Write("D=D+M");
                Write("@R13");
                Write("M=D"); // RAM[13] = *segment + i
                Write("@SP");
                Write("AM=M-1");
                Write("D=M"); // D = RAM[*(--SP)]
                Write("@R13");
                Write("A=M");
                Write("M=D"); // RAM[*segment + i] = RAM[*(--SP)]
            }
        }

        private string RegisterName(string segment, int i)
        {
            switch (segment)
            {
                case "local": return "LCL";
                case "argument": return "ARG";
                case "this": return "THIS";
                case "that": return "THAT";
                case "temp": return $"R{5 + i}";
                case "pointer": return $"R{3 + i}";
                default: return $"{_fileName}.{i}";
            }
        }

        private void WriteAdd()
        {
            Write("@SP");
            Write("AM=M-1");
            Write("D=M");
            Write("A=A-1");
            Write("M=D+M");
        }

        private void WriteNeg()
        {
            Write("@SP");
            Write("A=M-1");
            Write("M=-M");
        }

        private void WriteSub()
        {
            WriteNeg();
            WriteAdd();
        }

        // arithmetic compare
        private void WriteComapre(string flag)
        {
            string label1 = string.Format($"LABEL{_labelCounter++}"), 
                label2 = string.Format($"LABEL{_labelCounter++}");
            WriteSub();
            Write("@SP");
            Write("A=M-1");
            Write("D=M"); // D = RAM[*SP-1]
            Write($"@{label1}");
            Write($"D;{flag}"); // if RAM[*SP-1] = 0, jump to label1
            Write("@SP");
            Write("A=M-1");
            Write("M=0"); // RAM[*SP-1] = false
            Write($"@{label2}");
            Write("0;JMP"); // jump to label2
            Write($"({label1})", false); // (label1)
            Write("@SP");
            Write("A=M-1");
            Write("M=-1"); // RAM[*SP-1] = true
            Write($"({label2})", false); // (label2)
        }

        // arithmetic equal
        private void WriteEq()
        {
            WriteComapre("JEQ");
        }

        // arithmetic greater than
        private void WriteGt()
        {
            WriteComapre("JGT");
        }

        // arithmetic less than
        private void WriteLt()
        {
            WriteComapre("JLT");
        }

        // arithmetic and
        private void WriteAnd()
        {
            Write("@SP");
            Write("AM=M-1");
            Write("D=M"); // D = RAM[*SP-1]
            Write("A=A-1");
            Write("M=D&M"); // RAM[*SP-2] = RAM[*SP-1] & RAM[*SP-2]            
        }

        // arithmetic or
        private void WriteOr()
        {
            Write("@SP");
            Write("AM=M-1");
            Write("D=M"); // D = RAM[*SP-1]
            Write("A=A-1");
            Write("M=D|M"); // RAM[*SP-2] = RAM[*SP-1] | RAM[*SP-2]
        }

        private void WriteNot()
        {
            Write("@SP");
            Write("A=M-1");
            Write("M=!M"); // RAM[*SP-1] = !RAM[*SP-1]
        }
    }
}