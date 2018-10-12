using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms.Searching
{
    [TestFixture]
    public class LinearSearch
    {
        /*
         * Linear Search refers to a very basic search that is done in "linear time" which merely
         * represents traversing through a list and checking each current value to see if it matches
         * a key or not. In Big O notation, the time complexity of "linear" is written as O(n) and
         * spoken as "oh of n". This means that, worst case scenario, the run time of the algorithm
         * will have to look at every object in the list, so it can't be viewed as an optimized
         * search method, but rather is a very basic, and potentially slow, method.
         * 
         * This method is generally employed an an unsorted list, although it certainly could be
         * applied to a sorted list. It is clearly not a good method for searching sorted lists.
         * 
         * C# offers a linear search for arrays via Array.IndexOf, and for Lists via List.IndexOf.
        */

        // This function performs the same linear search for an integer array as Array.IndexOf does,
        // by returning the index of the first location of the key if found, otherwise -1.
        public int FindInIntegerArrayUsingLinearSearch(int[] array, int key)
        {
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] == key) return i;
            }
            return array.GetLowerBound(0) - 1;
        }

        [Test]
        public void ShouldFindInArrayUsingMyFunction()
        {
            var array = new int[] { 5, 6, 9812, 7, 44, 200, 1, 1, 200, 19 };
            const int searchValue = 7;

            var result = FindInIntegerArrayUsingLinearSearch(array, searchValue);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void ShouldNotFindInArrayUsingMyFunction()
        {
            var array = new int[] { 5, 6, 9812, 7, 44, 200, 1, 1, 200, 19 };
            const int searchValue = 77;

            var result = FindInIntegerArrayUsingLinearSearch(array, searchValue);

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void ShouldFindInArrayUsingBuiltInFunction()
        {
            var array = new int[] { 5, 6, 9812, 7, 44, 200, 1, 1, 200, 19 };
            const int searchValue = 7;

            Assert.AreEqual(3, Array.IndexOf(array, searchValue));
        }

        [Test]
        public void ShouldNotFindInArrayUsingBuiltInFunction()
        {
            var array = new int[] { 5, 6, 9812, 7, 44, 200, 1, 1, 200, 19 };
            const int searchValue = 77;

            Assert.AreEqual(-1, Array.IndexOf(array, searchValue));
        }
    }
}