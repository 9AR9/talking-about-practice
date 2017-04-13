using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Implementation.AppleAndOrange
{
    [TestFixture]
    public class Solution
    {
        static int FindNumberOfFallenFruitOnTheHouse(int startPoint, int endPoint, int[] fallDistances, int treeLocation)
        {
            var totalFallenOnHouse = 0;
            foreach (int distance in fallDistances)
            {
                var fallLocation = treeLocation + distance;
                if (fallLocation >= startPoint && fallLocation <= endPoint)
                {
                    totalFallenOnHouse++;
                }
            }

            return totalFallenOnHouse;
        }

        [Test]
        public void ShouldFindNumberOfFallenFruitOnTheHouse()
        {
            int startPoint = 7;
            int endPoint = 11;
            int appleTreePoint = 5;
            int orangeTreePoint = 15;
            const int numberOfApples = 3;
            const int numberOfOranges = 2;
            int[] appleFallDistances = new int[numberOfApples] {-2, 2, 1};
            int[] orangeFallDistances = new int[numberOfOranges] {5, -6};

            Assert.That(FindNumberOfFallenFruitOnTheHouse(startPoint, endPoint, appleFallDistances, appleTreePoint), Is.EqualTo(1));
            Assert.That(FindNumberOfFallenFruitOnTheHouse(startPoint, endPoint, orangeFallDistances, orangeTreePoint), Is.EqualTo(1));
        }
    }
}