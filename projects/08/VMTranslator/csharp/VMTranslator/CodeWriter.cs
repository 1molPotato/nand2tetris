using System;
using System.IO;
using System.Collections.Generic;

namespace VMTranslator
{
    public class CodeWriter : IDisposable
    {
        // Fields
        private readonly StreamWriter _writer;
        private string _fileName;
        private string _currentFunction;
        private int _retCounter;
        private int _labelCounter;
        
        // Constructor
        public CodeWriter(string path)
        {
            _writer = new StreamWriter(path);
            _fileName = Path.GetFileNameWithoutExtension(path);
            _currentFunction = "";
            _retCounter = 1;
            _labelCounter = 0;
        }

        // Implement IDisposable
        public void Dispose()
        {
            _writer.Close();
        }

        // Interfaces
        public void SetFileName(string fileName)
        {
            _fileName = fileName;
            //_retCounter = 1;
        }

        public void WriteInit()
        {
            Write("@256");
            Write("D=A"); // D = 256
            Write("@SP");
            Write("M=D"); // SP = 256
            WriteCall("Sys.init", 0);
        }

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

        public void WriteLabel(string label)
        {
            label = GetTrueLabel(label);
            Write($"// label {label} declaration", false);
            Write($"({label})", false);
        }

        public void WriteGoto(string label)
        {
            label = GetTrueLabel(label);
            Write($"// jump to {label}", false);
            Write($"@{label}");
            Write("0;JMP");
        }

        public void WriteIf(string label)
        {
            label = GetTrueLabel(label);
            Write($"// if pop is true, jump to {label}", false);
            Write("@SP");
            Write("AM=M-1");
            Write("D=M");
            Write($"@{label}");
            Write("D;JNE"); // if pop != false, jump to label
        }

        public void WriteFunction(string functionName, int numVars)
        {
            //_callerName = _currentFunction;
            _retCounter = 1;
            _currentFunction = functionName;
            Write($"// funtion {functionName} {numVars}", false);
            Write($"({functionName})", false);
            for (int i = 0; i < numVars; ++i)
                WritePush("local", 0); // initializes the local variables to 0s
        }

        public void WriteCall(string functionName, int numArgs)
        {
            Write($"// call {functionName} {numArgs}", false);
            var retAddrLabel = $"{_currentFunction}$ret.{_retCounter++}";
            Write($"@{retAddrLabel}");
            Write("D=A");
            Write("@SP");
            Write("A=M");
            Write("M=D");
            Write("@SP");
            Write("M=M+1"); // push retAddrLabel
            string[] ss = {"LCL", "ARG", "THIS", "THAT"};
            foreach (var label in ss)
            {
                Write($"@{label}");
                Write("D=M");
                Write("@SP");
                Write("A=M");
                Write("M=D"); // RAM[SP] = label
                Write("@SP");
                Write("M=M+1"); // push label
            }
            Write($"@{5 + numArgs}");
            Write("D=A"); // D = 5 + nArgs
            Write("@SP");
            Write("D=M-D"); // D = SP - 5 - nArgs
            Write("@ARG");
            Write("M=D"); // ARG = SP - 5 - nArgs
            Write("@SP");
            Write("D=M");
            Write("@LCL");
            Write("M=D"); // LCL = SP
            Write($"@{functionName}");
            Write("0;JMP"); // jump to appointed function
            Write($"({retAddrLabel})", false);
        }

        public void WriteReturn()
        {
            Write($"// function {_currentFunction} return", false);
            Write("@LCL");
            Write("D=M");
            Write("@R13");
            Write("M=D"); // RAM[13] = LCL
            Write("@SP");
            Write("A=M-1");
            Write("D=M"); // D = pop()
            Write("@ARG");
            Write("A=M");
            Write("M=D"); // *ARG = D = pop()
            Write("D=A"); // D = ARG
            Write("@SP");
            Write("M=D+1"); // SP = ARG + 1
            string[] ss = {"THAT", "THIS", "ARG", "LCL"};
            for (int i = 0; i < 4; ++i)
            {
                Write($"@{i + 1}");
                Write("D=A");
                Write("@R13");
                Write("A=M-D");
                Write("D=M");
                Write($"@{ss[i]}");
                Write("M=D"); // ss[i] = *(endFrame - i - 1)
            }
            Write("@5");
            Write("D=A");
            Write("@R13");
            Write("A=M-D");
            Write("A=M");
            Write("0;JMP"); // goto return address
            //_currentFunction = _callerName;
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

        private string GetTrueLabel(string label)
        {
            return $"{_currentFunction}${label}";
        }
    }
}