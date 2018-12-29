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
            
        }

        // Interface
        public void AddEntry(string symbol, int address)
        {
            if (!Contains(symbol))
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