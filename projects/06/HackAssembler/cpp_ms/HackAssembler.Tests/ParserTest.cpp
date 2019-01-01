#include "pch.h"
#include "../HackAssembler/Parser.h"
#include "../HackAssembler/Code.h"
#include <unordered_map>
//#include "gtest/gtest.h"

using namespace std;

const string TESTFILESDIR = "../../unitTestFiles/";

TEST(HasMoreCommandsTest, HandleEmptyFile) {
  
  string path = TESTFILESDIR + "empty.asm";
  Parser parser(path);
  ASSERT_FALSE(parser.hasMoreCommands());
}

TEST(HasMoreCommandsTest, HandleWhitespace) {
  string path = TESTFILESDIR + "whiteSpaces.asm";
  Parser parser(path);
  ASSERT_FALSE(parser.hasMoreCommands());
}

TEST(HasMoreCommandsTest, HandleComments) {
  string path = TESTFILESDIR + "comment.asm";
  Parser parser(path);
  ASSERT_FALSE(parser.hasMoreCommands());
}

TEST(HasMoreCommandsTest, HandleHasMoreCommands) {
  string path = TESTFILESDIR + "hasMoreCommands.asm";
  Parser parser(path);
  ASSERT_TRUE(parser.hasMoreCommands());
}

TEST(CommandTypeTest, HandleDifferentTypes) {
  string path = TESTFILESDIR + "commandType.asm";
  Parser parser(path);
  for (int i = 0; i < 3; ++i) {
    int expected = i;
    parser.advance();
    int actual = parser.commandType();
    ASSERT_EQ(expected, actual);
  }
}

TEST(SymbolTest, HandleSymbols) {
  string path = TESTFILESDIR + "getSymbols.asm";
  Parser parser(path);
  string exps[] = { "100", "sum", "LOOP" };
  for (auto expected : exps) {
    parser.advance();
    auto actual = parser.symbol();
    ASSERT_EQ(expected, actual);
  }
  parser.advance();
  ASSERT_ANY_THROW(parser.symbol());
}

class CInsTest : public ::testing::Test {
protected:
  void SetUp() override {
    string path = TESTFILESDIR + "cCommand.asm";
    _parser = make_shared<Parser>(path);
  }

  shared_ptr<Parser> _parser;
};

TEST_F(CInsTest, HandleGetDest) {
  _parser->advance();
  string expected = "D";
  string actual = _parser->dest();
  ASSERT_EQ(expected, actual);
  
  _parser->advance();
  expected = "";
  actual = _parser->dest();
  ASSERT_EQ(expected, actual);

  _parser->advance();
  expected = "AMD";
  actual = _parser->dest();
  ASSERT_EQ(expected, actual);
}

TEST_F(CInsTest, HandleGetComp) {
  _parser->advance();
  string expected = "M+1";
  string actual = _parser->comp();
  ASSERT_EQ(expected, actual);

  _parser->advance();
  expected = "0";
  actual = _parser->comp();
  ASSERT_EQ(expected, actual);
  
  _parser->advance();
  expected = "!D";
  actual = _parser->comp();
  ASSERT_EQ(expected, actual);
}

TEST_F(CInsTest, HandleGetJump) {
  _parser->advance();
  string expected = "JGT";
  string actual = _parser->jump();
  ASSERT_EQ(expected, actual);

  _parser->advance();
  expected = "JMP";
  actual = _parser->jump();
  ASSERT_EQ(expected, actual);

  _parser->advance();
  expected = "";
  actual = _parser->jump();
  ASSERT_EQ(expected, actual);
}

TEST(CodeTest, HandleDest) {
  unordered_map<string, string> m({
    {"", "000"}, {"M", "001"}, {"D", "010"}, {"MD", "011"},
    {"A", "100"}, {"AM", "101"}, {"AD", "110"}, {"AMD", "111"}
    });
  for (auto pair : m)
    ASSERT_EQ(pair.second, Code::dest(pair.first));  
}