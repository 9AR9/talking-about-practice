using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.GangOfFourDesignPatterns.Creational
{
    [TestFixture]
    public class BuilderPatternStructural
    {
        /*
         * Builder: Separates object construction from its representation
         * (source: http://www.dofactory.com/net/builder-design-pattern)
         * 
         * Definition:
         * Separate the construction of a complex object from its representation so that the same
         * construction process can create different representations.
         * 
         * Frequency of use: 2/5 (Medium low)
         * 
         * UML: See BuilderUml.gif  
         * 
         * Participants (The classes and objects participating in this pattern):
         *      - Builder (VehicleBuilder):
         *           - specifies an abstract interface for creating parts of a Product object
         *      - ConcreteBuilder (MotorcycleBuilder, CarBuilder, ScooterBuilder):
         *           - constructs and assembles parts of the product by implementing the Builder interface
         *           - defines and keeps track of the representation it creates
         *           - provides an interface for retrieving the product
         *      - Director (Shop):
         *           - constructs an object using the Builder interface
         *      - Product (Vehicle):
         *           - represents the complex object under construction. ConcreteBuilder builds the product's
         *             internal representation and defines the process by which it's assembled
         *           - includes classes that define the constituent parts, including interfaces for assembling
         *             the parts into the final result
        */


        /*
         * Structural code:
         * 
         * This structural code demonstrates the Builder pattern in which complex objects are created in a
         * step-by-step fashion. The construction process can create different object representations and
         * provides a high level of control over the assembly of the objects. 
        */

        /// <summary>
        /// The 'Director' class
        /// </summary>
        class Director
        {
            // Builder uses a complex series of steps
            public void Construct(Builder builder)
            {
                builder.BuildPartA();
                builder.BuildPartB();
            }
        }

        /// <summary>
        /// The 'Builder' abstract class
        /// </summary>
        abstract class Builder
        {
            public abstract void BuildPartA();
            public abstract void BuildPartB();
            public abstract Product GetResult();
        }

        /// <summary>
        /// The 'ConcreteBuilder1' class
        /// </summary>
        class ConcreteBuilder1 : Builder
        {
            private readonly Product _product = new Product();

            public override void BuildPartA()
            {
                _product.Add("PartA");
            }

            public override void BuildPartB()
            {
                _product.Add("PartB");
            }

            public override Product GetResult()
            {
                return _product;
            }
        }

        /// <summary>
        /// The 'ConcreteBuilder2' class
        /// </summary>
        class ConcreteBuilder2 : Builder
        {
            private readonly Product _product = new Product();

            public override void BuildPartA()
            {
                _product.Add("PartX");
            }

            public override void BuildPartB()
            {
                _product.Add("PartY");
            }

            public override Product GetResult()
            {
                return _product;
            }
        }

        /// <summary>
        /// The 'Product' class
        /// </summary>
        class Product
        {
            private List<string> _parts = new List<string>();

            public void Add(string part)
            {
                _parts.Add(part);
            }

            public void Show()
            {
                Console.WriteLine("\nProduct Parts -------");
                foreach (string part in _parts)
                    Console.WriteLine(part);
            }
        }

        [Test]
        public void ShouldConstructTwoDifferentProductsUsingBuildersWithCommonBaseAbstraction()
        {
            var consoleOutput = new ConsoleOutput();
            Director director = new Director();
            Builder builder1 = new ConcreteBuilder1();
            Builder builder2 = new ConcreteBuilder2();

            director.Construct(builder1);
            Product product1 = builder1.GetResult();
            product1.Show();

            director.Construct(builder2);
            Product product2 = builder2.GetResult();
            product2.Show();

            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("\nProduct Parts -------"));
            Assert.That(outputLines[1], Is.EqualTo("PartA"));
            Assert.That(outputLines[2], Is.EqualTo("PartB"));
            Assert.That(outputLines[3], Is.EqualTo("\nProduct Parts -------"));
            Assert.That(outputLines[4], Is.EqualTo("PartX"));
            Assert.That(outputLines[5], Is.EqualTo("PartY"));
        }

    }

    public class BuilderPatternRealWorld
    {
        /*
         * Real-world code:
         * 
         * This real-world code demonstates the Builder pattern in which different vehicles are assembled in a
         * step-by-step fashion. The Shop uses VehicleBuilders to construct a variety of Vehicles in a series
         * of sequential steps. 
        */

        class Shop
        {
            // Builder uses a complex series of steps
            public void Construct(VehicleBuilder vehicleBuilder)
            {
                vehicleBuilder.BuildFrame();
                vehicleBuilder.BuildEngine();
                vehicleBuilder.BuildWheels();
                vehicleBuilder.BuildDoors();
            }
        }

        /// <summary>
        /// The 'Builder' abstract class
        /// </summary>
        abstract class VehicleBuilder
        {
            // Gets vehicle instance
            public Vehicle Vehicle { get; protected set; }

            // Abstract build methods
            public abstract void BuildFrame();
            public abstract void BuildEngine();
            public abstract void BuildWheels();
            public abstract void BuildDoors();
        }

        /// <summary>
        /// The 'ConcreteBuilder1' class
        /// </summary>
        class MotorcycleBuilder : VehicleBuilder
        {
            public MotorcycleBuilder()
            {
                Vehicle = new Vehicle("Motorcycle");
            }

            public override void BuildFrame()
            {
                Vehicle["frame"] = "Motorcycle Frame";
            }

            public override void BuildEngine()
            {
                Vehicle["engine"] = "500 cc";
            }

            public override void BuildWheels()
            {
                Vehicle["wheels"] = "2";
            }

            public override void BuildDoors()
            {
                Vehicle["doors"] = "0";
            }
        }


        /// <summary>
        /// The 'ConcreteBuilder2' class
        /// </summary>
        class CarBuilder : VehicleBuilder
        {
            public CarBuilder()
            {
                Vehicle = new Vehicle("Car");
            }

            public override void BuildFrame()
            {
                Vehicle["frame"] = "Car Frame";
            }

            public override void BuildEngine()
            {
                Vehicle["engine"] = "2500 cc";
            }

            public override void BuildWheels()
            {
                Vehicle["wheels"] = "4";
            }

            public override void BuildDoors()
            {
                Vehicle["doors"] = "4";
            }
        }

        /// <summary>
        /// The 'ConcreteBuilder3' class
        /// </summary>
        class ScooterBuilder : VehicleBuilder
        {
            public ScooterBuilder()
            {
                Vehicle = new Vehicle("Scooter");
            }

            public override void BuildFrame()
            {
                Vehicle["frame"] = "Scooter Frame";
            }

            public override void BuildEngine()
            {
                Vehicle["engine"] = "50 cc";
            }

            public override void BuildWheels()
            {
                Vehicle["wheels"] = "2";
            }

            public override void BuildDoors()
            {
                Vehicle["doors"] = "0";
            }
        }

        /// <summary>
        /// The 'Product' class
        /// </summary>
        class Vehicle
        {
            private readonly string _vehicleType;
            private readonly Dictionary<string, string> _parts = new Dictionary<string, string>();

            // Constructor
            public Vehicle(string vehicleType)
            {
                this._vehicleType = vehicleType;
            }

            // Indexer property - allows for dictionary values to be set or retreieved using descriptive keys
            public string this[string key]
            {
                get { return _parts[key]; }
                set { _parts[key] = value; }
            }

            public void Show()
            {
                Console.WriteLine("\n---------------------------");
                Console.WriteLine("Vehicle Type: {0}", _vehicleType);
                Console.WriteLine(" Frame  : {0}", _parts["frame"]);
                Console.WriteLine(" Engine : {0}", _parts["engine"]);
                Console.WriteLine(" #Wheels: {0}", _parts["wheels"]);
                Console.WriteLine(" #Doors : {0}", _parts["doors"]);
            }
        }

        [Test]
        public void ShouldConstructThreeDifferentTypesOfAutomobileUsingBuildersWithCommonBaseAbstraction()
        {
            var consoleOutput = new ConsoleOutput();
            Shop shop = new Shop();
            MotorcycleBuilder motorcycleBuilder = new MotorcycleBuilder();
            CarBuilder carBuilder = new CarBuilder();
            ScooterBuilder scooterBuilder = new ScooterBuilder();

            shop.Construct(scooterBuilder);
            scooterBuilder.Vehicle.Show();
            shop.Construct(carBuilder);
            carBuilder.Vehicle.Show();
            shop.Construct(motorcycleBuilder);
            motorcycleBuilder.Vehicle.Show();

            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("\n---------------------------"));
            Assert.That(outputLines[1], Is.EqualTo("Vehicle Type: Scooter"));
            Assert.That(outputLines[2], Is.EqualTo(" Frame  : Scooter Frame"));
            Assert.That(outputLines[3], Is.EqualTo(" Engine : 50 cc"));
            Assert.That(outputLines[4], Is.EqualTo(" #Wheels: 2"));
            Assert.That(outputLines[5], Is.EqualTo(" #Doors : 0"));
            Assert.That(outputLines[6], Is.EqualTo("\n---------------------------"));
            Assert.That(outputLines[7], Is.EqualTo("Vehicle Type: Car"));
            Assert.That(outputLines[8], Is.EqualTo(" Frame  : Car Frame"));
            Assert.That(outputLines[9], Is.EqualTo(" Engine : 2500 cc"));
            Assert.That(outputLines[10], Is.EqualTo(" #Wheels: 4"));
            Assert.That(outputLines[11], Is.EqualTo(" #Doors : 4"));
            Assert.That(outputLines[12], Is.EqualTo("\n---------------------------"));
            Assert.That(outputLines[13], Is.EqualTo("Vehicle Type: Motorcycle"));
            Assert.That(outputLines[14], Is.EqualTo(" Frame  : Motorcycle Frame"));
            Assert.That(outputLines[15], Is.EqualTo(" Engine : 500 cc"));
            Assert.That(outputLines[16], Is.EqualTo(" #Wheels: 2"));
            Assert.That(outputLines[17], Is.EqualTo(" #Doors : 0"));
        }
    }

}