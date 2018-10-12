using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms.Searching
{
    [TestFixture]
    public class BinarySearch
    {
        /*
         * The Binary Search method is a fast search algorithm that must be performed on a
         * sorted array in order to work. It works by finding the middle point of an array's
         * index, checking to see if that value is indeed the value we're looking for. If not, 
         * we can at least know if our search term is larger than or smaller than the mid, so we
         * then take just that half of the array and perform the same check. The process repeats until
         * the search term is found, or determined to not be in the array at all.
         * 
         * Binary Search uses a "divide-and-conquer" strategy that yields a running time of O(log n)
         * (oh log of n), or more specifically O(log2 n) (oh log base 2 of n). Binary Search is therefor
         * referred to as a "log n problem" or "logarithmic".
         * 
         * This logarithmic nature of the notation implies that the worst case scenario for finding
         * the search value would be less time than a standard linear search with O(N), because the
         * divide-and-conquer strategy guarantees less comparison operations will be needed to find
         * search value. The larger the data set to search, the more comparisons will be needed to
         * be done (worst case). However, these additional comparisons get faster, and we can
         * exponentially do more of them, as time increases linearly, so if we were to plot the time
         * vs. n on a graph, we would see a curve where the rise in time decelerates as n grows
         * larger. Its complexity is logarithmic. This concept is sometimes better understood with
         * visuals: https://stackoverflow.com/questions/2307283/what-does-olog-n-mean-exactly
         * 
         * Two versions of the basic binary search algorithm are below - one using recursion, and
         * the other using an iterative While loop. Both approaches are equally viable and performant.
         * 
         * C# offers a binary search for sorted arrays via Array.BinarySearch and for sorted Lists via
         * List.BinarySearch. It returns the index of the specified value if found, and a negative
         * number when it is not.
        */

        public static bool BasicRecursiveBinarySearch(int[] array, int x)
        {
            Array.Sort(array);
            return BasicRecursiveBinarySearch(array, x, 0, array.Length - 1);
        }

        public static bool BasicRecursiveBinarySearch(int[] array, int x, int left, int right)
        {
            if (left > right)
                return false;

            int mid = (left + right) / 2; // Reminder: integer division - rounds DOWN to last integer lower than any decimal result
            if (array[mid] == x)
            {
                return true;
            }
            if (x < array[mid])
            {
                return BasicRecursiveBinarySearch(array, x, left, mid - 1);
            }
            return BasicRecursiveBinarySearch(array, x, mid + 1, right);
        }

        public static bool BasicIterativeBinarySearch(int[] array, int x)
        {
            Array.Sort(array);
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2; // Reminder: integer division - rounds DOWN to last integer lower than any decimal result
                if (array[mid] == x)
                {
                    return true;
                }
                if (x < array[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return false;
        }

        [Test]
        public void ShouldPerformBasicRecursiveBinarySearch()
        {
            int[] values = { 8, 50, 77, 3, 9, 55, 1, 111, 1111, 34, 8, 6, 6, 6, 6, 66, 900076, 4, 1, 1999, 2009, 2019 };

            Assert.That(BasicRecursiveBinarySearch(values, 1999), Is.EqualTo(true));
            Assert.That(BasicRecursiveBinarySearch(values, 1998), Is.EqualTo(false));
        }

        [Test]
        public void ShouldPerformBasicIterativeBinarySearch()
        {
            int[] values = { 8, 50, 77, 3, 9, 55, 1, 111, 1111, 34, 8, 6, 6, 6, 6, 66, 900076, 4, 1, 1999, 2009, 2019 };

            Assert.That(BasicIterativeBinarySearch(values, 1999), Is.EqualTo(true));
            Assert.That(BasicIterativeBinarySearch(values, 1998), Is.EqualTo(false));
        }

        [Test]
        public void ShouldPerformBinarySearchUsingBuiltInFunction()
        {
            int[] values = { 8, 50, 77, 3, 9, 55, 1, 111, 1111, 34, 8, 6, 6, 6, 6, 66, 900076, 4, 1, 1999, 2009, 2019 };
            Array.Sort(values);

            Assert.That(Array.BinarySearch(values, 1999), Is.GreaterThan(-1));
            Assert.That(Array.BinarySearch(values, 1998), Is.LessThan(0));
        }
    }
}