using System.Numerics;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Implementation.ExtraLongFactorials
{
    [TestFixture]
    public class Solution
    {
        static BigInteger GenerateFactorial(int n)
        {
            if (n == 0) return 1;
            BigInteger factorial = n;

            // Cycle backwards through all positive integers preceding the starting number,
            // multiplying along the way
            for (int i = n - 1; i > 0; i--)
            {
                factorial *= i;
            }

            return factorial;
        }




        [Test]
        public void ShouldGenerateFactorial()
        {
            Assert.That(GenerateFactorial(4), Is.EqualTo((BigInteger)24));
        }
        [Test]
        public void ShouldGenerateFactorialAsVeryLargeNumber()
        {
            Assert.That(GenerateFactorial(25), Is.EqualTo(BigInteger.Parse("15511210043330985984000000")));
        }
    }
}