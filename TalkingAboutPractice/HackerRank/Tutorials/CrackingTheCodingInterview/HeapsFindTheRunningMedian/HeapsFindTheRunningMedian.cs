using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.HeapsFindTheRunningMedian
{
    [TestFixture]
    public class Solution
    {
        // This solution only passes the first 3 test cases on HackerRank, while the other 7 time out.
        // C# does not have a Heap data type, so one would probably need to be hand-rolled in order to
        // mock the behavior and increase the performance.
        static void FindTheRunningMedian(int[] values)
        {
            ArrayList runningList = new ArrayList();

            for (int i = 0; i < values.Length; i++)
            {
                runningList.Add(values[i]);
                if (runningList.Count < 2)
                {
                    Console.WriteLine(Math.Round((double)(int)runningList[i], 1).ToString("#.0", CultureInfo.InvariantCulture));
                }
                else
                {
                    var rawMedian = FindMedian(runningList.OfType<int>().ToArray());
                    Console.WriteLine(rawMedian.ToString("#.0", CultureInfo.InvariantCulture));
                }
            }
        }

        static double FindMedian(int[] values)
        {
            Array.Sort(values);
            int count = values.Length;
            double median;
            if (count % 2 == 0)
                median = Math.Round((((double)values[count / 2 - 1] + values[count / 2]) / 2), 1);
            else
                median = values[count / 2];

            return median;
        }


        [Test]
        public void ShouldFindTheRunningMedian()
        {
            var consoleOutput = new ConsoleOutput();
            int[] values = { 12, 4, 5, 3, 8, 7 };

            FindTheRunningMedian(values);
            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("12.0"));
            Assert.That(outputLines[1], Is.EqualTo("8.0"));
            Assert.That(outputLines[2], Is.EqualTo("5.0"));
            Assert.That(outputLines[3], Is.EqualTo("4.5"));
            Assert.That(outputLines[4], Is.EqualTo("5.0"));
            Assert.That(outputLines[5], Is.EqualTo("6.0"));
        }
    }
}