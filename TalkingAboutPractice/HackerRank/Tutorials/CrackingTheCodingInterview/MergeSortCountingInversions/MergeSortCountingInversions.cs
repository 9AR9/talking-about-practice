using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.MergeSortCountingInversions
{
    [TestFixture]
    public class Solution
    {
        /*
         * Merge Sort is known as a pretty efficient sorting algorithm, always giving an O(N log N) 
         * runtime, and can best be conceptualized recursively. It entails splitting a collection into
         * physical halves, sorting those halves individually, then merging the sorted halves back together
         * in a sorted order. The way that merge is accomplished is through recursive application of the
         * merge sort just described.
         * 
         * Its downside is that merging two arrays together generally requires extra memory to accomplish,
         * because you need to copy the elements into new arrays, merge them, and then copy them back.
         * 
        */

        public static int[] BasicMergeSort(int[] array)
        {
            BasicMergeSort(array, new int[array.Length], 0, array.Length - 1);
            return array;
        }

        public static void BasicMergeSort(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd)
            {
                // When recursion leads these values to cross, all sorting is finished, and we return
                return;
            }
            int middle = (leftStart + rightEnd) / 2;

            // Sort left side, sort right side, then merge
            BasicMergeSort(array, temp, leftStart, middle);
            BasicMergeSort(array, temp, middle + 1, rightEnd);
            BasicMergeHalves(array, temp, leftStart, rightEnd);
        }

        public static void BasicMergeHalves(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            int leftEnd = (leftStart + rightEnd) / 2;
            int rightStart = leftEnd + 1;
            int size = rightEnd - leftStart + 1;

            int leftIterator = leftStart;
            int rightIterator = rightStart;
            int indexIntoTemp = leftStart;

            while (leftIterator <= leftEnd && rightIterator <= rightEnd)
            {
                if (array[leftIterator] <= array[rightIterator])
                {
                    temp[indexIntoTemp] = array[leftIterator];
                    leftIterator++;
                }
                else
                {
                    temp[indexIntoTemp] = array[rightIterator];
                    rightIterator++;
                }
                indexIntoTemp++;
            }

            // Only one of these two lines will have an effect, since one side will always run out of iterations before the other.
            // The point is to copy in any values remaining on either side of the merge.
            Array.Copy(array, leftIterator, temp, indexIntoTemp, leftEnd - leftIterator + 1);
            Array.Copy(array, rightIterator, temp, indexIntoTemp, rightEnd - rightIterator + 1);

            Array.Copy(temp, leftStart, array, leftStart, size);
        }

        [Test]
        public void ShouldPerformBasicMergeSort()
        {
            int[] values = { 12, 4, 5, 3, 8, 7 };
            int[] sortedValues = { 3, 4, 5, 7, 8, 12 };
            int[] values2 = { 99, 4, 7, 45, 2, 38, 37, 94, 49, 48, 88, 11, 9, 5000, 4011, 6 };
            int[] sortedValues2 = { 2, 4, 6, 7, 9, 11, 37, 38, 45, 48, 49, 88, 94, 99, 4011, 5000 };

            Assert.That(BasicMergeSort(values), Is.EqualTo(sortedValues));
            Assert.That(BasicMergeSort(values2), Is.EqualTo(sortedValues2));
        }







        /*
         * Now that we have a basic Merge Sort algorithm in place, we can modify it to count and return the
         * inversions, instead of returning the sorted array. This set of functions represents a modified
         * copy of the basic solution, with only slight changes to introduce the inversions counter in the
         * merge function, and to return these values back up the recursion.
         * 
        */
        public static long MergeSortAndCountInversions(int[] array)
        {
            long inversions = MergeSortAndCountInversions(array, new int[array.Length], 0, array.Length - 1);
            return inversions;
        }

        public static long MergeSortAndCountInversions(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            if (leftStart >= rightEnd)
            {
                // When recursion leads these values to cross, all sorting is finished, and we return
                return 0;
            }
            int middle = (leftStart + rightEnd) / 2;

            // Sort left side, sort right side, then merge
            long a = MergeSortAndCountInversions(array, temp, leftStart, middle);
            long b = MergeSortAndCountInversions(array, temp, middle + 1, rightEnd);
            long c = MergeHalves(array, temp, leftStart, rightEnd);

            return a + b + c;
        }

        public static long MergeHalves(int[] array, int[] temp, int leftStart, int rightEnd)
        {
            int leftEnd = (leftStart + rightEnd) / 2;
            int rightStart = leftEnd + 1;
            int size = rightEnd - leftStart + 1;
            long inversions = 0; // New inversions counter

            int leftIterator = leftStart;
            int rightIterator = rightStart;
            int indexIntoTemp = leftStart;

            while (leftIterator <= leftEnd && rightIterator <= rightEnd)
            {
                if (array[leftIterator] <= array[rightIterator])
                {
                    temp[indexIntoTemp] = array[leftIterator];
                    leftIterator++;
                }
                else
                {
                    temp[indexIntoTemp] = array[rightIterator];
                    rightIterator++;

                    // We now count inversions here, when the left value is greater than the right value.
                    // Since an inversion swap is defined as a swap of two adjacent elements, we need the
                    // count of how many adjacent swaps are necessary to bring the right value in front
                    // of the current left value, which is the difference of the last index and the current
                    // index, plus one to inclue that current index (i.e. if the left end index is 4 and
                    // the current index is 1, it would a total of 4 swaps to move the right value in front
                    // of the current (4 - 1 + 1);
                    inversions += (leftEnd - leftIterator + 1); 
                }
                indexIntoTemp++;
            }

            // Only one of these two lines will have an effect, since one side will always run out of iterations before the other.
            // The point is to copy in any values remaining on either side of the merge.
            Array.Copy(array, leftIterator, temp, indexIntoTemp, leftEnd - leftIterator + 1);
            Array.Copy(array, rightIterator, temp, indexIntoTemp, rightEnd - rightIterator + 1);

            Array.Copy(temp, leftStart, array, leftStart, size);

            return inversions;
        }




        [Test]
        public void ShouldCountInversions()
        {
            int[] values1 = { 2, 4, 1 };
            int[] values2 = { 1, 1, 1, 2, 2 };
            int[] values3 = { 2, 1, 3, 1, 2 };

            long inversions1 = MergeSortAndCountInversions(values1);
            long inversions2 = MergeSortAndCountInversions(values2);
            long inversions3 = MergeSortAndCountInversions(values3);

            Assert.That(inversions1, Is.EqualTo(2));
            Assert.That(inversions2, Is.EqualTo(0));
            Assert.That(inversions3, Is.EqualTo(4));
        }
    }
}