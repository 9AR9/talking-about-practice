using System;
using System.Globalization;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.PlusMinus
{
    [TestFixture]
    public class Solution
    {
        static void CalculatePlusMinusAndZeroFractions(int[] arr)
        {
            var size = arr.Length;
            int positives = 0, negatives = 0, zeroes = 0;
            foreach (var member in arr)
            {
                if (member > 0)
                {
                    positives++;
                }
                else if (member < 0)
                {
                    negatives++;
                }
                else zeroes++;
            }

            Console.WriteLine(Math.Round((decimal)positives / size, 6).ToString("#0.000000", CultureInfo.InvariantCulture));
            Console.WriteLine(Math.Round((decimal)negatives / size, 6).ToString("#0.000000", CultureInfo.InvariantCulture));
            Console.WriteLine(Math.Round((decimal)zeroes / size, 6).ToString("#0.000000", CultureInfo.InvariantCulture));
        }


        [Test]
        public void ShouldCalculatePlusMinusAndZeroFractions()
        {
            var consoleOutput = new ConsoleOutput();
            int size = 6;
            int[] values = { -4, 3, -9, 0, 4, 1 };

            CalculatePlusMinusAndZeroFractions(values);
            string[] outputLines = consoleOutput.GetOutputLines();
            
            Assert.That(outputLines[0], Is.EqualTo("0.500000"));
            Assert.That(outputLines[1], Is.EqualTo("0.333333"));
            Assert.That(outputLines[2], Is.EqualTo("0.166667"));
        }
    }
}