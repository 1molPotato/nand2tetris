using System;
using Xunit;
using HackAssembler;
using System.Collections.Generic;

namespace HackAssembler.UnitTests
{
    public class CodeTest
    {
        [Fact]
        public void DestTest()
        {
            string dest = null;
            Assert.Equal("000", Code.Dest(dest));
            dest = "M";
            Assert.Equal("001", Code.Dest(dest));
            dest = "D";
            Assert.Equal("010", Code.Dest(dest));
            dest = "MD";
            Assert.Equal("011", Code.Dest(dest));
            dest = "A";
            Assert.Equal("100", Code.Dest(dest));
            dest = "AM";
            Assert.Equal("101", Code.Dest(dest));
            dest = "AD";
            Assert.Equal("110", Code.Dest(dest));
            dest = "AMD";
            Assert.Equal("111", Code.Dest(dest));
        }

        [Fact]
        public void CompTest()
        {
            var compTable = new Dictionary<string, string>()
            {
                {"0", "0101010"}, {"1", "0111111"}, {"-1", "0111010"}, {"D", "0001100"},
                {"A", "0110000"}, {"M", "1110000"}, {"!D", "0001101"}, {"!A", "0110001"},
                {"!M", "1110001"}, {"-D", "0001111"}, {"-A", "0110011"}, {"-M", "1110011"},
                {"D+1", "0011111"}, {"A+1", "0110111"}, {"M+1", "1110111"}, {"D-1", "0001110"},
                {"A-1", "0110010"}, {"D+A", "0000010"}, {"D-A", "0010011"}, {"A-D", "0000111"},
                {"D&A", "0000000"}, {"D|A", "0010101"}, {"M-1", "1110010"}, {"D+M", "1000010"},
                {"D-M", "1010011"}, {"M-D", "1000111"}, {"D&M", "1000000"}, {"D|M", "1010101"}
            };
            foreach (var comp in compTable)
            {
                Assert.Equal(comp.Value, Code.Comp(comp.Key));
            }
        }

        [Fact]
        public void JumpTest()
        {
            string jump = null;
            Assert.Equal("000", Code.Jump(jump));
            jump = "JGT";
            Assert.Equal("001", Code.Jump(jump));
            jump = "JEQ";
            Assert.Equal("010", Code.Jump(jump));
            jump = "JGE";
            Assert.Equal("011", Code.Jump(jump));
            jump = "JLT";
            Assert.Equal("100", Code.Jump(jump));
            jump = "JNE";
            Assert.Equal("101", Code.Jump(jump));
            jump = "JLE";
            Assert.Equal("110", Code.Jump(jump));
            jump = "JMP";
            Assert.Equal("111", Code.Jump(jump));
        }
    }
}