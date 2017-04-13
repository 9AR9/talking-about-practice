using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.Quicksort2Sorting
{
    [TestFixture]
    public class Solution
    {
        /*
         * Expanding on the solution for Quicksort 1 (which allowed stitching of two arrays together) but this
         * time we'll make it recursive so that what is ultimately returned is a fully sorted array, not just
         * a first-partitioned array.
        */

        public int[] QuicksortViaRecursivePartition(int[] array)
        {
            if (array.Length < 2)
                return array;

            int pivot = array[0];
            var left = new ArrayList();
            var right = new ArrayList();

            foreach (int value in array)
            {
                if (value > pivot)
                    right.Add(value);
                else if (value < pivot)
                    left.Add(value);
            }

            int[] leftArray = QuicksortViaRecursivePartition(left.OfType<int>().ToArray());
            int[] rightArray = QuicksortViaRecursivePartition(right.OfType<int>().ToArray());
            var finalArray = new int[left.Count + 1 + right.Count];

            Array.Copy(leftArray, finalArray, leftArray.Length);
            finalArray[left.ToArray().Length] = pivot;
            Array.Copy(rightArray, 0, finalArray, leftArray.Length + 1, rightArray.Length);

            Console.WriteLine(string.Join(" ", finalArray.Select(x => x.ToString())));
            return finalArray;
        }


        [Test]
        public void ShouldSortUsingRecursivePartition()
        {
            int[] arrayOfIntegers = { 5, 8, 1, 3, 7, 9, 2 };
            int[] sortedArray = { 1, 2, 3, 5, 7, 8, 9 };
            Assert.That(QuicksortViaRecursivePartition(arrayOfIntegers), Is.EqualTo(sortedArray));

            int[] array2 = { 0, -3, 6, 4, -10, 8, -5, 2, -7 };
            int[] sortedArray2 = { -10, -7, -5, -3, 0, 2, 4, 6, 8 };
            Assert.That(QuicksortViaRecursivePartition(array2), Is.EqualTo(sortedArray2));
        }
    }
}