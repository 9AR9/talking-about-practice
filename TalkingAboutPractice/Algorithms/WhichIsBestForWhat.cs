using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TalkingAboutPractice.Algorithms
{

    /*
     * WHICH SORT ALGORITHM IS THE BEST?
     * ---------------------------------
     * While the true answer is that there's no one sort algorithm that is best for all cases, you can
     * generally lean on Quicksort as being the best general algorithm you can use. Heapsort and
     * Merge Sort are both also known to be at the elite level of sorts. All three of these sorts
     * yield an elite O(n log n) ideal running time, yet they all have their own quirks that can make
     * them better or worse than others in some situations. Insertion sort comes in slower at O(n²),
     * but it still also in the conversation a lot for smaller data sets.
     *
     * C# includes built-in Array.Sort and List.Sort methods to perform sorts for you, and these methods
     * use an "introspective sort" approach to decide the right sorting algorithm to employ under the
     * covers. This will end up choosing the Insertion Sort method for only the cases where search
     * partitions are smaller than 16. It chooses Quicksort otherwise, unless the partitions exceed a
     * recursion depth level based on the log of the number of elements being sorted, in which case it
     * uses Heapsort.
     *
     *
     *
     * WHICH SEARCH ALGORITHM RULES THE HARDEST?
     * -----------------------------------------
     * Searching for data is a pretty difficult task, unless your data is first sorted.
     *
     * When dealing with unsorted data, your standard approach is going to be Linear Search, which
     * traverses each value in order until it finds a match, yielding a runtime of O(n) which is
     * certainly not good. The more data you have, the longer your search might be. To improve
     * upon linear searching, "parallel" linear search techniques exist out there. Gnarly stuff.
     *
     * Once you have sorted data, then searching is generally best done using Binary Search, which
     * uses a divide-and-conquer strategy, starting at the middle of a sorted set, and making
     * comparisons from there to reduce the search set with each iteration. This yields a much
     * better O(log n) runtime. The Binary Search Trees pattern also exists, and yields a
     * similar O(log n) runtime as Binary Search.
     *
     * In C#, if you attempt to use the BinarySearch() method on an unsorted Array or List, you
     * will receive back a negative number, which should match the bitwise complement of the
     * index of the first element in the list that is larger. A negative return value in general
     * would trigger a non-find, logically, so that's bad, mmkay? The C# Find() method exists
     * to search an unsorted Array or List for a specific value, and, behind the scenes, it
     * performs an introspective sort in-memory of your data, before then using a Binary Search
     * to find the search value.
     */

    public class WhichIsBestForWhat
    {
    }
}