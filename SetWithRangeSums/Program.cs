using System;
using System.Collections.Generic;
using System.Linq;

namespace SetWithRangeSums
{
    public class Program
    {
        public List<QueryTriple> Queries { get; set; }
        public List<TreeNode> TreeNodes { get; set; }
        public TreeNode Root { get; set; }
        public List<string> QueryResults { get; set; }
        public long M { get; set; }
        public long RunningSum { get; set; }

        public Program()
        {
            Queries = new List<QueryTriple>();
            TreeNodes = new List<TreeNode>();
            QueryResults = new List<string>();
            Root = new TreeNode();
            RunningSum = 0;
            M = 1000000001;
        }

        internal void ExecuteQueries()
        {
            foreach (var query in Queries)
            {
                var operation = query.Operation;
                var operand = (query.Low + RunningSum) % M;

                var highRange = (query.High + RunningSum) % M;
                if (operation != Operations.Sum)
                    highRange = 0;

                switch (operation)
                {
                    case Operations.Add:
                        if (!TreeNodes.Any() || Root == null)
                        {
                            TreeNodes.Add(new TreeNode(operand));
                            Root = TreeNodes[0];
                            UpdateSum(Root);
                            break;
                        }
                        SplayInsert(operand, Root);
                        break;
                    case Operations.Find:
                        if (!TreeNodes.Any())
                        {
                            QueryResults.Add(Results.NotFound);
                            break;
                        }
                        var foundNode = SplayFind(operand, Root);
                        if (foundNode == null || foundNode.Value != operand)
                        {
                            QueryResults.Add(Results.NotFound);
                            break;
                        }
                        QueryResults.Add(Results.Found);
                        break;
                    case Operations.Del:
                        if (!TreeNodes.Any())
                            break;
                        SplayDel(operand);
                        break;
                    case Operations.Sum:
                        if (!TreeNodes.Any())
                        {
                            QueryResults.Add("0");
                            break;
                        }
                        var rangeSum = SumRange(operand, highRange);
                        QueryResults.Add(rangeSum.ToString());
                        RunningSum = rangeSum;
                        break;
                }
            }
        }

        public void SplayDel(long deleteTerm)
        {
            if (Root == null)
                return;

            var nodeToDelete = Find(deleteTerm, Root);
            var replacementNode = Next(nodeToDelete);
            Splay(replacementNode);
            Splay(nodeToDelete);

            Del(Root);
        }

        private void Del(TreeNode root)
        {
            if (root == null)
                return;

            var rightChild = root.RightChild;
            var leftChild = root.LeftChild;

            if (rightChild == null && leftChild == null)
            {
                Root = null;
                DeleteNode(root);
            }

            TreeNode replacementNode;
            if (rightChild == null)
            {
                replacementNode = leftChild;
                if (leftChild != null)
                    leftChild.Parent = null;

                UpdateSum(leftChild);
                Root = leftChild;
            }
            else
            {
                replacementNode = Next(root);
                replacementNode.Parent = null;
                
                if (leftChild != null)
                {
                    replacementNode.LeftChild = leftChild;
                    leftChild.Parent = replacementNode;
                }

                if (replacementNode != rightChild)
                {
                    replacementNode.RightChild = rightChild;
                    rightChild.Parent = replacementNode;
                }

                UpdateSum(replacementNode);
                Root = replacementNode;
            }

            DeleteNode(root);
        }

        private void DeleteNode(TreeNode root)
        {
            TreeNodes.Remove(root);
            root.LeftChild = null;
            root.RightChild = null;
            root.Parent = null;
        }

        private TreeNode Next(TreeNode node)
        {
            if (node.RightChild != null)
            {
                var leftDescendant = LeftDescendant(node.RightChild);
                return leftDescendant;
            }
            var rightAncestor = RightAncestor(node);
            return  rightAncestor;
        }

        private TreeNode RightAncestor(TreeNode node)
        {
            if (node.Parent == null)
                return node;
            return node.Value < node.Parent.Value ? node.Parent : RightAncestor(node.Parent);
        }

        private TreeNode LeftDescendant(TreeNode node)
        {
            return node.LeftChild == null ? node : LeftDescendant(node.LeftChild);
        }

        public void SplayInsert(long insertionTerm, TreeNode root)
        {
            Insert(insertionTerm, root);
            SplayFind(insertionTerm, root);
        }

        internal void Insert(long insertionTerm, TreeNode root)
        {
            var parent = Find(insertionTerm, root);

            if (parent.Value == insertionTerm)
                return;

            var newNode = new TreeNode(insertionTerm, null, null, parent);

            TreeNodes.Add(newNode);

            if (insertionTerm < parent.Value && parent.LeftChild == null)
                parent.LeftChild = newNode;
            else
                parent.RightChild = newNode;

            UpdateSum(newNode);
            UpdateSum(root);
            Root = root;
        }

        public TreeNode SplayFind(long searchTerm, TreeNode root)
        {                
            var foundNode = Find(searchTerm, root);

            if (foundNode == null)
                return null;

            Splay(foundNode);
            Root = foundNode;
            return foundNode;
        }

        internal TreeNode Find(long searchTerm, TreeNode root)
        {
            if (root == null)
                return null;

            var rootVal = root.Value;
            if (rootVal == searchTerm)
                return root;

            if (rootVal > searchTerm)
                return root.LeftChild != null ? Find(searchTerm, root.LeftChild) : root;
            return root.RightChild != null ? Find(searchTerm, root.RightChild) : root;
        }

        public void Splay(TreeNode inputNode)
        {
            switch (DetermineZigZigZag(inputNode))
            {
                case ZiggaZigAh.ZigLeft:
                    ZigLeft(inputNode);
                    break;
                case ZiggaZigAh.ZigRight:
                    ZigRight(inputNode);
                    break;
                case ZiggaZigAh.ZigZigLeft:
                    ZigZigLeft(inputNode);
                    break;
                case ZiggaZigAh.ZigZigRight:
                    ZigZigRight(inputNode);
                    break;
                case ZiggaZigAh.ZigZagLeft:
                    ZigZagLeft(inputNode);
                    break;
                case ZiggaZigAh.ZigZagRight:
                    ZigZagRight(inputNode);
                    break;
            }

            if (inputNode.Parent != null)
                Splay(inputNode);

            Root = inputNode;
        }

        internal void ZigZagLeft(TreeNode splayNode)
        {
            var splayLeftChild = splayNode.LeftChild;
            var splayRightChild = splayNode.RightChild;
            var parent = splayNode.Parent;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.Parent = null;
            if (greatGrandparent != null)
            {
                splayNode.Parent = greatGrandparent;

                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;
            }

            splayNode.LeftChild = parent;
            splayNode.RightChild = grandparent;

            parent.Parent = splayNode;
            parent.RightChild = null;
            if (splayLeftChild != null)
            {
                parent.RightChild = splayLeftChild;
                splayLeftChild.Parent = parent;
            }

            grandparent.Parent = splayNode;
            grandparent.LeftChild = null;
            if (splayRightChild != null)
            {
                grandparent.LeftChild = splayRightChild;
                splayRightChild.Parent = grandparent;
            }

            UpdateSum(grandparent);
            UpdateSum(parent);
            UpdateSum(splayNode);
        }

        internal void ZigZagRight(TreeNode splayNode)
        {
            var splayLeftChild = splayNode.LeftChild;
            var splayRightChild = splayNode.RightChild;
            var parent = splayNode.Parent;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.Parent = null;
            if (greatGrandparent != null)
            {
                splayNode.Parent = greatGrandparent;
                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;
            }

            splayNode.LeftChild = grandparent;
            splayNode.RightChild = parent;

            parent.Parent = splayNode;
            parent.LeftChild = null;
            if (splayRightChild != null)
            {
                parent.LeftChild = splayRightChild;
                splayRightChild.Parent = parent;
            }

            grandparent.Parent = splayNode;
            grandparent.RightChild = null;
            if (splayLeftChild != null)
            {
                grandparent.RightChild = splayLeftChild;
                splayLeftChild.Parent = grandparent;
            }

            UpdateSum(grandparent);
            UpdateSum(parent);
            UpdateSum(splayNode);
        }

        internal void ZigZigLeft(TreeNode splayNode)
        {
            var splayNodeRightChild = splayNode.RightChild;
            var parent = splayNode.Parent;
            var parentRightChild = parent.RightChild;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.RightChild = parent;
            splayNode.Parent = null;

            parent.LeftChild = splayNodeRightChild;

            if (parentRightChild != null)
            {
                parentRightChild.Parent = grandparent;
                grandparent.LeftChild = parentRightChild;
            }
            else
                grandparent.LeftChild = null;

            parent.RightChild = grandparent;
            parent.Parent = splayNode;

            if (splayNodeRightChild != null)
                splayNodeRightChild.Parent = parent;

            grandparent.Parent = parent;

            if (greatGrandparent != null)
            {
                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;

                splayNode.Parent = greatGrandparent;
            }

            UpdateSum(grandparent);
            UpdateSum(parent);
            UpdateSum(splayNode);
        }

        internal void ZigZigRight(TreeNode splayNode)
        {
            var splayNodeLeftChild = splayNode.LeftChild;
            var parent = splayNode.Parent;
            var parentLeftChild = parent.LeftChild;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.LeftChild = parent;
            splayNode.Parent = null;

            parent.RightChild = splayNodeLeftChild;

            grandparent.RightChild = null;
            if (parentLeftChild != null)
            {
                parentLeftChild.Parent = grandparent;
                grandparent.RightChild = parentLeftChild;
            }
            else
                grandparent.LeftChild = null;

            parent.LeftChild = grandparent;
            parent.Parent = splayNode;

            if (splayNodeLeftChild != null)
                splayNodeLeftChild.Parent = parent;

            grandparent.Parent = parent;

            if (greatGrandparent != null)
            {
                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;

                splayNode.Parent = greatGrandparent;
            }

            UpdateSum(grandparent);
            UpdateSum(parent);
            UpdateSum(splayNode);
        }

        internal void ZigLeft(TreeNode splayNode)
        {
            var parentNode = splayNode.Parent;
            parentNode.LeftChild = splayNode.RightChild;
            parentNode.Parent = splayNode;

            var splayNodeRightChild = splayNode.RightChild;
            if (splayNodeRightChild != null)
                splayNodeRightChild.Parent = parentNode;

            splayNode.RightChild = parentNode;
            splayNode.Parent = null;

            UpdateSum(parentNode);
            UpdateSum(splayNode);
            Root = splayNode;
        }

        internal void ZigRight(TreeNode splayNode)
        {
            var parentNode = splayNode.Parent;
            parentNode.RightChild = splayNode.LeftChild;
            parentNode.Parent = splayNode;

            var splayNodeLeftChild = splayNode.LeftChild;
            if (splayNodeLeftChild != null)
                splayNodeLeftChild.Parent = parentNode;

            splayNode.LeftChild = parentNode;
            splayNode.Parent = null;

            UpdateSum(parentNode);
            UpdateSum(splayNode);
            Root = splayNode;
        }

        internal void AddRawInputToList(string[] input)
        {
            var operation = input[0];
            var low = int.Parse(input[1]);
            var high = -1;

            if (input.Length == 3)
                high = int.Parse(input[2]);

            Queries.Add(new QueryTriple(operation, low, high));
        }

        public string DetermineZigZigZag(TreeNode splayNode)
        {
            if (splayNode.Parent == null)
                return "none";

            string zigZigZag;

            var parentNode = splayNode.Parent;
            if (!NodeHasGrandparent(splayNode))
            {
                if (parentNode.LeftChild == splayNode)
                    return ZiggaZigAh.ZigLeft;
                return ZiggaZigAh.ZigRight;
            }
            var grandparentNode = GetGrandparentNode(splayNode);

            var splayNodeIsWhichChildOfParent = "left";
            if (parentNode.RightChild == splayNode)
                splayNodeIsWhichChildOfParent = "right";

            var parentNodeIsWhichChildOfParent = "left";
            if (grandparentNode.RightChild == parentNode)
                parentNodeIsWhichChildOfParent = "right";

            if (splayNodeIsWhichChildOfParent == "left" && parentNodeIsWhichChildOfParent == "left")
                zigZigZag = ZiggaZigAh.ZigZigLeft;
            else if (splayNodeIsWhichChildOfParent == "right" && parentNodeIsWhichChildOfParent == "right")
                zigZigZag = ZiggaZigAh.ZigZigRight;
            else if (splayNodeIsWhichChildOfParent == "left" && parentNodeIsWhichChildOfParent == "right")
                zigZigZag = ZiggaZigAh.ZigZagRight;
            else
                zigZigZag = ZiggaZigAh.ZigZagLeft;
            return zigZigZag;
        }

        private static TreeNode GetGrandparentNode(TreeNode parentNode)
        {
            return parentNode.Parent.Parent;
        }

        public bool NodeHasGrandparent(TreeNode node)
        {
            if (node.Parent != null)
            {
                return node.Parent.Parent != null;
            }
            return false;
        }

        public SplitRoots SplaySplit(long searchTerm, TreeNode node)
        {
            if (node == null)
                return new SplitRoots();

            var foundNode = Find(searchTerm, node);
            Splay(foundNode);
            var roots = Split(searchTerm, foundNode);

            var leftRoot = roots.LeftRoot;
            if (leftRoot != null)
            {
                leftRoot.Parent = null;
                UpdateSum(leftRoot);
            }

            var rightRoot = roots.RightRoot;
            if (rightRoot != null)
            {
                rightRoot.Parent = null;
                UpdateSum(rightRoot);
            }

            return roots;
        }

        public SplitRoots Split(long searchTerm, TreeNode rootNode)
        {
            if (rootNode == null)
                return new SplitRoots();

            if (searchTerm <= rootNode.Value)
            {
                var splitRoots = Split(searchTerm, rootNode.LeftChild);
                var mergedGreaterThanRoot = MergeWithRoot(splitRoots.RightRoot, 
                    rootNode.RightChild, rootNode);
                return new SplitRoots(splitRoots.LeftRoot, mergedGreaterThanRoot);
            }
            else
            {
                var splitRoots = Split(searchTerm, rootNode.RightChild);
                var mergedLessThanRoot = MergeWithRoot(rootNode.LeftChild, 
                    splitRoots.LeftRoot, rootNode);
                return new SplitRoots(mergedLessThanRoot, splitRoots.RightRoot);
            }
        }

        public TreeNode MergeWithRoot(TreeNode lessThanRoot, 
            TreeNode greaterThanRoot, TreeNode newRoot)
        {
            newRoot.LeftChild = lessThanRoot;
            newRoot.RightChild = greaterThanRoot;

            if (lessThanRoot != null)
                lessThanRoot.Parent = newRoot;

            if (greaterThanRoot !=null)
                greaterThanRoot.Parent = newRoot;

            UpdateSum(newRoot);
            return newRoot;
        }

        public TreeNode Merge(TreeNode leftRoot, TreeNode rightRoot)
        {
            if (leftRoot == null)
                return rightRoot;

            if (rightRoot == null)
                return leftRoot;

            var smallestNodeInRightTree = Find(int.MinValue, rightRoot);
            Splay(smallestNodeInRightTree);
            
            if (leftRoot.LeftChild == null && leftRoot.RightChild == null)
                leftRoot = null;

            smallestNodeInRightTree.LeftChild = leftRoot;

            if (leftRoot != null)
                leftRoot.Parent = rightRoot;

            UpdateSum(smallestNodeInRightTree);
            Root = smallestNodeInRightTree;

            return smallestNodeInRightTree;
        }

        public void UpdateSum(TreeNode node)
        {
            long leftSubtreeSum = 0;
            if (node != null && node.LeftChild != null)
                leftSubtreeSum = node.LeftChild.SubtreeSum;

            long rightSubtreeSum = 0;
            if (node != null && node.RightChild != null)
                rightSubtreeSum = node.RightChild.SubtreeSum;

            if (node != null) node.SubtreeSum = (node.Value ?? 0) + leftSubtreeSum + rightSubtreeSum;
        }

        public long SumRange(long lowerBound, long upperBound)
        {
            var leftAndMiddleRoots = SplaySplit(lowerBound, Root);
            var middleAndRightRoots = SplaySplit(upperBound + 1, leftAndMiddleRoots.RightRoot);

            long sum = 0;
            if (middleAndRightRoots.LeftRoot != null)
                sum = middleAndRightRoots.LeftRoot.SubtreeSum;


            Merge(leftAndMiddleRoots.LeftRoot, middleAndRightRoots.LeftRoot);
            Merge(middleAndRightRoots.LeftRoot, middleAndRightRoots.RightRoot);

            if (middleAndRightRoots.LeftRoot != null)
                Root = middleAndRightRoots.LeftRoot;
            else if (middleAndRightRoots.RightRoot != null)
                Root = middleAndRightRoots.RightRoot;
                
            return sum;
        }

        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################

        public void ReadData()
        {
            var numQueries = Console.ReadLine().Split(' ')
                .Select(n => Convert.ToInt64(n)).ToArray()[0];
            for (long i = 0; i < numQueries; i++)
            {
                var query = Console.ReadLine().Split(' ')
                    .Select(n => Convert.ToString(n)).ToArray();
                AddRawInputToList(query);
            }
        }

        public void WriteResponse()
        {
            foreach (var queryResult in QueryResults)
            {
                Console.WriteLine(queryResult);
            }
            //Console.ReadLine();
        }

        public void Run()
        {
            ReadData();
            ExecuteQueries();
            WriteResponse();
        }

        static void Main()
        {
            new Program().Run();
        }

        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
        // ######################################################################
    }

    public static class Results
    {
        public const string Found = "Found";
        public const string NotFound = "Not found";
    }

    public static class ZiggaZigAh
    {
        public const string ZigLeft = "zig left";
        public const string ZigRight = "zig right";
        public const string ZigZigLeft = "zigzig left";
        public const string ZigZigRight = "zigzig right";
        public const string ZigZagLeft = "zigzag left";
        public const string ZigZagRight = "zigzag right";
        public const string Error = "hi, something went wrong";
        public const string None = "none";
    }

    public static class Operations
    {
        public const string Add = "+";
        public const string Del = "-";
        public const string Find = "?";
        public const string Sum = "s";
    }

    public class TreeNode
    {
        public long? Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public TreeNode Parent { get; set; }
        public long SubtreeSum { get; set; }

        public TreeNode(long val = -1, TreeNode left = null, 
            TreeNode right = null, TreeNode parent = null, long subtreeSum = 0)
        {
            Value = val;
            LeftChild = left;
            RightChild = right;
            Parent = parent;
            SubtreeSum = subtreeSum;
        }
    }

    public class SplitRoots
    {
        public TreeNode LeftRoot { get; set; }
        public TreeNode RightRoot { get; set; }

        public SplitRoots(TreeNode left = null, TreeNode right = null)
        {
            LeftRoot = left;
            RightRoot = right;
        }
    }

    public class QueryTriple
    {
        public string Operation { get; set; }
        public long Low { get; set; }
        public long High { get; set; }

        public QueryTriple(string op, long low, long high = -1)
        {
            Operation = op;
            Low = low;
            High = high;
        }
    }
}
