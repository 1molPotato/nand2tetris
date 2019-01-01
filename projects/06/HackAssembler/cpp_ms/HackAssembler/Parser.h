#pragma once
#include <string>
#include <fstream>
#include <memory>

class Parser {
  const int A_COMMAND = 0;
  const int C_COMMAND = 1;
  const int L_COMMAND = 2;
 public:
  Parser();
  Parser(std::string);
  ~Parser();
  // Interfaces
  bool hasMoreCommands();
  void advance();
  int commandType();
  std::string symbol();
  std::string dest();
  std::string comp();
  std::string jump();

 private:
  // Fields
  std::shared_ptr<std::ifstream> _ifs;
  std::string _current;
  std::string _symbol;
  std::string _dest;
  std::string _comp;
  std::string _jump;

 private:
  // Utilities
  void skipWhitespaces();
  void parseCurrent();
  std::string trim(std::string);
};

