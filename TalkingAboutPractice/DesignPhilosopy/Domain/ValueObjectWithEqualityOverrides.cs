using System;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace TalkingAboutPractice.DesignPhilosopy.Domain
{
    /*
     * Our Name class is an example of a "value object" in our domain - an object with a 
     * life span of no interest and which need not be uniquely identified by an ID or key.
     * Two instances of a value object of the same type are said to be the same if all
     * their properties are the same.
     * 
     * Following the above definition of a value object, it follows that value objects
     * are immutable - they cannot be changed once they've been defined.
     * 
     * Immutability is enforced using private setters for properties, which are set only
     * by the object's constructor.
     * 
     * Lacking any identifier property, we want to ensure the concepts of uniqueness
     * and equality can be properly enforced, so we start with an override of the object
     * class' GetHashCode() function, using a scheme that incorporates the hashes of each
     * individual property value of an object instance. This helps to ensure the uniqueness
     * of the values in a dictionary or hashtable is indeed directly related to their
     * combination of values. If the hash code for two items does not match, they may
     * never be considered equal (as Equals will never get called).
     * http://stackoverflow.com/questions/371328/why-is-it-important-to-override-gethashcode-when-equals-method-is-overridden
     * 
     * Simiarly, we have overridden the Equals() function to check the null status,
     * reference, and type of one instance to another, with a backup Equals function
     * that will ultimately compare the values of each property in the event of a
     * hash code collision. This overriding should always be done in conjunction with
     * the hash code overriding, and the logic of these two things should reflect each
     * other (in our case, they both look at the full combination of property values).
     * 
     * Two equal objects will always return the same hash code, but unfortunately, the
     * reverse is not true; equal hash codes do not imply object equality, because
     * different (unequal) objects can have identical hash codes. The .NET framework
     * does not guarantee the default implemenation of GetHashCode(), and the value
     * it returns may differ between different framework versions (such as 32-bit vs.
     * 64-bit). For these reasons, you don't want to depend on the default implementation
     * as a unique object identifier for hashing purposes, and instead, we override
     * the two methods (GetHashCode() and Equals()) as shown here.
    */
    public class Name
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }

        public Name(string firstName, string middleName, string lastName)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name must be defined.");
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name must be defined.");

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public override int GetHashCode()
        {
            // 397 is just a happy prime number that ReSharper, and books like Gabriel Schenker's, use:
            // http://stackoverflow.com/questions/102742/why-is-397-used-for-resharper-gethashcode-override
            const int happyPrimeNumberForHashCodeDistribution = 397;

            unchecked
            {
                var hashCode = FirstName.GetHashCode();
                hashCode = (hashCode * happyPrimeNumberForHashCodeDistribution) ^ (MiddleName != null ? MiddleName.GetHashCode() : 0);
                hashCode = (hashCode * happyPrimeNumberForHashCodeDistribution) ^ LastName.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(Name other)
        {
            return string.Equals(LastName, other.LastName) && string.Equals(FirstName, other.FirstName) && string.Equals(MiddleName, other.MiddleName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Name) obj);
        }
    }

    public class NameWithoutOverrides
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }

        public NameWithoutOverrides(string firstName, string middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name must be defined.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name must be defined.");

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void ShouldTreatTwoLikeObjectsAsNotEqualByDefault()
        {
            NameWithoutOverrides name1 = new NameWithoutOverrides("Joe", "Bob", "Smith");
            NameWithoutOverrides name2 = new NameWithoutOverrides("Joe", "Bob", "Smith");

            Assert.That(name1, Is.Not.EqualTo(name2));
        }

        [Test]
        public void ShouldTreatTwoLikeObjectsAsEqualWhenProperOverridesAreInPlace()
        {
            Name name1 = new Name("Joe", "Bob", "Smith");
            Name name2 = new Name("Joe", "Bob", "Smith");

            Assert.That(name1, Is.EqualTo(name2));
        }
    }
}
