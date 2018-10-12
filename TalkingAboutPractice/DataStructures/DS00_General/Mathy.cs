using System;
using System.Security.Principal;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS00_General
{
    [TestFixture]
    public class Mathy
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * Mathiness to be mindful of.
            */
        }

        [TestFixture]
        public class Overflow
        {
            [Test]
            public void ShouldDemonstrateThatArithmeticOperationWithVariableWillNotRaiseCompilerOrRunTimeOverflowError()
            {
                int maxIntegerValue = int.MaxValue; // maximum integer value is 2,147,483,647
                int minIntegerValue = int.MinValue; // minimum integer value is -2,147,483,648
                int overflowShift = 10;
                int minIntegerValuePlus10 = minIntegerValue + overflowShift - 1;

                // This will overflow at run-time (generating an unexpectedly negative value), but will not
                // throw an error, as expressions that contain non-constant terms (like the two variables here)
                // are "unchecked" by default.
                int addItUp = maxIntegerValue + overflowShift;

                Assert.That(addItUp, Is.EqualTo(minIntegerValuePlus10));
            }

            [Test]
            public void ShouldDemonstrateThatArithmeticOperationWithOnlyConstantValuesWillRaiseCompilerError()
            {
                const int overflowShift = 10;

                // This line will not compile, as operations with only constant values are "checked" by default
                //int addItUp = int.MaxValue + overflowShift;
            }

            [Test]
            public void ShouldDemonstrateExplicitlyCheckedEnvironment()
            {
                bool isException = false;
                int overflowShift = 10;

                try
                {
                    // The checked block ensures that the overflow will raise a run-time error, instead of generating an unexpected value
                    checked
                    {
                        int addItUp = int.MaxValue + overflowShift;
                    }
                }
                catch(OverflowException exception)
                {
                    isException = true;
                }

                Assert.That(isException, Is.True);
            }

            [Test]
            public void ShouldDemonstrateExplicitlyUncheckedEnvironment()
            {
                const int overflowShift = 10;
                int minIntegerValuePlus10 = int.MinValue + overflowShift - 1;
                int iWouldNormallyOverflow;

                // The unchecked block ensures that the overflow will not raise a compiler error, or run-time error, instead generating a (potentially) unexpected value
                unchecked
                {
                    iWouldNormallyOverflow = int.MaxValue + overflowShift;
                }

                Assert.That(iWouldNormallyOverflow, Is.EqualTo(minIntegerValuePlus10));
            }
        }

    }
}