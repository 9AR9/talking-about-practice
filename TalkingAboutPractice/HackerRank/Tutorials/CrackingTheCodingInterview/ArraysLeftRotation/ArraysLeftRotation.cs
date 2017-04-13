using System;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.ArraysLeftRotation
{
    [TestFixture]
    public class Solution
    {
        public static string GetLeftShiftedArray(int[] arr, int leftShifts)
        {
            int count = arr.Length;
            int[] temp = new int[count];

            for (int i = 0; i < arr.Length; i++)
            {
                if (i - leftShifts < 0)
                {
                    temp[count + (i - leftShifts)] = arr[i];
                }
                else
                {
                    temp[i - leftShifts] = arr[i];
                }
            }

            return String.Join(" ", temp.Select(x => x.ToString()));
        }


        [Test]
        public void ShouldGetCharacterDeleteCountToMakeAnagrams()
        {
            string input1 = "5 4";
            string input2 = "1 2 3 4 5";
            int[] values1 = Array.ConvertAll(input1.Split(' '), int.Parse);
            int[] values2 = Array.ConvertAll(input2.Split(' '), int.Parse);
            int count = values1[0];
            int leftShifts = values1[1];

            Assert.That(GetLeftShiftedArray(values2, leftShifts), Is.EqualTo("5 1 2 3 4"));
        }
    }
}