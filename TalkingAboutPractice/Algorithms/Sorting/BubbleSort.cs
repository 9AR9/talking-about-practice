using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms.Sorting
{
    [TestFixture]
    public class BubbleSort
    {
        /*
         * Bubble Sort refers to a simple sort method that traverses through the items in a collection,
         * comparing each to the next in the collection, and swapping them if the next is lower than
         * the current. It is said to have O(n²) timing, as each member of the collection needs to be
         * inspected with each pass (n), and there will likely be as many passes as there are members
         * (also n), so n x n = n². This makes it inefficient as a sort algorithm (and sometimes referred
         * to as "naive"), and you'd rarely ever need to implement it in the real world.
         * 
         * Merge Sort is generally considered to be asymptotically more efficient than Bubble Sort,
         * but for small arrays, Bubble Sort might actually end up being more efficient, as Merge Sort
         * not only has the recursion calls, but also the operations to recombine sorted array halves,
         * whereas Bubble Sort simply loops through an array quadratically, swapping pairs of array
         * values as needed. Overall, Merge Sort must perform fewer steps, but these steps are more
         * expensive than those involved in Bubble Sort. For large arrays, the extra expense per step
         * is negligable, so you can generally count on Merge Sort's O(n log n) running time to be
         * faster than Bubble Sort's O(n²) timing.
         * 
         * Nevertheless, it is popular as an interview question and we're supposed to know how to do this.
        */

        static int[] BasicBubbleSort(int[] array)
        {
            var count = array.Length;
            bool isSorted = false;
            int fullPasses = 0;

            while (!isSorted)
            {
                isSorted = true;
                // comparison is (i < count-1) and not (i <= count-1) because the last position will have no next value to compare with
                for (int i = 0; i < count - 1 - fullPasses; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        isSorted = false;
                        Swap(array, i + 1, i);
                    }
                }
                fullPasses++;
            }

            return array;
        }

        static string BubbleSortWithSwapCount(int[] array)
        {
            var count = array.Length;
            var totalSwaps = 0;
            for (int i = 0; i < count; i++)
            {
                // Track number of elements swapped during a single array traversal
                int numberOfSwaps = 0;

                for (int j = 0; j < count - 1; j++)
                {
                    // Swap adjacent elements if they are in decreasing order
                    if (array[j] > array[j + 1])
                    {
                        Swap(array, j, j + 1);
                        numberOfSwaps++;
                    }
                }

                // If no elements were swapped during a traversal, array is sorted
                if (numberOfSwaps == 0)
                {
                    break;
                }
                totalSwaps += numberOfSwaps;
            }

            return $"Array is sorted in {totalSwaps} swaps. First element: {array[0]}; Last element: {array[count-1]}.";
        }

        private static void Swap(int[] array, int indexA, int indexB)
        {
            var temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
        }

        [Test]
        public void ShouldPerformBasicBubbleSort()
        {
            int[] values = { 12, 4, 5, 3, 8, 7 };
            int[] sortedValues = { 3, 4, 5, 7, 8, 12 };

            Assert.That(BasicBubbleSort(values), Is.EqualTo(sortedValues));
        }

        [Test]
        public void ShouldPerformBubbleSortWithSwapCount()
        {
            var consoleOutput = new ConsoleOutput();
            int[] values = { 3, 2, 1 };

            var result = BubbleSortWithSwapCount(values);

            Assert.That("Array is sorted in 3 swaps. First element: 1; Last element: 3.", Is.EqualTo(result));
        }
    }
}