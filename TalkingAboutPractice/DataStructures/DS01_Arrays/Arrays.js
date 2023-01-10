/*
    A JavaScript array is a special variable which can hold more than one variable at a time, 
    which you can access by referencing an index number.

    Unlike arrays in typed languages like C#, a JavaScript array can hold values of different types.
    It can even hold objects, functions, or other arrays as a value. Also unlike C# arrays, a JavaScript
    array does not have a fixed size, and can be added to on-the-fly, due to JavaScript's dynamic,
    uncompiled nature. The push method can be used to add a value to the end of an array, as can
    using the index of the current length. However, adding using higher numbers can leave
    unidentified "holes" in the array, which will carry a value of undefined.

    Using an array literal is the easiest way to create an array (i.e. var array_name = [1, 2, 3];).
    The new keyword can be used to instantiate an array as well, but there is no real need to
    do that, so for simplicity, readability and execution speed, always use the literal notation.

    Arrays are a special type of object in JavaScript. The typeof operator will return "object" for
    an array, but they are still best described as an array. Arrays use a numeric index to access
    the elements, while standard objects use names to access the elements.

    Associative arrays, like C# Hashtable and Dictionary types, where named indexes can be used
    instead of numeric ones, are NOT supported by JavaScript. As an alternative, simple objects
    can be used to add as many name/value pairs as desired, and then referenced via the named index.
*/

describe("Arrays", function () {
  var it = window.it;
  var expect = window.expect;

  it("should allow indexing by integer with array literal", function () {
    var integers = [7, 8, 9];
    var strings = ["Yes", "No", "Probably"];

    expect(typeof integers).toEqual("object");
    expect(typeof strings).toEqual("object");
    expect(integers[0]).toEqual(7);
    expect(strings[1]).toEqual("No");
  });

  it("should allow indexing by integer with array instantiated with new keyword", function () {
    var integers = new Array(7, 8, 9);
    var strings = new Array("Yes", "No", "Probably");

    expect(typeof integers).toEqual("object");
    expect(typeof strings).toEqual("object");
    expect(integers[0]).toEqual(7);
    expect(strings[1]).toEqual("No");
  });

  it("should demonstrate array compared to standard object", function () {
    var personArray = ["John", "Doe", 46];
    var personObject = { firstName: "John", lastName: "Doe", Age: 46 };

    expect(typeof personArray).toEqual("object");
    expect(typeof personObject).toEqual("object");
    expect(personArray[1]).toEqual("Doe");
    expect(personObject.lastName).toEqual("Doe");
    expect(personObject["lastName"]).toEqual("Doe");
  });

  it("should allow an object as an array member", function () {
    var array = ["A", "B", 1999];
    var personObject = { firstName: "John", lastName: "Doe", Age: 46 };
    var anotherArray = [1, "two"];
    array[3] = personObject;
    array[4] = anotherArray;

    expect(array.length).toEqual(5);
    expect(typeof array[3]).toEqual("object");
    expect(array[3]).toEqual(personObject);
    expect(array[3]).toEqual({ firstName: "John", lastName: "Doe", Age: 46 });
    expect(typeof array[4]).toEqual("object");
    expect(array[4]).toEqual(anotherArray);
    expect(array[4]).toEqual([1, "two"]);
  });

  it("should loop through an array using for loop", function () {
    var array = [1999, "A", "B"];
    var i,
      concatenation = "";

    for (i = 0; i < array.length; i++) {
      concatenation += array[i];
    }

    expect(concatenation).toEqual("1999AB");
  });

  it("should add values to the end of the array", function () {
    var ramones = ["Joey", "Johnny"];

    ramones.push("Dee Dee");
    ramones[ramones.length] = "Tommy";

    expect(ramones.length).toEqual(4);
    expect(ramones[2]).toEqual("Dee Dee");
    expect(ramones[3]).toEqual("Tommy");
  });

  it("should leave unexpected holes if adding using index higher than current length", function () {
    var cats = ["Sparky", "Toonces"];

    cats[cats.length + 2] = "Harold";

    expect(cats.length).toEqual(5);
    expect(cats[2]).toEqual(undefined);
    expect(cats[3]).toEqual(undefined);
    expect(cats[4]).toEqual("Harold");
  });

  it("should simulate associative array using an object", function () {
    var dictionary = [];
    dictionary.artist = "Me First And The Gimme Gimmes";
    dictionary.album = "Are A Drag";
    dictionary.songTitle = "Summertime";

    expect(dictionary["album"]).toEqual("Are A Drag");
    expect(dictionary.length).toEqual(0);
  });

  it("should demonstrate confusing issue with new Array()", function () {
    var wack = new Array(40);

    expect(wack.length).toEqual(40);
    expect(wack[17]).toBeUndefined();

    var moreThanOneIntegerParameterNowUsesNumbersAsValuesInsteadOfLength =
      new Array(40, 99);

    expect(
      moreThanOneIntegerParameterNowUsesNumbersAsValuesInsteadOfLength.length
    ).toEqual(2);
  });

  it("should use ECMAScript 5 isArray function to determine if object is an array, in modern browsers only", function () {
    var integers = [7, 8, 9];

    expect(typeof integers).toEqual("object");
    expect(Array.isArray(integers)).toEqual(true);
  });

  it("should use instanceof operator to determine if object is an array", function () {
    var integers = [7, 8, 9];
    var somethingElse = "cheese";

    expect(integers instanceof Array).toEqual(true);
    expect(somethingElse instanceof Array).toEqual(false);
  });

  it("should sort array of strings", function () {
    var strings = ["zebra", "flood", "arc", "tool"];

    strings.sort(); // Default sort behavior is alphabetical

    expect(strings).toEqual(["arc", "flood", "tool", "zebra"]);

    strings.reverse(); // Reversing the alphabetized string gives us reverse alphabetical order

    expect(strings).toEqual(["zebra", "tool", "flood", "arc"]);
  });

  it("should sort array of numbers", function () {
    var integers = [100, 7, 200, 9, 8];

    integers.sort(function (a, b) {
      return a - b;
    }); // A negative result means the first value is lower than the second for sort; results in numeric order

    expect(integers).toEqual([7, 8, 9, 100, 200]);

    integers.sort(function (a, b) {
      return b - a;
    }); // The opposite comparison gives us reverse numeric order

    expect(integers).toEqual([200, 100, 9, 8, 7]);
  });

  it("should sort array of objects", function () {
    var cars = [
      { type: "Volvo", year: 2016 },
      { type: "Saab", year: 2001 },
      { type: "BMW", year: 2010 },
    ];

    cars.sort(function (a, b) {
      return a.year - b.year;
    }); // We can sort based on a specific object property with same numeric approach

    expect(cars).toEqual([
      { type: "Saab", year: 2001 },
      { type: "BMW", year: 2010 },
      { type: "Volvo", year: 2016 },
    ]);

    cars.sort(function (a, b) {
      // Textual sort on an object property requires explicit same-cased comparison
      var x = a.type.toLowerCase();
      var y = b.type.toLowerCase();
      if (x < y) {
        return -1;
      } // Negative return value means a is lower than b in alphabetical order
      if (x > y) {
        return 1;
      } // Postitive return value means b is lower than a in alphabetical order
      return 0; // Zero return value means a and b are the same value
    });

    expect(cars).toEqual([
      { type: "BMW", year: 2010 },
      { type: "Saab", year: 2001 },
      { type: "Volvo", year: 2016 },
    ]);
  });
});
