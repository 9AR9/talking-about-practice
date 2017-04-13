using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.CompareTheTriplets
{
    [TestFixture]
    public class Solution
    {
        public static string CompareTheTriplets(int[] arrA, int[] arrB)
        {
            int size = arrA.Length;
            if (arrB.Length < size) size = arrB.Length;

            int pointsA = 0, pointsB = 0;
            for (var i = 0; i < size; i++)
            {
                if (arrA[i] == arrB[i]) continue;
                if (arrA[i] > arrB[i]) pointsA++;
                else pointsB++;
            }

            return pointsA + " " + pointsB;
        }


        [Test]
        public void ShouldCompareTheTriplets()
        {
            int[] aliceNumbers = { 5, 6, 7 };
            int[] bobNumbers = { 3, 6, 10 };

            Assert.That(CompareTheTriplets(aliceNumbers, bobNumbers), Is.EqualTo("1 1"));
        }
    }
}