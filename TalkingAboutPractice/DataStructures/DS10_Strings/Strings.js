/*
    A JavaScript string simply stores a series of characters. It can hold any text, and is declared with
    quotes, either single or double. You can embed quotes into strings so long as they don't match the
    quotes surrounding the string; when they do match, you'll need to use the escape character backslash
    (\) to denote them.

    The built-in length property tells us how many characters are in the string.

    Normally, JavaScript strings are primitive values, created from literals (i.e. var s = "word";).
    However, strings can also be created with the new keyword (i.e. var s = new String("word");).
    The new keyword SHOULD NOT BE USED for strings, as it slows down execution speed, complicates
    the code, and can produce unexpected results.

    Equality between two string values can be found using ==, but this comparison only looks for equality
    in values, using runtime conversion when necessary before comparing the values. The better equality
    check uses the === operator, which will require equality in both value AND type in order to generate a
    true value. However, two objects will never be true when using ===, even if their values are the same.
    (Another reason not to use the new keyword for primitive data types.)

    Normally, primitive values cannot have properties or methods, but with JavaScript, methods and properties
    are also available to primitive values, because JavaScript treats primitive values as objects when
    executing methods and properties. The length property is a good example. The indexOf() method returns the
    index of the first occurance of a substring, while lastIndexOf() returns the last occurance. Three
    methods exist for extracting parts of a string: slice(start, end), substring(start, end) and
    substr(start, length). Slice and substring are very similar, the difference being that slice can use
    negative indexes to count backward from the end of the string. The substr method is also similar to
    slice, only the second parameter represents the length of the substring instead of the end index.
*/

describe('Strings', function () {

    it('should allow quotes within quotes', function() {
        var one = "Don't trip.";
        var two = 'Don\'t trip.';
        var three = 'Do not say "no".';
        var four = "Do not say \"no\".";

        expect(one).toEqual(two);
        expect(three).toEqual(four);
    });

    it('should demonstrate equality (or lack thereof)', function () {
        var a = "Space Ghost";
        var b = new String("Space Ghost");

        expect(a == b).toBeTruthy();
        expect(a === b).toBeFalsy();

        var x = "Lemmy";
        var y = x;

        expect(x == y).toBeTruthy();
        expect(x === y).toBeTruthy();

        var p = new String("toast");
        var q = new String("toast");

        expect(p == q).toEqual(false);
        expect(p === q).toEqual(false);
    });

    it('should report length', function () {
        var a = "Space Ghost";

        expect(a.length).toEqual(11);
    });

    it('should find indexOf', function () {
        var a = "Space Ghost Coast To Coast";

        expect(a.indexOf('Coast')).toEqual(12);
        expect(a.lastIndexOf('Coast')).toEqual(21);
        expect(a.indexOf('toast')).toEqual(-1);
    });

    it('should slice a string', function () {
        var a = "Apple, Banana, Kiwi";

        expect(a.slice(7, 13)).toEqual("Banana");
        expect(a.substring(7, 13)).toEqual("Banana");
        expect(a.slice(7)).toEqual("Banana, Kiwi");
        expect(a.substring(7)).toEqual("Banana, Kiwi");
        expect(a.slice(-12, -6)).toEqual("Banana");
        expect(a.slice(-12)).toEqual("Banana, Kiwi");

        expect(a.substr(7, 6)).toEqual("Banana");
        expect(a.substr(7)).toEqual("Banana, Kiwi");
        expect(a.substr(-12, 6)).toEqual("Banana");
        expect(a.substr(-12)).toEqual("Banana, Kiwi");
    });

    it('should find a character from a string', function () {
        var a = "Hey, dude";

        expect(a.charAt(0)).toEqual('H');
        expect(a.charCodeAt(0)).toEqual(72);
    });

    it('should convert string to character array using split', function () {
        var a = "a,b,c,d,e";
        var allChars = a.split("");
        var justLetters = a.split(",");

        expect(typeof allChars).toEqual("object");
        expect(typeof justLetters).toEqual("object");
        expect(allChars[1]).toEqual(',');
        expect(justLetters[1]).toEqual('b');
    });

});