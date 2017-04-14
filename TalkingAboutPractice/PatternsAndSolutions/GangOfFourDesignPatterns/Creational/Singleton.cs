using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;

namespace TalkingAboutPractice.PatternsAndSolutions.GangOfFourDesignPatterns.Creational
{
    [TestFixture]
    public class SingletonPatternStructural
    {
        /*
         * Singleton: A class of which only a single instance can exist
         * (source: http://www.dofactory.com/net/singleton-design-pattern)
         * 
         * Definition:
         * Ensure a class has only one instance and provide a global point of access to it. 
         * 
         * Frequency of use: 4/5 (Medium high)
         * 
         * UML: See SingletonUml.gif
         * 
         * Participants (The classes and objects participating in this pattern):
         *      - Singleton (LoadBalancer):
         *           - defines an Instance operation that lets clients access its unique instance.
         *             Instance is a class operation.
         *           - responsible for creating and maintaining its own unique instance.
        */


        /*
         * Structural code:
         * 
         * This structural code demonstrates the Singleton pattern which assures only a single instance
         * (the singleton) of the class can be created. 
        */


        /// <summary>
        /// The 'Singleton' class
        /// </summary>
        class Singleton
        {
            // the static nature of the _instance variable guarantees there can only be one
            private static Singleton _instance;

            // Constructor is 'protected', which prevents the use of the new operator on it outside of the class
            protected Singleton()
            {
            }

            // the public Instance method will only return the _instance, and create it if it has to (the first time it's called)
            public static Singleton Instance()
            {
                // Uses lazy initialization. NOTE: this is not thread safe.
                if (_instance == null)
                {
                    _instance = new Singleton();
                }

                return _instance;
            }
        }

        [Test]
        public void ShouldVerifyThatMultipleCallsToSingletonInstanceMethodReferToTheSameObject()
        {
            Singleton singleton1 = Singleton.Instance();
            Singleton singleton2 = Singleton.Instance();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            Assert.That(singleton1, Is.SameAs(singleton2));
            Assert.That(sb1, Is.Not.SameAs(sb2));
        }

    }

    public class SingletonPatternRealWorld
    {
        /*
         * Real-world code:
         * 
         * This real-world code demonstrates the Singleton pattern as a LoadBalancing object. Only a single
         * instance (the singleton) of the class can be created because servers may dynamically come on- or
         * off-line and every request must go through the one object that has knowledge about the state of
         * the (web) farm. 
        */

        /// <summary>
        /// The 'Singleton' class
        /// </summary>
        class LoadBalancer
        {
            // The _instance variable is static; the other private variables allow for state manipulation within the singleton
            private static LoadBalancer _instance;
            private List<string> _servers = new List<string>();
            private Random _random = new Random();

            // Lock synchronization object - also static, like the _instance
            private static object syncLock = new object();

            // Constructor (protected)
            protected LoadBalancer()
            {
                // List of available servers
                _servers.Add("ServerI");
                _servers.Add("ServerII");
                _servers.Add("ServerIII");
                _servers.Add("ServerIV");
                _servers.Add("ServerV");
            }

            public static LoadBalancer GetLoadBalancer()
            {
                // Support multithreaded applications through a 'Double checked locking' pattern, which,
                // once the instance exists, avoids locking each time the method is invoked
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LoadBalancer();
                        }
                    }
                }

                return _instance;
            }

            // Simple, but effective random load balancer
            public string Server
            {
                get
                {
                    int r = _random.Next(_servers.Count);
                    return _servers[r];
                }
            }
        }

        [Test]
        public void ShouldVerifyThatMultipleCallsToSingletonInstanceMethodReferToTheSameObject()
        {
            var consoleOutput = new ConsoleOutput();
            LoadBalancer loadBalancer1 = LoadBalancer.GetLoadBalancer();
            LoadBalancer loadBalancer2 = LoadBalancer.GetLoadBalancer();
            LoadBalancer loadBalancer3 = LoadBalancer.GetLoadBalancer();
            LoadBalancer loadBalancer4 = LoadBalancer.GetLoadBalancer();

            Assert.That(loadBalancer1, Is.SameAs(loadBalancer2));
            Assert.That(loadBalancer2, Is.SameAs(loadBalancer3));
            Assert.That(loadBalancer3, Is.SameAs(loadBalancer4));

            // The results of this iteration of 15 random load balancing server selections
            // can be observed in the Immediate window when running in Debug mode
            for (int i = 0; i < 15; i++)
            {
                Debug.WriteLine("Dispatch request to: " + loadBalancer4.Server);
            }
        }
    }

}