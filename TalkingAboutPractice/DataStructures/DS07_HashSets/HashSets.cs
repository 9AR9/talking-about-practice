using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS07_HashSets
{
    [TestFixture]
    public class HashSets
    {
        private HashSet<int> _integerHashSet;
        [SetUp]
        public void Initialize()
        {
            /*
             * The C# HashSet, introduced with .NET 3.5, very strongly resembles the List data structure.
             * The very important characteristic that makes it different is that it DOES NOT ALLOW DUPLICATE VALUES.
             * Attempts to add a duplicated value to the HashSet are simply ignored, and not reflected in the Set.
             * The point of this is simplify the SPEED at which a Set can be checked for an existing value match before then
               adding it to the collection.
                    - The same can be achieved with a List using code like [[if (!myList.Contains(element)) myList.Add(element);]], but...
                    - It is much faster with HashSet because HashSet is not a simple array, and is specifically designed to allow fast
                      search times which dramatically improves the performance of the duplicate value check.
            */

            _integerHashSet = new HashSet<int>();
        }

        [Test]
        public void ShouldAllowDynamicLoadingOfValuesForSpecificType()
        {
            _integerHashSet.Add(1);
            _integerHashSet.Add(2);
            _integerHashSet.Add(1);
            _integerHashSet.Add(3);
            List<int> listFromIntegerHashSet = _integerHashSet.ToList<int>();

            Assert.AreEqual(1, listFromIntegerHashSet[0]);
            Assert.AreEqual(2, listFromIntegerHashSet[1]);
            Assert.AreEqual(3, listFromIntegerHashSet[2]);
            Assert.AreEqual(3, listFromIntegerHashSet.Count);

            Assert.AreEqual(typeof(System.Collections.Generic.HashSet<int>), _integerHashSet.GetType());
        }

        [Test]
        public void ShouldReturnFalseWhenDuplicateValueAddIsAttempted()
        {
            _integerHashSet.Add(1);
            _integerHashSet.Add(44);
            bool duplicateTry = _integerHashSet.Add(1);

            Assert.That(duplicateTry, Is.False);
            Assert.That(_integerHashSet.Count, Is.EqualTo(2));
            Assert.That(_integerHashSet.ToList<int>()[1], Is.EqualTo(44));
        }

        [Test]
        public void ShouldNotNeedToCastRetrievedValueBackToOriginalType()
        {
            // This is practically a moot point considering you need a typed list to start from, but, here's the example anyway.
            _integerHashSet.Add(9009);
            Assert.AreEqual(9009, _integerHashSet.ToList<int>()[0]);
        }
    }
}
