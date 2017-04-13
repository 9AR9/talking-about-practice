using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.InsertionSort
{
    [TestFixture]
    public class Solution
    {
        public static int[] SortViaInsertion(int[] array)
        {
            for (int i = 1; i < array.Length; i++)      // Start with 2nd item of array, looping forward
            {
                int value = array[i];                   // Comparison starts with a key at that second value, to be compared with anything before it
                int h = i - 1;                          // h represents the index right before i (the current) and is what we inspect against
                while (h >= 0 && array[h] > value)      // Loop backwards through the array, until iterator gets to 0 (the beginning), or is less than the current, and thus found its sorted place
                {
                    array[h + 1] = array[h];            // Copy the h value forward one so that it is in front of the i value it was compared with
                    WriteOutArray(array);
                    h--;                                // Decrease h by 1, moving backwards to start the comparison again until the insertion point is foundkeep moving values
                }
                array[h + 1] = value;                   // The while loop has finished, so now the key value can be inserted into the earliest possible slot                                            
            }
            WriteOutArray(array);
            return array;
        }
        static void WriteOutArray(int[] array)
        {
            Console.WriteLine(string.Join(" ", array));
        }


        [Test]
        public void ShouldSortViaInsertion()
        {
            int[] arrayOfIntegers = { 5, 44, 0, 1987, 1, 97 };
            int[] sortedIntegers = { 0, 1, 5, 44, 97, 1987 };
            Assert.AreEqual(sortedIntegers, SortViaInsertion(arrayOfIntegers));
        }
    }
}