using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.QueuesATaleOfTwoStacks
{
    [TestFixture]
    public class Solution
    {
        public class MyQueue
        {
            private readonly Stack<int> _incoming = new Stack<int>();   // Will act as standard stack, adding new values when Enqueue is called
            private readonly Stack<int> _outgoing = new Stack<int>();   // Will act as a reverse order stack of the added values, activated when first Dequeue is called

            public void Enqueue(int value)
            {
                _incoming.Push(value);
            }

            // The magic is all in the Dequeue method here. When first called, outgoing stack will be empty, and
            // everything from incoming stack will be added to outgoing, in reverse order, before removing the
            // last value of outgoing (which is the first value of the incoming queue). The outgoing stack will
            // retain any other values added for future Dequeue requests, and will only refresh once it's been
            // reduced to zero and a Dequeue has been called, in which case the cycle repeats and outgoing will once
            // again be filled with the reverse-order values from incoming.
            //
            // In this sense, the full queue is represented, at any time, by the reverse order of the outgoing stack
            // plus the standard order of the incoming stack.
            public int Dequeue(bool remove = true)
            {
                if (_outgoing.Count == 0)
                {
                    while (_incoming.Count > 0)
                    {
                        _outgoing.Push(_incoming.Pop());
                    }
                }

                return remove ? _outgoing.Pop() : _outgoing.Peek();
            }
        }

        public static void PrintOutQueryResults(string[] queries)
        {
            MyQueue q = new MyQueue();

            foreach (string query in queries)
            {
                int[] currentQuery = query.Split(' ').Select(x => int.Parse(x)).ToArray();

                switch (currentQuery[0])
                {
                    case 1:
                        q.Enqueue(currentQuery[1]);
                        break;
                    case 2:
                        q.Dequeue();
                        break;
                    case 3:
                        Console.WriteLine(q.Dequeue(false));
                        break;
                    default:
                        break;
                }
            }
        }

        [Test]
        public void ShouldDetermineIfBracketsAreBalanced()
        {
            string line1 = "10";
            string[] lines = new string[] { "1 42", "2", "1 14", "3", "1 28", "3", "1 60", "1 78", "2", "2" };

            var currentConsoleOut = Console.Out;
            var consoleOutput = new ConsoleOutput();

            PrintOutQueryResults(lines);

            string[] delimiters = { "\r\n" };
            string[] outputLines = consoleOutput.GetOutput().Split(delimiters, StringSplitOptions.None);

            Assert.That(outputLines[0], Is.EqualTo("14"));
            Assert.That(outputLines[1], Is.EqualTo("14"));
        }
    }
}