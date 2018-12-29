using System;
using System.IO;
using System.Linq;

namespace HackAssembler
{
    public class Parser : IDisposable
    {
        private readonly string _input = "test.asm";
        private readonly StreamReader _reader;
        private string _current;
        private string _symbol;
        private string _dest;
        private string _comp;
        private string _jump;

        // Constructor
        public Parser() : this("")
        {
            // empty
        }

        public Parser(string input)
        {
            if (input != "")
                _input = input;            
            // Open a stream reader
            _reader = new StreamReader(_input);
            // Initialise fields
            // _current = null;
            // _symbol = null;
            // _dest = null;
            // _comp = null;
            // _jump = null;
        }

        // Implement IDisposable
        public void Dispose()
        {
            // Close the stream reader
            _reader.Close();
        }

        // Interfaces
        public bool HasMoreCommands()
        {
            SkipWhiteSpaces();
            return _reader.Peek() > -1;            
        }

        public void Advance()
        {            
            if (HasMoreCommands())            
                ParseCurrent();
        }

        // return 0 for A_COMMAND, 1 for C_COMMAND, 2 for L_COMMAND
        public int CommandType()
        {
            if (_current.StartsWith('@'))
                return 0;
            else if (_current.StartsWith('('))
                return 2;
            return 1;
        }

        public string GetSymbol()
        {
            if (CommandType() == 0 || CommandType() == 2)
                return _symbol;
            throw new InvalidOperationException("GetSymbol() should be called only when CommandType() is A_COMMAND or L_COMMAND"); 
        }

        public string GetDest()
        { 
            if (CommandType() == 1)
                return _dest;
            throw new InvalidOperationException("GetDest() should be called only when CommandType() is C_COMMAND");            
        }

        public string GetComp()
        {
            if (CommandType() == 1)
                return _comp;
            throw new InvalidOperationException("GetComp() should be called only when CommandType() is C_COMMAND");              
        }

        public string GetJump()
        {
            if (CommandType() == 1)
                return _jump;
            throw new InvalidOperationException("GetJump() should be called only when CommandType() is C_COMMAND"); 
        }

        // Utilities
        private void SkipWhiteSpaces()
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

        private void ParseCurrent()
        {
            _current = TrimWhiteSpacesAndComments(_reader.ReadLine());
            if (CommandType() == 1)
            {
                _dest = null;
                _jump = null;
                char[] toSplit = {'=', ';'};
                var parts = _current.Split(toSplit);
                if (parts.Count() == 1)
                    _comp = _current;
                else if (parts.Count() == 3)
                {
                    _dest = parts[0];
                    _comp = parts[1];
                    _jump = parts[2];
                }
                else if (_current.Contains('='))
                {
                    _dest = parts[0];
                    _comp = parts[1];
                }
                else
                {
                    _comp = parts[0];
                    _jump = parts[1];
                }
            }
            else if (CommandType() == 0)
                _symbol = _current.Substring(1);
            else
                _symbol = _current.Substring(1, _current.Length - 2);          
        }

        private string TrimWhiteSpacesAndComments(string line)
        {
            string ret = line.Split('/')[0];
            return new string(ret.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

    }
}