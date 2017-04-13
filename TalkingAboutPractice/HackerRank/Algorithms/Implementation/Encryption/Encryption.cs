using System;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Algorithms.Implementation.Encryption
{
    [TestFixture]
    public class Solution
    {
        public static string EncryptWithSquareRootDerivedScheme(string input)
        {
            int rows = (int)Math.Floor(Math.Sqrt(input.Length));
            int columns = (int)Math.Ceiling(Math.Sqrt(input.Length));
            string result = "";

            // Since floor(sqrt(length)) is <= length, we need to increase the rows value
            // by one if the input's length is greater than the total current area of the grid
            if (input.Length > rows * columns)
            {
                rows++;
            }

            string[,] wordGrid = new string[rows, columns]; // Multidimensional array for a "word" grid
            int index = 0;

            // Fill grid left to right, top-down, with individual characters of the string
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (index < input.Length)
                        wordGrid[i, j] = input.Substring(index, 1);
                    index++;
                }
            }

            // Iterate through grid columns to print out result
            for (int k = 0; k < columns; k++)
            {
                for (int l = 0; l < rows; l++)
                {
                    if (wordGrid[l, k] != null)
                        result += wordGrid[l, k];
                }
                result += " ";
            }

            return result.Trim();
        }

        [Test]
        public void ShouldEncryptUsingSquareRootDerivedScheme()
        {
            Assert.That(EncryptWithSquareRootDerivedScheme("haveaniceday"), Is.EqualTo("hae and via ecy"));
            Assert.That(EncryptWithSquareRootDerivedScheme("feedthedog"), Is.EqualTo("fto ehg ee dd"));
            Assert.That(EncryptWithSquareRootDerivedScheme("chillout"), Is.EqualTo("clu hlt io"));
        }
    }
}