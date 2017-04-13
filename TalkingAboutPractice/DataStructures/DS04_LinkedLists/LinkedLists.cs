using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS04_LinkedLists
{
    [TestFixture]
    public class LinkedLists
    {
        LinkedList<int> _integerLinkedList;

        [SetUp]
        public void Initialize()
        {
            /*
             * And now for a completely different type of data structure: the LinkedList!
             * A LinkedList is a series of objects which, instead of having their references indexed (like an array), stay together by
               linking to each other, in Nodes.
             * A C# LinkedList Node has basically three nodes: the Object's value, a reference to the next Node, and a reference to
               the previous node. A C# LinkedList is then said to be a "double-linked list" due to it's ability to link in both directions.
               It cannot, however, be "circular" or contain "cycles" - meaning two nodes pointing at each other via a property like Next -
               like in many other languages. An attempt to point one node to another in the same LinkedList will throw an error telling
               you that the LinkedList node already belongs to a LinkedList. However, such a class can be created manually to provide such
               behavior if one desired.
             * What's the point?
                    - Adding values to the middle of the list is much faster compared to any other Array-type of data structure.
                    - It keeps memory costs down to a minimum (whereas Lists use extra space to make future insertions as fast as possible.
             * Retrieving a value is not as straightforward, however, as referential functions are used, instead of explicitly numbered indices.
             * Time operations/Performance:
                    - Since element insertion and removal is done by updating a couple references, they can be done in constant time.
                    - The tradeoff is that accessing elements is no longer a constant time operation, like with an array's index, which can
                      always be instantly accessed. With a LinkedList, the references between nodes always has to be followed until the desired
                      element is found.
                    - A LinkedList certainly can be less efficient than a properly-sized array or List of the elements, and this is because of
                      the way objects are allocated in memory by the .NET framework. Each node of a LinkedList will require a separate root
                      in the garbage collector, as opposed to arrays and Lists where many references are stored in a single block on the managed
                      heap together, reducing the work for garbage collection.
                    - For runtime performance in general, reducing levels of abstraction is important, as it will reduce the CPU and memory accesses.
                      Each time a reference in a LinkedList is encountered, another level of indirection occurs and performance decreases. Thus,
                      accessing elements tightly-packed in an array or List is faster than in a LinkedList. The pointers can be accessed faster.
                    - THE KEY PERFORMANCE GAIN for LinkedList is the time required for insertion/removal.
                    - THE PRIMARY ADVANTAGE of LinkedList over arrays is that the links provide us with the capability to rearrange the items efficiently.
             * The AddLast() method on LinkedList is equivalent to the Add() method on List, but the implementation is different. No internal
               array is used or needed.
             * The easiest way to traverse and examine all contents of a LinkedList is with the foreach loop [[foreach (int value in integerLinkedList)]]
            */

            _integerLinkedList = new LinkedList<int>();
        }

        [Test]
        public void ShouldAllowReferentialInsertionAtAnyPoint()
        {
            _integerLinkedList.AddLast(2);
            Assert.AreEqual(2, _integerLinkedList.First.Value);
            Assert.AreEqual(2, _integerLinkedList.Last.Value);
            Assert.That(_integerLinkedList.Count, Is.EqualTo(1));

            _integerLinkedList.AddLast(3);
            Assert.AreEqual(2, _integerLinkedList.First.Value);
            Assert.AreEqual(3, _integerLinkedList.Last.Value);

            _integerLinkedList.AddFirst(1);
            Assert.AreEqual(1, _integerLinkedList.First.Value);
            Assert.AreEqual(2, _integerLinkedList.First.Next.Value);
            Assert.AreEqual(3, _integerLinkedList.Last.Value);

            _integerLinkedList.AddAfter(_integerLinkedList.First, 100);
            Assert.AreEqual(1, _integerLinkedList.First.Value);
            Assert.AreEqual(100, _integerLinkedList.First.Next.Value);
            Assert.AreEqual(2, _integerLinkedList.First.Next.Next.Value);

            Assert.AreEqual(typeof(System.Collections.Generic.LinkedList<int>), _integerLinkedList.GetType());
        }

        [Test]
        public void ShouldNotNeedToCastRetrievedValueBackToOriginalType()
        {
            _integerLinkedList.AddFirst(9009);
            Assert.AreEqual(9009, _integerLinkedList.Last.Value);
        }

        [Test]
        public void ShouldFindPointToInsertIntoAndRemoveFromTheLinkedList()
        {
            LinkedList<string> words = new LinkedList<string>();

            words.AddLast("one");
            words.AddLast("two");
            words.AddLast("three");
            LinkedListNode<string> foundNode = words.Find("one");
            words.AddAfter(foundNode, "inserted");
            Assert.AreEqual("one inserted two three!", SentencitizeLinkedListOfStrings(words));

            foundNode = words.Find("inserted");
            words.Remove(foundNode);
            Assert.AreEqual("one two three!", SentencitizeLinkedListOfStrings(words));
            words.Remove("two");
            Assert.AreEqual("one three!", SentencitizeLinkedListOfStrings(words));
        }

        [Test]
        public void ShouldReflectProperPreviousAndNextValuesWhenInsertedIntoTheLinkedList()
        {
            var ll = new LinkedList<int>();
            ll.AddFirst(7);
            ll.AddLast(77);
            var found1 = ll.Find(7);
            ll.AddAfter(found1, 777);
            var found2 = ll.Find(777);

            Assert.That(ll.Count, Is.EqualTo(3));
            Assert.That(found2.Previous.Value, Is.EqualTo(7));
            Assert.That(found2.Next.Value, Is.EqualTo(77));
        }

        public string SentencitizeLinkedListOfStrings(LinkedList<string> words)
        {
            // This can be done in a single line, instead of writing the foreach loop (commented out below)
            return String.Join(" ", words.ToArray()) + "!";

            //string seeTheWords = string.Empty;
            //foreach (string word in words)
            //{
            //    seeTheWords += word + " ";
            //}
            //return seeTheWords.Trim() + "!";
        }
    }
}