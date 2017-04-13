using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS08_Stacks
{
    [TestFixture]
    public class Stacks
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * The Stack class is one of may C# data structures that resembles a List. Like List, it has adding and getting
               methods, with a slight difference in behavior.
             * The focus with a Stack is always on the "top" object.
                    - The Push() method is used to add to the Stack (equivalent to the Add() method of List).
                    - The Pop() method with both remove and return the last object added (i.e. what is current top of the stack).
                    - The Peek() method will only return the value of the last object added, but not remove it from the stack.
                    - The resulting behavior is called LIFO (last in, first out). This particular data structure is helpful
                      when you need to retrace your steps, so-to-speak.
             * There are two formats to define a Stack.
                    - [[Stack stack = new Stack();]] :: works with any object type
                    - [[Stack<string> stack = new Stack<string>();]] :: works with a specific type only
            */
        }

        [Test]
        public void ShouldStackItemsOfSpecifiedType()
        {
            Stack<string> stack = new Stack<string>();

            stack.Push("stack");
            stack.Push("down");
            stack.Push("moving");
            string stackPath = string.Empty;
            while (stack.Count > 0)
            {
                stackPath += stack.Pop();
                if (stack.Count > 0) stackPath += " ";
            }
            Assert.AreEqual("moving down stack", stackPath);
        }

        [Test]
        public void ShouldStackItemsOfMultipleTypes()
        {
            Stack stack = new Stack();

            stack.Push("stack");
            stack.Push(666);
            stack.Push(new Form());
            stack.Push("moving");
            string stackPath = string.Empty;
            while (stack.Count > 0)
            {
                stackPath += stack.Pop();
                if (stack.Count > 0) stackPath += " ";
            }
            Assert.AreEqual("moving System.Windows.Forms.Form, Text:  666 stack", stackPath);
        }
    }
}