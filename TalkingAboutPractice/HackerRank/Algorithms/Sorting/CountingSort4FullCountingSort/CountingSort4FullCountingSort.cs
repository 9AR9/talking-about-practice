using System;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.CountingSort4FullCountingSort
{
    [TestFixture]
    public class Solution
    {
        // Output string accompanying each integer in the input, maintaining original order for strings associated with the same integer
        public static string FullCountingSort(int[] numbers, string[] strings)
        {
            int highestValue = numbers.Max();
            int[] counts = new int[highestValue + 1];
            int[] totalValuesLessOrEqual = new int[highestValue + 1];
            int countAdder = 0;
            var sortedStrings = new string[numbers.Length];

            // Replace first half of strings with a dash, per client request (The Twist)
            for (int i = 0; i < strings.Length / 2; i++)
            {
                strings[i] = "-";
            }

            // Loop through all values and count the occurences of each
            foreach (int value in numbers)
            {
                counts[value]++;
            }

            // Create the running totals of array entries where number is less than or equal to current number
            for (int i = 0; i < counts.Length; i++)
            {
                countAdder += counts[i];
                totalValuesLessOrEqual[i] = countAdder;
            }

            // Loop through all possible individual values to determine the starting location for each. A sub-loop
            // interates through the full set of a particular integer to find the string matches, and place the strings
            // into a final collection that retains original order of same numeric values.
            for (int i = 0; i <= highestValue; i++)
            {
                int startingLocation = 0;
                if (i > 0)
                    startingLocation = totalValuesLessOrEqual[i] - counts[i];

                if (startingLocation < numbers.Length)
                {
                    var counter = 0;
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        if (numbers[j] == i)
                        {
                            sortedStrings[startingLocation + counter] = strings[j];
                            counter++;
                        }
                    }
                }
            }

            return String.Join(" ", sortedStrings);
        }
        [Test]
        public void ShouldPerformFullCountingSort()
        {
            int[] numbers = new[] { 0, 6, 0, 6, 4, 0, 6, 0, 6, 0, 4, 3, 0, 1, 5, 1, 2, 4, 2, 4 };
            string[] strings = new[] { "ab", "cd", "ef", "gh", "ij", "ab", "cd", "ef", "gh", "ij", "that", "be", "to", "be", "question", "or", "not", "is", "to", "the" };
            var expectedOutput = "- - - - - to be or not to be - that is the question - - - -";

            Assert.That(FullCountingSort(numbers, strings), Is.EqualTo(expectedOutput));
        }
    }
}