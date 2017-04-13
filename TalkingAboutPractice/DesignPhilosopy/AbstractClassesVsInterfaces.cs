namespace TalkingAboutPractice.DesignPhilosopy
{
    public class AbstractClassesVsInterfaces
    {
        /*
         * (source: https://msdn.microsoft.com/en-us/library/scsyfw1d(v=vs.71).aspx)
         * 
         * The choice of whether to design your functionality as an interface or an abstract class can sometimes be a difficult one.
         * An abstract class is a class that cannot be instantiated, but must be inherited from. An abstract class may be fully implemented,
         * but is more usually partially implemented or not implemented at all, thereby encapsulating common functionality for inherited
         * classes.
         * 
         * An interface, by contrast, is a totally abstract set of members that can be thought of as defining a contract for conduct. The
         * implementation of an interface is left completely to the developer.
         * 
         * Both interfaces and abstract classes are useful for component interaction. If a method requires an interface as an argument,
         * then any object that implements that interface can be used in the argument. For example:
        */

        public interface IWidget
        {
            string Describe();
        }

        public void Spin(IWidget widget) { }

        /*
         * The Spin method above could accept any object that implemented IWidget as the widget argument, even though the implementations
         * of IWidget might be quite different. Absract classes also allow for this kind of polymorphism, but with a few caveats:
         * 
         *   - Classes may inherit from only one base class, so if you want to use abstract classes to provide polymorphism to a group of
         *     classes, they must all inherit from that class.
         *   - Abstract classes may also provide members that have already been implemented. Therefore, you can ensure a certain amount
         *     of identical functionality with an abstract class, but cannot with an interface.
         *     
         * Here are some recommendations to help you to decide whether to use an interface or an abstract class to provide polymorphism
         * for your components.
         * 
         *   - If you anticipate creating multiple versions of your component, create an ABSTRACT CLASS. Abstract classes provide a simple
         *     and easy way to version your components. By updating the base class, all inheriting classes are automatically updated with
         *     the change. Interfaces, on the other hand, cannot be changed once created. If a new version of an interface is required,
         *     you must create a whole new interface.
         *   - If the functionality you are creating will be useful across a wide range of disparate objects, use an INTERFACE. Abstract
         *     classes should be used primarily for objects that are closely related, whereas interfaces are best suited for providing
         *     common functionality to unrelated classes.
         *   - If you are designing small, concise bits of functionality, use INTERFACES. If you are designing large functional units,
         *     use an abstract class.
         *   - If you want to provide common, implemented functionality among all implementations of your component, use an abstract
         *     class. Abstract classes allow you to partially implement your class, whereas interfaces contain no implementation
         *     for any members.
        */
    }
}