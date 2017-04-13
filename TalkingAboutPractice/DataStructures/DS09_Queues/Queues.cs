using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS09_Queues
{
    [TestFixture]
    public class Queues
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * The Queue data structure in C# is similar to Stack but with one major difference.
             * Rather than follow a LIFO (last in, first out) pattern, the Queue goes FIFO (first in, first out).
               This is a classic waiting list or line scenario: each request follows all previous ones before it in priority.
                    - The Enqueue() method is used to add to the Queue (equivalent to the Add() method of List, Push() of Stack).
                    - The Dequeue() method will remove the "oldest" item from the Queue, i.e. the one that was first in.
                    - The Peek() method will only return the value of the "oldest" item, but not remove it from the Queue.
             * Like Stack, Queue also has two instantiation formats, one for a specific type and one allowing any object.
            */
        }

        [Test]
        public void ShouldQueueItemsOfSpecifiedType()
        {
            Queue<string> queue = new Queue<string>();

            queue.Enqueue("first");
            queue.Enqueue("second");
            queue.Enqueue("third");
            string queuePath = string.Empty;
            while (queue.Count > 0)
            {
                queuePath += queue.Dequeue();
                if (queue.Count > 0) queuePath += " ";
            }
            Assert.AreEqual("first second third", queuePath);
        }

        [Test]
        public void ShouldQueueItemsOfMultipleTypes()
        {
            Queue queue = new Queue();

            queue.Enqueue("first");
            queue.Enqueue(2);
            queue.Enqueue(new Form());
            queue.Enqueue("third");
            string queuePath = string.Empty;
            while (queue.Count > 0)
            {
                queuePath += queue.Dequeue();
                if (queue.Count > 0) queuePath += " ";
            }
            Assert.AreEqual("first 2 System.Windows.Forms.Form, Text:  third", queuePath);
        }
    }
}