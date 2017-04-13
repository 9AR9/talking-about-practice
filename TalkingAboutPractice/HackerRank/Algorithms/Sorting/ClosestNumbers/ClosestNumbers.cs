using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.ClosestNumbers
{
    [TestFixture]
    public class Solution
    {
        // In an unsorted list of integers, find the two numbers with the smallest absolute difference between them
        public static string ClosestNumbers(int[] array)
        {
            int minDiff = 0;
            int tempDiff = 0;
            int count = array.Length;
            StringBuilder output = new StringBuilder();
            Dictionary<int, ArrayList> differences = new Dictionary<int, ArrayList>();

            // Sort array in ascending order first, then iterate forward, subtracting current
            // value from next value, guaranteeing a positive temporary diff value. For each
            // diff, store a dictionary entry and add the two compared values to its ArrayList.
            // Along the way, keep track of the minimum difference encountered.
            Array.Sort(array);
            for (int i = 0; i < count - 1; i++)
            {
                tempDiff = array[i + 1] - array[i];
                if (!differences.ContainsKey(tempDiff))
                    differences.Add(tempDiff, new ArrayList());
                differences[tempDiff].Add(array[i]);
                differences[tempDiff].Add(array[i + 1]);
                if (minDiff > tempDiff || i == 0)
                    minDiff = tempDiff;
            }

            // Output a string concatenating all values found where diff generated was the lowest (the minDiff)
            foreach (int value in differences[minDiff])
            {
                output.Append(value + " ");
            }

            return output.ToString().Trim();
        }
        [Test]
        public static void ShouldFindClosetNumbers()
        {
            int[] values = new int[] { 5, 4, 3, 2 };

            Assert.That(ClosestNumbers(values), Is.EqualTo("2 3 3 4 4 5"));
        }
    }
}