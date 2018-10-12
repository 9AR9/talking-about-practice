using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.BinarySearchIceCreamParlor
{
    [TestFixture]
    public class Solution
    {
        /*
         * The Binary Search method is a failry performant search algorithm that must be performed
         * on a sorted array in order to work. It works by finding the middle point of array's
         * index, checking to see if that value is indeed the value we're looking for. If not, 
         * we can at least know if our search term is larger than or smaller than the mid, so we
         * then take just that half of the array and perform the same check. The process repeats until
         * the search term is found, or determined to not be in the array at all.
         * 
         * Binary Search uses a "divide-and-conquer" strategy that yields a running time of O(log N)
         * (oh log of N), or more specifically O(log2N) (oh log base 2 of N). Binary Search is therefor
         * referred to as a "log N problem".
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

            int mid = (left + right) / 2;
            if (array[mid] == x)
            {
                return true;
            }
            else if (x < array[mid])
            {
                return BasicRecursiveBinarySearch(array, x, left, mid - 1);
            }
            else
            {
                return BasicRecursiveBinarySearch(array, x, mid + 1, right);
            }
        }

        [Test]
        public void ShouldPerformBasicRecursiveBinarySearch()
        {
            int[] values = { 8, 50, 77, 3, 9, 55, 1, 111, 1111, 34, 8, 6, 6, 6, 6, 66, 900076, 4, 1, 1999, 2009, 2019 };

            Assert.That(BasicRecursiveBinarySearch(values, 1999), Is.EqualTo(true));
            Assert.That(BasicRecursiveBinarySearch(values, 1998), Is.EqualTo(false));
        }



        public static bool BasicIterativeBinarySearch(int[] array, int x)
        {
            Array.Sort(array);
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (array[mid] == x)
                {
                    return true;
                }
                if (x < array[mid])
                {
                    right = mid - 1;
                    //return BasicRecursiveBinarySearch(array, x, left, mid - 1);
                }
                else
                {
                    left = mid + 1;
                    //return BasicRecursiveBinarySearch(array, x, mid + 1, right);
                }
            }
            return false;
        }

        [Test]
        public void ShouldPerformBasicIterativeBinarySearch()
        {
            int[] values = { 8, 50, 77, 3, 9, 55, 1, 111, 1111, 34, 8, 6, 6, 6, 6, 66, 900076, 4, 1, 1999, 2009, 2019 };

            Assert.That(BasicIterativeBinarySearch(values, 1999), Is.EqualTo(true));
            Assert.That(BasicIterativeBinarySearch(values, 1998), Is.EqualTo(false));
        }




        /*
         * Binary Search Ice Cream Parlor (TODO!)
        */
    }
}