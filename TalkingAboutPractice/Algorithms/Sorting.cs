using System;
using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms
{
    public class Sorting
    {
        [Test]
        public void ShouldSortViaInsertion()
        {
            int[] arrayOfIntegers = { 5, 44, 0, 1987, 1, 97 };
            int[] sortedIntegers = { 0, 1, 5, 44, 97, 1987 };
            Assert.AreEqual(sortedIntegers, SortViaInsertion(arrayOfIntegers));

            string[] arrayOfStrings = { "man", "freaking sweet", "strings can be", "sorted using the", "IComparable interface" };
            string[] sortedStrings = { "freaking sweet", "IComparable interface", "man", "sorted using the", "strings can be" };
            Assert.AreEqual(sortedStrings, SortViaInsertion(arrayOfStrings));
        }

        // ************************************************************************************************************************************************** //

        public int[] SortViaInsertion(int[] array)
        {
            for (int i = 1; i < array.Length; i++)  // Start with second item in the zero-based array (i = 1)
            {
                int value = array[i];               // Key starts with that second value, to be compared with anything before it in the array
                int j = i - 1;                      // j is used to inspect the thing immediately before i in the array (i - 1)
                while (j >= 0 && array[j] > value)  // Loop backwards through the array, so long as j has not gone below 0, and the j value is greater than the starting i value
                {
                    array[j + 1] = array[j];        // Copy the j value forward into the current position for i
                    j--;                            // Decrease j by 1 (moving backwards to continue moving values forward until the key's insertion point is found)
                }
                array[j + 1] = value;               // The while loop has finished and the key value can now be inserted into the earliest possible slot (j + 1)
            }
            return array;
        }

        public IComparable[] SortViaInsertion(IComparable[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                IComparable value = array[i];
                int j = i - 1;
                while (j >= 0 && array[j].CompareTo(value) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = value;
            }
            return array;
        }
    }
}