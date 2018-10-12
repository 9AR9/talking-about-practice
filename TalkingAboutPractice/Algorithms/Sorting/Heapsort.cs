using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms.Sorting
{
    /*
     * Heapsort is known as an efficient sorting algorithm which can be thought of as an improved
     * Selection Sort. Like that algorithm, it divides its input into a sorted and an unsorted
     * region, and iteratively shrinks the unsorted region by extracting the largest element and
     * moving that to the sorted region. The improvement consists of the use of a heap data structure
     * rather than a linear-time search to find the maximum. It is an "in-place" algorithm, meaning
     * it only requires a constant amount O(1) of additional memory space to perform. It is also
     * not a "stable" sort, meaning that the relative order of equal items in a collection is not
     * necessarily preserved.
     * 
     * In terms of complexity, it is generally known to be slower in practice on most machines than
     * a well-implemented Quicksort, however, it actually does have the advantage of a more favorable
     * worst-case O(n log n) runtime.
     * 
     * The concept of a "heap" was introduced by J. W. J. Williams in 1964 as a data structure for
     * his Heapsort sorting algorithm. Specifically, he introduced the "binary heap", in which
     * a binary tree (a structure of nodes in which each has at most two children) is used to
     * structure the data. This binary heap is a common way of implementing a "priority queue" -
     * an abstract data type that works like a typical queue or stack data structure, but where
     * each element has an additional "priority" associated with it, and that priority is used
     * to serve higher priority elements before lower priority elements.
     * 
     * A "binary heap" is defined as a binary tree with two additional constraints:
     *      1. Shape property: A binary heap is a "complete binary tree" in that all levels of the
     *         tree, except possibly the last (deepest) one, are fully filled, and, if the last
     *         level of the tree is not complete, the nodes of that level are filled from left
     *         to right.
     *      2. Heap property: The key stored in each node is either greater than or equal to
     *         ("max-heap") or less than or equal to ("min-heap") the keys in the node's children.
     *         
     * Heapsort is often compared to Binary Search Trees because of their similarities regarding
     * usage of binary trees. The KILLER FEATURE of using the binary heap when sorting is that
     * its average insertion time is O(1), whereas with BST it is O(log n). Conversely, BST has
     * an advantage over binary heap in that searching for arbitrary elements is O(log n), where
     * for binary heap it's generally O(n) except for the largest element which is O(1).
    */



    /*
     * Here, we will create an explicit class to represent a max heap, with a length, an array, and
     * a constructor. The heap is essentially an array that is represented and viewed as a binary tree
     * structure, however, this binary tree structure is partially unsorted.
    */
    public class MaxHeap
    {
        public int Length;
        public int[] Array;
        public MaxHeap(int length, int[] array)
        {
            Length = length;
            Array = array;
        }
    }

    [TestFixture]
    public class HeapsortDemonstrative
    {


        public static int[] SortViaHeapsort(int[] array)
        {
            int n = array.Length;
            MaxHeap maxHeap = CreateHeap(array, n);

            /*
             * Once initial heap has been created, and the "heap property" has been restored to the
             * array (all nodes are greater in value than their child nodes), we can swap the largest
             * item to the end of the heap, "remove" it from sorting by moving it to its final position
             * in the sorted array and reducing the heap size that we will inspect going forward, then
             * continue heapifying with the new first node. The process continues until the heap size
             * is down to 1, when everything should be sorted!
            */
            while (maxHeap.Length > 1)
            {
                Swap(maxHeap, 0, maxHeap.Length - 1);
                maxHeap.Length--;
                Heapify(maxHeap, 0);
            }

            return maxHeap.Array;
        }

        /* 
         * The heap is built recursively, calling Heapify to sort. For our zero-based arrays, the
         * root node is stored at index 0. For any node i, the parent can be found using integer
         * division via (i-1)/2. We start with the last node's parent.
        */
        public static MaxHeap CreateHeap(int[] array, int n)
        {
            MaxHeap maxHeap = new MaxHeap(n, array);  // MaxHeap object created
            int i = ((maxHeap.Length - 1) -1) / 2;    // i starts at parent of last node

            while (i >= 0)
            {
                maxHeap = Heapify(maxHeap, i);
                i--;
            }

            return maxHeap;
        }

        /*
         * Heapify performs our sorting logic. It is recursive in nature, and is called first
         * during the initial build of the max heap to restore the heap property to the array,
         * and then once again after each highest value is moved out into the sorted array,
         * until the size of the array to sort is down to 1.
        */
        public static MaxHeap Heapify(MaxHeap maxHeap, int n)
        {
            int largest = n;        // index of root, where a higher value child will swap to
            int left = 2 * n + 1;   // index of the left child (each tree level doubles in size)
            int right = 2 * n + 2;  // index of the right child

            // If left or right are not less than the length, then we know they exist as children!
            // If they exist, and are higher in value than their parent, the values will be swapped.
            if (left < maxHeap.Length && maxHeap.Array[left] > maxHeap.Array[largest])
            {
                largest = left;
            }
            if (right < maxHeap.Length && maxHeap.Array[right] > maxHeap.Array[largest])
            {
                largest = right;
            }
            if (largest != n)
            {
                // Swap, then start again, from the higher value node whose value you just swapped
                Swap(maxHeap, largest, n);
                Heapify(maxHeap, largest);
            }

            return maxHeap;
        }

        public static void Swap(MaxHeap maxHeap, int a, int b)
        {
            int temp = maxHeap.Array[a];
            maxHeap.Array[a] = maxHeap.Array[b];
            maxHeap.Array[b] = temp;
        }

        [Test]
        public void ShouldSortViaHeapsort()
        {
            int[] arrayOfIntegers = { 1, 113, 9, 999, 412, 12, 8, 2, 7007, 27, 23, 5 };
            int[] sortedArrayOfIntegers = { 1, 2, 5, 8, 9, 12, 23, 27, 113, 412, 999, 7007 };

            arrayOfIntegers = SortViaHeapsort(arrayOfIntegers);

            Assert.That(arrayOfIntegers, Is.EqualTo(sortedArrayOfIntegers));
        }
    }

    [TestFixture]
    public class HeapsortElegant
    {
        /*
         * This "elegant" version of the algorithm code is a little more terse than above, having
         * no specific class created to contain the max heap, though is logically equivalent.
        */

        public static void SortViaElegantHeapsort(int[] array)
        {
            //Build max-heap
            int heapSize = array.Length;
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
            {
                MaxHeapify(array, heapSize, p);
            }

            for (int i = array.Length - 1; i > 0; i--)
            {
                //Swap
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                heapSize--;
                MaxHeapify(array, heapSize, 0);
            }
        }

        public static void MaxHeapify(int[] array, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest;

            if (left < heapSize && array[left] > array[index])
                largest = left;
            else
                largest = index;

            if (right < heapSize && array[right] > array[largest])
                largest = right;

            if (largest != index)
            {
                //Swap
                int temp = array[index];
                array[index] = array[largest];
                array[largest] = temp;

                MaxHeapify(array, heapSize, largest);
            }
        }

        [Test]
        public void ShouldSortViaElegantHeapsort()
        {
            int[] arrayOfIntegers = { 1, 113, 9, 999, 412, 12, 8, 2, 7007, 27, 23, 5 };
            int[] sortedArrayOfIntegers = { 1, 2, 5, 8, 9, 12, 23, 27, 113, 412, 999, 7007 };

            SortViaElegantHeapsort(arrayOfIntegers);

            Assert.That(arrayOfIntegers, Is.EqualTo(sortedArrayOfIntegers));
        }
    }
}