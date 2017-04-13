using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.MinMaxSum
{
    [TestFixture]
    public class Solution
    {
        public static string CalculateMinAndMaxSumOfFourOutOfFiveIntegers(long[] array)
        {
            var max = array.Max();
            var min = array.Min();
            var sum = array.Sum();
            var minSum = sum - max;
            var maxSum = sum - min;

            return minSum + " " + maxSum;
        }


        [Test]
        public void ShouldCalculateMinAndMaxSumOfFourOutOfFiveIntegers()
        {
            long[] values = { 1, 2, 3, 4, 5 };
            
            Assert.That(CalculateMinAndMaxSumOfFourOutOfFiveIntegers(values), Is.EqualTo("10 14"));
        }
    }
}