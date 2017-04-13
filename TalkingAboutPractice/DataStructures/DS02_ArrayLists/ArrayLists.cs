using System.Collections;
using System.Windows.Forms;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS02_ArrayLists
{
    [TestFixture]
    public class ArrayLists
    {
        ArrayList _myArrayList;

        [SetUp]
        public void Initialize()
        {
            /*
             * The C# ArrayList is a dynamic array, allowing for any amount of objects and of any type.
             * It was designed and created to simplify the process of adding new elements into an array.
             * Under the hood, it is an array that is doubled in size every time it runs out of space, while
               still providing O(1) access.
                    - This is an effective strategy that reduces the amount of element-copying in the long run.
             * The downside of ArrayList is one must cast the retrieved values back into their original type.
             * System.Collections.ArrayList
            */

            _myArrayList = new ArrayList();
        }

        [Test]
        public void ShouldAllowDynamicLoadingOfAnyValueAndType()
        {
            var form = new Form();

            _myArrayList.Add(56);
            _myArrayList.Add("String");
            _myArrayList.Add(form);
            Assert.AreEqual(56, _myArrayList[0]);
            Assert.AreEqual("String", _myArrayList[1]);
            Assert.AreEqual(form, _myArrayList[2]);

            Assert.AreEqual(typeof(System.Collections.ArrayList), _myArrayList.GetType());
        }

        [Test]
        public void ShouldNeedToCastRetrievedValueBackToOriginalType()
        {
            _myArrayList.Add(9);
            Assert.AreEqual(9, (int)_myArrayList[0]);
        }
    }
}