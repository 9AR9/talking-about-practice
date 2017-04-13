using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Implementation.GradingStudents
{
    [TestFixture]
    public class Solution
    {
        static int CalculateFinalGrade(int grade)
        {
            var finalGrade = grade;
            var difference = 5 - (grade % 5);
            if (difference < 3 && grade > 37)
            {
                finalGrade = grade + difference;
            }

            return finalGrade;
        }

        [Test]
        public void ShouldEncryptUsingSquareRootDerivedScheme()
        {
            Assert.That(CalculateFinalGrade(73), Is.EqualTo(75));
            Assert.That(CalculateFinalGrade(67), Is.EqualTo(67));
            Assert.That(CalculateFinalGrade(38), Is.EqualTo(40));
            Assert.That(CalculateFinalGrade(33), Is.EqualTo(33));
        }
    }
}