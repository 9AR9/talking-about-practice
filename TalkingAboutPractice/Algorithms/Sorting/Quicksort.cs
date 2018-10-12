using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TalkingAboutPractice.Algorithms.Sorting
{
    [TestFixture]
    public class Quicksort
    {
        /*
         * Quicksort (sometimes called "partition-exchange sort") is famous for being an efficient sorting
         * algorithm. It dates back to 1959, but is still a commonly used algorithm for sorting, even
         * being a key part of C#'s Array.Sort and List.Sort methods. In most implementations, for
         * efficiency, it is not implemented to be "stable", meaning that the relative order of equal
         * items in the collection is not necessarily preserved. It does operate "in-place" on an array,
         * meaning it only requires a constant amount O(1) of additional memory space to perform.
         * 
         * In terms of complexity, its worse case takes O(n²) comparisons to sort n items, making it
         * technically "quadratic" like Insertion Sort. However, this behavior is rare, and mathematical
         * analysis shows that, on average, Quicksort will only take O(n log n) comparisons to sort n items.
         * 
         * Quicksort uses a "divide-and-conquer" strategy similar to the one employed by Binary Search.
         * It first divides a large array into two smaller sub-arrays (the low and high elements) and then
         * recursively sorts them. The steps to be followed are:
         *      1. Pick an element, called a "pivot", from the array
         *      2. Partitioning: reorder the array so that all elements with values less than the pivot
         *         come after it (equal values can go either way). After this partioning, the pivot is in
         *         its final position.
         *      3. Recursively apply the above steps to the sub-array of elements with smaller values
         *         and separately to the sub-array of elements with greater values.
         * 
         * The below solution will employ a few methods to make this happen.
        */

        // SortViaQuicksort will run (and call itself recursively) so long as the sub-array to analyze is more than
        // a single integer. First, it calls Partition which reorders the array by moving its last value to
        // a location that guaratees it is in between all values lower and higher than it (even if those sets
        // themselves are not yet fully sorted). Then, it calls itself recursively twice - once for the lower
        // set, and once for the higher set. Along the way, all "Swap" operations are performed against the 
        // same ever-changing array, until everything is properly sorted.
        private static int[] SortViaQuicksort(int[] array)
        {
            return SortViaQuicksort(array, 0, array.Length - 1);
        }

        private static int[] SortViaQuicksort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int pos = Partition(array, lowIndex, highIndex);
                SortViaQuicksort(array, lowIndex, pos - 1);
                SortViaQuicksort(array, pos + 1, highIndex);
            }

            return array;
        }

        // The Partition function takes in an array along with two integers specifying the lower and upper
        // indexes, analyzes the last item in that array-or-subarry with all previous items, and then moves
        // the last item in front of all the found values that are lower than it (using the Swap method).
        //
        // The function uses a "SmallIndex" value that starts at 1 less than the lower index, and will act as
        // counter to find the ultimate index to move the "PivotItem" to. A loop is performed through the array,
        // and for each value found that is lower than the PivotItem, the SmallIndex counter increases by one,
        // and a Swap is performed using that SmallIndex counter to move the current value back the minimum
        // currently-known number of locations that it must move (though it is still possible there will be more
        // higher values to the left of it after the Swap that the logic just hasn't identified yet - and this
        // is why we must recurse with the SortViaQuicksort method).
        //
        // When finished looping, one final Swap is performed to move the PivotItem back to the location of the
        // final SmallIndex + 1, which should be in front of all numbers larger than it that were moved to the
        // front of the array, and then it returns that final index position.
        public static int Partition(int[] arr, int lowIndex, int highIndex)
        {
            int pivotItem = arr[highIndex];
            int smallIndex = lowIndex - 1;

            for (int k = lowIndex; k < highIndex; k++)
            {
                if (arr[k] <= pivotItem)
                {
                    smallIndex++;
                    Swap(arr, k, smallIndex);     // Is this just for optimization? Yes, and it really helps. Need it.
                }
            }

            int finalIndex = smallIndex + 1;
            Swap(arr, highIndex, finalIndex);

            return finalIndex;
        }

        private static void Swap(int[] arr, int k, int small)
        {
            var temp = arr[k];
            arr[k] = arr[small];
            arr[small] = temp;
        }

        [Test]
        public void ShouldSortViaThisApparentlySolidQuicksort()
        {
            int[] arrayOfIntegers = { 1, 3, 9, 8, 2, 7, 5 };
            int[] sortedArrayOfIntegers = { 1, 2, 3, 5, 7, 8, 9 };

            int[] resultingArray = SortViaQuicksort(arrayOfIntegers);

            Assert.That(resultingArray, Is.EqualTo(sortedArrayOfIntegers));
        }
    }
}