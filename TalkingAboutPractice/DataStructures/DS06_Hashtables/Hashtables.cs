using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS06_Hashtables
{
    [TestFixture]
    public class Hashtables
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * The C# Hashtable structure is the precursor to the Dictionary data structure. It also takes in data as key/value
               pairs, but it does so as generic objects instead of as typed data.
             * Values are then stored in order according to their key's HashCode - meaning that the order in which items are
               added to a C# Hashtable is NOT preserved (another difference from Dictionary).
             * Speed is the primary benefit of the Hashtable vs. the Dictionary. A Hashtable stores items faster than a Dictionary,
               which sacrifices speed for the sake of order.
             * Traversing to get values of the Hashtable is performed using a foreach loop using DictionaryEntry as the iterator type
               and the Hashtable itself as the collection. DictionaryEntry is a struct from System.Collections and is the underlying
               instance type that is returned for instances of collections based on IDictionary.
             * The Hashtable can be thought of as the ".NET 1.0 way of doing things" when it comes to name/value pair sets, as
               the concept of type parameters was not introduced until Generics was added in .NET 2.0.
             * Run-time errors can be encountered, if bad assumptions are made about the data that needs to be cast when retrieving
               a value. This can happen easily since a Hashtable allows values to be of different types.
             * In Java, HashMap is the counterpart of C#'s Hashtable (whereas TreeMap is like Dictionary).
             * Expanding a Hashtable is not an inexpensive operation, so, if you have an estimate on how many items your Hashtable
               will end up containing, you should set the Hashtable's initial capacity accordingly in the constructor so as to
               avoid unnecessary expansions.
             * 
             * 
             * TODO: What is this business about counting occurences of each value easily with a Hashtable?
            */
        }

        [Test]
        public void ShouldAllowKeysThatAreNotIntegers()
        {
            Hashtable myHashtable = new Hashtable();

            myHashtable.Add("one", 1);
            Assert.That(myHashtable["one"], Is.EqualTo(1));
            myHashtable.Add("twenty", 20);
            Assert.That(myHashtable["twenty"], Is.EqualTo(20));

            int total = 0;
            foreach (string key in myHashtable.Keys)
                total += (int)myHashtable[key];
            Assert.That(total, Is.EqualTo(21));

            Assert.That(myHashtable.GetType(), Is.EqualTo(typeof(System.Collections.Hashtable)));
        }

        [Test]
        public void ShouldAllowDifferentKeyTypesToBeAdded()
        {
            Hashtable myHashtable = new Hashtable {{"one", 1}, {2, "two"}, {3333333333333333333, 3}};

            Assert.That(myHashtable.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldErrorWhenAddingIndexThatAlreadyExists()
        {
            Hashtable myHashtable = new Hashtable();
            myHashtable.Add("one", 1);

            Assert.Throws<System.ArgumentException>(() => myHashtable.Add("one", 2));
        }

        [Test]
        public void ShouldErrorWhenMakingFalseAssumptionAboutDataType()
        {
            var myHashtable = new Hashtable() { {"one", 1}, {2, "two"} };
            int total = 0;
            List<double> notString = null;

            Assert.Throws<System.FormatException>(() => total += int.Parse(myHashtable[2].ToString()));
            Assert.Throws<System.InvalidCastException>(() => notString = (List<double>)myHashtable[2]);
            Assert.That(total, Is.EqualTo(0));
            Assert.That(notString, Is.Null);
        }

        [Test]
        public void ShouldAllowDuplicateValues()
        {
            Hashtable myHashtable = new Hashtable();
            myHashtable.Add("one", 1);
            myHashtable.Add("two", 1);

            Assert.That(myHashtable.Count, Is.EqualTo(2));
        }

        [Test]
        public void ShouldIterateUsingKeyValuePairsButNotRetainOrderOfAddition()
        {
            var myHashtable = new Hashtable() { { 2, "two" }, { "one", 1 }, { "three", new List<double>() } };
            string keysConcatenated = "";
            string valuesConcatenated = "";

            foreach (DictionaryEntry de in myHashtable)
            {
                keysConcatenated += de.Key;
                valuesConcatenated += de.Value;
            }

            Assert.That(keysConcatenated, Is.Not.EqualTo("2onethree"));
            Assert.That(valuesConcatenated, Is.Not.EqualTo("two1System.Collections.Generic.List`1[System.Double]"));
        }

        [Test]
        public void ShouldSetInitialSizeOfHashtableIfEstimateIsKnown()
        {
            Hashtable employees = new Hashtable(1000);
            employees.Add("PHIL","Phillip");
            employees.Add("JOSE","Josephina");
            employees.Add("MARK","Mark");

            Assert.That(employees.Count, Is.EqualTo(3));
        }
    }
}
