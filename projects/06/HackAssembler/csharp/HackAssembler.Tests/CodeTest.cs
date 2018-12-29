using System;
using Xunit;
using HackAssembler;

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
            string comp = "0";
            Assert.Equal("0101010", Code.Comp(comp));
            comp = "1";
            Assert.Equal("0111111", Code.Comp(comp));
            comp = "-1";
            Assert.Equal("0111010", Code.Comp(comp));
            comp = "D";
            Assert.Equal("0001100", Code.Comp(comp));
            comp = "A";
            Assert.Equal("0110000", Code.Comp(comp));
            comp = "M";
            Assert.Equal("1110000", Code.Comp(comp));
            comp = "!D";
            Assert.Equal("0001101", Code.Comp(comp));
            comp = "!A";
            Assert.Equal("0110001", Code.Comp(comp));
            comp = "!M";
            Assert.Equal("1110001", Code.Comp(comp));
            comp = "-D";
            Assert.Equal("0001111", Code.Comp(comp));
            comp = "-A";
            Assert.Equal("0110011", Code.Comp(comp));
            comp = "-M";
            Assert.Equal("1110011", Code.Comp(comp));
            comp = "D+1";
            Assert.Equal("0011111", Code.Comp(comp));
            comp = "A+1";
            Assert.Equal("0110111", Code.Comp(comp));
            comp = "M+1";
            Assert.Equal("1110111", Code.Comp(comp));
            comp = "D-1";
            Assert.Equal("0001110", Code.Comp(comp));
            
            comp = "A-1";
            Assert.Equal("0110010", Code.Comp(comp));
            comp = "D+A";
            Assert.Equal("0000010", Code.Comp(comp));
            comp = "D-A";
            Assert.Equal("0010011", Code.Comp(comp));
            comp = "A-D";
            Assert.Equal("0000111", Code.Comp(comp));
            comp = "D&A";
            Assert.Equal("0000000", Code.Comp(comp));
            comp = "D|A";
            Assert.Equal("0010101", Code.Comp(comp));
            
            comp = "M-1";
            Assert.Equal("1110010", Code.Comp(comp));
            comp = "D+M";
            Assert.Equal("1000010", Code.Comp(comp));
            comp = "D-M";
            Assert.Equal("1010011", Code.Comp(comp));
            comp = "M-D";
            Assert.Equal("1000111", Code.Comp(comp));
            comp = "D&M";
            Assert.Equal("1000000", Code.Comp(comp));
            comp = "D|M";
            Assert.Equal("1010101", Code.Comp(comp));
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