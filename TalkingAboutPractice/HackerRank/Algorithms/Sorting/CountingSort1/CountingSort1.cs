using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.CountingSort1
{
    [TestFixture]
    public class Solution
    {
        // Output number of times each number from 0 to 99 is encountered in the input
        public static string CountingSort1(int[] array)
        {
            int[] counts = new int[array.Length];
            StringBuilder output = new StringBuilder();

            foreach (int value in array)
            {
                counts[value]++;
            }

            for (int i = 0; i < 100; i++)
            {
                output.Append(counts[i] + " ");
            }

            return output.ToString().Trim();
        }


        [Test]
        public void ShouldOutputNumberOfTimesEachIntegerFrom0To99IsEncountered()
        {
            var line1 = "100";
            var line2 = "63 25 73 1 98 73 56 84 86 57 16 83 8 25 81 56 9 53 98 67 99 12 83 89 80 91 39 86 76 85 74 39 25 90 59 10 94 32 44 3 89 30 27 79 46 96 27 32 18 21 92 69 81 40 40 34 68 78 24 87 42 69 23 41 78 22 6 90 99 89 50 30 20 1 43 3 70 95 33 46 44 9 69 48 33 60 65 16 82 67 61 32 21 79 75 75 13 87 70 33";
            int[] array = line2.Split(' ').Select(int.Parse).ToArray();
            var expectedOutput = "0 2 0 2 0 0 1 0 1 2 1 0 1 1 0 0 2 0 1 0 1 2 1 1 1 3 0 2 0 0 2 0 3 3 1 0 0 0 0 2 2 1 1 1 2 0 2 0 1 0 1 0 0 1 0 0 2 1 0 1 1 1 0 1 0 1 0 2 1 3 2 0 0 2 1 2 1 0 2 2 1 2 1 2 1 1 2 2 0 3 2 1 1 0 1 1 1 0 2 2";

            Assert.That(CountingSort1(array), Is.EqualTo(expectedOutput));
        }
    }
}