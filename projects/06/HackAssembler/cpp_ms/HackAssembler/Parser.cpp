#include "pch.h"
#include "Parser.h"
#include <stdexcept>

using namespace std;

Parser::Parser() {
  // empty
}

Parser::Parser(string inputFile) {
  if (inputFile != "")
    _ifs = make_shared<ifstream>(inputFile);
  if (!_ifs->good())
    throw "Invalid input file path.";
}

Parser::~Parser() {
  if (_ifs != nullptr)
    _ifs->close();
}

bool Parser::hasMoreCommands() {
  skipWhitespaces();
  return _ifs->peek() != EOF;
}

void Parser::advance() {
  if (hasMoreCommands())
    parseCurrent();
}

int Parser::commandType() {
  if (_current[0] == '@')
    return A_COMMAND;
  else if (_current[0] == '(')
    return L_COMMAND;
  return C_COMMAND;
}

string Parser::symbol() {
  if (commandType() == A_COMMAND || commandType() == L_COMMAND)
    return _symbol;
  throw "symbol() should be called only when commandType() is A_COMMAND or L_COMMAND";
}

string Parser::dest() {
  if (commandType() == C_COMMAND)
    return _dest;
  throw "dest() should be called only when commandType() is C_COMMAND";
}

string Parser::comp() {
  if (commandType() == C_COMMAND)
    return _comp;
  throw "comp() should be called only when commandType() is C_COMMAND";
}

string Parser::jump() {
  if (commandType() == C_COMMAND)
    return _jump;
  throw "jump() should be called only when commandType() is C_COMMAND";
}

void Parser::skipWhitespaces() {
  char c = (char)_ifs->peek();
  while (1) {
    if (c == ' ' || c == '\t' || c == '\r' || c == '\n') {
      _ifs->get();
      c = _ifs->peek();
      continue;
    } else if (c == '/') {
      while (c != '\n' && !_ifs->eof())
        _ifs->get(c);
      c = _ifs->peek();
      continue;
    }
    break;
  }
}

void Parser::parseCurrent() {
  getline(*_ifs, _current);
  _current = trim(_current);
  if (commandType() == A_COMMAND)
    _symbol = _current.substr(1);
  else if (commandType() == L_COMMAND)
    _symbol = _current.substr(1, _current.size() - 2);
  else {
    _dest = "";
    _jump = "";
    size_t pos = 0;
    auto destEnd = _current.find("=", pos);
    if (destEnd != string::npos) {
      _dest = _current.substr(0, destEnd);
      pos = destEnd + 1;
    }
    auto compEnd = _current.find(";", pos);
    if (compEnd != string::npos) {
      _comp = _current.substr(pos, compEnd - pos);     
      pos = compEnd + 1;
      _jump = _current.substr(pos);
    } else {
      _comp = _current.substr(pos);
    }
  }
}

string Parser::trim(string line) {
  string ret = "";
  for (int i = 0; line[i] != '\0'; ++i) {
    if (line[i] == '/') break;
    if (line[i] == ' ' || line[i] == '\r' || line[i] == '\t')
      continue;
    ret += line[i];
  }
  return ret;
}

