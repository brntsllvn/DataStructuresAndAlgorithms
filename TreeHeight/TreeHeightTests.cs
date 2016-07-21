//using NUnit.Framework;
//using Shouldly;
//using System.Linq;

//namespace TreeHeight
//{
//    [TestFixture]
//    class TreeHeightTests
//    {
//        [TestCase("A", 1, new[] { -1 }, 1)]
//        [TestCase("B", 2, new[] { -1, 0 }, 2)]
//        [TestCase("C", 3, new[] { -1, 0, 1 }, 3)]
//        [TestCase("D", 4, new[] { -1, 0, 1, 2 }, 4)]
//        [TestCase("E", 5, new[] { -1, 0, 1, 2, 3 }, 5)]
//        [TestCase("F", 6, new[] { -1, 0, 1, 2, 3, 4 }, 6)]
//        [TestCase("G", 5, new[] { 4, -1, 4, 1, 1 }, 3)]
//        //[TestCase("H", 5, new[] { -1, 0, 4, 0, 3 }, 4)]
//        public void Test_1(string caseName, int numberVertices, int[] input, int expected)
//        {
//            var f0 = new Launcher();
//            var tree = f0.ConstructTreeFromArray(input);
//            var root = tree.FirstOrDefault(node => node.Parent == null);
//            var treeHeight = f0.CalculateTreeHeight(root);
//            treeHeight.ShouldBe(expected);
//        }

//        [Test]
//        public void Large_Input()
//        {
//            var expected = 100000;

//            var large = new int[expected];
//            large[0] = -1;
//            for (int i = 0; i < expected; i++)
//                large[i] = i - 1;

//            var f0 = new Launcher();
//            var tree = f0.ConstructTreeFromArray(large);
//            var root = tree.FirstOrDefault(node => node.Parent == null);
//            var treeHeight = f0.CalculateTreeHeight(root);
//            treeHeight.ShouldBe(expected);
//        }


//    }

//    //[TestCase("A", 1, new[] { -1 }, 1)]
//    //[TestCase("B", 2, new[] { -1, 0 }, 2)]
//    //[TestCase("C", 3, new[] { -1, 0, 1 }, 3)]
//    //[TestCase("D", 4, new[] { -1, 0, 1, 2 }, 4)]
//    //[TestCase("E", 5, new[] { -1, 0, 1, 2, 3 }, 5)]
//    //[TestCase("F", 6, new[] { -1, 0, 1, 2, 3, 4 }, 6)]
//    //[TestCase("G", 5, new[] { 4, -1, 4, 1, 1 }, 3)]
//    //[TestCase("H", 5, new[] { -1, 0, 4, 0, 3 }, 4)]
//    //// test "forest" situations (i.e. disjoint trees)
//    //public void Test_1(string caseName, int numberVertices, int[] input, int expected)
//    //{
//    //    var tree = new TreeHeight(numberVertices, input);
//    //    var height = tree.CalculateTreeHeight();
//    //    height.ShouldBe(expected);
//    //}

//    //[Test]
//    //public void Large_Input()
//    //{
//    //    var expected = 10000;

//    //    var large = new int[expected];
//    //    large[0] = -1;
//    //    for (int i = 0; i < expected; i++)
//    //        large[i] = i - 1;

//    //    var tree = new TreeHeight(expected, large);
//    //    var height = tree.CalculateTreeHeight();
//    //    height.ShouldBe(expected);
//    //}
//}

