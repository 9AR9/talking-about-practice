using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Warmup.DiagonalDifference
{
    [TestFixture]
    public class Solution
    {
        static int DifferenceTheDiagonals(int squareSize, int[][] arr)
        {
            int primarySum = 0, secondarySum = 0;

            for (var i = 0; i < squareSize; i++)
            {
                for (var p = 0; p < squareSize; p++)
                {
                    if (p == i) primarySum += arr[i][p];
                    if (p == (squareSize - 1 - i)) secondarySum += arr[i][p];
                }
            }

            return Math.Abs(primarySum - secondarySum);
        }


        [Test]
        public void ShouldDifferenceTheDiagonals()
        {
            int size = 3;
            int[][] lines = new int[size][];
            lines[0] = new []{ 11, 2, 4 };
            lines[1] = new []{ 4, 5, 6 };
            lines[2] = new []{ 10, 8, -12 };

            Assert.That(DifferenceTheDiagonals(size, lines), Is.EqualTo(15));
        }
    }
}