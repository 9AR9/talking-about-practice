using System;
using System.Collections;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.Quicksort1Partition
{
    [TestFixture]
    public class Solution
    {
        public static string Partition(int[] array)
        {
            int pivot = array[0];
            var left = new ArrayList();
            var right = new ArrayList();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > pivot)
                {
                    right.Add(array[i]);
                }
                else if (array[i] < pivot)
                {
                    left.Add(array[i]);
                }
            }

            StringBuilder result = new StringBuilder();
            result.Append(String.Join(" ", left.ToArray().Select(x => x.ToString()).ToArray()));
            result.Append(" " + pivot + " ");
            result.Append(String.Join(" ", right.ToArray().Select(x => x.ToString()).ToArray()));
            return result.ToString().Trim();

            // OR: You can use Array.Copy to put this all together, though I find it hard to read
            // 
            //var finalArray = new int[left.Count + 1 + right.Count];
            //Array.Copy(left.ToArray(), finalArray, left.ToArray().Length);
            //finalArray[left.ToArray().Length] = pivot;
            //Array.Copy(right.ToArray(), 0, finalArray, left.ToArray().Length + 1, right.ToArray().Length);
            //return String.Join(" ", finalArray.Select(x => x.ToString()));
        }


        [Test]
        public void ShouldPartition()
        {
            int[] arrayOfIntegers = { 4, 5, 3, 7, 2 };
            var expectedOutput = "3 2 4 5 7";

            Assert.That(Partition(arrayOfIntegers), Is.EqualTo(expectedOutput));

            int[] array2 = { 0, -3, 6, 4, -10, 8, -5, 2, -7 };
            Assert.That(Partition(array2), Is.EqualTo("-3 -10 -5 -7 0 6 4 8 2"));
        }
    }
}