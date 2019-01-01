// main.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "Parser.h"
#include "Code.h"
#include "SymbolTable.h"
#include <string>
#include <iostream>
#include <fstream>
#include <bitset>

using namespace std;



void firstPass(const string& inputFile, SymbolTable& table) {
  Parser parser(inputFile);
  int counter = 0;
  while (parser.hasMoreCommands()) {
    parser.advance();
    if (parser.commandType() == 2)
      // L_COMMAND
      table.addEntry(parser.symbol(), counter);
    else 
      counter++;    
  }
}

void translate(ofstream& writer, const string& inputFile, SymbolTable& table) {
  Parser parser(inputFile);
  string line;
  int counter = 16;
  while (parser.hasMoreCommands()) {
    parser.advance();
    if (parser.commandType() == 0) {
      // A_COMMAND
      auto symbol = parser.symbol();
      int address = 0;
      if (symbol[0] - '0' >= 0 && symbol[0] - '0' <= 9)
        address = stoi(symbol);
      else if (table.contains(symbol))
        address = table.getAddress(symbol);
      else {
        address = counter++;
        table.addEntry(symbol, address);
      }
      line = bitset<16>(address).to_string();
    } else if (parser.commandType() == 1) {
      // C_COMMAND
      auto dest = Code::dest(parser.dest());
      auto comp = Code::comp(parser.comp());
      auto jump = Code::jump(parser.jump());
      line = "111" + comp + dest + jump;
    } else
      continue; // skip L_COMMAND
    writer << line << '\n';
  }
}


int main(int argc, char **argv) {
  cout << "Hack Assembler starts to work...\n";
  cout << "argc: " << argc << endl;
  string inputFile;
  if (argc == 2)
    inputFile = argv[1];
  else
    return 1;
  SymbolTable symbolTable;
  firstPass(inputFile, symbolTable);
  string outputFile(inputFile.begin(), inputFile.end() - 3);
  outputFile += "hack";
  ofstream writer(outputFile);
  if (writer.good())
    translate(writer, inputFile, symbolTable);
  writer.close();

  return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
