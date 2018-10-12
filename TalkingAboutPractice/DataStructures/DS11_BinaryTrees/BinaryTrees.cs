using System.Collections.Generic;
using NUnit.Framework;

namespace TalkingAboutPractice.DataStructures.DS11_BinaryTrees
{

    /*
     * Binary trees are a common value structure that is NOT included in the .NET Framework
     * Base Class Library. Whereas arrays arrange value linearly, binary trees can be
     * envisioned as storing value in two dimensions. A special kind of binary tree, called
     * "binary search tree", or BST, allows for much more optimized search time than using
     * classic linear search on unsorted arrays.
     *
     * The List, Stack, Queue, Hashtable, and Dictionary value structures all use an underlying
     * array as the means by which their value is stored. This means that, under the covers,
     * those value structures are bound by the limitations imposed imposed by an array. Arrays
     * are stored linearly in memory, requires explicit resizing when the array's capacity is
     * reached, and suffers from linear searching time.
     *  
     * Tree structures can be used to arrange value in a hierarchy, where there is precisely
     * one root node, all nodes except the root node have precisely one parent, and there are
     * no cycles (paths move only in one direction down the hierarchy, and there is no path
     * back to the root node from any given node).
     *
     * A "binary tree" is a special kind of tree in which all nodes have at most two children.
     * For a given node in a binary tree, the first child is referred to as the "left child",
     * and the second as the "right child". Nodes with one or two children are called
     * "internal nodes" while nodes with no children are "leaf nodes".
     *
     * The .NET framework does not have any class to represent trees, as it is more of a
     * pattern than a value structure, and there are many ways you could choose to implement
     * such a thing (and many different classifications of trees out there in the ether).
     * Thus, we'll create our own BinaryTree class.
    */



    /*
     * First, we need a class that represents the nodes of the binary tree. Since a binary
     * tree is just one implementation of the tree structure pattern, we'll start by creating a
     * base Node class that can then be extended to meet the specific needs of a binary tree
     * node through inheritance. The base Node class represents a node in a general tree - one
     * whose nodes can have an arbitrary number of children. The NodeList class represents
     * the collection of children for any given Node. The Node concept is a perfect opportunity
     * to leverage the power of Generics, which will allow us to let the developer using the
     * class decide at develop time what what type of value to store in the node.
    */

    public class Node<T>
    {
        public T Data { get; set; }
        public NodeList<T> ChildNodes { get; set; }

        public Node() { }
        public Node(T data) : this(data, null) { }
        public Node(T data, NodeList<T> childNodes)
        {
            Data = data;
            ChildNodes = childNodes;
        }
    }

    public class NodeList<T> : List<Node<T>>
    {
        public NodeList(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                Add(default(Node<T>));
            }
        }
    }

    /*
     * Now we have a Node class that is adequate for any generic tree, but a binary tree has
     * tighter restrictions. As discussed before, they have at most two children, commonly
     * referred to as left and right. We can extend the base Node class to create a
     * BinaryTreeNode class that exposes two properties - Left and Right - that operate on
     * the base class's ChildNodes property.
    */

    public class BinaryTreeNode<T> : Node<T>
    {
        public BinaryTreeNode<T> Left
        {
            get
            {
                // Null propagation! C# 6.0 feature that uses the ? after a variable (but before indexer,
                // if there is one, like this example) to return the value if it exists, and false if it doesn't.
                return (BinaryTreeNode<T>) ChildNodes?[0];
            }
            set
            {
                if (ChildNodes == null) ChildNodes = new NodeList<T>(2);
                ChildNodes[0] = value;
            }
        }

        public BinaryTreeNode<T> Right
        {
            get { return (BinaryTreeNode<T>) ChildNodes?[1]; }
            set
            {
                if (ChildNodes == null) ChildNodes = new NodeList<T>(2);
                ChildNodes[1] = value;
            }
        }

        public BinaryTreeNode() { }
        public BinaryTreeNode(T value) : base(value) { }
        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right) : base(value)
        {
            Data = value;
            ChildNodes = new NodeList<T>(2) {[0] = left, [1] = right};
        }
    }

    /*
     * Now that we have a complete BinaryTreeNode class, creating a BinaryTree class is pretty easy.
     * The BinaryTree class contains a single private member variable - root. The root is of type
     * BinaryTreeNode and represents the root of the binary tree. (Derp!) The private member variable
     * is exposed as a public property. The BinaryTree class also has a single public method, Clear(),
     * which clears out the contents of the tree. Clear() works by simply setting the root to null.
     * Beyond the root, and a Clear() method, the BinaryTree class contains no more properties or
     * methods. Crafting the contents of the binary tree is the responsibility of the developer
     * using this data structure.
    */

    public class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root { get; set; }

        public BinaryTree() { Root = null; }

        public virtual void Clear()
        {
            Root = null;
        }
    }

    /*
     * Now, we have all we need to assign a BinaryTreeNode as the root of a BinaryTree, and set the
     * children of each node accordingly, as far down the hierarchy as one would like.
     * ---
     * Now, to review time complexity, the classic array offers linear search time, given that its
     * data is stored contiguously in memory so that each value must be traversed and checked in
     * a search of unsorted data, and constant lookup time, given an index lookup is just as fast
     * regardless of how big the array is. With this binary tree structure we've created, instead
     * of a contiguous stream of values in memory, we're dealing with a hierarchy of objects that
     * can be scattered throughout the managed heap. The binary tree's lookup time is linear, being
     * that you need to search the binary tree's set of nodes, looking for a particular node, and
     * have no direct access option like an array. The binary tree's search time is also linear, as
     * potentially all nodes will need to be examined, so as that number increases, so does the
     * search time. Clearly, the binary tree does not give us a more performant method for either
     * lookup or search than an array (in fact, lookup time gets worse). So why is this structure
     * any better than an array? Well, a generic binary tree doesn't offer any benefit over an array,
     * however, by intelligently organizing the items in a binary tree, we can greatly improve the
     * search time (and, therefore, the lookup time as well).
     * ---
     * From here, please reference Binary Search Trees under Algorithms/Searching to see an
     * implementation of Binary Search Trees, using the domain objects defined in this file.
    */



    [TestFixture]
    public class BinaryTrees
    {
        [Test]
        public void ShouldFindDefaultDataInNodesWhenCreatedWithoutData()
        {
            Node<int> plainOldBoringNode = new Node<int>();
            NodeList<string> ambiguousNodeList = new NodeList<string>(6);
            BinaryTreeNode<int> plainOldBoringBinaryTreeNode = new BinaryTreeNode<int>();
            BinaryTree<int> plainOldBoringBinaryTree = new BinaryTree<int>();

            Assert.That(plainOldBoringNode.Data, Is.EqualTo(0));
            Assert.That(plainOldBoringNode.ChildNodes, Is.Null);
            Assert.That(ambiguousNodeList[2], Is.Null);
            Assert.That(plainOldBoringBinaryTreeNode.Data, Is.EqualTo(0));
            Assert.That(plainOldBoringBinaryTreeNode.Left, Is.Null);
            Assert.That(plainOldBoringBinaryTreeNode.Right, Is.Null);
            Assert.That(plainOldBoringBinaryTree.Root, Is.Null);
        }

        [Test]
        public void ShouldFindDataInNodesWhenCreatedWithData()
        {
            Node<int> nodeWithSomeStuff = new Node<int>
            {
                Data = 99,
                ChildNodes = new NodeList<int>(2) { [0] = new Node<int>(999), [1] = new Node<int>(9999) }
            };
            NodeList<int> nodeListWithANodeInIt = new NodeList<int>(6) { [3] = nodeWithSomeStuff };
            BinaryTreeNode<int> binaryTreeNodeWithSomeStuff =
                new BinaryTreeNode<int>(77, new BinaryTreeNode<int>(777), new BinaryTreeNode<int>(7777));
            BinaryTree<int> binaryTreeWithARoot = new BinaryTree<int>() { Root = binaryTreeNodeWithSomeStuff };

            Assert.That(nodeWithSomeStuff.Data, Is.EqualTo(99));
            Assert.That(nodeWithSomeStuff.ChildNodes[0].Data, Is.EqualTo(999));
            Assert.That(nodeWithSomeStuff.ChildNodes[1].Data, Is.EqualTo(9999));
            Assert.That(nodeListWithANodeInIt[2], Is.Null);
            Assert.That(nodeListWithANodeInIt[3], Is.SameAs(nodeWithSomeStuff));
            Assert.That(binaryTreeNodeWithSomeStuff.Data, Is.EqualTo(77));
            Assert.That(binaryTreeNodeWithSomeStuff.Left.Data, Is.EqualTo(777));
            Assert.That(binaryTreeNodeWithSomeStuff.Right.Data, Is.EqualTo(7777));
            Assert.That(binaryTreeWithARoot.Root, Is.SameAs(binaryTreeNodeWithSomeStuff));
        }

        [Test]
        public void ShouldClearOutBinaryTreeData()
        {
            BinaryTreeNode<int> binaryTreeNodeWithSomeStuff =
                new BinaryTreeNode<int>(77, new BinaryTreeNode<int>(777), new BinaryTreeNode<int>(7777));
            BinaryTree<int> binaryTreeWithARoot = new BinaryTree<int>() { Root = binaryTreeNodeWithSomeStuff };

            Assert.That(binaryTreeWithARoot.Root, Is.SameAs(binaryTreeNodeWithSomeStuff));

            binaryTreeWithARoot.Clear();

            Assert.That(binaryTreeWithARoot.Root, Is.Null);
        }

        [Test]
        public void ShouldBuildUpANiceTastyBinaryTree()
        {
            BinaryTree<string> binaryTree = new BinaryTree<string>();
            binaryTree.Root = new BinaryTreeNode<string>()
            {
                Data = "Pimp",
                Left = new BinaryTreeNode<string>("Mack 1"),
                Right = new BinaryTreeNode<string>("Mack 2")
            };
            binaryTree.Root.Left.Left = new BinaryTreeNode<string>("Player 1");
            binaryTree.Root.Left.Right = new BinaryTreeNode<string>("Player 2");
            binaryTree.Root.Right.Left = new BinaryTreeNode<string>("Player 3");
            binaryTree.Root.Right.Right = new BinaryTreeNode<string>("Player 4");

            Assert.That(binaryTree.Root.Data, Is.EqualTo("Pimp"));
            Assert.That(binaryTree.Root.Left.Data, Is.EqualTo("Mack 1"));
            Assert.That(binaryTree.Root.Left.Left.Data, Is.EqualTo("Player 1"));
            Assert.That(binaryTree.Root.Left.Right.Data, Is.EqualTo("Player 2"));
            Assert.That(binaryTree.Root.Right.Data, Is.EqualTo("Mack 2"));
            Assert.That(binaryTree.Root.Right.Left.Data, Is.EqualTo("Player 3"));
            Assert.That(binaryTree.Root.Right.Right.Data, Is.EqualTo("Player 4"));
        }
    }



    /*
     * CLASSIC REFERENCE HAPPINESS:
     *      • https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx
    */
}