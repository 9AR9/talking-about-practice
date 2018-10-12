using System.Collections.Generic;
using TalkingAboutPractice.DataStructures.DS11_BinaryTrees;
using NUnit.Framework;

namespace TalkingAboutPractice.Algorithms.Searching
{

    /*
     * A binary search tree is a special kind of binary tree designed to improve the efficiency
     * of searching through the contents of a binary tree. BSTs exhibit the following property:
     * for any node n, every descendant node's value in the left subtree of n is less than the
     * value of n, and every descendant node's value in the right subtree is greater than the
     * value of n. A subtree rooted at node n is the tree formed by imagining node n was a root.
     *
     * In order for the binary search tree property to be upheld, the data stored in the nodes of
     * a BST must be able to be compared to one another. Specifically, given two nodes, a BST
     * must be able to determine if one is less than, greater than, or equal to the other.
     *
     * Now, imagine that you want to search a BST for a particular node. A BST, like a regular
     * binary tree, only has a direct reference to one node, its root. To see if a specific
     * value exists in the tree, there is a better method than just searching through each node
     * of the tree. To find a specific value, you can start with the root.
     *
     * <<< Example: Figure 5a of https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx >>>
     *
     *                                           7
     *                                     _____/\_____
     *                                    3            11
     *                                 __/\__        __/\__
     *                                1      5     10      15
     *                                    __/
     *                                   4
     *
     * In this example, the root is 7, and its right value is 11, which itself has 10 and 15 as
     * its children. Let's say we don't know whether the value 10 exists in the tree, and we want
     * to search for it. Starting at the root, we see that the root's value of 7 is less than
     * the value of the node we're looking for. Therefore, if 10 does exist in the BST, it must
     * be in the root's right subtree. So, we continue our search at node 11 (the right node of
     * the root). Here, we notice that 10 is less than 11, so if 10 exists in the BST it must
     * exist in the left subtree of 11. Moving onto the left child of 11, we find node 10, and
     * have located the node we are looking for.
     *
     * What happens if we search for a node that does not exist in the tree? Imagine we wanted to
     * find node 9. We'd start by repeating the same steps above. Upon reaching node 10, we'd see
     * that node 10 was greater than 9, so 9, if it exists, must exist on 10's left subtree.
     * However, we'd notice that 10 has no left child, therefore, 9 must not exist in the tree.
     *
     * More formally, this search algorithm employs a node n we wish to find (or, determine if it
     * exists at all) and we have a reference to the BST's root. This algorithm performs a number
     * of comparisons until a null reference is hit, or until the node we are searching for is
     * found. At each step, we are dealing with two nodes: a node in the tree, which we'll call c,
     * that we are currently comparing with n, the node we are searching for. Initially, c is the
     * root of the BST, but will change as the algorithm progresses. We apply the following steps:
     *
     * Searching for a Node in a BST!!!!
     * ---------------------------------
     *      1. If c is a null reference, then exit the algorithm. n is not in the BST.
     *      2. Compare c's value an n's value.
     *          a. If the values are equal, then we found n.
     *          b. If n's value is less than c's then n, if it exists, must be in c's LEFT
     *             subtree; therefore, return to step 1, letting c be c's left child.
     *          c. If n's value is greater than c's then n, if it exists, must be in c's RIGHT
     *             subtree; therefore, return to step 1, letting c be c's right child.
     *
     * Why is this good? Let's analyze.
     *
     * To find a node in a BST, at each stage we ideally reduce the number of nodes we have to
     * check by half, given the general binary structure of the tree. This is the most important
     * concept to understand with this structure. Compared to searching an array, where all
     * elements need to be searched, one at a time, you are whittling down the problem much
     * faster. After each check, you are narrowing the search down to n/2 nodes, compared to
     * array's n-1. Boom.
     *
     * Searching a binary search tree is similar in analysis to searching a sorted array. With an
     * ideally arranged BST, the midpoint is the root. We then traverse down the tree, navigating
     * to the left and right children as needed. These approaches cut the search space in half at
     * each step. Such algorithms that exhibit this property have an asymptotic running time of
     * log2 n ("log2 of n"), commonly abbreviated as log n. Generally speaking, this is a good
     * timing for a search algorithm, because, as n grows, the amount of time it takes to find a
     * particular n value grows very slowly in comparison (log2 * n). Due to this slower growth
     * than linear time, algorithms like this that run in asymptotic time log2 n are said to be
     * "sublinear".
     *
     * To appreciate the difference between linear and sublinear growth, consider searching an
     * array with 1000 elements versus searching a BST with 1000 elements. For the array, we'll
     * have to search up to 1000 elements. For the BST, we'd ideally have to search no more than
     * TEN nodes! (note that 1og10 1024 equals 10). The word "ideally" here is used because the
     * search time for a BST depends upon its "topology", or, how the nodes are laid out with
     * respect to one another. "Ideally" refers to a best-case scenario, which is what log2 n
     * timing represents, but a worst-case scenario could also exist for a BST and would require
     * linear time. An example would be a BST in which all nodes only have a right child,
     * making it essentially a sorted array in which all values may need to be traversed.
     * The topology of a BST is dependent upon the order with which the nodes are inserted.
     * Therefore, the order with which the nodes are inserted affects the running time of the
     * BST search algorithm.
     *
     * Inserting Nodes into a BST!!!!
     * ------------------------------
     * When adding a node to a BST, we can't just add them arbitrarily. Rather, we have to add
     * the new node such that the binary search tree property is maintained. New nodes will
     * always be inserted as leaf nodes. The only challenge, then, is finding the node in the
     * BST which will become the new node's parent. Like with the search algorithm, we'll be
     * making comparisons between a node c and the node to be inserted, n. We'll also need to
     * keep track of c's parent node. Initially, c is the BST root and parent is a null reference.
     * Locating the new parent node is accomplished by using the following algorithm:
     *
     *      1. If c is a null reference, then parent will be the parent of n. If n's value is
     *         less than parent's value, then n will be parent's new left child; otherwise, n
     *         will be parent's new right child.
     *      2. Compare c and n's values:
     *          a. If c's value equals n's value, then the user is attempting to insert a
     *             duplicate node. Either simply discard the new node, or raise an exception.
     *             (Note that the nodes' values in a BST must be unique.)
     *          b. If n's value is less than c's value, the n must end up in c's left subtree.
     *             Let parent equal c and c equal c's left child, then return to step 1.
     *          c. If n's value is greater than c's value, then n must end up in c's right
     *             subtree. Let parent equal c and c equal c's right child, and return to step 1.
     *
     * This algorithm terminates when the appropriate leaf is found, which attaches the new node
     * to the BST by making the new node an appropriate child of parent. There's one special
     * case you have to worry about with the insert algorithm -- if the BST does not contain a
     * root, then parent will be null, so the step of adding the new node as a child of parent
     * is bypassed. Furthermore, in this case, the BST's root must be assigned to the new node.
     *
     * Deleting Nodes from a BST!!!!
     * -----------------------------
     * Yo. Deleting nodes from a BST is slightly more difficult than inserting a node, because
     * deleting a node that has children requires that some other node be chosen to replace the
     * hole created by the deleted node. If the node to replace this hole is not chosen with care,
     * the binary search tree property may be violated. (BAD.) For example, consider the BST in
     * the image binary-search-tree-example-1.jpg. If node 150 is deleted, some node must be moved
     * to the hole created by the delete. If we arbitrarily choose to move, say, node 92 there,
     * the BST property is lost since 92's new left subtree will have nodes 95 and 111, both of
     * which are greater than 92, and thereby violating the binary search tree property. BAD!
     *
     * The first step in the algorithm to delete a node is to locate the node to delete. This
     * can be done using the search algorithm discussed earlier, and therefore has a log2 n
     * running time. Next a node from the BST must be selected to take the deleted node's
     * position. There are three cases to consider when choosing the replacement node, all of
     * which are illustrated in binary-search-tree-node-deletion-replacement.gif:
     *      Case 1:
     *          If the node being deleted has no right child, then the node's left child can be
     *          used as the replacement. The BST property is maintained because the node's
     *          left children are guaranteed to be less than the node's parent.
     *      Case 2:
     *          If the deleted node's right child has no left child, then the deleted node's right
     *          child can be used as the replacement. The BST property is maintained because the
     *          deleted node's right child is greater than all nodes in the deleted node's left
     *          subtree and is either greater than or less than the deleted node's parent, depending
     *          on whether the deleted node was a right or left child itself. Therefore, replacing
     *          the deleted node with its right child will maintain the BST property.
     *      Case 3:
     *          Finally, if the deleted node's right child DOES have a left child, then the deleted
     *          node needs to be replaced by the deleted node's right child's left-most descendant.
     *          That is, we replace, the deleted node with the deleted node's right subtree's
     *          SMALLEST value ("left-most" meaning smallest, "right-most" meaning largest). This
     *          replacement choice maintains the BST property because it chooses the smallest node
     *          from the deleted node's right subtree, which is guaranteed to be larger than all
     *          nodes in the deleted node's left subtree. Also, since it's the smallest node from
     *          the deleted node's right subtree, by placing it at the deleted node's position, all
     *          of the nodes in its right subtree will be greater.
     *
     * Traversing the Nodes of a BST!!!!
     * ---------------------------------
     * With the linear, contiguous ordering of an array's elements, iterating through an array is
     * straightforward - start at the first element, and step through each element sequentially. For
     * BSTs, there are three different kinds of traversals that are commonly used:
     *      1. Preorder traversal: visit current node, then its left child, then its right child
     *      2. Inorder traversal: visit current node's left child, then current node, then right child
     *      3. Postorder traversal: visit current node's left child, then its right child, then current node
     * Essentially, all three traversals work roughly in the same manner. They start at the root and
     * visit that node and its children. The difference among these three traversal methods is the
     * order with which they visit the node itself versus visiting its children. Realize that all three
     * traversal times exhibit liner asymptotic running time, because each option visits each and every
     * node in the BST precisely once. (If size is doubled, so is amount of traversal work.)
     *
     * The cost of Recursion!!!!
     * -------------------------
     * Recursive functions are often ideal for visualizing an algorithm, as they can often eloquently
     * describe an algorithm in a few short lines of code. However, when iterating through a data structure's
     * elements in practice, recursive functions are usually sub-optimal. Iterating through a data
     * structure's elements involves stepping through the items and returning them to the developer
     * utilizing the data structure, one at a time. Recursion is not suited to stopping abruptly at each
     * step of the process. For this reason, the enumerator for the BinarySearchTree class uses a
     * non-recursive solution to iterate through the elements.
     *
     * OK, let's implement a BST class, eh?
     * ------------------------------------
     * Sure! After all, the .NET Framework Base Class Library does not include any binary tree or binary
     * search tree class, being that these are more implementations of a tree pattern than data structures.
     * But whatevs. Let's do this.
     *
     * Ahem. The reason BSTs are important to study is that they offer sublinear search times. Therefore, it
     * only makes sense to first examine the BST's Contains() method. This method accepts a single parameter
     * and returns a Boolean indicating if that value exists in the BST. Contains() starts at the root and
     * iteratively percolates down the tree until it either reaches a null reference, in which case false is
     * returned, or until it finds the node being searched for, in which case true is returned. In the while
     * loop, Contains() compares the Data of the current BinaryTreeNode instance to the data being searched
     * for, and snakes its way down the right or left subtree accordingly. The comparison is performed by a
     * private member variable, comparer, which is of type IComparer<T> (where T is the type defined via the
     * Generics syntax for the BST). By default, comparer is assigned the default Comparer class for the
     * type T, although the BST class's constructor has an overload to specify a custom Comparer class
     * instance.
    */

    public class BinarySearchTree<T>
    {
        private readonly IComparer<T> _comparer;
        public BinaryTreeNode<T> Root { get; protected set; }

        public BinarySearchTree(T[] data)
        {
            _comparer = Comparer<T>.Default;
            foreach (var value in data)
            {
                Add(value);
            }
        }

        public void Add(T data)
        {
            BinaryTreeNode<T> newNode = new BinaryTreeNode<T>(data);
            BinaryTreeNode<T> current = Root;
            BinaryTreeNode<T> parent = null;
            int comparisonResult;

            while (current != null)
            {
                comparisonResult = _comparer.Compare(current.Data, data);
                if (comparisonResult == 0)
                    // Values are equal. A duplicate add is being attempted. Do nothing.
                    return;
                if (comparisonResult > 0)
                {
                    // current.Data > data. Must add newNode to current's left subtree.
                    parent = current;
                    current = current.Left;
                } else {
                    // current.Data < data. Must add newNode to current's right subtree.
                    parent = current;
                    current = current.Right;
                }
            }

            // Once either a duplicate value is encountered, or an empty node is found for insertion,
            // we are ready to add the node
            if (parent == null)
                // The tree was empty, and we will make the new node the root
                Root = newNode;
            else
            {
                comparisonResult = _comparer.Compare(parent.Data, data);
                if (comparisonResult > 0)
                    // parent.Data > data, so newNode must be added to the left subtree
                    parent.Left = newNode;
                else
                    // parent.Data < data, so newNode must be added to the right subtree
                    parent.Right = newNode;
            }
        }

        public bool Contains(T data)
        {
            // Search the tree for a node that contains the data you're looking for
            // THIS IS WHERE THE MAGIC OF BINARY SEARCH TREES COMES OUT!!!!! yay.
            BinaryTreeNode<T> current = Root;

            while (current != null)
            {
                var comparisonResult = _comparer.Compare(current.Data, data);
                if (comparisonResult == 0)
                    // Data match was found
                    return true;
                current = comparisonResult > 0 ? current.Left : current.Right;
            }

            return false;
        }

        public bool Remove(T data)
        {
            // First, ensure that some data exists in this tree
            if (Root == null) return false;

            // Now, try to find the data in the tree
            BinaryTreeNode<T> current = Root, parent = null;
            int comparisonResult = _comparer.Compare(current.Data, data);
            while (comparisonResult != 0)
            {
                if (comparisonResult > 0)
                {
                    // current.Data > data, so if data exists, it's in the left subtree
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    // current.Data < data, so if data exists, it's in the right subtree
                    parent = current;
                    current = current.Right;
                }

                if (current == null)
                    // If current is null, then we didn't find the item to remove
                    return false;
                comparisonResult = _comparer.Compare(current.Data, data);
            }

            // If we haven't exited yet, then we've found the node to remove. We now need to
            // "re-thread" the tree. There are 3 possible cases for this.

            // CASE 1: If current has no right child, then its left child becomes the node
            //         pointed to by the parent
            if (current.Right == null)
            {
                // If parent is null, then we're removing the root.
                if (parent == null)
                    Root = current.Left;
                else
                {
                    comparisonResult = _comparer.Compare(parent.Data, current.Data);
                    if (comparisonResult > 0)
                        // parent.Data > current.Data, so make current's left child a left child of parent
                        parent.Left = current.Left;
                    else if (comparisonResult < 0)
                        // parent.Data < current.Data, so make current's left child a right child of parent
                        parent.Right = current.Left;
                }
            }

            // CASE 2: If current's right child has no left child, then current's right child replaces
            //         current in the tree
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;

                if (parent == null)
                    Root = current.Right;
                else
                {
                    comparisonResult = _comparer.Compare(parent.Data, current.Data);
                    if (comparisonResult > 0)
                        // parent.Data > current.Data, so make current's right child a left child of parent
                        parent.Left = current.Right;
                    else if (comparisonResult < 0)
                        // parent.Data < current.Data, so make current's right child a right child of parent
                        parent.Right = current.Right;
                }
            }

            // CASE 3: If current's right child has a left child, replace current with current's right child's
            //         left-most descendant
            else
            {
                // First, we need to find the right node's left-most child
                BinaryTreeNode<T> leftmost = current.Right.Left, leftmostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // The parent's left subtree becomes the leftmost's right subtree
                leftmostParent.Left = leftmost.Right;

                // Assign leftmost's left and right to current's left and right children
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                    Root = leftmost;
                else
                {
                    comparisonResult = _comparer.Compare(parent.Data, current.Data);
                    if (comparisonResult > 0)
                        // parent.Data > current.Data, so make leftmost a left child of parent
                        parent.Left = leftmost;
                    else if (comparisonResult < 0)
                        // parent.Data < current.Data, so make leftmost a right child of parent
                        parent.Right = leftmost;
                }
            }

            return true;
        }

        public void Clear()
        {
            Root = null;
        }

        /*
         * Binary Search Trees in the real world!!!!
         * -----------------------------------------
         * While binary search trees IDEALLY exhibit sublinear running times for insertion, searches, and deletes, the
         * actual running time is dependent up on the BST's topology, which, itself, is dependent upon the order with
         * which the data is added to the BST. Data that is ordered or near-ordered will cause the BST's topology to
         * resemble a long, skinny tree, rather than a short and fat one. In many real-world scenarios, data is
         * naturally in an ordered or near-ordered state.
         *
         * This demonstrates the problem with BSTs - they can become easily "unbalanced". A balanced binary tree is one
         * that exhibits a good ratio of breadth to depth. A "self-balancing" BST would be one that, as new nodes are
         * added or existing ones deleted, automatically adjusts its topology to maintain an optimal balance. With an
         * ideal balance, the running time for inserts, searches, and deletes, even in the worst case, is log2 n.
         *
         * An additional improvement to this BST implementation would be to add a method for Sort, which would
         * traverse the tree in order (using one of the three traversal approaches) and return an ordered collection.
        */
    }



    [TestFixture]
    public class BinarySearchTrees
    {
        [Test]
        public void ShouldBeAbleToAddListOfIntegersToBinarySearchTreeAndFindThemOrderedForSearch()
        {
            int[] aBunchOfNumbers = { 99, 999, 9999, 77, 7, 14, 9, 1, 5002, 4502, 3502, 4602 };
            var binarySearchTree = new BinarySearchTree<int>(aBunchOfNumbers);

            Assert.That(binarySearchTree.Root.Data, Is.EqualTo(99));
            Assert.That(binarySearchTree.Root.Left.Data, Is.EqualTo(77));
            Assert.That(binarySearchTree.Root.Left.Left.Data, Is.EqualTo(7));
            Assert.That(binarySearchTree.Root.Left.Left.Left.Data, Is.EqualTo(1));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Data, Is.EqualTo(14));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Left.Data, Is.EqualTo(9));
            Assert.That(binarySearchTree.Root.Right.Data, Is.EqualTo(999));
            Assert.That(binarySearchTree.Root.Right.Right.Data, Is.EqualTo(9999));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Data, Is.EqualTo(5002));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Left.Data, Is.EqualTo(4502));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Left.Left.Data, Is.EqualTo(3502));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Left.Right.Data, Is.EqualTo(4602));

            binarySearchTree.Add(27);

            Assert.That(binarySearchTree.Root.Left.Left.Right.Right.Data, Is.EqualTo(27));
        }

        [Test]
        public void ShouldSearchBinarySearchTreeForValues()
        {
            int[] aBunchOfNumbers = { 99, 999, 9999, 77, 7, 14, 9, 1, 1002 };
            var binarySearchTree = new BinarySearchTree<int>(aBunchOfNumbers);

            bool found = binarySearchTree.Contains(14);

            Assert.That(found, Is.True);

            found = binarySearchTree.Contains(4014);

            Assert.That(found, Is.False);
        }

        [Test]
        public void ShouldRemoveNodeWithNoRightChildAndFindItsLeftChildBecomeTheNewNodePointedToByParent()
        {
            int[] aBunchOfNumbers = { 99, 999, 9999, 77, 7, 14, 9, 1, 1002 };
            var binarySearchTree = new BinarySearchTree<int>(aBunchOfNumbers);
            binarySearchTree.Remove(9999);

            Assert.That(binarySearchTree.Contains(9999), Is.False);
            Assert.That(binarySearchTree.Root.Data, Is.EqualTo(99));
            Assert.That(binarySearchTree.Root.Left.Data, Is.EqualTo(77));
            Assert.That(binarySearchTree.Root.Left.Left.Data, Is.EqualTo(7));
            Assert.That(binarySearchTree.Root.Left.Left.Left.Data, Is.EqualTo(1));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Data, Is.EqualTo(14));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Left.Data, Is.EqualTo(9));
            Assert.That(binarySearchTree.Root.Right.Data, Is.EqualTo(999));
            Assert.That(binarySearchTree.Root.Right.Right.Data, Is.EqualTo(1002));
        }

        [Test]
        public void ShouldRemoveNodeWithRightChildHavingNoLeftChildAndFindThatRightChildBecomeTheNewNodePointedToByParent()
        {
            int[] aBunchOfNumbers = { 99, 999, 9999, 77, 7, 14, 9, 1, 1002 };
            var binarySearchTree = new BinarySearchTree<int>(aBunchOfNumbers);
            binarySearchTree.Remove(99);

            Assert.That(binarySearchTree.Contains(99), Is.False);
            Assert.That(binarySearchTree.Root.Data, Is.EqualTo(999));
            Assert.That(binarySearchTree.Root.Left.Data, Is.EqualTo(77));
            Assert.That(binarySearchTree.Root.Left.Left.Data, Is.EqualTo(7));
            Assert.That(binarySearchTree.Root.Left.Left.Left.Data, Is.EqualTo(1));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Data, Is.EqualTo(14));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Left.Data, Is.EqualTo(9));
            Assert.That(binarySearchTree.Root.Right.Data, Is.EqualTo(9999));
            Assert.That(binarySearchTree.Root.Right.Left.Data, Is.EqualTo(1002));
        }

        [Test]
        public void ShouldRemoveNodeWithRightChildHavingALeftChildAndFindThatRightChildsLeftmostDescendantBecomeTheNewNodePointedToByParent()
        {
            int[] aBunchOfNumbers = { 99, 999, 9999, 77, 7, 14, 9, 1, 5002, 4502, 3502, 4602 };
            var binarySearchTree = new BinarySearchTree<int>(aBunchOfNumbers);
            binarySearchTree.Remove(999);

            Assert.That(binarySearchTree.Contains(999), Is.False);
            Assert.That(binarySearchTree.Root.Data, Is.EqualTo(99));
            Assert.That(binarySearchTree.Root.Left.Data, Is.EqualTo(77));
            Assert.That(binarySearchTree.Root.Left.Left.Data, Is.EqualTo(7));
            Assert.That(binarySearchTree.Root.Left.Left.Left.Data, Is.EqualTo(1));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Data, Is.EqualTo(14));
            Assert.That(binarySearchTree.Root.Left.Left.Right.Left.Data, Is.EqualTo(9));
            Assert.That(binarySearchTree.Root.Right.Data, Is.EqualTo(3502));
            Assert.That(binarySearchTree.Root.Right.Right.Data, Is.EqualTo(9999));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Data, Is.EqualTo(5002));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Left.Data, Is.EqualTo(4502));
            Assert.That(binarySearchTree.Root.Right.Right.Left.Left.Right.Data, Is.EqualTo(4602));
        }
    }



    /*
     * CLASSIC REFERENCE HAPPINESS:
     *      • https://msdn.microsoft.com/en-us/library/ms379572(v=vs.80).aspx
     *      • https://code.msdn.microsoft.com/windowsapps/Drawing-Binary-Search-Trees-4c49410f
    */
}