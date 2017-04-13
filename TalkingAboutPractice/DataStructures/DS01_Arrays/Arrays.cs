using System;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS01_Arrays
{
    [TestFixture]
    public class Arrays
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * C# arrays are basically just lists of objects.
             * Their defining traits are that all objects are the same type (in most cases) and there is a specific number of them.
             * The array's nature allows for very fast access [O(1)] to elements based on their position within the list (AKA the index).
             * An array can be initialized with no elements (default type values) or a set of existing values.
             * Arrays are efficient so long as they do not have to be expanded to include more values, as all existing elements need
                    to be copied into the new, bigger array.
             * 
            */
        }

        [Test]
        public void ShouldInstantiateArraysWithDefaultValues()
        {
            int[] arrayOfIntegers = new int[3];
            Assert.AreEqual(typeof(System.Int32[]), arrayOfIntegers.GetType());
            Assert.IsTrue(arrayOfIntegers.All(integer => integer == 0));

            string[] arrayOfStrings = new string[3];
            Assert.AreEqual(typeof(System.String[]), arrayOfStrings.GetType());
            Assert.IsTrue(arrayOfStrings.All(str => str == null));

            Action[] arrayOfActions = new Action[3];
            Assert.AreEqual(typeof(System.Action[]), arrayOfActions.GetType());
            Assert.IsTrue(arrayOfActions.All(action => action == null));
        }

        [Test]
        public void ShouldAllowSettingAndGettingOfIndexValues()
        {
            int[] arrayOfIntegers = new int[] { 1, 2, 3 };
            Assert.AreEqual(2, arrayOfIntegers[1]);

            arrayOfIntegers[1] = 99;
            Assert.AreEqual(99, arrayOfIntegers[1]);
        }

        [Test]
        public void ShouldAlphabetizeListOfStrings()
        {
            string[] words = { "square", "circle", "jazz" };
            
            //Array.Sort(words, (x, y) => String.Compare(x, y));
            Array.Sort(words, String.Compare); // The default behavior of String.Compare performs the same integer-converted alphabetical sorting as the above, longer lambda version

            Assert.That(words[0], Is.EqualTo("circle"));
            Assert.That(words[1], Is.EqualTo("jazz"));
            Assert.That(words[2], Is.EqualTo("square"));
        }

        [Test]
        public void ShouldReverseAlphabetizeListOfStrings()
        {
            string[] words = { "square", "circle", "jazz" };

            Array.Sort(words, (x, y) => String.CompareOrdinal(y, x)); // A lambda Comparison delegate that subrtracts x from y in the Comparison reverses the sort order

            Assert.That(words[0], Is.EqualTo("square"));
            Assert.That(words[1], Is.EqualTo("jazz"));
            Assert.That(words[2], Is.EqualTo("circle"));
        }



        public class InterviewQuestions
        {
            public bool StringIsAllUniqueCharacters(string testString)
            {
                bool[] characterLog = new bool[256];            // Assumption made: char set is ASCII
                char[] array = testString.ToCharArray();        // Convert the input string to an array, to allow index checking

                for (int i = 0; i < testString.Length; i++)     // Loop through source string characters
                {
                    int value = array[i];
                    if (characterLog[value]) return false;      // Check each character to see if it's already been encountered
                    characterLog[value] = true;                 // If not, add it to the log
                }
                return true;                                    // If function makes it this far, no dupes were found

                // Time complexity: O(n); Space complexity: O(n) [n is length of string]
            }
            [Test]
            public void ShouldDetermineIfStringHasAllUniqueCharacters()
            {
                Assert.IsTrue(StringIsAllUniqueCharacters("abcdefg"));
                Assert.IsFalse(StringIsAllUniqueCharacters("abcdaefg"));
                Assert.IsFalse(StringIsAllUniqueCharacters("a b c"));
            }



            public string ReverseAString(string inputString)
            {
                char[] inputChars = inputString.ToCharArray();
                var length = inputChars.Length;
                var reversedChars = new char[length];

                for (int i = 0; i < length; i++)
                {
                    reversedChars[(length - 1) - i] = inputChars[i];
                }

                return new string(reversedChars);
            }
            [Test]
            public void ShouldReverseString()
            {
                var s1 = "abcdefgh";
                var s1reversed = "hgfedcba";
                var s2 = "Ff!>?fuck)(90off~dingus:)";
                var s2reversed = "):sugnid~ffo09()kcuf?>!fF";

                Assert.That(ReverseAString(s1), Is.EqualTo(s1reversed));
                Assert.That(ReverseAString(s1), Is.EqualTo(s1.Reverse()));
                Assert.That(ReverseAString(s2), Is.EqualTo(s2reversed));
                Assert.That(ReverseAString(s2), Is.EqualTo(s2.Reverse()));
            }



            /*
             * HOMIE CLUSTERS
             * ---------------
             * There are n homies living in Los Angeles, and Mork and Mindy are trying to track them down to see who is initiating new homies. 
             * New people only become homies after being scratched by an existing homie. For this reason, homieism is transitive. This means
             * that if homie 0 knows homie 1 and homie 1 knows homie 2, then homie 0 is connected to homie 2. A "homie cluster" is a
             * group of homie who are directly or indirectly linked through the other homie they know (such as the one who scratched them
             * or initiated them).
             * 
             * Complete the homie cluster function. It has 1 parameter, an array of binary strings (i.e. composed of 0s and 1s) named homies
             * that describes an n x n matrix of know connected homies; if homies[i][j] is 0, then the ith and jth homies do NOT know one
             * another (otherwise the cell contains a 1 and they DO know one another). Your function must return an integer denoting the
             * number of homie clusters Mork and Mindy have identified in Los Angeles.
             * 
             * See homie-clusters-problem.jpg for added visualization of this problem.
            */
            public static int CountHomieClusters(string[] homies)
            {
                int n = homies.Length;
                int[] homieClusterTracker = new int[n];
                int clusterCounter = 0;

                // Hmm...maybe a map isn't needed. Need to go through each individual line of chars, one line per homie.
                // Create a single master int[n] array to associate each homie with a homie cluster number to which
                // they belong, and a cluster counter to count the different clusters as you go. Going left to right,
                // if there is a 1 in the cell, we will look to record the proper cluster number in the tracker (the logic
                // for which will depend on whether it's the first cell in the homie's associations or not). We'll do
                // nothing for the cells with 0.
                // --
                // When there was a 1 in the cell, and it's the first cell, there's no previous connection to check for a
                // cluster number, so we'll simply keep the existing cluster number if one already exists, or increment the
                // cluster counter and use that value if none had previously been recorded in the tracker (i.e. it was at 0).
                // For all subsequent cells to the right, when the value is 1, we can check the previous j-1 connection, and
                // if it is not 0, we can carry over its tracked cluster number for the current homie. Again, if it is 0,
                // we need to increment the counter and use that value in the tracker.
                //
                // At the end, our tracker should represent the cluster that each homie is part of, and the maximum of these
                // numbers will represent the total number of clusters we have.
                for (int i = 0; i < n; i++)
                {
                    int[] connections = homies[i].ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
                    for (int j = 0; j < n; j++)
                    {
                        if (connections[j] > 0)
                        {
                            if (j == 0)
                            {
                                if (homieClusterTracker[j] == 0)
                                {
                                    clusterCounter++;
                                    homieClusterTracker[j] = clusterCounter;
                                }
                            }
                            else
                            {
                                var prev = connections[j - 1];
                                if (prev > 0)
                                {
                                    homieClusterTracker[j] = homieClusterTracker[j - 1];
                                }
                                else
                                {
                                    if (homieClusterTracker[j] == 0)
                                    {
                                        clusterCounter++;
                                        homieClusterTracker[j] = clusterCounter;
                                    }
                                }
                            }
                        }
                    }
                }

                return homieClusterTracker.Max();
            }
            [Test]
            public void ShouldCountHomieClusters()
            {
                string[] input1 = { "1100", "1110", "0110", "0001" };
                string[] input2 = { "10000", "01000", "00100", "00010", "00001" };

                Assert.That(CountHomieClusters(input1), Is.EqualTo(2));
                Assert.That(CountHomieClusters(input2), Is.EqualTo(5));
            }
        }
    }
}