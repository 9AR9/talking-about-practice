using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.AVeryBigSum
{
    [TestFixture]
    public class Solution
    {
        static long AddEmUp(int[] arr)
        {
            long sum = 0;
            foreach (int member in arr)
            {
                sum += member;
            }
            return sum;
        }


        [Test]
        public void ShouldAddLargeNumbers()
        {
            int[] set1 = { 1000000001, 1000000002, 1000000003, 1000000004, 1000000005 };

            Assert.That(AddEmUp(set1), Is.EqualTo(5000000015));
        }
    }
}