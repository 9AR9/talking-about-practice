using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.TimeConversion
{
    [TestFixture]
    public class Solution
    {
        public static string PrintTimeInMilitaryFormat(string time)
        {
            bool isPm = (time.IndexOf("P", StringComparison.Ordinal) > 0);
            int hour = int.Parse(time.Substring(0, 2));
            string minute = time.Substring(3, 2);
            string second = time.Substring(6, 2);

            if (isPm && hour < 12)
            {
                hour += 12;
            }
            if (!isPm && hour == 12)
            {
                hour = 0;
            }

            string hourString = hour.ToString();
            if (hourString.Length == 1) hourString = "0" + hourString;

            return hourString + ":" + minute + ":" + second;
        }


        [Test]
        public void ShouldPrintTimeInMilitaryFormat()
        {
            var input1 = "07:05:45PM";
            var input2 = "12:05:39AM";

            Assert.That(PrintTimeInMilitaryFormat(input1), Is.EqualTo("19:05:45"));
            Assert.That(PrintTimeInMilitaryFormat(input2), Is.EqualTo("00:05:39"));
        }
    }
}