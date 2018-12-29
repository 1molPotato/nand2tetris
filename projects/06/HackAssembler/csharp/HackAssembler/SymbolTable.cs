using System;
using System.Collections.Generic;

namespace HackAssembler
{
    class SymbolTable
    {
        private readonly Dictionary<string, int> _symbolTable;

        // Constructor
        public SymbolTable()
        {
            _symbolTable = new Dictionary<string, int>();
            Initialise();
        }

        private void Initialise()
        {
            for (int i = 0; i < 16; ++i)
                _symbolTable[string.Format($"R{i}")] = i;
            var predefiend = new Dictionary<string, int>()
            {
                {"SCREEN", 16384}, {"KBD", 24576}, {"SP", 0}, {"LCL", 1}, {"ARG", 2},
                {"THIS", 3}, {"THAT", 4}, {"LOOP", 4}, {"STOP", 18}, {"END", 22}
            };
            foreach (var pair in predefiend)
                _symbolTable[pair.Key] = pair.Value;
        }

        // Interface
        public void AddEntry(string symbol, int address)
        {
            _symbolTable[symbol] = address;
        }

        public bool Contains(string symbol)
        {
            return _symbolTable.ContainsKey(symbol);
        }

        public int GetAddress(string symbol)
        {
            if (Contains(symbol))
                return _symbolTable[symbol];
            else
                throw new ArgumentException(string.Format($"The symbol {symbol} is not in SymbolTable."));
        }
    }
}