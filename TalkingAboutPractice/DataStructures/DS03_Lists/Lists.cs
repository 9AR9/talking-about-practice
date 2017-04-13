using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS03_Lists
{
    [TestFixture]
    public class Lists
    {
        List<int> integerHashSet;

        [SetUp]
        public void Initialize()
        {
            /*
             * The C# List data structure, introduced with .NET 2.0, is a generic version of Array List,
                    in that it behaves in exactly the same way, but for only one specified type.
             * Since List<> is taylored to a specific data type, there is no need to cast when retrieving values.
             * This results in much cleaner, and often faster, code.
             * You should always use List<> over ArrayList() (unless working with .NET framework 1.1).
             * Note: List<Object> is perfectly legal, although it defeats the purpose of having a generic dynamic array collection.
            */

            integerHashSet = new List<int>();
        }

        [Test]
        public void ShouldAllowDynamicLoadingOfValuesForSpecificType()
        {
            integerHashSet.Add(987);
            integerHashSet.Add(44);
            Assert.AreEqual(987, integerHashSet[0]);
            Assert.AreEqual(44, integerHashSet[1]);
        }

        [Test]
        public void ShouldNotNeedToCastRetrievedValueBackToOriginalType()
        {
            integerHashSet.Add(9009);
            Assert.AreEqual(9009, integerHashSet[0]);
        }
    }
}