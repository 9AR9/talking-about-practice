using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS03_Lists
{
    [TestFixture]
    public class Lists
    {

        [SetUp]
        public void Initialize()
        {
            /*
             * The C# List data structure, introduced with .NET 2.0, is a generic version of Array List,
             *      in that it behaves in exactly the same way, but for only one specified type, leveraging
             *      the Generics feature of .NET 2.0 to allow the user to specify the type at the time
             *      of creation. Additionally, there are many helpful methods built in to the List type,
             *      such as Contains(), IndexOf(), BinarySearch(), Find(), FindAll(), Sort(), and ConvertAll().
             * This makes List, in essence, a homogenous, self-redimensioning array.
             * Since List<> is tailored to a specific data type, there is no need to cast when retrieving values.
             * This results in much cleaner, and often faster, code.
             * You should always use List<> over ArrayList() (unless working with .NET framework 1.1).
             * Note: List<Object> is perfectly legal, although it defeats the purpose of having a generic
             *      dynamic array collection.
            */
        }

        [Test]
        public void ShouldAllowDynamicLoadingOfValuesForSpecificTypeAndRetainOrder()
        {
            List<int> integerList = new List<int>();

            integerList.Add(987);
            integerList.Add(44);
            Assert.AreEqual(987, integerList[0]);
            Assert.AreEqual(44, integerList[1]);
        }

        [Test]
        public void ShouldNotNeedToCastRetrievedValueBackToOriginalType()
        {
            List<int> integerList = new List<int>();

            integerList.Add(9009);
            Assert.AreEqual(9009, integerList[0]);
        }

        [Test]
        public void ShouldSortListOfIntegersInNumericOrder()
        {
            List<int> integers = new List<int>() { 88, 5, 16, 198, 40, 77, 12 };

            // The default behavior of Sort places numbers in numeric order (same as commented longer version below, with lambda comparison delegate)
            //integers.Sort((x, y) => x.CompareTo(y));
            integers.Sort();

            Assert.That(String.Join(",", integers), Is.EqualTo("5,12,16,40,77,88,198"));
        }

        [Test]
        public void ShouldSortListOfIntegersInReverseNumericOrder()
        {
            List<int> integers = new List<int>() { 88, 5, 16, 198, 40, 77, 12 };

            // Passing this comparison as a parameter of Sort reverses the default comparison check, resulting in reversed order
            integers.Sort((x,y) => y.CompareTo(x));

            Assert.That(String.Join(",", integers), Is.EqualTo("198,88,77,40,16,12,5"));
        }

        [Test]
        public void ShouldSortListOfStringsInAlphabeticalOrder()
        {
            List<string> integers = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };

            // The default behavior of Sort places strings in alphabetic order (same as commented longer version below, with lambda comparison delegate,
            // which performs an integer-converted alphabetical sorting)
            //integers.Sort((x, y) => String.CompareOrdinal(x, y));
            integers.Sort();

            Assert.That(String.Join(",", integers), Is.EqualTo("Candy,Change,Disc,Key,Tissue,Wallet"));
        }

        [Test]
        public void ShouldSortListOfStringsInReverseAlphabeticalOrder()
        {
            List<string> words = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };

            // Passing this comparison as a parameter of Sort reverses the default comparison check, resulting in reversed order
            words.Sort((x,y) => String.CompareOrdinal(y, x));

            Assert.That(String.Join(",", words), Is.EqualTo("Wallet,Tissue,Key,Disc,Change,Candy"));
        }

        [Test]
        public void ShouldFindExistingItemInListUsingContains()
        {
            List<string> words = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };

            var contains = words.Contains("Key");

            Assert.IsTrue(contains);

            contains = words.Contains("Blowtorch");

            Assert.IsFalse(contains);
        }

        [Test]
        public void ShouldFindIndexOfExistingItemInListUsingContainsAndIndexOf()
        {
            List<string> words = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };
            int foundIndex = -1;
            string searchValue = "Disc";

            if (words.Contains(searchValue))
                foundIndex = words.IndexOf(searchValue);

            Assert.That(foundIndex, Is.EqualTo(5));
        }

        [Test]
        public void ShouldFindIndexOfExistingItemInSortedListUsingBinarySearch()
        {
            List<string> words = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };
            int foundIndex = -1;
            string searchValue = "Disc";

            //words.Sort();
            foundIndex = words.BinarySearch(searchValue);

            Assert.That(foundIndex, Is.EqualTo(2));
        }

        [Test]
        public void ShouldReturnNegativeValueWhenItemNotFoundUsingBinarySearch()
        {
            List<string> words = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };
            int foundIndex = -1;
            string searchValue = "Horse";

            words.Sort();
            foundIndex = words.BinarySearch(searchValue);

            Assert.That(foundIndex, Is.Negative);
        }

        [Test]
        public void ShouldUseNegativeReturnValueOfNotFoundBinarySearchItemToInsertNewItemInListViaBitwiseComplementOfNegativeValue()
        {
            List<string> words = new List<string>() { "Candy", "Wallet", "Change", "Tissue", "Key", "Disc" };
            int foundIndex = -1;
            string searchValue = "Horse";

            words.Sort();
            foundIndex = words.BinarySearch(searchValue);
            if (foundIndex < 0)
            {
                // The bitwise complement (two's complement, 8-bit) provides the flipped-bits representation of a signed binary
                // integer. Since the number 2 is represented as 0000 0010, it's flipped-bit counterpart is 1111 1101. In 2's
                // complement representation for signed numbers, the first bit on the left represents the sign, 0 meaning positive
                // and 1 meaning negative. So our flipped version of 2 actually represents a negative number, but which negative
                // number? Well, 1111 1101 actually represents -3, because the bitwise complement is actually the bit-flipped
                // version of a positive number plus 1. If we were to flip 3, which is 0000 0011, we get 1111 1100, and then we
                // add 1, which gives us 1111 1101 - the same as our bitwise complement of 2.
                //
                // The shorthand for this complement is to negate the number and subtract 1 -- so the bitwise complement of
                // 2 is (-2 - 1) which equals -3; conversely, the bitwise complement of -3 is (3 - 1) which equals 2.
                words.Insert(~foundIndex, searchValue);
            }

            Assert.That(String.Join(",", words), Is.EqualTo("Candy,Change,Disc,Horse,Key,Tissue,Wallet"));
            Assert.That(~2, Is.EqualTo(-3));
            Assert.That(~-3, Is.EqualTo(2));
            Assert.That(~1176, Is.EqualTo(-1177));
            Assert.That(~-1177, Is.EqualTo(1176));
        }
    }
}