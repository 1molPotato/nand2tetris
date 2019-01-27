using System;
using System.IO;
using Xunit;
using VMTranslator;

namespace VMTranslator.Tests
{

    public class BranchingHandlingTest
    {
        [Fact]
        public void BasicLoopTest()
        {
            string path = "../../../../../../ProgramFlow/BasicLoop/BasicLoop.vm";
            string[] args = {path};
            Program.Main(args);
            Assert.True(File.Exists(Path.ChangeExtension(path, ".asm")));
        }

        [Fact]
        public void FibonacciSeriesTest()
        {
            string path = "../../../../../../ProgramFlow/FibonacciSeries/FibonacciSeries.vm";
            string[] args = {path};
            Program.Main(args);
            Assert.True(File.Exists(Path.ChangeExtension(path, ".asm")));
        }
    }

    public class FunctionCallsTest
    {
        [Fact]
        public void FibonacciElementTest()
        {
            string path = "../../../../../../FunctionCalls/FibonacciElement/";
            string[] args = {path};
            Program.Main(args);
            Assert.True(File.Exists($"{path}FibonacciElement.asm"));
        }

        [Fact]
        public void SimpleFunctionTest()
        {
            string path = "../../../../../../FunctionCalls/SimpleFunction/SimpleFunction.vm";
            string[] args = {path};
            Program.Main(args);
            Assert.True(File.Exists(Path.ChangeExtension(path, ".asm")));
        }
    }
}