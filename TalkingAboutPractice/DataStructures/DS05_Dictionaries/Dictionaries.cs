using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS05_Dictionaries
{
    [TestFixture]
    public class Dictionaries
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * The Dictionary in C# is an extremely useful data structure since it lets the programmer handle the index keys. Whereas
               the ArrayList automatically makes its "keys" integers that go up by 1 (1, 2, 3, etc.), a Dictionary allows us to specify
               keys, which can be any type of object. A Dictionary introduces key control to the "collection of keys and values" paradigm.
             * It is convenient in that you do not need to cast between types when referencing elements of the Dictionary.
             * Being that any type is acceptable in a Dictionary, a nested Dictionary is perfectly legal.
             * Order is preserved when adding items to a Dictionary. Speed is sacrificed for this feature, which separates the Dictionary
               from the Hashtable - a close cousin that gives up the order for faster access.
             * Traversing to get values of the Dictionary is as easy as getting a List of the type used for the index via the Keys
               property of the Dictionary [[List<string> keys = new List<string>(myDictionary.Keys);]] and using foreach to
               loop through these keys, and use them to reference the Dictionary's values.
             * Traversing can also be performed using a foreach loop using KeyValuePair<TKey, TValue> as the iterator type and the Dictionary
               itself as the collection. KeyValuePair<TKey, TValue> is a struct from System.Collections.Generic and is the underlying
               instance type that is returned for instances of collections based on IDictionary<TKey, TValue>.
             * The Dictionary can be thought of as the "generified" version of Hashtable, having been added with .NET 2 as part of Generics.
             * In Java, TreeMap is the counterpart of C#'s Dictionary (whereas HashMap is like Hashtable).
            */
        }

        [Test]
        public void ShouldAllowKeysThatAreNotIntegers()
        {
            Dictionary<string, int> myDictionary = new Dictionary<string, int>();
            myDictionary.Add("one", 1);
            myDictionary.Add("twenty", 20);

            Assert.AreEqual(1, myDictionary["one"]);

            List<string> keys = new List<string>(myDictionary.Keys);
            int total = 0;
            foreach (string key in keys)
                total += myDictionary[key];

            Assert.AreEqual(21, total);
            Assert.AreEqual(typeof(System.Collections.Generic.Dictionary<string, int>), myDictionary.GetType());
        }

        [Test]
        public void ShouldIterateUsingListOfKeysAndRetainOrderOfAddition()
        {
            var myDictionary = new Dictionary<string, int> {{ "celery", 100 }, { "bubblegum", 10 }, { "light bulb", 1 }, { "space ghost", 1000 }};
            string concatenation = "";
            int total = 0;
            var keys = new List<string>(myDictionary.Keys);

            foreach (string key in keys)
            {
                concatenation += key;
                total += myDictionary[key];
            }

            Assert.That(concatenation, Is.EqualTo("celerybubblegumlight bulbspace ghost"));
            Assert.That(total, Is.EqualTo(1111));
        }

        [Test]
        public void ShouldIterateUsingKeyValuePairsAndRetainOrderOfAddition()
        {
            var myDictionary = new Dictionary<string, int> { {"celery", 100}, {"bubblegum", 10}, {"light bulb", 1}, {"space ghost", 1000} };
            string concatenation = "";
            int total = 0;

            foreach (KeyValuePair<string, int> kvp in myDictionary)
            {
                concatenation += kvp.Key;
                total += kvp.Value;
            }

            Assert.That(concatenation, Is.EqualTo("celerybubblegumlight bulbspace ghost"));
            Assert.That(total, Is.EqualTo(1111));
        }



        public class InterviewQuestions
        {
            /*
             * DETERMINE ELECTION WINNER
             * -------------------------
             * An array of strings represents the simplified names of candidates in an election. Each individual string represents
             * one vote in favor of the candidate represented by the string.
             * 
             * If only one candidate wins the most votes, then they win the election. If multiple candidates are found to have
             * the same highest number of votes, then the candidate whose name comes last in alphabetical order is declared
             * the winner.
             * 
             * Determine the election winner, given a list of strings representing the votes.
            */
            public static string DetermineElectionWinner(string[] votes)
            {
                Dictionary<string, int> tallies = new Dictionary<string, int>();

                foreach (string vote in votes)
                {
                    if (tallies.ContainsKey(vote))
                    {
                        tallies[vote]++;
                    }
                    else
                    {
                        tallies.Add(vote, 1);
                    }
                }

                long maxVotes = tallies.Max(x => x.Value);
                long maxCandidates = tallies.Count(x => x.Value == maxVotes);
                string[] maxMatches = new string[maxCandidates];

                var indexCounter = 0;
                foreach (KeyValuePair<string, int> kvp in tallies)
                {
                    if (kvp.Value == maxVotes)
                    {
                        maxMatches[indexCounter] = kvp.Key;
                        indexCounter++;
                    }
                }
                Array.Sort(maxMatches, (x, y) => String.CompareOrdinal(y, x));

                return maxMatches[0];
            }
            [Test]
            public void ShouldFindElectionWinner()
            {
                string[] input1 = { "Bob", "Carol", "Bob", "Bob", "Carol", "Carol" };
                string[] input2 = { "Alex", "Michael", "Harry", "Dave", "Michael", "Victor", "Harry", "Alex", "Mary", "Mary" };
                string[] input3 = { "Al", "Dick", "Dick" };

                Assert.That(DetermineElectionWinner(input1), Is.EqualTo("Carol"));
                Assert.That(DetermineElectionWinner(input2), Is.EqualTo("Michael"));
                Assert.That(DetermineElectionWinner(input3), Is.EqualTo("Dick"));
            }
        }















    }
}