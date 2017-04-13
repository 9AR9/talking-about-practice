using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.GangOfFourDesignPatterns.Creational
{
    [TestFixture]
    public class FactoryMethodPatternStructural
    {
        /*
         * Factory Method: Creates an instance of several derived classes
         * (source: http://www.dofactory.com/net/factory-method-design-pattern)
         * 
         * Definition:
         * Define an interface for creating an object, but let subclasses decide which class to instantiate.
         * Factory Method lets a class defer instantiation to subclasses.
         * 
         * Frequency of use: 5/5 (High)
         * 
         * UML: See FactoryMethodUml.gif
         * 
         * Participants (The classes and objects participating in this pattern):
         *      - Product (Page):
         *           - defines the interface of objects the factory method creates
         *      - ConcreteProduct (SkillsPage, EducationPage, ExperiencePage):
         *           - implements the Product interface
         *      - Creator (Document):
         *           - declares the factory method, which returns an object of type Product. Creator may also
         *             define a default implementation of the factory method that returns a default
         *             ConcreteProduct object. 
         *      - ConcreteCreator (Report, Resume):
         *           - overrides the factory method to return an instance of a ConcreteProduct.
        */


        /*
         * Structural code:
         * 
         * This structural code demonstrates the Factory method offering great flexibility in creating
         * different objects. The Abstract class may provide a default object, but each subclass can
         * instantiate an extended version of the object. 
        */


        /// <summary>
        /// The 'Product' abstract class
        /// </summary>
        abstract class Product
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class ConcreteProductA : Product
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class ConcreteProductB : Product
        {
        }

        /// <summary>
        /// The 'Creator' abstract class
        /// </summary>
        abstract class Creator
        {
            public abstract Product FactoryMethod();
        }

        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        class ConcreteCreatorA : Creator
        {
            public override Product FactoryMethod()
            {
                return new ConcreteProductA();
            }
        }

        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        class ConcreteCreatorB : Creator
        {
            public override Product FactoryMethod()
            {
                return new ConcreteProductB();
            }
        }

        [Test]
        public void ShouldUseOverriddenFactoryMethodOfCreatorBaseClassToConstructObjects()
        {
            var consoleOutput = new ConsoleOutput();
            Creator concreteCreatorA = new ConcreteCreatorA();
            Console.WriteLine("Created {0}", concreteCreatorA.FactoryMethod().GetType().Name);
            Creator concreteCreatorB = new ConcreteCreatorB();
            Console.WriteLine("Created {0}", concreteCreatorB.FactoryMethod().GetType().Name);

            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("Created ConcreteProductA"));
            Assert.That(outputLines[1], Is.EqualTo("Created ConcreteProductB"));
        }

    }

    public class FactoryMethodPatternRealWorld
    {
        /*
         * Real-world code:
         * 
         * This real-world code demonstrates the Factory method offering flexibility in creating
         * different documents. The derived Document classes Report and Resume instantiate extended
         * versions of the Document class. Here, the Factory Method is called in the constructor
         * of the Document base class. 
        */

        /// <summary>
        /// The 'Product' abstract class
        /// </summary>
        abstract class Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class SkillsPage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class EducationPage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class ExperiencePage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class IntroductionPage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class ResultsPage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class ConclusionPage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class SummaryPage : Page
        {
        }

        /// <summary>
        /// A 'ConcreteProduct' class
        /// </summary>
        class BibliographyPage : Page
        {
        }

        /// <summary>
        /// The 'Creator' abstract class
        /// </summary>
        abstract class Document
        {
            private List<Page> _pages = new List<Page>();

            // Constructor calls abstract Factory method
            public Document()
            {
                this.CreatePages();
            }

            public List<Page> Pages
            {
                get { return _pages; }
            }

            // Factory Method
            public abstract void CreatePages();
        }

        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        class Resume : Document
        {
            // Factory Method implementation
            public override void CreatePages()
            {
                Pages.Add(new SkillsPage());
                Pages.Add(new EducationPage());
                Pages.Add(new ExperiencePage());
            }
        }

        /// <summary>
        /// A 'ConcreteCreator' class
        /// </summary>
        class Report : Document
        {
            // Factory Method implementation
            public override void CreatePages()
            {
                Pages.Add(new IntroductionPage());
                Pages.Add(new ResultsPage());
                Pages.Add(new ConclusionPage());
                Pages.Add(new SummaryPage());
                Pages.Add(new BibliographyPage());
            }
        }

        [Test]
        public void ShouldUseOverriddenFactoryMethodOfCreatorBaseClassToConstructObjects()
        {
            var consoleOutput = new ConsoleOutput();
            Document[] documents = new Document[2];
            documents[0] = new Resume();
            documents[1] = new Report();

            foreach (Document document in documents)
            {
                Console.WriteLine("\n" + document.GetType().Name + " -------");
                foreach (Page page in document.Pages)
                {
                    Console.WriteLine(" " + page.GetType().Name);
                }
            }

            string[] outputLines = consoleOutput.GetOutputLines();

            Assert.That(outputLines[0], Is.EqualTo("\nResume -------"));
            Assert.That(outputLines[1], Is.EqualTo(" SkillsPage"));
            Assert.That(outputLines[2], Is.EqualTo(" EducationPage"));
            Assert.That(outputLines[3], Is.EqualTo(" ExperiencePage"));
            Assert.That(outputLines[4], Is.EqualTo("\nReport -------"));
            Assert.That(outputLines[5], Is.EqualTo(" IntroductionPage"));
            Assert.That(outputLines[6], Is.EqualTo(" ResultsPage"));
            Assert.That(outputLines[7], Is.EqualTo(" ConclusionPage"));
            Assert.That(outputLines[8], Is.EqualTo(" SummaryPage"));
            Assert.That(outputLines[9], Is.EqualTo(" BibliographyPage"));
        }
    }

}