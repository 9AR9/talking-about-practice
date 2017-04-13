using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.HackerRank.Tutorials.CrackingTheCodingInterview.StacksBalancedBrackets
{
    [TestFixture]
    public class Solution
    {
        public static string AreBracketsBalanced(string bracketmania)
        {
            Stack<char> stack = new Stack<char>();
            char[] characters = bracketmania.ToCharArray();
            char[] rightBracketTerminators = new char[] {')', '}', ']'};

            foreach (char bracket in characters)
            {
                if(stack.Count != 0 && BracketsMatch(bracket, stack.Peek()))
                {
                    stack.Pop();
                }
                else
                {
                    if (Array.IndexOf(rightBracketTerminators, bracket) > -1)
                        return "NO";

                    stack.Push(bracket);
                }
            }

            if (stack.Count == 0)
                return "YES";
            return "NO";
        }
        // As Stacks can be considered antiquated by some in favor of the ArrayList, this function
        // replaces the Stack for an ArrayList, with only slight variation in logic.
        public static string AreBracketsBalancedUsingArrayList(string bracketmania)
        {
            ArrayList stack = new ArrayList();
            char[] characters = bracketmania.ToCharArray();
            char[] rightBracketTerminators = new char[] { ')', '}', ']' };

            foreach (char bracket in characters)
            {
                if (stack.Count != 0 && BracketsMatch(bracket, (char)stack[stack.Count - 1]))
                {
                    stack.Remove(stack[stack.Count - 1]);
                }
                else
                {
                    if (Array.IndexOf(rightBracketTerminators, bracket) > -1)
                        return "NO";

                    stack.Add(bracket);
                }
            }

            if (stack.Count == 0)
                return "YES";
            return "NO";
        }

        public static bool BracketsMatch(char currentBracket, char topBracket)
        {
            string bracketPair = topBracket.ToString() + currentBracket;
            switch (bracketPair)
            {
                case "()":
                case "[]":
                case "{}":
                    return true;
                default:
                    return false;
            }
        }

        [Test]
        public void ShouldDetermineIfBracketsAreBalanced()
        {
            string line1 = "3";
            string line2 = "{[()]}";
            string line3 = "{[(])}";
            string line4 = "{{[[(())]]}}";

            Assert.That(AreBracketsBalanced(line2), Is.EqualTo("YES"));
            Assert.That(AreBracketsBalancedUsingArrayList(line2), Is.EqualTo("YES"));
            Assert.That(AreBracketsBalanced(line3), Is.EqualTo("NO"));
            Assert.That(AreBracketsBalancedUsingArrayList(line3), Is.EqualTo("NO"));
            Assert.That(AreBracketsBalanced(line4), Is.EqualTo("YES"));
            Assert.That(AreBracketsBalancedUsingArrayList(line4), Is.EqualTo("YES"));
        }
    }
}