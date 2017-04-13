using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.Staircase
{
    [TestFixture]
    public class Solution
    {
        public static void PrintStaircase(int size)
        {
            string prefix, hashes;
            for (var i = 1; i <= size; i++)
            {
                prefix = new string(' ', size - i);
                hashes = new string('#', i);
                Console.WriteLine(prefix + hashes);
            }
        }


        [Test]
        public void ShouldCalculatePlusMinusAndZeroFractions()
        {
            var consoleOutput = new ConsoleOutput();
            int size = 6;

            PrintStaircase(size);
            string[] outputLines = consoleOutput.GetOutputLines();
            
            Assert.That(outputLines[0], Is.EqualTo("     #"));
            Assert.That(outputLines[1], Is.EqualTo("    ##"));
            Assert.That(outputLines[2], Is.EqualTo("   ###"));
            Assert.That(outputLines[3], Is.EqualTo("  ####"));
            Assert.That(outputLines[4], Is.EqualTo(" #####"));
            Assert.That(outputLines[5], Is.EqualTo("######"));
        }
    }
}