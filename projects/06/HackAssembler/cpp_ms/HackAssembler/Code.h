#pragma once
#include <string>
#include <unordered_map>
#include <iostream>

class Code {
public:
  Code();
  ~Code();
  // Interfaces
  static std::string dest(std::string);
  static std::string comp(std::string);
  static std::string jump(std::string);

private:
  static std::unordered_map<std::string, std::string> destMap;
  static std::unordered_map<std::string, std::string> compMap;
  static std::unordered_map<std::string, std::string> jumpMap;
};