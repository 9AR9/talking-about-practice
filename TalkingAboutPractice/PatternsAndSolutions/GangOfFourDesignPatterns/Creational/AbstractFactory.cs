using System;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.GangOfFourDesignPatterns.Creational
{
    [TestFixture]
    public class AbstractFactoryPatternStructural
    {
        /*
         * Abstract Factory: Creates an instance of several families of classes
         * (source: http://www.dofactory.com/net/abstract-factory-design-pattern)
         * 
         * Definition:
         * Provide an interface for creating families of related or dependent objects without specifying
         * their concrete classes.
         * 
         * Frequency of use: 5/5 (High)
         * 
         * UML: See AbstractFactoryUml.gif  
         * 
         * Participants (The classes and objects participating in this pattern):
         *      - AbstractFactory (ContinentFactory):
         *           - declares an interface for operations that create abstract products
         *      - ConcreteFactory (AfricaFactory, AmericaFactory):
         *           - implements the operations to create concrete product objects
         *      - AbstractProduct (Herbivore, Carnivore):
         *           - declares an interface for a type of product object
         *      - Product (Wildebeest, Lion, Bison, Wolf):
         *           - defines a product object to be created by the corresponding concrete factory 
         *           - implements the AbstractProduct interface
         *      - Client (AnimalWorld):
         *           - uses interfaces declared by AbstractFactory and AbstractProduct classes 
         *        
        */


        /*
         * Structural code:
         * 
         * This structural code demonstrates the Abstract Factory pattern creating parallel hierarchies of objects. Object creation
         * has been abstracted and there is no need for hard-coded class names in the client code, driven by a test following
         * the implementation.
        */

        /// <summary>
        /// The 'AbstractFactory' abstract class
        /// </summary>
        abstract class AbstractFactory
        {
            public abstract AbstractProductA CreateProductA();
            public abstract AbstractProductB CreateProductB();
        }

        /// <summary>
        /// The 'ConcreteFactory1' class
        /// </summary>
        class ConcreteFactory1 : AbstractFactory
        {
            public override AbstractProductA CreateProductA()
            {
                return new ProductA1();
            }

            public override AbstractProductB CreateProductB()
            {
                return new ProductB1();
            }
        }

        /// <summary>
        /// The 'ConcreteFactory2' class
        /// </summary>
        class ConcreteFactory2 : AbstractFactory
        {
            public override AbstractProductA CreateProductA()
            {
                return new ProductA2();
            }

            public override AbstractProductB CreateProductB()
            {
                return new ProductB2();
            }
        }

        /// <summary>
        /// The 'AbstractProductA' abstract class
        /// </summary>
        abstract class AbstractProductA
        {
        }

        /// <summary>
        /// The 'AbstractProductB' abstract class
        /// </summary>
        abstract class AbstractProductB
        {
            public abstract void Interact(AbstractProductA a);
        }

        /// <summary>
        /// The 'ProductA1' class
        /// </summary>
        class ProductA1 : AbstractProductA
        {
        }

        /// <summary>
        /// The 'ProductB1' class
        /// </summary>
        class ProductB1 : AbstractProductB
        {
            public override void Interact(AbstractProductA a)
            {
                Console.WriteLine(this.GetType().Name + " interacts with " + a.GetType().Name);
            }
        }

        /// <summary>
        /// The 'ProductA2' class
        /// </summary>
        class ProductA2 : AbstractProductA
        {
        }

        /// <summary>
        /// The 'ProductB2' class
        /// </summary>
        class ProductB2 : AbstractProductB
        {
            public override void Interact(AbstractProductA a)
            {
                Console.WriteLine(this.GetType().Name + " interacts with " + a.GetType().Name);
            }
        }

        /// <summary>
        /// The 'Client' class. Interaction environment for the products.
        /// </summary>
        class Client
        {
            private AbstractProductA _abstractProductA;
            private AbstractProductB _abstractProductB;

            // Constructor
            public Client(AbstractFactory factory)
            {
                _abstractProductB = factory.CreateProductB();
                _abstractProductA = factory.CreateProductA();
            }

            public void Run()
            {
                _abstractProductB.Interact(_abstractProductA);
            }
        }

        [Test]
        public void ShouldVerifyInteractionWithinFamilyOfObjectsCreatedFromAbstractFactories()
        {

            var consoleOutput = new ConsoleOutput();
            AbstractFactory factory1 = new ConcreteFactory1();
            AbstractFactory factory2 = new ConcreteFactory2();

            Client client = new Client(factory1);
            client.Run();
            client = new Client(factory2);
            client.Run();
            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("ProductB1 interacts with ProductA1"));
            Assert.That(outputLines[1], Is.EqualTo("ProductB2 interacts with ProductA2"));
        }

    }

    public class AbstractFactoryPatternRealWorld
    {
        /*
         * Real-world code:
         * 
         * This real-world code demonstrates the creation of different animal worlds for a computer game using different factories. Although
         * the animals created by the Continent factories are different, the interactions among the animals remain the same. 
        */

        /// <summary>
        /// The 'AbstractFactory' abstract class
        /// </summary>
        abstract class ContinentFactory
        {
            public abstract Herbivore CreateHerbivore();
            public abstract Carnivore CreateCarnivore();
        }

        /// <summary>
        /// The 'ConcreteFactory1' class
        /// </summary>
        class AfricaFactory : ContinentFactory
        {
            public override Herbivore CreateHerbivore()
            {
                return new Wildebeest();
            }
            public override Carnivore CreateCarnivore()
            {
                return new Lion();
            }
        }

        /// <summary>
        /// The 'ConcreteFactory2' class
        /// </summary>
        class AmericaFactory : ContinentFactory
        {
            public override Herbivore CreateHerbivore()
            {
                return new Bison();
            }
            public override Carnivore CreateCarnivore()
            {
                return new Wolf();
            }
        }

        /// <summary>
        /// The 'AbstractProductA' abstract class
        /// </summary>
        abstract class Herbivore
        {
        }

        /// <summary>
        /// The 'AbstractProductB' abstract class
        /// </summary>
        abstract class Carnivore
        {
            public abstract void Eat(Herbivore h);
        }

        /// <summary>
        /// The 'ProductA1' class
        /// </summary>
        class Wildebeest : Herbivore
        {
        }

        /// <summary>
        /// The 'ProductB1' class
        /// </summary>
        class Lion : Carnivore
        {
            public override void Eat(Herbivore h)
            {
                // Eat Wildebeest
                Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
            }
        }

        /// <summary>
        /// The 'ProductA2' class
        /// </summary>
        class Bison : Herbivore
        {
        }

        /// <summary>
        /// The 'ProductB2' class
        /// </summary>
        class Wolf : Carnivore
        {
            public override void Eat(Herbivore h)
            {
                // Eat Bison
                Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
            }
        }

        /// <summary>
        /// The 'Client' class 
        /// </summary>
        class AnimalWorld
        {
            private Herbivore _herbivore;
            private Carnivore _carnivore;

            // Constructor
            public AnimalWorld(ContinentFactory factory)
            {
                _carnivore = factory.CreateCarnivore();
                _herbivore = factory.CreateHerbivore();
            }

            public void RunFoodChain()
            {
                _carnivore.Eat(_herbivore);
            }
        }

        [Test]
        public void ShouldVerifyInteractionWithinContinentsCreatedFromAbstractFactories()
        {
            var consoleOutput = new ConsoleOutput();
            ContinentFactory africa = new AfricaFactory();
            ContinentFactory america = new AmericaFactory();

            AnimalWorld world = new AnimalWorld(africa);
            world.RunFoodChain();
            world = new AnimalWorld(america);
            world.RunFoodChain();
            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("Lion eats Wildebeest"));
            Assert.That(outputLines[1], Is.EqualTo("Wolf eats Bison"));
        }
    }

}