using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.LinkedListsDetectACycle
{
    [TestFixture]
    public class Solution
    {
        /*
         * Since .NET LinkedLists do not allow for ciruclar references, I have just created my own simple
         * implementation here. The node class carries only Value and Next properties, while the list class
         * itself carries only a Head property and an Add function - enough to replicate the issue and
         * solve the problem.
         */

        public class MyLinkedListNode
        {
            public MyLinkedListNode Next { get; set; }
            public object Value { get; set; }
        }

        public class MyLinkedList
        {
            public MyLinkedListNode Head { get; set; }

            public void Add(object value)
            {
                MyLinkedListNode nodeToAdd = new MyLinkedListNode { Value = value };
                MyLinkedListNode currentNode = Head;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = nodeToAdd;
            }
        }

        // Finding the cycle is easy enough, leveraging the HashSet to quickly tell us the
        // first point in the linked list where we encounter a node reference for the
        // second time.
        public static bool ContainsCycle(MyLinkedListNode head)
        {
            var currentNode = head;
            var visitedNodes = new HashSet<MyLinkedListNode>();
            while (currentNode != null)
            {
                if (!visitedNodes.Add(currentNode))
                    return true;
                currentNode = currentNode.Next;
            }
            return false;
        }
        // As HashSets are less popular than ArrayLists, this second version utilizes an ArrayList
        // instead of a HashSet to store the visited nodes, and checking for the existence of
        // each node first before adding it if not found. Very slight difference in logic.
        public static bool ContainsCycleUsingArrayList(MyLinkedListNode head)
        {
            var currentNode = head;
            var visitedNodes = new ArrayList();
            while (currentNode != null)
            {
                if (visitedNodes.IndexOf(currentNode) > -1)
                    return true;
                visitedNodes.Add(currentNode);
                currentNode = currentNode.Next;
            }
            return false;
        }

        [Test]
        public void ShouldFindCircularReferenceInLinkedList()
        {
            MyLinkedList myLinkedList = new MyLinkedList();
            MyLinkedListNode head = new MyLinkedListNode() { Value = 1 };
            myLinkedList.Head = head;
            myLinkedList.Head.Next = new MyLinkedListNode() { Value = 2 };
            myLinkedList.Head.Next.Next = myLinkedList.Head;

            Assert.That(ContainsCycle(myLinkedList.Head), Is.True);
            Assert.That(ContainsCycleUsingArrayList(myLinkedList.Head), Is.True);
        }

        [Test]
        public void ShouldFindNoCircularReferenceInLinkedListEvenWhenTwoValuesAreSame()
        {
            MyLinkedList myLinkedList = new MyLinkedList();
            MyLinkedListNode head = new MyLinkedListNode() { Value = 1 };
            myLinkedList.Head = head;
            myLinkedList.Add(2);
            myLinkedList.Add(2);
            myLinkedList.Add(3);

            Assert.That(ContainsCycle(myLinkedList.Head), Is.False);
            Assert.That(ContainsCycleUsingArrayList(myLinkedList.Head), Is.False);
        }

        [Test]
        public void ShouldFindCircularReferenceOnlyUsingNodes()
        {
            MyLinkedListNode head = new MyLinkedListNode() { Value = 1 };
            head.Next = new MyLinkedListNode() { Value = 2 };
            head.Next.Next = head;

            Assert.That(ContainsCycle(head), Is.True);
            Assert.That(ContainsCycleUsingArrayList(head), Is.True);
        }
    }
}