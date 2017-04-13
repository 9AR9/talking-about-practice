using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Implementation.NonDivisibleSubset
{
    [TestFixture]
    public class Solution
    {
        static int FindNonDivisibleSubset(int[] values, int divisor)
        {
            int[] remainders = new int[divisor];
            int result = 0;

            // Divide all values by the divisor and count how many of each possible remainder value is encountered
            foreach (int value in values)
            {
                remainders[value % divisor] += 1;
            }

            // Since no two elements added together are allowed to be divisible by the divisor, this also means that
            // the modulo(divisor) of no two numbers can sum to divisor.
            //
            // For example, if divisor is 10, you can't have two numbers where (first_number % divisor) is 1 and
            // (second_number % divisor) is 9, since those would sum to 10. Thus, we'll either take all of the
            // numbers whose value of % divisor is 1, or all whose value of % divisor is 9, whichever is larger.
            //
            // The i * 2 in the for loop guarantees that we'll only interate through the first half of the total
            // integers counting up toward the divisor, and we'll find our "opposite" modulo value for each iteration.
            // We can then compare the i with its opposite, and if they are different, we'll take the higher count of
            // the two. When they are the same, we're encountering the 0 value - which reflects a value that is the same
            // as the divisor itself, and we will add no more than 1 to the total, if indeed this number was part of
            // the value set.
            for (int i = 0; i * 2 <= divisor; i++)
            {
                int opposite = (divisor - i) % divisor;
                if (i == opposite)
                {
                    result += Math.Min(remainders[i], 1);
                }
                else
                {
                    result += Math.Max(remainders[i], remainders[opposite]);
                }
            }

            return result;
        }
        [Test]
        public void ShouldFindNonDivisibleSubset()
        {
            int[] values = { 1, 7, 2, 4 };
            int divisor = 3;

            int result = FindNonDivisibleSubset(values, divisor);
            Assert.AreEqual(3, result);
        }
    }
}