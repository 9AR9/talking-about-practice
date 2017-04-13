using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.SimpleArraySum
{
    [TestFixture]
    public class Solution
    {
        public static int AddArrayOfIntegersUsingSum(int[] arr)
        {
            return arr.Sum();
        }

        public static int AddArrayOfIntegersUsingIteration(int[] arr)
        {
            var sum = 0;
            foreach (int i in arr)
                sum += i;

            return sum;
        }


        [Test]
        public void ShouldAddArrayOfIntegersUsingSum()
        {
            int[] arrayOfIntegers = { 1, 2, 3, 4, 10, 11 };

            Assert.That(AddArrayOfIntegersUsingSum(arrayOfIntegers), Is.EqualTo(31));
        }

        [Test]
        public void ShouldAddArrayOfIntegersUsingIteration()
        {
            int[] arrayOfIntegers = { 1, 2, 3, 4, 10, 11 };

            Assert.That(AddArrayOfIntegersUsingIteration(arrayOfIntegers), Is.EqualTo(31));
        }
    }
}