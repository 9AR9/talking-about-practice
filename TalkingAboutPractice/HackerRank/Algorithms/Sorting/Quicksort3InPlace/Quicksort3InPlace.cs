using System;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Sorting.Quicksort3InPlace
{
    [TestFixture]
    public class Solution
    {
        /*
         * This time we will sort "in-place" instead of using multiple sub-arrays to copy into, further optimizing
         * the work needed to perform the sort.
        */


        // QuickSort will run so long as (and call itself recursively until) the sub-array to analyze is more than a single integer.
        // First, it calls Partition which reorders the array by moving the last value of the array to a location that guaratees
        // it is in between all values lower and higher than it (even if those sets themselves are not yet fully sorted).
        // Then, it calls itself recursively twice - once for the lower set, and once for the higher set. Along the way, all
        // "Swap" operations are performed against the same ever-changing array, until everything is properly sorted.
        private static string QuickSort(int[] arr, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int pos = Partition(arr, lowIndex, highIndex);
                QuickSort(arr, lowIndex, pos - 1);
                QuickSort(arr, pos + 1, highIndex);
            }

            return String.Join(" ", arr.ToArray().Select(x => x.ToString()).ToArray());
        }

        // The Partition function takes in an array along with two integers specifying the lower and upper indexes,
        // analyzes the last item in that array-or-subarry with all previous items, and then moves the last item
        // in front of all the found values that are lower than it (using the Swap method).
        //
        // The function uses a "SmallIndex" value that starts at 1 less than the lower index, and will act as counter
        // to find the ultimate index to move the "PivotItem" to. A loop is performed through the array, and for
        // each value found that is lower than the PivotItem, the SmallIndex counter increases by one, and a Swap
        // is performed using that SmallIndex counter to move the current value back the minimum currently-known number of
        // locations that it must move (though it is still possible there will be more values to the left of it after the
        // Swap that are higher than it which the logic just hasn't identified yet - and this is why we must recurse
        // with the QuickSort method).
        //
        // When finished looping, one final Swap is performed to move the PivotItem back to the location of the final
        // SmallIndex + 1, which should be in front of all numbers larger than it that were moved to the front of the
        // array, and then it returns that final index position.
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

            //Console.WriteLine("Pivot = " + arr[smallIndex + 1]);
            Console.WriteLine(String.Join(" ", arr));

            return finalIndex;
        }

        private static void Swap(int[] arr, int k, int small)
        {
            int temp;
            temp = arr[k];
            arr[k] = arr[small];
            arr[small] = temp;
        }


        [Test]
        public void ShouldSortViaThisApparentlySolidQuicksort()
        {
            var consoleOutput = new ConsoleOutput();
            int[] arrayOfIntegers = { 1, 3, 9, 8, 2, 7, 5 };

            QuickSort(arrayOfIntegers, 0, arrayOfIntegers.Length - 1);
            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("1 3 2 5 9 7 8"));
            Assert.That(outputLines[1], Is.EqualTo("1 2 3 5 9 7 8"));
            Assert.That(outputLines[2], Is.EqualTo("1 2 3 5 7 8 9"));
        }
    }
}