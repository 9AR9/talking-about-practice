using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.GangOfFourDesignPatterns.Creational
{
    [TestFixture]
    public class PrototypePatternStructural
    {
        /*
         * Prototype: A fully inititalized instance to be copied or cloned
         * (source: http://www.dofactory.com/net/prototype-design-pattern)
         * 
         * Definition:
         * Define an interface for creating an object, but let subclasses decide which class to instantiate.
         * Factory Method lets a class defer instantiation to subclasses.
         * 
         * Frequency of use: 3/5 (Medium)
         * 
         * UML: See PrototypeUml.gif
         * 
         * Participants (The classes and objects participating in this pattern):
         *      - Prototype (ColorPrototype):
         *           - declares an interface for cloning itself
         *      - ConcretePrototype (Color):
         *           - implements an operation for cloning itself
         *      - Client (ColorManager):
         *           - creates a new object by asking a prototype to clone itself
        */


        /*
         * Structural code:
         * 
         * This structural code demonstrates the Prototype pattern in which new objects are created by
         * copying pre-existing objects (prototypes) of the same class. 
        */


        /// <summary>
        /// A simple reference type to help demonstrate the same-reference nature of shallow copy with reference types
        /// </summary>
        public class RefType
        {
            public int Integer { get; set; }
        }

        /// <summary>
        /// The 'Prototype' abstract class
        /// </summary>
        abstract class Prototype
        {
            private string _id;
            private string _thing;
            private RefType _refType;

            // Constructor
            public Prototype(string id)
            {
                this._id = id;
            }

            // Gets id
            public string Id
            {
                get { return _id; }
            }

            // Gets and sets a second value type property, to help demonstrate the copied nature of shallow copy with value types
            public string Thing
            {
                get { return _thing; } set { _thing = value; }
            }

            // Gets and sets a third property, this time a reference type, to help demonstrate the same-reference nature of shallow copy with reference types
            public RefType RefType
            {
                get { return _refType; } set { _refType = value; }
            }

            public abstract Prototype Clone();
        }

        /// <summary>
        /// A 'ConcretePrototype' class 
        /// </summary>
        class ConcretePrototype1 : Prototype
        {
            // Constructor
            public ConcretePrototype1(string id) : base(id)
            {
            }

            // Returns a shallow copy (a reference to the same object)
            public override Prototype Clone()
            {
                return (Prototype)this.MemberwiseClone();
            }
        }

        /// <summary>
        /// A 'ConcretePrototype' class 
        /// </summary>
        class ConcretePrototype2 : Prototype
        {
            // Constructor
            public ConcretePrototype2(string id) : base(id)
            {
            }

            // Returns a shallow copy (a reference to the same object)
            public override Prototype Clone()
            {
                return (Prototype)this.MemberwiseClone();
            }
        }

        [Test]
        public void ShouldPerformShallowCopyOfObjectsAndObserveDifferencesInValueAndReferenceTypeCopying()
        {
            var consoleOutput = new ConsoleOutput();
            ConcretePrototype1 prototype1 = new ConcretePrototype1("I");
            prototype1.Thing = "starter value";
            prototype1.RefType = new RefType() { Integer = 9 };
            ConcretePrototype1 clone1 = (ConcretePrototype1)prototype1.Clone();
            Console.WriteLine("Cloned: {0}", clone1.Id);

            ConcretePrototype2 prototype2 = new ConcretePrototype2("II");
            ConcretePrototype2 clone2 = (ConcretePrototype2)prototype2.Clone();
            Console.WriteLine("Cloned: {0}", clone2.Id);

            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("Cloned: I"));
            Assert.That(outputLines[1], Is.EqualTo("Cloned: II"));

            prototype1.Thing = "Hey, I changed the thing on the first prototype!";
            prototype1.RefType.Integer = 99;
            
            Assert.That(clone1.Thing, Is.EqualTo("starter value")); // The clone does not contain the original's value type change, as value types are copied bit-by-bit in a shallow copy
            Assert.That(clone1.RefType.Integer, Is.EqualTo(99));    // But the clone DOES contain the original's reference type change, as ref types are a copy of the reference, not an actual copy of the object, in a shallow copy
        }

    }

    public class PrototypePatternRealWorld
    {
        /*
         * Real-world code:
         * 
         * This real-world code demonstrates the Prototype pattern in which new Color objects are created by copying pre-existing,
         * user-defined Colors of the same type. 
        */

        /// <summary>
        /// A simple reference type to help demonstrate the same-reference nature of shallow copy with reference types
        /// </summary>
        public class ColorDescriptor
        {
            public int IdCode { get; set; }
        }

        /// <summary>
        /// The 'Prototype' abstract class
        /// </summary>
        abstract class ColorPrototype
        {
            public int FavorabilityRank { get; set; }       // Value type property, to help demonstrate the copied nature of shallow copy with value types
            public ColorDescriptor Descriptor { get; set; } // Reference type property, to help demonstrate the same-reference nature of shallow copy with reference types
            public abstract ColorPrototype Clone();
        }

        /// <summary>
        /// The 'ConcretePrototype' class
        /// </summary>
        class Color : ColorPrototype
        {
            private int _red;
            private int _green;
            private int _blue;

            // Constructor
            public Color(int red, int green, int blue)
            {
                this._red = red;
                this._green = green;
                this._blue = blue;
            }

            // Create a shallow copy
            public override ColorPrototype Clone()
            {
                Console.WriteLine("Cloning color RGB: {0,3},{1,3},{2,3}", _red, _green, _blue);

                return this.MemberwiseClone() as ColorPrototype;
            }
        }

        /// <summary>
        /// Prototype manager
        /// </summary>
        class ColorManager
        {
            private Dictionary<string, ColorPrototype> _colors = new Dictionary<string, ColorPrototype>();

            // Indexer
            public ColorPrototype this[string key]
            {
                get { return _colors[key]; }
                set { _colors.Add(key, value); }
            }
        }

        public void ShouldPerformShallowCopyOfObjectsAndObserveDifferencesInValueAndReferenceTypeCopying()
        {
            var consoleOutput = new ConsoleOutput();
            ColorManager manager = new ColorManager
            {
                ["red"] = new Color(255, 0, 0),
                ["blue"] = new Color(0, 255, 0),
                ["green"] = new Color(0, 0, 255),
                ["angry"] = new Color(255, 54, 0),
                ["peace"] = new Color(128, 211, 128)
            };
            Color flameColor = new Color(211, 34, 20);
            flameColor.Descriptor = new ColorDescriptor() {IdCode = 666666};
            flameColor.FavorabilityRank = 10;
            manager["flame"] = flameColor;
            Color colorClone1 = manager["red"].Clone() as Color;
            Color colorClone2 = manager["peace"].Clone() as Color;
            Color colorClone3 = manager["flame"].Clone() as Color;
            
            string[] outputLines = consoleOutput.GetOutputLines();
            Assert.That(outputLines[0], Is.EqualTo("Cloning color RGB: 255,  0,  0")); // Color codes, all value types (integers), have been copied
            Assert.That(outputLines[1], Is.EqualTo("Cloning color RGB: 128,211,128"));
            Assert.That(outputLines[2], Is.EqualTo("Cloning color RGB: 211, 34, 20"));

            manager["red"].FavorabilityRank = 6;
            manager["red"].Descriptor.IdCode = 55555;

            Assert.That(colorClone1.FavorabilityRank, Is.EqualTo(10));      // The clone does not contain the original's value type change, as value types are copied bit-by-bit in a shallow copy
            Assert.That(colorClone1.Descriptor.IdCode, Is.EqualTo(55555));  // But the clone DOES contain the original's reference type change, as ref types are a copy of the reference, not an actual copy of the object, in a shallow copy
        }
    }

}