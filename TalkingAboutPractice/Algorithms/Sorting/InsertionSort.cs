using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms.Sorting
{
    [TestFixture]
    public class InsertionSort
    {
        /*
         * Insertion Sort is a simple sorting algorithm that builds a final sorted array or list one
         * item at a time. It is much less efficient on large lists than more advanced search
         * algorithms such as Quicksort, Heapsort or Merge Sort, but it still provides several
         * advantages:
         *      1. Simple implementation
         *      2. Efficient for very small data sets, much like other quadratic sorting algorithms
         *      3. More efficient in practice than most other simple quadratic algorithms such as
         *         Selection Sort or Bubble Sort.
         *      4. Adaptive (i.e. efficient for data sets already substantially sorted)
         *      5. Stable (i.e. does not change the relative order of elements with equal keys)
         *      6. In-place (i.e. only requires a constant amount O(1) of additional memory space)
         *      7. Online (i.e. can sort a list as it receives it)
         *      
         * In terms of complexity, Insertion Sort is most often referred to as "quadratic" and O(n²).
         * This timing is approaching the slower side of the traditional algorithm hierarchy (clearly
         * representing a higher number of potential operations that linear time or the logarithmic
         * complexities) but can still be, however, very useful.
         * 
         * C# includes built-in Array.Sort and List.Sort methods to perform sorts for you, and these
         * methods use an "introspective sort" approach to decide the right sorting algorithm to
         * employ under the covers. This will end up choosing the Insertion Sort method for only the
         * cases where search partitions are smaller than 16. It chooses Quicksort otherwise, unless
         * the partitions exceed a recursion depth level based on the log of the number of elements
         * being sorted, in which case it uses Heapsort.
        */

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

        [Test]
        public void ShouldSortViaInsertion()
        {
            int[] arrayOfIntegers = { 5, 44, 0, 1987, 1, 97 };
            int[] sortedIntegers = { 0, 1, 5, 44, 97, 1987 };
            Assert.AreEqual(sortedIntegers, SortViaInsertion(arrayOfIntegers));

            IComparable[] arrayOfStrings = { "man", "freaking sweet", "strings can be", "sorted using the", "IComparable interface" };
            string[] sortedStrings = { "freaking sweet", "IComparable interface", "man", "sorted using the", "strings can be" };
            Assert.AreEqual(sortedStrings, SortViaInsertion(arrayOfStrings));
        }
    }
}