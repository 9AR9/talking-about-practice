using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.FluentInterface
{
    [TestFixture]
    public class FluentInterface
    {
        /*
         * Fluent Interface
         * 
         * Benefits:
         *   - Make code more readable and easy to understand, by providing a succession of method calls that
         *     read close to natural language
         *   - Can force a programmer to perform certain steps before they perform others.
         *     
         * With a properly written fluent interface, you can enforce "rules of grammar" and make it so that
         * certain methods cannot be called until all required setup methods have been called.
         * 
         * 
         * Step 1: Define natural language syntax
         *   - Determine all possible combinations you want to use in your fluent interface.
         *   - For this example, we will build a music Album, allowing for a small number of combinations:
         *   
         *   1) MakeAlbumFor("artist").Named("title").OfGenre("genre").OnLabel("label").Make();
         *   2) MakeAlbumFor("artist").Named("title").OnLabel("label").OfGenre("genre").Make();
         *   3) MakeAlbumFor("artist").Named("title").Make();
         *   
         *   - The syntax statements above demonstrate that I want an album to be named first, before then
         *     being able to add genre or label. Both genre and label can be added once name is set, and
         *     can be chanined off each other, so their order is not important so long as a title exists.
         *     The Make function, which is meant to finalize the creation of an album, can be chained from
         *     any of the name, genre or label functions, which tells us that both genre and label are
         *     not needed in order to create an album, but that at least a name is required.
         *   - This brings us to the categorization of the methods, which can follow the ICE acronymn:
         *     - I - Initiating/Instantiating :: MakeAlbumFor()
         *         - Methods used to start the call. Here we have only one.
         *     - C - Chaining/Continuing :: Named(), OfGenre(), OnLabel()
         *         - Methods we call in the middle of the statement, and that let us call other methods
         *           after them, known as method chaining. We have three of these
         *     - E - Executing/Ending :: Make()
         *         - Methods that finally do some action and end the statement. We only have one.
         *
         * 
         * Step 2: Create interfaces to enforce the grammar
         *   - Because we want our methods to allow for method chaining, we need each to return an object.
         *     And since we are ultimately building a single object, we need each method (with the exception
         *     of the Executing method(s)) to actually return the SAME object, so we can continue to build it
         *     up as we move through the statement.
         *   - We know, however, that we can't allow any method to be called at any time, so we'll determine
         *     the sets of functions each is allowed to interact with, and then create inerfaces for the
         *     different sets of rules.
         *     - MakeAlbumFor can only call Named. We can call this interface ICanName.
         *     - Named can call OfGenre, OnLabel, and Make. We can all this interface ICanSetGenreAndLabelOrMake.
         *     - Both OfGenre and OnLabel can actually do the same three things as Named, so it also fits
         *       the ICanSetGenreAndLabelOrMake interface.
         *   - So we have two sets of rules, and we can create an interface for each, and a simple domain object
         *     to return from the Executing method.
         *   - Once we have those, we are ready to create the fluent object that brings it all together, which
         *     we will call FluentAlbumFactory. It will utilize the interfaces to return different
         *     representations of the same object as we go through the chain, only exposing the methods that
         *     match the category of method for each call in the chain.
        */

        public class Album
        {
            public string Artist { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Label { get; set; }
        }

        public interface ICanName
        {
            ICanSetGenreAndLabelOrMake Named(string albumTitle);
        }

        public interface ICanSetGenreAndLabelOrMake
        {
            ICanSetGenreAndLabelOrMake OfGenre(string genreName);
            ICanSetGenreAndLabelOrMake OnLabel(string labelName);
            Album Make();
        }

        /*
         * Step 3: We can now build the class that implements our interfaces: FluentAlbumFactory
        */

        public class FluentAlbumFactory : ICanName, ICanSetGenreAndLabelOrMake
        {
            private readonly string _artistName;
            private string _albumTitle;
            private string _genreName;
            private string _labelName;

            // Private constructor, to force object instantiation from the fluent method(s)
            private FluentAlbumFactory(string artistName)
            {
                _artistName = artistName;
            }

            // Instantiating method, static; creates the object that the rest of the chain will use;
            // Interface as return type limits the methods that will actually be returned
            public static ICanName MakeAlbumFor(string artistName)
            {
                return new FluentAlbumFactory(artistName);
            }

            // Chaining method 1
            public ICanSetGenreAndLabelOrMake Named(string albumTitle)
            {
                _albumTitle = albumTitle;
                return this;
            }

            // Chaining method 2
            public ICanSetGenreAndLabelOrMake OfGenre(string genreName)
            {
                _genreName = genreName;
                return this;
            }

            // Chaining method 3
            public ICanSetGenreAndLabelOrMake OnLabel(string labelName)
            {
                _labelName = labelName;
                return this;
            }

            // Executing method
            public Album Make()
            {
                return new Album() {Genre = _genreName, Label = _labelName, Artist = _artistName, Title = _albumTitle};
            }
        }


        [Test]
        public void ShouldBuildAlbumsUsingFluentInterface()
        {
            var album1 = FluentAlbumFactory.MakeAlbumFor("Lonnie Smith").Named("Turning Point").Make();
            Assert.That(album1, Is.TypeOf<Album>());
            Assert.That(album1.Artist, Is.EqualTo("Lonnie Smith"));
            Assert.That(album1.Title, Is.EqualTo("Turning Point"));
            Assert.That(album1.Genre, Is.Null);
            Assert.That(album1.Label, Is.Null);

            var album2 = FluentAlbumFactory.MakeAlbumFor("Pavement").Named("Slanted & Enchanted").OfGenre("Rock").Make();
            Assert.That(album2, Is.TypeOf<Album>());
            Assert.That(album2.Artist, Is.EqualTo("Pavement"));
            Assert.That(album2.Title, Is.EqualTo("Slanted & Enchanted"));
            Assert.That(album2.Genre, Is.EqualTo("Rock"));
            Assert.That(album2.Label, Is.Null);

            var album3 = FluentAlbumFactory.MakeAlbumFor("Aloe Blacc").Named("Shine Through").OnLabel("Stones Throw").Make();
            Assert.That(album3, Is.TypeOf<Album>());
            Assert.That(album3.Artist, Is.EqualTo("Aloe Blacc"));
            Assert.That(album3.Title, Is.EqualTo("Shine Through"));
            Assert.That(album3.Genre, Is.Null);
            Assert.That(album3.Label, Is.EqualTo("Stones Throw"));

            var album4 = FluentAlbumFactory.MakeAlbumFor("EPMD").Named("Strictly Business").OfGenre("Hip-Hop").OnLabel("Fresh").Make();
            Assert.That(album4, Is.TypeOf<Album>());
            Assert.That(album4.Artist, Is.EqualTo("EPMD"));
            Assert.That(album4.Title, Is.EqualTo("Strictly Business"));
            Assert.That(album4.Genre, Is.EqualTo("Hip-Hop"));
            Assert.That(album4.Label, Is.EqualTo("Fresh"));
        }

        [Test]
        public void ShouldRestrictChainableMethodsBasedOnMethodCategoryUsingInterfaces()
        {
            var iCanName = FluentAlbumFactory.MakeAlbumFor("Sublime");
            Assert.That(iCanName, Is.AssignableTo(typeof(ICanName)));

            var iCanSetGenreAndLabelOrMake1 = FluentAlbumFactory.MakeAlbumFor("Sublime").Named("40 Oz. To Freedom");
            Assert.That(iCanSetGenreAndLabelOrMake1, Is.AssignableTo(typeof(ICanSetGenreAndLabelOrMake)));

            var iCanSetGenreAndLabelOrMake2 = FluentAlbumFactory.MakeAlbumFor("Sublime").Named("40 Oz. To Freedom").OnLabel("Skunk");
            Assert.That(iCanSetGenreAndLabelOrMake2, Is.AssignableTo(typeof(ICanSetGenreAndLabelOrMake)));

            var iCanSetGenreAndLabelOrMake3 = FluentAlbumFactory.MakeAlbumFor("Sublime").Named("40 Oz. To Freedom").OnLabel("Skunk").OfGenre("Rock");
            Assert.That(iCanSetGenreAndLabelOrMake3, Is.AssignableTo(typeof(ICanSetGenreAndLabelOrMake)));
        }
    }

}