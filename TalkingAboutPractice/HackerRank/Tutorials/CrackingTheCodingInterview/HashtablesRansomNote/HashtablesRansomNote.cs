using System;
using System.Collections;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.HashtablesRansomNote
{
    [TestFixture]
    public class Solution
    {
        /*
         * This is the first function I wrote for the ransom note question, using an ArrayList to deconstruct
         * the magazine word collection, removing words from it that are not in the ransom note, and then
         * comparing the values at the end to see if they match. It works fine for the first few test cases,
         * but timed out on many of the larger test cases upon submission. That led me to rethink the
         * algorithm for optimization, and to also take a cue from the problem's title, and get some
         * Hashtables in the mix!
         */
        public static string RansomNoteCanBeReplicatedFromMagazine(string[] magazine, string[] ransomNote)
        {
            int ransomCount = ransomNote.Length;
            int matches = 0;
            ArrayList tempMagazine = new ArrayList(magazine);

            foreach (string word in ransomNote)
            {
                if (tempMagazine.Contains(word))
                {
                    matches++;
                    tempMagazine.Remove(word);
                }
            }

            return ransomCount == matches ? "Yes" : "No";
        }

        /*
         * After rethiking the approach for faster performance, I wrote this second function, which does
         * pass all the test cases. This time, I use Hashtables to store all the found words along with 
         * occurance counts for both data sets. I then iterate through the ransom note words, and terminate
         * early if any of them are not found in magazine, or if they are found but the magazine does not
         * have enough of them. If neither of those conditions ever hits, then a Yes can be returned
         * after the iteration has completed.
         */
        public static string RansomNoteCanBeReplicatedFromMagazineUsingHashtable(string[] magazine, string[] ransomNote)
        {
            Hashtable magazineMap = new Hashtable();
            Hashtable ransomNoteMap = new Hashtable();

            // Populate a Hashtable with magazine words as key and a counter integer as value
            foreach (string word in magazine)
            {
                if (!magazineMap.ContainsKey(word))
                {
                    magazineMap.Add(word, 1);
                }
                else
                {
                    int current = (int)magazineMap[word];
                    magazineMap[word] = current + 1;
                }
            }

            // Populate a Hashtable with ransom note words as key and a counter integer as value
            foreach (string word in ransomNote)
            {
                if (!ransomNoteMap.ContainsKey(word))
                {
                    ransomNoteMap.Add(word, 1);
                }
                else
                {
                    int current = (int)ransomNoteMap[word];
                    ransomNoteMap[word] = current + 1;
                }
            }

            // Iterate through ransom note; if any word is not in magazine, stop and fail; if any word is found but in too few occurances, stop and fail.
            foreach (string word in ransomNoteMap.Keys)
            {
                if (!magazineMap.ContainsKey(word))
                    return "No";

                int magazineCount = (int)magazineMap[word];
                int ransomNoteCount = (int)ransomNoteMap[word];

                if (magazineCount < ransomNoteCount)
                    return "No";
            }

            return "Yes";
        }


        [Test]
        public void ShouldDetermineIfRansomNoteCanBeReplicated()
        {
            string input1 = "6 4";
            string input2 = "give me one grand today night";
            string input3 = "give one grand today";
            int[] values1 = Array.ConvertAll(input1.Split(' '), int.Parse);
            string[] values2 = input2.Split(' ');
            string[] values3 = input3.Split(' ');
            int count = values1[0];
            int leftShifts = values1[1];

            Assert.That(RansomNoteCanBeReplicatedFromMagazineUsingHashtable(values2, values3), Is.EqualTo("Yes"));

            string input1b = "15 17";
            string input2b = "o l x imjaw bee khmla v o v o imjaw l khmla imjaw x";
            string input3b = "imjaw l khmla x imjaw o l l o khmla v bee o o imjaw imjaw o";
            int[] values1b = Array.ConvertAll(input1b.Split(' '), int.Parse);
            string[] values2b = input2b.Split(' ');
            string[] values3b = input3b.Split(' ');
            int countb = values1b[0];
            int leftShiftsb = values1b[1];

            Assert.That(RansomNoteCanBeReplicatedFromMagazineUsingHashtable(values2b, values3b), Is.EqualTo("No"));
        }
    }
}