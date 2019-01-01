#pragma once
#include <iostream>
#include <string>
#include <unordered_map>

class SymbolTable
{
public:
  SymbolTable();
  ~SymbolTable();
  // Interfaces
  void addEntry(const std::string&, int);
  bool contains(const std::string&);
  int getAddress(const std::string&);

private:
  void init();

private:
  std::unordered_map<std::string, int> symbolTable;
};

