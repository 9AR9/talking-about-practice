using System.Collections;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.StringsMakingAnagrams
{
    [TestFixture]
    public class Solution
    {
        public static int GetCharacterDeleteCountToMakeAnagrams(string a, string b)
        {
            char[] aChars = a.ToCharArray();
            char[] bChars = b.ToCharArray();
            ArrayList larger, smaller;

            if (aChars.Length > bChars.Length)
            {
                larger = new ArrayList(aChars);
                smaller = new ArrayList(bChars);
            }
            else
            {
                larger = new ArrayList(bChars);
                smaller = new ArrayList(aChars);
            }

            int largerIndex = larger.Count - 1;
            while (smaller.Count > 0 && larger.Count > 0 && largerIndex >= 0)
            {
                var smallerIndex = smaller.IndexOf(larger[largerIndex]);
                if (smallerIndex != -1)
                {
                    larger.RemoveAt(largerIndex);
                    smaller.RemoveAt(smallerIndex);
                }
                largerIndex--;
            }

            return larger.Count + smaller.Count;
        }


        [Test]
        public void ShouldGetCharacterDeleteCountToMakeAnagrams()
        {
            string a = "cde";
            string b = "abc";
            string c = "zzoboezzzzzzz";
            string d = "xxxxbooexxxx";

            Assert.That(GetCharacterDeleteCountToMakeAnagrams(a, b), Is.EqualTo(4));
            Assert.That(GetCharacterDeleteCountToMakeAnagrams(c, d), Is.EqualTo(17));
        }
    }
}