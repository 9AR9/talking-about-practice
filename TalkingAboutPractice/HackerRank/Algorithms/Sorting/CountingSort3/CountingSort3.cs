using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.CountingSort3
{
    [TestFixture]
    public class Solution
    {
        // Output number of elements that are less than or equal to each number from 0 to 99
        public static string CountingSort3(int[] array)
        {
            int[] counts = new int[100];
            int[] startingLocations = new int[100];
            int countAdder = 0;
            StringBuilder output = new StringBuilder("");

            foreach (int value in array)
            {
                counts[value]++;
            }

            for (int i = 0; i < counts.Length; i++)
            {
                countAdder += counts[i];
                startingLocations[i] = countAdder;
            }

            foreach (int location in startingLocations)
            {
                output.Append(location + " ");
            }

            return output.ToString().Trim();
        }


        [Test]
        public void ShouldPerformCountingSort3()
        {
            int[] numbers = new[] { 4, 3, 0, 1, 5, 1, 2, 4, 2, 4 };
            string expectedOutput = "1 3 5 6 9 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10 10";

            Assert.That(CountingSort3(numbers), Is.EqualTo(expectedOutput));
        }
    }
}