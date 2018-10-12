using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS00_General
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
        public void StringConversions()
        {
            // String to integer
            int four = int.Parse("4");

            // String to decmial
            decimal aDecimal = decimal.Parse("11.24");
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

        [Test]
        public void Should()
        {
            List<string> stringy = new List<string>();
            stringy.Add("No");
            stringy.Add("More");
            stringy.Add("Tears");

            stringy.Sort((x,y) => String.CompareOrdinal(y, x));

            //Assert.That(stringy[2], Is.EqualTo("Tears"));
            Assert.That(stringy[2], Is.EqualTo("More"));
            //Assert.That(String.Join(",", stringy), Is.EqualTo("More,No,Tears"));
            Assert.That(String.Join(",", stringy), Is.EqualTo("Tears,No,More"));

            List<int> ints = new List<int>();
            ints.Add(1);
            ints.Add(10);
            ints.Add(100);

            var commas = String.Join(",", ints);
            Assert.That(commas, Is.EqualTo("1,10,100"));

            int[] arrayOfInts = new[] {1, 2, 999, 30};
            var strang = string.Join(",", arrayOfInts);
            Assert.That(strang, Is.EqualTo("1,2,999,30"));

            List<string> stringy2 = new List<string>();
            stringy2.Add("No");
            stringy2.Add("More");
            stringy2.Add("Tears");
            stringy2.Add("Beers");

            stringy2.Sort();
            var ding = stringy2.BinarySearch("Beers");
            Assert.That(ding, Is.EqualTo(0));

            var booo = stringy2.BinarySearch("oof");
            if (booo < 0)
            {
                stringy2.Insert(~booo, "oof");
            }

            Assert.That(stringy2.Count, Is.EqualTo(5));
            Assert.That(stringy2[3], Is.EqualTo("oof"));

            int int1;
            unchecked
            {
                int1 = int.MaxValue + 2147483647 + 10;
            }
            //int1 = int.MaxValue + 1;
            Assert.That(int1, Is.Not.Null);

        }
    }
}