using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS00_General
{
    [TestFixture]
    public class ValueVsReference
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * Value Types vs. Reference Types
             
             * Value Types:
                    - Derive from System.ValueType
                    - Variables based on Value Types directly contain values. Assigning one Value Type variable to another
                      copies the contained value (as opposed to a reference to the object, like Reference Types).
                    - Two main categories: Structs (numeric types, bool, user defined structs) and Enumerations.
                    - Declarations: bool, byte, char, decimal, double, enum, float, int, long, sbyte, short, struct, uint, ulong, ushort
             
             * Reference Types:
                    - Inherit from System.Object (except of course for "object" which actually is the System.Object object)
                    - Variables based on Reference Types are actually references to objects created in memory. Assigning one
                      Reference Type variable to another creates a reference to the same object in memory (as opposed to a copy
                      of the value like with Value Types).
                    - Declarations: class, interface, delegate, object, string
            */
        }

        public class MyInt
        {
            // This is a simple class representing a Reference Type, which contains a Value Type member
            public int MyValue;
        }

        [Test]
        public void ValueTypeShouldCopyAndNotReference()
        {
            int x = new int();
            x = 7;

            int y = new int();
            y = x;
            y = 8;

            Assert.AreEqual(7, x); // value of x is not affected by change in y; x is not y
        }


        [Test]
        public void ReferenceTypeShouldReferenceSameValueAndNotCopy()
        {
            MyInt p = new MyInt();
            p.MyValue = 9;

            MyInt q = new MyInt();
            q = p;
            q.MyValue = 10;

            Assert.AreEqual(10, p.MyValue); // p's value IS affected by change in q; p and q are references to the same object
        }
    }
}