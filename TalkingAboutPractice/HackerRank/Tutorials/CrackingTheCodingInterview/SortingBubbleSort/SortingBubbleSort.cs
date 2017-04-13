using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.SortingBubbleSort
{
    [TestFixture]
    public class Solution
    {
        /*
         * Bubble Sort refers to a simple sort method that traverses through the items in a collection,
         * comparing each to the next in the collection, and swapping them if the next is lower than
         * the current. It is said to have O(N²) timing, as each member of the collection needs to be
         * inspected with each pass (N), and there will likely be as many passes as there are members
         * (also N), so NxN = N². This makes it inefficient as a sort algorithm, and you'd rarely ever
         * need to implement it in the real world.
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
                for (int i = 0; i < count - 1 - fullPasses; i++) // comparison is (i < count-1) and not (i <= count-1) because the last position will have no next value to compare with
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

        static void BubbleSortWithSwapCount(int[] array)
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

            Console.WriteLine("Array is sorted in " + totalSwaps + " swaps.");
            Console.WriteLine("First Element: " + array[0]);
            Console.WriteLine("Last Element: " + array[count - 1]);
        }

        static void Swap(int[] array, int indexA, int indexB)
        {
            int temp = array[indexA];
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

            BubbleSortWithSwapCount(values);
            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("Array is sorted in 3 swaps."));
            Assert.That(outputLines[1], Is.EqualTo("First Element: 1"));
            Assert.That(outputLines[2], Is.EqualTo("Last Element: 3"));
        }
    }
}