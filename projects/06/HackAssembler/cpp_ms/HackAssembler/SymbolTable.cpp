#include "pch.h"
#include "SymbolTable.h"

using namespace std;

SymbolTable::SymbolTable() {
  init();
}


SymbolTable::~SymbolTable() {
  // empty
}

void SymbolTable::init() {
  for (int i = 0; i < 16; ++i)
    symbolTable["R" + to_string(i)] = i;
  symbolTable["SP"] = 0;
  symbolTable["LCL"] = 1;
  symbolTable["ARG"] = 2;
  symbolTable["THIS"] = 3;
  symbolTable["THAT"] = 4;
  symbolTable["SCREEN"] = 16384;
  symbolTable["KBD"] = 24576;
}

void SymbolTable::addEntry(const string& symbol, int address) {
  symbolTable[symbol] = address;
}

bool SymbolTable::contains(const string& symbol) {
  if (symbolTable.find(symbol) != symbolTable.end())
    return true;
  return false;
}

int SymbolTable::getAddress(const string& symbol) {
  if (contains(symbol))
    return symbolTable[symbol];
  cout << "getAddress(symbol) should be called only when contanis(symbol) is true." << endl;
  exit(2);
}
