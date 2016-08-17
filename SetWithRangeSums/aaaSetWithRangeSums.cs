using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace SetWithRangeSums
{
    [TestFixture]
    class AaaSetWithRangeSums
    {
        [Test]
        public void Find_Nothing_EmptyTree()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("?", 3));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("Not found");
        }

        [Test]
        public void Add_Node_ToEmptyTree()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 42));

            program.ExecuteQueries();

            var firstTreeNode = program.TreeNodes[0];
            firstTreeNode.Value.ShouldBe(42);
            firstTreeNode.LeftChild.ShouldBeNull();
            firstTreeNode.RightChild.ShouldBeNull();
            firstTreeNode.Parent.ShouldBeNull();
        }

        [Test]
        public void SplayAdd_SmallTree()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 0));
            program.Queries.Add(new QueryTriple("+", 7));

            program.ExecuteQueries();

            var oldRoot = program.TreeNodes[0];
            oldRoot.Value.ShouldBe(0);
            oldRoot.LeftChild.ShouldBeNull();
            oldRoot.RightChild.ShouldBeNull();
            oldRoot.Parent.Value.ShouldBe(7);

            var newRoot = oldRoot.Parent;
            newRoot.Value.ShouldBe(7);
            newRoot.Parent.ShouldBeNull();
            newRoot.LeftChild.Value.ShouldBe(0);
            newRoot.RightChild.ShouldBeNull();
        }

        [Test]
        public void SplayFind_SmallTree()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 0));
            program.Queries.Add(new QueryTriple("+", 7));
            program.Queries.Add(new QueryTriple("?", 7));

            program.ExecuteQueries();

            var oldRoot = program.TreeNodes[0];
            oldRoot.Value.ShouldBe(0);
            oldRoot.LeftChild.ShouldBeNull();
            oldRoot.RightChild.ShouldBeNull();
            oldRoot.Parent.Value.ShouldBe(7);

            var newRoot = oldRoot.Parent;
            newRoot.Value.ShouldBe(7);
            newRoot.Parent.ShouldBeNull();
            newRoot.LeftChild.Value.ShouldBe(0);
            newRoot.RightChild.ShouldBeNull();

            program.QueryResults[0].ShouldBe("Found");
        }

        [Test]
        public void Add_SameNodeMultipleTimes()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 42));
            program.Queries.Add(new QueryTriple("+", 42));
            program.Queries.Add(new QueryTriple("+", 42));

            program.ExecuteQueries();

            program.TreeNodes.Count.ShouldBe(1);
            var root = program.Root;
            root.Value.ShouldBe(42);
            root.LeftChild.ShouldBeNull();
            root.RightChild.ShouldBeNull();
            root.Parent.ShouldBeNull();
        }

        [Test]
        public void Del_ZeroNodes()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("-", 42));

            program.ExecuteQueries();

            program.Root.Value.ShouldBe(-1);
        }

        [Test]
        public void Del_OneNode()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 42));
            program.Queries.Add(new QueryTriple("-", 42));

            program.ExecuteQueries();

            program.Root.Value.ShouldBe(-1);
        }

        [Test]
        public void Del_Node_ThatHasNoRightChild_ParentsRight()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 25));
            program.Queries.Add(new QueryTriple("+", 75));
            program.Queries.Add(new QueryTriple("+", 60));
            program.Queries.Add(new QueryTriple("-", 50));

            program.ExecuteQueries();

            var root = program.Root;
            root.Value.ShouldBe(60);
            root.LeftChild.Value.ShouldBe(25);
            root.RightChild.Value.ShouldBe(75);
            root.Parent.ShouldBeNull();
        }

        [Test]
        public void Del_Node_ThatHasNoRightChild_ParentsLeft()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 25));
            program.Queries.Add(new QueryTriple("+", 75));
            program.Queries.Add(new QueryTriple("+", 12));
            program.Queries.Add(new QueryTriple("-", 25));

            program.ExecuteQueries();

            var root = program.Root;
            root.Value.ShouldBe(50);
            root.LeftChild.Value.ShouldBe(12);
            root.RightChild.Value.ShouldBe(75);

            var rightChild = root.RightChild;
            rightChild.Value.ShouldBe(75);
            rightChild.RightChild.ShouldBeNull();
            rightChild.LeftChild.ShouldBeNull();
            rightChild.Parent.Value.ShouldBe(50);

            var leftChild = root.LeftChild;
            leftChild.Value.ShouldBe(12);
            leftChild.LeftChild.ShouldBeNull();
            leftChild.RightChild.ShouldBeNull();
            leftChild.Parent.Value.ShouldBe(50);
        }

        [Test]
        public void Add_Del_Find_ProblemSetTestCase()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("?", 0));
            program.Queries.Add(new QueryTriple("+", 0));
            program.Queries.Add(new QueryTriple("?", 0));
            program.Queries.Add(new QueryTriple("-", 0));
            program.Queries.Add(new QueryTriple("?", 0));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe(Results.NotFound);
            program.QueryResults[1].ShouldBe(Results.Found);
            program.QueryResults[2].ShouldBe(Results.NotFound);
        }

        [Test]
        public void Find_Nothing_TreeWithNodes()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 42));
            program.Queries.Add(new QueryTriple("?", 3));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("Not found");
        }

        [Test]
        public void Find_Node_TreeWithNodes()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("?", 42));
            program.Queries.Add(new QueryTriple("+", 42));
            program.Queries.Add(new QueryTriple("?", 42));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("Not found");
            program.QueryResults[1].ShouldBe("Found");
        }

        [Test]
        public void Splay_ZigZigRight_ZigZagLeft()
        {
            var program = new Program();

            var great2Grandparent = new TreeNode(-2);
            var greatGrandparent = new TreeNode(-1, null, null, great2Grandparent);
            var grandparent = new TreeNode(0, null, null, greatGrandparent);
            var parent = new TreeNode(2, null, null, grandparent);
            var splay = new TreeNode(3, null, null, parent);

            great2Grandparent.LeftChild = greatGrandparent;
            greatGrandparent.RightChild = grandparent;
            grandparent.RightChild = parent;
            parent.RightChild = splay;

            program.Splay(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(greatGrandparent.Value);
            splay.RightChild.Value.ShouldBe(great2Grandparent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(2);
            parent.LeftChild.Value.ShouldBe(grandparent.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(greatGrandparent.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(parent.Value);
            greatGrandparent.Parent.Value.ShouldBe(splay.Value);

            great2Grandparent.Value.ShouldBe(-2);
            great2Grandparent.LeftChild.ShouldBeNull();
            great2Grandparent.RightChild.ShouldBeNull();
            great2Grandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Splay_ZigZigLeft_ZigZigRight()
        {
            var program = new Program();

            var greatGrandparent = new TreeNode(-1);
            var grandparent = new TreeNode(0, null, null, greatGrandparent);
            var parent = new TreeNode(2, null, null, grandparent);
            var splay = new TreeNode(3, null, null, parent);

            greatGrandparent.RightChild = grandparent;
            grandparent.LeftChild = parent;
            parent.LeftChild = splay;

            program.Splay(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(greatGrandparent.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(2);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Splay_ZigZagRight_ZigLeft()
        {
            var program = new Program();

            var greatGrandparent = new TreeNode(-1);
            var grandparent = new TreeNode(0, null, null, greatGrandparent);
            var parent = new TreeNode(2, null, null, grandparent);
            var splay = new TreeNode(3, null, null, parent);

            greatGrandparent.LeftChild = grandparent;
            grandparent.RightChild = parent;
            parent.LeftChild = splay;

            program.Splay(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(grandparent.Value);
            splay.RightChild.Value.ShouldBe(greatGrandparent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(2);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(greatGrandparent.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.Value.ShouldBe(parent.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsRoot_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(grandparent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Zig_Zag_Right_GransparentIsRoot_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, null, parent, null);
            parent.Parent = grandparent;

            program.ZigZagRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(grandparent.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsNotRoot_GreatgrandparentLeft_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            var greatGrandparent = new TreeNode(-1, grandparent, null, null);
            grandparent.Parent = greatGrandparent;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(grandparent.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.Value.ShouldBe(splay.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.ShouldBeNull();
        }

        [Test]
        public void Zig_Zag_Right_GransparentIsNotRoot_GreatgrandparentRight_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, null, parent, null);
            parent.Parent = grandparent;

            var greatGrandparent = new TreeNode(-1, null, grandparent, null);
            grandparent.Parent = greatGrandparent;

            program.ZigZagRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(grandparent.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(splay.Value);
            greatGrandparent.Parent.ShouldBeNull();
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsNotRoot_GreatgrandparentRight_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            var greatGrandparent = new TreeNode(-1, null, grandparent, null);
            grandparent.Parent = greatGrandparent;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(grandparent.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(splay.Value);
            greatGrandparent.Parent.ShouldBeNull();
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsRoot_SplayWithKids()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var splayLeftChild = new TreeNode(4, null, null, splay);
            splay.LeftChild = splayLeftChild;
            var splayRightChild = new TreeNode(5, null, null, splay);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(grandparent.Value);
            splay.Parent.ShouldBeNull();

            splayLeftChild.Value.ShouldBe(4);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(parent.Value);

            splayRightChild.Value.ShouldBe(5);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(grandparent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.Value.ShouldBe(splayLeftChild.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Zig_Zag_Right_GransparentIsRoot_SplayWithKids()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var splayLeftChild = new TreeNode(4, null, null, splay);
            splay.LeftChild = splayLeftChild;
            var splayRightChild = new TreeNode(5, null, null, splay);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, null, parent, null);
            parent.Parent = grandparent;

            program.ZigZagRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(grandparent.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();

            splayLeftChild.Value.ShouldBe(4);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(grandparent.Value);

            splayRightChild.Value.ShouldBe(5);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(parent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.Value.ShouldBe(splayLeftChild.Value);
            grandparent.Parent.Value.ShouldBe(splay.Value);
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsRoot_ParentAndGrandparentWithChild()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;
            var parentLeftChild = new TreeNode(4, null, null, parent);
            parent.LeftChild = parentLeftChild;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;
            var grandparentRightChild = new TreeNode(2, null, null, grandparent);
            grandparent.RightChild = grandparentRightChild;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(grandparent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(parentLeftChild.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            parentLeftChild.Value.ShouldBe(4);
            parentLeftChild.LeftChild.ShouldBeNull();
            parentLeftChild.RightChild.ShouldBeNull();
            parentLeftChild.Parent.Value.ShouldBe(parent.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.Value.ShouldBe(grandparentRightChild.Value);
            grandparent.Parent.Value.ShouldBe(splay.Value);

            grandparentRightChild.Value.ShouldBe(2);
            grandparentRightChild.LeftChild.ShouldBeNull();
            grandparentRightChild.RightChild.ShouldBeNull();
            grandparentRightChild.Parent.Value.ShouldBe(grandparent.Value);
        }

        [Test]
        public void Zig_Zig_Left_GrandparentIsRoot_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            program.ZigZigLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.ShouldBeNull();
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);
        }

        [Test]
        public void Zig_Zig_Right_GrandparentIsRoot_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, null, parent, null);
            parent.Parent = grandparent;

            program.ZigZigRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.ShouldBeNull();
            splay.Parent.ShouldBeNull();

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(grandparent.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);
        }

        [Test]
        public void Zig_Zig_Left_GrandparentIsNotRoot_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            var greatGrandparent = new TreeNode(-1,grandparent,null,null);
            grandparent.Parent = greatGrandparent;

            program.ZigZigLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.ShouldBeNull();
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.Value.ShouldBe(splay.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.ShouldBeNull();
        }


        [Test]
        public void Zig_Zig_Right_GrandparentIsNotRoot_NoSubtrees()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);
            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, null, parent, null);
            parent.Parent = grandparent;

            var greatGrandparent = new TreeNode(-1, null, grandparent, null);
            grandparent.Parent = greatGrandparent;

            program.ZigZigRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.ShouldBeNull();
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(grandparent.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(splay.Value);
            greatGrandparent.Parent.ShouldBeNull();
        }

        [Test]
        public void Zig_Zig_Left_GrandparentIsRoot_SplayWithLeftAndRightChildren()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var splayLeftChild = new TreeNode(5, null, null, splay);
            splay.LeftChild = splayLeftChild;

            var splayRightChild = new TreeNode(6, null, null, splay);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            program.ZigZigLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(splayLeftChild.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();

            splayLeftChild.Value.ShouldBe(5);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(splay.Value);

            splayRightChild.Value.ShouldBe(6);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(parent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);
        }


        [Test]
        public void Zig_Zig_Right_GrandparentIsRoot_SplayWithLeftAndRightChildren()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var splayLeftChild = new TreeNode(5, null, null, splay);
            splay.LeftChild = splayLeftChild;

            var splayRightChild = new TreeNode(6, null, null, splay);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var grandparent = new TreeNode(0, null, parent, null);
            parent.Parent = grandparent;

            program.ZigZigRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(splayRightChild.Value);
            splay.Parent.ShouldBeNull();

            splayLeftChild.Value.ShouldBe(5);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(parent.Value);

            splayRightChild.Value.ShouldBe(6);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(splay.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(grandparent.Value);
            parent.RightChild.Value.ShouldBe(splayLeftChild.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);
        }

        [Test]
        public void Zig_Zig_Left_GrandparentIsNotRoot_SplayWithLeftAndRightChildren_ParentWithChild_Grandparent_WithChild()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var splayLeftChild = new TreeNode(5, null, null, splay);
            splay.LeftChild = splayLeftChild;

            var splayRightChild = new TreeNode(6, null, null, splay);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, splay, null, null);
            splay.Parent = parent;

            var parentRightChild = new TreeNode(4, null, null, parent);
            parent.RightChild = parentRightChild;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            var grandparentRightChild = new TreeNode(2, null, null, grandparent);
            grandparent.RightChild = grandparentRightChild;

            var greatGrandparent = new TreeNode(-1, grandparent, null, null);
            grandparent.Parent = greatGrandparent;

            program.ZigZigLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(splayLeftChild.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            splayLeftChild.Value.ShouldBe(5);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(splay.Value);

            splayRightChild.Value.ShouldBe(6);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(parent.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            parentRightChild.Value.ShouldBe(4);
            parentRightChild.RightChild.ShouldBeNull();
            parentRightChild.LeftChild.ShouldBeNull();
            parentRightChild.Parent.Value.ShouldBe(grandparent.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.Value.ShouldBe(parentRightChild.Value);
            grandparent.RightChild.Value.ShouldBe(grandparentRightChild.Value);
            grandparent.Parent.Value.ShouldBe(parent.Value);

            grandparentRightChild.Value.ShouldBe(2);
            grandparentRightChild.RightChild.ShouldBeNull();
            grandparentRightChild.LeftChild.ShouldBeNull();
            grandparentRightChild.Parent.Value.ShouldBe(grandparent.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.Value.ShouldBe(splay.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.ShouldBeNull();
        }

        [Test]
        public void Zig_Zig_Right_GrandparentIsNotRoot_SplayWithLeftAndRightChildren_ParentWithChild_Grandparent_WithChild()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null);

            var splayLeftChild = new TreeNode(5, null, null, splay);
            splay.LeftChild = splayLeftChild;

            var splayRightChild = new TreeNode(6, null, null, splay);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, null, splay, null);
            splay.Parent = parent;

            var parentLeftChild = new TreeNode(4, null, null, parent);
            parent.LeftChild = parentLeftChild;

            var grandparent = new TreeNode(0, parent, null, null);
            parent.Parent = grandparent;

            var grandparentLeftChild = new TreeNode(2, null, null, grandparent);
            grandparent.LeftChild = grandparentLeftChild;

            var greatGrandparent = new TreeNode(-1, grandparent, null, null);
            grandparent.Parent = greatGrandparent;

            program.ZigZigRight(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(splayRightChild.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);

            splayLeftChild.Value.ShouldBe(5);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(parent.Value);

            splayRightChild.Value.ShouldBe(6);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(splay.Value);

            parent.Value.ShouldBe(1);
            parent.LeftChild.Value.ShouldBe(grandparent.Value);
            parent.RightChild.Value.ShouldBe(splayLeftChild.Value);
            parent.Parent.Value.ShouldBe(splay.Value);

            parentLeftChild.Value.ShouldBe(4);
            parentLeftChild.RightChild.ShouldBeNull();
            parentLeftChild.LeftChild.ShouldBeNull();
            parentLeftChild.Parent.Value.ShouldBe(grandparent.Value);

            grandparent.Value.ShouldBe(0);
            grandparent.LeftChild.Value.ShouldBe(grandparentLeftChild.Value);
            grandparent.RightChild.Value.ShouldBe(parentLeftChild.Value);
            grandparent.Parent.Value.ShouldBe(parent.Value);

            grandparentLeftChild.Value.ShouldBe(2);
            grandparentLeftChild.RightChild.ShouldBeNull();
            grandparentLeftChild.LeftChild.ShouldBeNull();
            grandparentLeftChild.Parent.Value.ShouldBe(grandparent.Value);

            greatGrandparent.Value.ShouldBe(-1);
            greatGrandparent.LeftChild.Value.ShouldBe(splay.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.ShouldBeNull();
        }


        [Test]
        public void ZigLeft_NoSubTrees()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var parentNode = new TreeNode(0, splayNode, null, null);
            splayNode.Parent = parentNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.ShouldBeNull();
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
        }

        [Test]
        public void ZigRight_NoSubTrees()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var parentNode = new TreeNode(0, null, splayNode, null);
            splayNode.Parent = parentNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.ShouldBeNull();
            splayNode.Parent.ShouldBeNull();

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
        }

        [Test]
        public void ZigLeft_RightNodeOnParent()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var otherNode = new TreeNode(2, null, null, null);
            var parentNode = new TreeNode(0, splayNode, otherNode, null);
            splayNode.Parent = parentNode;
            otherNode.Parent = parentNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.ShouldBeNull();
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.Value.ShouldBe(otherNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);

            otherNode.Value.ShouldBe(2);
            otherNode.LeftChild.ShouldBeNull();
            otherNode.RightChild.ShouldBeNull();
            otherNode.Parent.Value.ShouldBe(parentNode.Value);
        }

        [Test]
        public void ZigRight_LeftNodeOnParent()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var otherNode = new TreeNode(2, null, null, null);
            var parentNode = new TreeNode(0, otherNode, splayNode, null);
            splayNode.Parent = parentNode;
            otherNode.Parent = parentNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.ShouldBeNull();
            splayNode.Parent.ShouldBeNull();

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.Value.ShouldBe(otherNode.Value);
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.Value.ShouldBe(splayNode.Value);

            otherNode.Value.ShouldBe(2);
            otherNode.LeftChild.ShouldBeNull();
            otherNode.RightChild.ShouldBeNull();
            otherNode.Parent.Value.ShouldBe(parentNode.Value);
        }

        [Test]
        public void ZigLeft_LeftNodeOnSplayNode()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var splayChildNode = new TreeNode(3, null, null, splayNode);
            var parentNode = new TreeNode(0, splayNode, null, null);
            splayNode.Parent = parentNode;
            splayNode.LeftChild = splayChildNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(splayChildNode.Value);
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();

            splayChildNode.Value.ShouldBe(3);
            splayChildNode.LeftChild.ShouldBeNull();
            splayChildNode.RightChild.ShouldBeNull();
            splayChildNode.Parent.Value.ShouldBe(splayNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
        }

        [Test]
        public void ZigRight_RightNodeOnSplayNode()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var splayChildNode = new TreeNode(3, null, null, splayNode);
            var parentNode = new TreeNode(0, null, splayNode, null);
            splayNode.Parent = parentNode;
            splayNode.RightChild = splayChildNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.Value.ShouldBe(splayChildNode.Value);
            splayNode.Parent.ShouldBeNull();

            splayChildNode.Value.ShouldBe(3);
            splayChildNode.LeftChild.ShouldBeNull();
            splayChildNode.RightChild.ShouldBeNull();
            splayChildNode.Parent.Value.ShouldBe(splayNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
        }

        [Test]
        public void ZigLeft_RightNodeOnSplayNode()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var splayNodeRightChild = new TreeNode(3, null, null, splayNode);
            splayNode.RightChild = splayNodeRightChild;
            var parentNode = new TreeNode(0, splayNode, null, null);
            splayNode.Parent = parentNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.ShouldBeNull();
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();

            splayNodeRightChild.Value.ShouldBe(3);
            splayNodeRightChild.LeftChild.ShouldBeNull();
            splayNodeRightChild.RightChild.ShouldBeNull();
            splayNodeRightChild.Parent.Value.ShouldBe(parentNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.Value.ShouldBe(splayNodeRightChild.Value);
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
        }

        [Test]
        public void ZigRight_LeftNodeOnSplayNode()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var splayNodeLeftChild = new TreeNode(3, null, null, splayNode);
            splayNode.LeftChild = splayNodeLeftChild;
            var parentNode = new TreeNode(0, null, splayNode, null);
            splayNode.Parent = parentNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.ShouldBeNull();
            splayNode.Parent.ShouldBeNull();

            splayNodeLeftChild.Value.ShouldBe(3);
            splayNodeLeftChild.LeftChild.ShouldBeNull();
            splayNodeLeftChild.RightChild.ShouldBeNull();
            splayNodeLeftChild.Parent.Value.ShouldBe(parentNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.Value.ShouldBe(splayNodeLeftChild.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
        }

        [Test]
        public void ZigLeft_RightNodeOnSplayNode_RightNodeOnParent()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var splayRightChildNode = new TreeNode(3, null, null, splayNode);
            splayNode.RightChild = splayRightChildNode;
            var parentNode = new TreeNode(0, splayNode, null, null);
            splayNode.Parent = parentNode;
            var parentRightChildNode = new TreeNode(4, null, null, parentNode);
            parentNode.RightChild = parentRightChildNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.ShouldBeNull();
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();

            splayRightChildNode.Value.ShouldBe(3);
            splayRightChildNode.LeftChild.ShouldBeNull();
            splayRightChildNode.RightChild.ShouldBeNull();
            splayRightChildNode.Parent.Value.ShouldBe(parentNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.Value.ShouldBe(splayRightChildNode.Value);
            parentNode.RightChild.Value.ShouldBe(parentRightChildNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);

            parentRightChildNode.Value.ShouldBe(4);
            parentRightChildNode.LeftChild.ShouldBeNull();
            parentRightChildNode.RightChild.ShouldBeNull();
            parentRightChildNode.Parent.Value.ShouldBe(parentNode.Value);
        }

        [Test]
        public void ZigRight_LeftNodeOnSplayNode_LeftNodeOnParent()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var splayLeftChildNode = new TreeNode(3, null, null, splayNode);
            splayNode.LeftChild = splayLeftChildNode;
            var parentNode = new TreeNode(0, null, splayNode, null);
            splayNode.Parent = parentNode;
            var parentLeftChildNode = new TreeNode(4, null, null, parentNode);
            parentNode.LeftChild = parentLeftChildNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.ShouldBeNull();
            splayNode.Parent.ShouldBeNull();

            splayLeftChildNode.Value.ShouldBe(3);
            splayLeftChildNode.LeftChild.ShouldBeNull();
            splayLeftChildNode.RightChild.ShouldBeNull();
            splayLeftChildNode.Parent.Value.ShouldBe(parentNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.Value.ShouldBe(parentLeftChildNode.Value);
            parentNode.RightChild.Value.ShouldBe(splayLeftChildNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);

            parentLeftChildNode.Value.ShouldBe(4);
            parentLeftChildNode.LeftChild.ShouldBeNull();
            parentLeftChildNode.RightChild.ShouldBeNull();
            parentLeftChildNode.Parent.Value.ShouldBe(parentNode.Value);
        }

        [Test]
        public void ZigLeft_RightAndLeftNodesOnSplayNode_RightNodeOnParent()
        {
            var program = new Program();

            var splayNode = new TreeNode(1, null, null, null);
            var splayRightChildNode = new TreeNode(3, null, null, splayNode);
            splayNode.RightChild = splayRightChildNode;
            var splayLeftChildNode = new TreeNode(5, null, null, splayNode);
            splayNode.LeftChild = splayLeftChildNode;

            var parentNode = new TreeNode(0, splayNode, null, null);
            splayNode.Parent = parentNode;
            var parentRightChildNode = new TreeNode(4, null, null, parentNode);
            parentNode.RightChild = parentRightChildNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(splayLeftChildNode.Value);
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();

            splayRightChildNode.Value.ShouldBe(3);
            splayRightChildNode.LeftChild.ShouldBeNull();
            splayRightChildNode.RightChild.ShouldBeNull();
            splayRightChildNode.Parent.Value.ShouldBe(parentNode.Value);

            splayLeftChildNode.Value.ShouldBe(5);
            splayLeftChildNode.LeftChild.ShouldBeNull();
            splayLeftChildNode.RightChild.ShouldBeNull();
            splayLeftChildNode.Parent.Value.ShouldBe(splayNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.Value.ShouldBe(splayRightChildNode.Value);
            parentNode.RightChild.Value.ShouldBe(parentRightChildNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);

            parentRightChildNode.Value.ShouldBe(4);
            parentRightChildNode.LeftChild.ShouldBeNull();
            parentRightChildNode.RightChild.ShouldBeNull();
            parentRightChildNode.Parent.Value.ShouldBe(parentNode.Value);
        }

        [Test]
        public void ZigRight_RightAndLeftNodesOnSplayNode_LeftNodeOnParent()
        {
            var program = new Program();

            var splayNode = new TreeNode(1, null, null, null);
            var splayRightChildNode = new TreeNode(3, null, null, splayNode);
            splayNode.RightChild = splayRightChildNode;
            var splayLeftChildNode = new TreeNode(5, null, null, splayNode);
            splayNode.LeftChild = splayLeftChildNode;

            var parentNode = new TreeNode(0, null, splayNode, null);
            splayNode.Parent = parentNode;
            var parentLeftChildNode = new TreeNode(4, null, null, parentNode);
            parentNode.LeftChild = parentLeftChildNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.Value.ShouldBe(splayRightChildNode.Value);
            splayNode.Parent.ShouldBeNull();

            splayRightChildNode.Value.ShouldBe(3);
            splayRightChildNode.LeftChild.ShouldBeNull();
            splayRightChildNode.RightChild.ShouldBeNull();
            splayRightChildNode.Parent.Value.ShouldBe(splayNode.Value);

            splayLeftChildNode.Value.ShouldBe(5);
            splayLeftChildNode.LeftChild.ShouldBeNull();
            splayLeftChildNode.RightChild.ShouldBeNull();
            splayLeftChildNode.Parent.Value.ShouldBe(parentNode.Value);

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.Value.ShouldBe(parentLeftChildNode.Value);
            parentNode.RightChild.Value.ShouldBe(splayLeftChildNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);

            parentLeftChildNode.Value.ShouldBe(4);
            parentLeftChildNode.LeftChild.ShouldBeNull();
            parentLeftChildNode.RightChild.ShouldBeNull();
            parentLeftChildNode.Parent.Value.ShouldBe(parentNode.Value);
        }

        [Test]
        public void DetermineZigZag_None()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.None);
        }

        [Test]
        public void DetermineZigZag_Zig_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, splayNode, null, null);
            splayNode.Parent = parentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigLeft);
        }

        [Test]
        public void DetermineZigZag_Zig_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, null, splayNode, null);
            splayNode.Parent = parentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigRight);
        }

        [Test]
        public void DetermineZigZag_ZigZig_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, splayNode, null, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, parentNode, null, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZigLeft);
        }

        [Test]
        public void DetermineZigZag_ZigZig_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, null, splayNode, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, null, parentNode, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZigRight);
        }

        [Test]
        public void DetermineZigZag_Zig_Zag_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, null, splayNode, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, parentNode, null, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZagLeft);
        }

        [Test]
        public void DetermineZigZag_Zig_Zag_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, splayNode, null, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, null, parentNode, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZagRight);
        }

        [Test, TestCaseSource(nameof(Raw))]
        public void TransformArrayIntoInputTriple_Tests(string caseName, object[] input,
            List<QueryTriple> expected)
        {
            var program = new Program();
            program.AddRawInputToList(input);

            var operation = (string)input[0];
            var low = (int)input[1];
            var high = -1;
            if (input.Length == 3)
                high = (int)input[2];

            var triple = program.Queries[0];
            triple.Operation.ShouldBe(operation);
            triple.Low.ShouldBe(low);
            triple.High.ShouldBe(high);
        }

        private static readonly object[] Raw =
        {
                new object[] { "A", new object[] {"+",1},  new List<QueryTriple> {
                        new QueryTriple("+", 1, -1)
                    }
                },
                new object[] { "B", new object[] {"?",5},  new List<QueryTriple> {
                        new QueryTriple("?", 5, -1)
                    }
                },
                new object[] { "C", new object[] {"s",1,2},  new List<QueryTriple> {
                        new QueryTriple("s", 1, 2)
                    }
                },
                new object[] { "D", new object[] {"-",1},  new List<QueryTriple> {
                        new QueryTriple("-", 1, -1)
                    }
                },
                new object[] { "E", new object[] {"s",999999999,1000000000},  new List<QueryTriple> {
                        new QueryTriple("s",999999999,1000000000)
                    }
                },
        };
    }
}
