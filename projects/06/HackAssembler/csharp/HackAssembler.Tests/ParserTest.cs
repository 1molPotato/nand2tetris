using System;
using Xunit;
using HackAssembler;

namespace HackAssembler.UnitTests
{
    public class ParserTest
    {
        private static readonly string _testFileDirectory = "../../../../../unitTestFiles/";
        [Fact]
        public void SkipWhiteSpacesTest()
        {
            string testFile = _testFileDirectory + "empty.asm";
            using (var parser = new Parser(testFile))
            {
                Assert.False(parser.HasMoreCommands());
            }  
            
            testFile = _testFileDirectory + "whiteSpaces.asm";
            using (var parser = new Parser(testFile))
            {
                Assert.False(parser.HasMoreCommands());
            }        
            
            testFile = _testFileDirectory + "comment.asm";
            using (var parser = new Parser(testFile))
            {
                Assert.False(parser.HasMoreCommands());
            }

            testFile = _testFileDirectory + "hasMoreCommands.asm";
            using (var parser = new Parser(testFile))
            {
                Assert.True(parser.HasMoreCommands());
            }
        }

        [Fact]
        public void CommandTypeTest()
        {
            string testFile = _testFileDirectory + "commandType.asm";
            using (var parser = new Parser(testFile))
            {
                for (int i = 0; i < 3; ++i)
                {
                    // Arrange
                    int expected = i;

                    // Act
                    parser.Advance();
                    int actual = parser.CommandType();

                    // Assert
                    Assert.Equal(expected, actual);
                }                
            }            
        }

        [Fact]
        public void GetSymbolTest()
        {
            string testFile = _testFileDirectory + "getSymbols.asm";
            using (var parser = new Parser(testFile))
            {
                string[] expecteds = {"100", "sum", "LOOP"};
                foreach (var expected in expecteds)
                {
                    parser.Advance();
                    string actual = parser.GetSymbol();
                    Assert.Equal(expected, actual);
                }
                parser.Advance();
                var ex = Assert.Throws<InvalidOperationException>(() => parser.GetSymbol());
                Assert.Equal("GetSymbol() should be called only when CommandType() is A_COMMAND or L_COMMAND", ex.Message);
            }
        }

        [Fact]
        public void GetCInsPartsTest()
        {
            string testFile = _testFileDirectory + "cCommand.asm";
            using (var parser = new Parser(testFile))
            {
                string expected = "D", expected2 = "M+1", expected3 = "JGT";
                parser.Advance();
                string actual = parser.GetDest(), actual2 = parser.GetComp(), actual3 = parser.GetJump();
                Assert.Equal(expected, actual);
                Assert.Equal(expected2, actual2);
                Assert.Equal(expected3, actual3);

                expected2 = "0";
                expected3 = "JMP";
                parser.Advance();
                actual = parser.GetDest();
                actual2 = parser.GetComp();
                actual3 = parser.GetJump();
                Assert.Null(actual);
                Assert.Equal(expected2, actual2);
                Assert.Equal(expected3, actual3);

                expected = "AMD";
                expected2 = "!D";
                parser.Advance();                
                actual = parser.GetDest();
                actual2 = parser.GetComp();
                actual3 = parser.GetJump();
                Assert.Equal(expected, actual);
                Assert.Equal(expected2, actual2);
                Assert.Null(actual3);
            }
        }
    }
}
