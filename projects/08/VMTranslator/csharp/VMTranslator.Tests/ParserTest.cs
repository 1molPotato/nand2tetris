using System;
using Xunit;
using VMTranslator;

namespace VMTranslator.Tests
{
    public class ParserTest
    {
        const string PATH = "../../../../../TestFiles/";
        [Fact]
        public void SkipWhiteTest()
        {
            string file = PATH + "white.vm";
            var parser = new Parser(file);
            Assert.False(parser.HasMoreCommands());
        }

        [Fact]
        public void HasMoreCommandsTest()
        {
            string file = PATH + "hasMore.vm";
            var parser = new Parser(file);
            Assert.True(parser.HasMoreCommands());
        }

        [Fact]
        public void AdvanceTest()
        {
            string file = PATH + "advance.vm";
            var parser = new Parser(file);
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_ARITHMETIC, parser.CommandType());
            Assert.Equal("add", parser.Arg1());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_ARITHMETIC, parser.CommandType());
            Assert.Equal("sub", parser.Arg1());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_ARITHMETIC, parser.CommandType());
            Assert.Equal("not", parser.Arg1());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_POP, parser.CommandType());
            Assert.Equal("local", parser.Arg1());
            Assert.Equal(3, parser.Arg2());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_PUSH, parser.CommandType());
            Assert.Equal("constant", parser.Arg1());
            Assert.Equal(9, parser.Arg2());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_FUNCTION, parser.CommandType());
            Assert.Equal("mult", parser.Arg1());
            Assert.Equal(2, parser.Arg2());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_LABEL, parser.CommandType());
            Assert.Equal("LOOP", parser.Arg1());
            parser.Advance();
            Assert.Equal(CommandTypeEnum.C_RETURN, parser.CommandType());
            Assert.False(parser.HasMoreCommands());
        }
    }
}
