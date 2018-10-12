using System;
using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS10_Strings
{
    [TestFixture]
    public class Strings
    {
        [SetUp]
        public void Initialize()
        {
            /*
             * C# strings are text values that are stored internally as a sequential read-only collection of Char
             * objects. There is no null-terminating character at the end of a C# string; therefore a C# string can
             * contain any number of embedded null characters ('\0'). The Length property of a string represents
             * the number of Char objects it contains (not the number of unicode characters). Individual characters
             * of a string can be referenced using an index on the string variable.
             * 
             * If initialized without a value, a string will carry a value of null, though you will cause build errors
             * by trying to reference an unassigned local variable. You can manually assign it to null (string s = null)
             * to avoid that error, but it is more common to assign to String.Empty (or ""), which represents an empty
             * string with zero characters in it, and reduces the chances of a NullReferenceException occuring.
             * 
             * C# strings values are wrapped in double-quotes, while single-quotes are used to wrap character data.
             * 
             * C# strings are immutable - they cannot be changed after they are created. Methods that appear to modify
             * them actually return the results in a new string object. Because of this, references to a string will
             * contintue to represent the original value they were assigned, and will not change when the original string
             * is modified. Also, string concatenation can become costly when done repeatedly, and the StringBuilder
             * object should be employed for large scale string concatenation. StringBuilder also allows you to reassign
             * individual characters of a string value, which is something the built-in string data type does not support.
             * 
             * Escape characters in strings include a backslash character in front of them to denote the special character
             * status, when using regular string literals. However, there is also the verbatim string notation, which
             * places an @ character before the string, and then allows you to use backslashes as normal. [In this case,
             * the only escape character is two double-quotes to represent a single double-quote in the final rendered
             * string.] Verbatim strings are good for things like file paths that use lots of backslashes, and also for
             * multi-line strings, as verbatim strings also preserve new line characters.
             * 
             * Format strings can be used to dynamically fill data at runtime. The String.Format method provides this
             * functionality by embedding placeholders in curly braces that will be replaced by other values at runtime.
             * 
             * The Substring method can return a portion of a string by taking in the starting index for the substring,
             * and the number of characters to include in the substring (starting from the starting index moving right).
             * 
             * The IndexOf method can be used to return the zero-based index of the first location of a character, or
             * substring, within a string. It has overloads for also specifying the starting index to search from, as
             * well as specifying both the starting and ending index to search within.
             * 
             * The Replace method can be used to replace one substring within a string with a new substring.
             * 
            */
        }

        [Test]
        public void ShouldManuallyInitializeToEmptyString()
        {
            string s = String.Empty;

            Assert.That(s.Length, Is.EqualTo(0));
        }

        [Test]
        public void ShouldManuallyInitializeToNullEvenThoughThisIsNotRecommended()
        {
            string s = null;

            Assert.That(String.IsNullOrEmpty(s), Is.True);
        }

        [Test]
        public void ShouldNotModifyReferencedStringWhichDemonstratesImmutability()
        {
            string one = "tennis";
            string two = one;

            one += " ball";

            Assert.That(one, Is.EqualTo("tennis ball"));
            Assert.That(two, Is.EqualTo("tennis"));
        }

        [Test]
        public void ShouldAllowMultipleNullCharacters()
        {
            string s = "\0\0\0\0X";

            Assert.That(s.Length, Is.EqualTo(5));
            Assert.That(s[0], Is.EqualTo('\0'));
            Assert.That(s[0], Is.EqualTo('\u0000'));
            Assert.That(s[4], Is.EqualTo('X'));
        }

        [Test]
        public void ShouldRetainBackslashesAndNewLineCharactersAndSpacesInVerbatimStrings()
        {
            string regularString = "Line1\r\nLine2\r\nLine3";
            string verbatimString = @"What\the\heck
                        is going on?";

            Assert.That(regularString, Is.EqualTo("Line1\r\nLine2\r\nLine3"));
            Assert.That(verbatimString, Is.EqualTo("What\\the\\heck\r\n                        is going on?"));
        }

        [Test]
        public void ShouldFormatStringAtRuntime()
        {
            var car = "Mini Cooper S";
            var color = "black";

            var description = $"My whip is a {color} {car} and it is sweet.";
            
            Assert.That(description, Is.EqualTo("My whip is a black Mini Cooper S and it is sweet."));
        }

        [Test]
        public void ShouldFindFirstIndexOfSubstringAndCharacter()
        {
            string thing = "Dog cat jukebox cat jewel";
            int indexOfSubstring = thing.IndexOf("cat", StringComparison.Ordinal);
            int indexOfCharacter = thing.IndexOf('j');

            Assert.That(indexOfSubstring, Is.EqualTo(4));
            Assert.That(indexOfCharacter, Is.EqualTo(8));
        }

        [Test]
        public void ShouldReplaceSubstring()
        {
            string statement = "I sure do like mushrooms.";

            string modifiedStatement = statement.Replace("like", "hate");

            Assert.That(statement, Is.EqualTo("I sure do like mushrooms."));
            Assert.That(modifiedStatement, Is.EqualTo("I sure do hate mushrooms."));
        }

        [Test]
        public void ShouldUseStringBuilderForRepeatedStringConcatenationsAndCharacterReplacements()
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 1; i < 11; i++)
            {
                sb.Append(i);
            }

            sb[8] = 'N';

            Assert.That(sb.ToString(), Is.EqualTo("12345678N10"));
        }

        [Test]
        public void ShouldCreateStringOfRepeatedChars()
        {
            string repeated = new string('q', 17);

            Assert.That(repeated, Is.EqualTo("qqqqqqqqqqqqqqqqq"));
        }

        [Test]
        public void ShouldSplitStringWithCharDelimiter()
        {
            string derp = "For your health.";
            string[] words = derp.Split(' ');

            Assert.That(words[0], Is.EqualTo("For"));
            Assert.That(words[1], Is.EqualTo("your"));
            Assert.That(words[2], Is.EqualTo("health."));
        }

        [Test]
        public void ShouldSplitStringUsingCustomStringDelimiters()
        {
            string stuff = "dingdangdoo\r\ndiddle";
            string[] delims = { "dang", "wow", "\r\n" };
            string[] splits = stuff.Split(delims, StringSplitOptions.None);

            Assert.That(splits[0], Is.EqualTo("ding"));
            Assert.That(splits[1], Is.EqualTo("doo"));
            Assert.That(splits[2], Is.EqualTo("diddle"));
        }

        [Test]
        public void ShouldSplitStringUsingCustomCharDelimiters()
        {
            string stuff = "tweextwerpytangle369";
            char[] delims = { 'x', 'y', '3' };
            string[] splits = stuff.Split(delims, StringSplitOptions.None);

            Assert.That(splits[0], Is.EqualTo("twee"));
            Assert.That(splits[1], Is.EqualTo("twerp"));
            Assert.That(splits[2], Is.EqualTo("tangle"));
            Assert.That(splits[3], Is.EqualTo("69"));
        }
    }
}