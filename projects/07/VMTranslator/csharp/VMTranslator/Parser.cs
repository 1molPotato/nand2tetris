using System;
using System.IO;
using System.Linq;

namespace VMTranslator
{
    public enum CommandTypeEnum
    {
        C_ARITHMETIC, C_PUSH, C_POP, C_LABEL, C_GOTO, C_IF, C_FUNCTION, C_RETURN, C_CALL
    }
    public class Parser : IDisposable
    {        

        // Fields
        private readonly StreamReader _reader;
        private CommandTypeEnum _commandType;
        private string _arg1;
        private int _arg2;
        
        // Constructor
        public Parser(string path)
        {
            _reader = new StreamReader(path);            
        }

        // Implement IDisposable
        public void Dispose()
        {
            _reader.Close();
        }

        // Interfaces
        public bool HasMoreCommands()
        {
            SkipWhitespaces();
            return !_reader.EndOfStream;
        }

        public void Advance()
        {
            if (HasMoreCommands())
                ParseCommand();
            else
                throw new InvalidOperationException("Advance() should be called only when HasMoreCommands() is true");
        }

        public CommandTypeEnum CommandType()
        {
            return _commandType;
        }

        public string Arg1()
        {
            if (CommandType() != CommandTypeEnum.C_RETURN)
                return _arg1;
            throw new InvalidOperationException("Arg1() should be called only when CommandType() is not C_RETURN");
        }

        public int Arg2()
        {
            CommandTypeEnum[] arr = {CommandTypeEnum.C_PUSH, CommandTypeEnum.C_POP, CommandTypeEnum.C_FUNCTION, CommandTypeEnum.C_CALL};
            if (arr.Contains(CommandType()))
                return _arg2;
            throw new InvalidOperationException("Arg2() should be called only when CommandType() is C_PUSH, C_POP, C_FUNCTION or C_CALL");
        }

        // Utilities
        private void SkipWhitespaces()
        {
            char next = (char)_reader.Peek();
            while (true)
            {                
                if (next == ' ' || next == '\t' || next == '\r' || next == '\n')
                {
                    _reader.Read();
                    next = (char)_reader.Peek();
                    continue;
                }
                if (next == '/')
                {
                    // This line is a comment
                    _reader.ReadLine();
                    next = (char)_reader.Peek();
                    continue;
                }
                break;
            }
        }

        private void ParseCommand()
        {
            var words = _reader.ReadLine().Split('/')[0].Split(' ');
            if (words.Count() == 1 && words[0] != "return")
            {
                _commandType = CommandTypeEnum.C_ARITHMETIC;
                _arg1 = words[0];
            }
            else if (words[0] == "push")
            {
                _commandType = CommandTypeEnum.C_PUSH;
                _arg1 = words[1];
                _arg2 = Int32.Parse(words[2]);
            }
            else if (words[0] == "pop")
            {
                _commandType = CommandTypeEnum.C_POP;
                _arg1 = words[1];
                _arg2 = Int32.Parse(words[2]);
            }
            else
                throw new NotImplementedException();
        }
    }
}