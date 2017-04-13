using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice
{
    [TestFixture]
    public class InputAndConversion
    {
        public void ReadConsoleInput()
        {
            // Single integer line (example: 5)
            int total = int.Parse(Console.ReadLine());

            // Multiple integer line, space sparated, convert from strings to integers (example: 1 3 45 6 12 421 9)
            string[] arr_temp = Console.ReadLine().Split(' ');
            int[] values = Array.ConvertAll(arr_temp, int.Parse);
        }

        [Test]
        public void ArrayConversions()
        {
            // Array of integers --> array of strings
            int[] arrayOfInts = { 1, 2, 3, 4, 5 };
            string[] arrayOfStrings = arrayOfInts.Select(x => x.ToString()).ToArray();

            Assert.That(arrayOfStrings[0], Is.EqualTo("1"));



            // Array of integers to space-delimited string
            string spaceDelimitedInts = String.Join(" ", arrayOfInts.Select(x => x.ToString()).ToArray());

            Assert.That(spaceDelimitedInts, Is.EqualTo("1 2 3 4 5"));



            // ArrayList to array of integers (will ignore non-integer values)
            ArrayList arrayListOfInts = new ArrayList();
            arrayListOfInts.Add(4);
            arrayListOfInts.Add(78);
            int[] arrayOfIntsFromArrayList = arrayListOfInts.OfType<int>().ToArray();

            Assert.That(arrayOfIntsFromArrayList[1], Is.EqualTo(78));



            // LinkedList of strings to space-delimited string
            LinkedList<string> linkedListOfStrings = new LinkedList<string>();
            linkedListOfStrings.AddLast("last");
            linkedListOfStrings.AddFirst("first");
            string spaceDelimitedStringOfStrings = String.Join(" ", linkedListOfStrings.ToArray());

            Assert.That(spaceDelimitedStringOfStrings, Is.EqualTo("first last"));



            // LinkedList of integers to space-delimited string
            LinkedList<int> linkedListOfIntegers = new LinkedList<int>();
            linkedListOfIntegers.AddLast(99);
            linkedListOfIntegers.AddFirst(1);
            string spaceDelimitedStringOfIntegers = String.Join(" ", linkedListOfIntegers.ToArray().Select(x => x.ToString()).ToArray());

            Assert.That(spaceDelimitedStringOfIntegers, Is.EqualTo("1 99"));
        }
    }
}