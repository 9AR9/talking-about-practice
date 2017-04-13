using System;
using System.IO;
using NUnit.Framework;

namespace TalkingAboutPractice
{
    public class ConsoleOutput : IDisposable
    {
        private StringWriter _stringWriter;
        private TextWriter _originalOutput;

        public ConsoleOutput()
        {
            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);
        }

        public string GetOutput()
        {
            return _stringWriter.ToString();
        }

        public string[] GetOutputLines()
        {
            string[] delimiters = { "\r\n" };
            return _stringWriter.ToString().Split(delimiters, StringSplitOptions.None);
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }
    }

    [TestFixture]
    public class ConsoleOutputTests
    {
        [Test]
        public void ShouldDemonstrateAbilityToReadAndAssertAgainstConsoleOutput()
        {
            var currentConsoleOut = Console.Out;
            var consoleOutput = new ConsoleOutput();

            Console.WriteLine("Line one");
            Console.WriteLine("Line two");
            Console.Write("Line tre");
            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("Line one"));
            Assert.That(outputLines[1], Is.EqualTo("Line two"));
            Assert.That(outputLines[2], Is.EqualTo("Line tre"));
        }
    }

}