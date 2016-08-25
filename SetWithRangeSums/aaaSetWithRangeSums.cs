using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace SetWithRangeSums
{
    [TestFixture]
    class AaaSetWithRangeSums
    {
        [Test]
        public void Five()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("s", 88127140, 859949755));
            program.Queries.Add(new QueryTriple("s", 407584225, 906606553));
            program.Queries.Add(new QueryTriple("+", 885530090)); // 885,530,090
            program.Queries.Add(new QueryTriple("+", 234423189)); // 234,423,189
            program.Queries.Add(new QueryTriple("s", 30746291, 664192454)); // sum( 30,746,291 ...  664,192,454 )
            program.Queries.Add(new QueryTriple("+", 465752492)); //  465,752,492 + 234,423,189 =  700,175,681 ... running sum = 234,423,189
            program.Queries.Add(new QueryTriple("s", 848498590, 481606032));
            // sum(848,498,590 ... 481,606,032) = sum(82,921,778 ... 716,029,221) = 234,423,189 + 700,175,681 = 934,598,870
            program.Queries.Add(new QueryTriple("+", 844636782)); // 844,636,782, running sum = 934,598,870
            program.Queries.Add(new QueryTriple("+", 251529178)); // 251,529,178
            program.Queries.Add(new QueryTriple("+", 182631153)); // 182,631,153

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("0"); // nothing to sum
            program.QueryResults[1].ShouldBe("0"); // nothing to sum
            program.QueryResults[2].ShouldBe("234423189"); // running sum = 234,423,189
            program.QueryResults[3].ShouldBe("934598870");
        }

        [Test]
        public void Delete_Wrong_Element()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 5));
            program.Queries.Add(new QueryTriple("-", 6));

            program.ExecuteQueries();

            program.Root.Value.ShouldBe(5);
        }

        [Test]
        public void Same_Sum_Range()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 5));
            program.Queries.Add(new QueryTriple("s", 5, 5));

            program.ExecuteQueries();

            program.Root.Value.ShouldBe(5);
            program.QueryResults[0].ShouldBe("5");
        }

        [Test]
        public void Sum_Split_Merge_Bug()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 15));
            program.Queries.Add(new QueryTriple("s", 8, 11));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("10");

            program.Root.Value.ShouldBe(15);
            program.Root.SubtreeSum.ShouldBe(25);
            program.Root.LeftChild.Value.ShouldBe(10);
        }

        [Test]
        public void Merge_Bug()
        {
            var program = new Program();


        }

        [Test]
        public void Another_Sum_Related_Bug()
        {

            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 66257759)); // + 66,257,759
            program.Queries.Add(new QueryTriple("s", 25751289, 70170547)); // rs = 66,257,759 
            program.Queries.Add(new QueryTriple("s", 28248247, 617849094)); // s(94,506,006 ... 684,106,853)
            program.Queries.Add(new QueryTriple("-", 954357244)); // deletes the only node,  954,357,244
            program.Queries.Add(new QueryTriple("+", 477444954)); //  +477,444,954
            program.Queries.Add(new QueryTriple("?", 608389416)); // "Not found"
            program.Queries.Add(new QueryTriple("s", 400483980, 423330836)); // s( 400,483,980 ...  423,330,836 )

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[1].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[2].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[3].ShouldBe("0           ".TrimEnd(' '));

            program.Root.Value.ShouldBe(477444954);
            program.Root.LeftChild.Value.ShouldBe(66257759);
            program.Root.LeftChild.Parent.Value.ShouldBe(477444954);
        }

        [Test]
        public void Not_Setting_Children_After_Sum()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 66257759)); // + 66,257,759
            program.Queries.Add(new QueryTriple("s", 25751289, 70170547)); // rs = 66,257,759 
            program.Queries.Add(new QueryTriple("s", 28248247, 617849094)); // s(94,506,006 ... 684,106,853)
            program.Queries.Add(new QueryTriple("-", 954357244)); // deletes the only node,  954,357,244
            program.Queries.Add(new QueryTriple("+", 477444954)); //  +477,444,954
            program.Queries.Add(new QueryTriple("?", 608389416)); // "Not found"
            program.Queries.Add(new QueryTriple("s", 400483980, 423330836)); // s( 400,483,980 ...  423,330,836 )
            program.Queries.Add(new QueryTriple("-", 477444954)); // running sum = rs = 0
            program.Queries.Add(new QueryTriple("?", 441393551)); // "Not found"
            program.Queries.Add(new QueryTriple("s", 66257759, 66257759)); // s( 66,257,759 ...  66,257,759 ) = 0

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[1].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[2].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[3].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[4].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[5].ShouldBe("66257759    ".TrimEnd(' '));
        }

        [Test]
        public void Split_And_Merge_1()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 1));
            program.Queries.Add(new QueryTriple("+", 2));
            program.Queries.Add(new QueryTriple("+", 3));
            program.Queries.Add(new QueryTriple("s", 0, 0));

            program.ExecuteQueries();

            var root = program.Root;
            root.Value.ShouldBe(1);
            root.RightChild.Value.ShouldBe(2);
            root.RightChild.RightChild.Value.ShouldBe(3);

            program.QueryResults[0].ShouldBe("0");
        }

        [Test]
        public void Split_And_Merge_2()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 1));
            program.Queries.Add(new QueryTriple("+", 2));
            program.Queries.Add(new QueryTriple("+", 3));
            program.Queries.Add(new QueryTriple("s", 0, 1));

            program.ExecuteQueries();

            var root = program.Root;
            root.Value.ShouldBe(2);
            root.RightChild.Value.ShouldBe(3);
            root.LeftChild.Value.ShouldBe(1);

            var left = root.LeftChild;
            left.Value.ShouldBe(1);
            left.Parent.Value.ShouldBe(2);

            var right = root.RightChild;
            right.Value.ShouldBe(3);
            right.Parent.Value.ShouldBe(2);

            program.QueryResults[0].ShouldBe("1");
        }

        [Test]
        public void Split_And_Merge_3()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 1));
            program.Queries.Add(new QueryTriple("+", 2));
            program.Queries.Add(new QueryTriple("+", 3));
            program.Queries.Add(new QueryTriple("s", 1, 2));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("3");

            var root = program.Root;
            root.Value.ShouldBe(3);
            root.LeftChild.Value.ShouldBe(2);
            root.LeftChild.LeftChild.Value.ShouldBe(1);

            var left = root.LeftChild;
            left.Value.ShouldBe(2);
            left.Parent.Value.ShouldBe(3);

            var right = left.LeftChild;
            right.Value.ShouldBe(1);
            right.Parent.Value.ShouldBe(2);
        }

        [Test]
        public void Split_And_Merge_4000()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 20));
            program.Queries.Add(new QueryTriple("+", 40));
            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 30));
            program.Queries.Add(new QueryTriple("s", 0, 0));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("0");

            var root = program.Root;
            root.Value.ShouldBe(10);
            root.LeftChild.ShouldBeNull();
            root.RightChild.Value.ShouldBe(30);
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var rightChild = root.RightChild;
            rightChild.Value.ShouldBe(30);
            rightChild.LeftChild.Value.ShouldBe(20);
            rightChild.RightChild.Value.ShouldBe(40);
            rightChild.Parent.Value.ShouldBe(10);
            rightChild.SubtreeSum.ShouldBe(140);

            var rightLeft = rightChild.LeftChild;
            rightLeft.Value.ShouldBe(20);
            rightLeft.LeftChild.ShouldBeNull();
            rightLeft.RightChild.ShouldBeNull();
            rightLeft.Parent.Value.ShouldBe(30);
            rightLeft.SubtreeSum.ShouldBe(20);

            var rightChildRight = rightChild.RightChild;
            rightChildRight.Value.ShouldBe(40);
            rightChildRight.LeftChild.ShouldBeNull();
            rightChildRight.RightChild.Value.ShouldBe(50);
            rightChildRight.Parent.Value.ShouldBe(30);
            rightChildRight.SubtreeSum.ShouldBe(90);

            var rightRightRight = rightChildRight.RightChild;
            rightRightRight.Value.ShouldBe(50);
            rightRightRight.LeftChild.ShouldBeNull();
            rightRightRight.RightChild.ShouldBeNull();
            rightRightRight.Parent.Value.ShouldBe(40);
            rightRightRight.SubtreeSum.ShouldBe(50);
        }

        [Test]
        public void Split_And_Merge_5000()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 20));
            program.Queries.Add(new QueryTriple("+", 40));
            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 30));
            program.Queries.Add(new QueryTriple("s", 0, 10));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("10");

            var root = program.Root;
            root.Value.ShouldBe(20);
            root.LeftChild.Value.ShouldBe(10);
            root.RightChild.Value.ShouldBe(30);
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var left = root.LeftChild;
            left.Value.ShouldBe(10);
            left.LeftChild.ShouldBeNull();
            left.RightChild.ShouldBeNull();
            left.Parent.Value.ShouldBe(20);
            left.SubtreeSum.ShouldBe(10);

            var right = root.RightChild;
            right.Value.ShouldBe(30);
            right.LeftChild.ShouldBeNull();
            right.RightChild.Value.ShouldBe(40);
            right.Parent.Value.ShouldBe(20);
            right.SubtreeSum.ShouldBe(120);

            var rightRight = right.RightChild;
            rightRight.Value.ShouldBe(40);
            rightRight.LeftChild.ShouldBeNull();
            rightRight.RightChild.Value.ShouldBe(50);
            rightRight.Parent.Value.ShouldBe(30);
            rightRight.SubtreeSum.ShouldBe(90);

            var rightRightRight = rightRight.RightChild;
            rightRightRight.Value.ShouldBe(50);
            rightRightRight.LeftChild.ShouldBeNull();
            rightRightRight.RightChild.ShouldBeNull();
            rightRightRight.Parent.Value.ShouldBe(40);
            rightRightRight.SubtreeSum.ShouldBe(50);
        }

        [Test]
        public void Split_And_Merge_6000()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 20));
            program.Queries.Add(new QueryTriple("+", 40));
            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 30));
            program.Queries.Add(new QueryTriple("s", 10, 20));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("30");

            var root = program.Root;
            root.Value.ShouldBe(30);
            root.LeftChild.Value.ShouldBe(20);
            root.RightChild.Value.ShouldBe(40);
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var left = root.LeftChild;
            left.Value.ShouldBe(20);
            left.LeftChild.Value.ShouldBe(10);
            left.RightChild.ShouldBeNull();
            left.Parent.Value.ShouldBe(30);
            left.SubtreeSum.ShouldBe(30);

            var leftLeft = left.LeftChild;
            leftLeft.Value.ShouldBe(10);
            leftLeft.LeftChild.ShouldBeNull();
            leftLeft.RightChild.ShouldBeNull();
            leftLeft.Parent.Value.ShouldBe(20);
            leftLeft.SubtreeSum.ShouldBe(10);

            var right = root.RightChild;
            right.Value.ShouldBe(40);
            right.LeftChild.ShouldBeNull();
            right.RightChild.Value.ShouldBe(50);
            right.Parent.Value.ShouldBe(30);
            right.SubtreeSum.ShouldBe(90);

            var rightRight = right.RightChild;
            rightRight.Value.ShouldBe(50);
            rightRight.RightChild.ShouldBeNull();
            rightRight.LeftChild.ShouldBeNull();
            rightRight.Parent.Value.ShouldBe(40);
            rightRight.SubtreeSum.ShouldBe(50);
        }

        [Test]
        public void Split_And_Merge_7()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 20));
            program.Queries.Add(new QueryTriple("+", 40));
            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 30));
            program.Queries.Add(new QueryTriple("s", 10, 1000));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("150");

            var root = program.Root;
            root.Value.ShouldBe(50);
            root.LeftChild.Value.ShouldBe(10);
            root.RightChild.ShouldBeNull();
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var left = root.LeftChild;
            left.Value.ShouldBe(10);
            left.LeftChild.ShouldBeNull();
            left.RightChild.Value.ShouldBe(40);
            left.Parent.Value.ShouldBe(50);
            left.SubtreeSum.ShouldBe(100);

            var leftRight = left.RightChild;
            leftRight.Value.ShouldBe(40);
            leftRight.LeftChild.Value.ShouldBe(30);
            leftRight.RightChild.ShouldBeNull();
            leftRight.Parent.Value.ShouldBe(10);
            leftRight.SubtreeSum.ShouldBe(90);

            var leftRightLeft = leftRight.LeftChild;
            leftRightLeft.Value.ShouldBe(30);
            leftRightLeft.LeftChild.Value.ShouldBe(20);
            leftRightLeft.RightChild.ShouldBeNull();
            leftRightLeft.Parent.Value.ShouldBe(40);
            leftRightLeft.SubtreeSum.ShouldBe(50);

            var lrlr = leftRightLeft.LeftChild;
            lrlr.Value.ShouldBe(20);
            lrlr.LeftChild.ShouldBeNull();
            lrlr.RightChild.ShouldBeNull();
            lrlr.Parent.Value.ShouldBe(30);
            lrlr.SubtreeSum.ShouldBe(20);
        }

        [Test]
        //[Ignore]
        public void Split_And_Merge_9()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 20));
            program.Queries.Add(new QueryTriple("+", 40));
            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 30));
            program.Queries.Add(new QueryTriple("s", 50, 1000));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("50");

            var root = program.Root;
            root.Value.ShouldBe(50);
            root.LeftChild.Value.ShouldBe(40);
            root.RightChild.ShouldBeNull();
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var left = root.LeftChild;
            left.Value.ShouldBe(40);
            left.LeftChild.Value.ShouldBe(30);
            left.RightChild.ShouldBeNull();
            left.Parent.Value.ShouldBe(50);
            left.SubtreeSum.ShouldBe(100);

            var ll = left.LeftChild;
            ll.Value.ShouldBe(30);
            ll.LeftChild.Value.ShouldBe(10);
            ll.RightChild.ShouldBeNull();
            ll.Parent.Value.ShouldBe(40);
            ll.SubtreeSum.ShouldBe(60);

            var final = ll.LeftChild;
            final.Value.ShouldBe(10);
            final.LeftChild.ShouldBeNull();
            final.RightChild.Value.ShouldBe(20);
            final.Parent.Value.ShouldBe(30);
            final.SubtreeSum.ShouldBe(30);

            var aFin = final.RightChild;
            aFin.Value.ShouldBe(20);
            aFin.LeftChild.ShouldBeNull();
            aFin.RightChild.ShouldBeNull();
            aFin.Parent.Value.ShouldBe(10);
            aFin.SubtreeSum.ShouldBe(20);
        }

        [Test]
        //[Ignore]
        public void Split_And_Merge_10()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 20));
            program.Queries.Add(new QueryTriple("+", 40));
            program.Queries.Add(new QueryTriple("+", 10));
            program.Queries.Add(new QueryTriple("+", 30));
            program.Queries.Add(new QueryTriple("s", 999, 1000));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("0");

            var root = program.Root;
            root.Value.ShouldBe(50);
            root.LeftChild.Value.ShouldBe(40);
            root.RightChild.ShouldBeNull();
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var left = root.LeftChild;
            left.Value.ShouldBe(40);
            left.LeftChild.Value.ShouldBe(30);
            left.RightChild.ShouldBeNull();
            left.Parent.Value.ShouldBe(50);
            left.SubtreeSum.ShouldBe(100);

            var ll = left.LeftChild;
            ll.Value.ShouldBe(30);
            ll.LeftChild.Value.ShouldBe(10);
            ll.RightChild.ShouldBeNull();
            ll.Parent.Value.ShouldBe(40);
            ll.SubtreeSum.ShouldBe(60);

            var final = ll.LeftChild;
            final.Value.ShouldBe(10);
            final.LeftChild.ShouldBeNull();
            final.RightChild.Value.ShouldBe(20);
            final.Parent.Value.ShouldBe(30);
            final.SubtreeSum.ShouldBe(30);

            var aFin = final.RightChild;
            aFin.Value.ShouldBe(20);
            aFin.LeftChild.ShouldBeNull();
            aFin.RightChild.ShouldBeNull();
            aFin.Parent.Value.ShouldBe(10);
            aFin.SubtreeSum.ShouldBe(20);
        }

        [Test]
        //[Ignore]
        public void Problem_Twenty()
        {
            var program = new Program();
#region
            program.Queries.Add(new QueryTriple("s", 40279559, 89162572)); // 0
            program.Queries.Add(new QueryTriple("-", 774613289)); // - 
            program.Queries.Add(new QueryTriple("s", 869592654, 915517087)); // 0
            program.Queries.Add(new QueryTriple("-", 165280355)); // -
            program.Queries.Add(new QueryTriple("-", 776346290)); // -
            program.Queries.Add(new QueryTriple("-", 221187096)); // -
            program.Queries.Add(new QueryTriple("s", 421986248, 742826969)); // 0
            program.Queries.Add(new QueryTriple("s", 83228103, 852190011)); // 0
            program.Queries.Add(new QueryTriple("-", 640319482)); // -
            program.Queries.Add(new QueryTriple("?", 528689193)); // "Not found"
            program.Queries.Add(new QueryTriple("?", 75245219)); // "Not found"
            program.Queries.Add(new QueryTriple("-", 617070033)); // -
            program.Queries.Add(new QueryTriple("+", 66257759)); // + 66,257,759
            program.Queries.Add(new QueryTriple("s", 25751289, 70170547)); // rs = 66,257,759 
            program.Queries.Add(new QueryTriple("s", 28248247, 617849094)); // s(94,506,006 ... 684,106,853)
            program.Queries.Add(new QueryTriple("-", 954357244)); // deletes the only node,  954,357,244
            program.Queries.Add(new QueryTriple("+", 477444954)); //  +477,444,954
            program.Queries.Add(new QueryTriple("?", 608389416)); // "Not found"
            program.Queries.Add(new QueryTriple("s", 400483980, 423330836)); // s( 400,483,980 ...  423,330,836 )
            program.Queries.Add(new QueryTriple("-", 477444954)); // running sum = rs = 0
            program.Queries.Add(new QueryTriple("?", 441393551)); // "Not found"
            program.Queries.Add(new QueryTriple("s", 66257759, 66257759)); // s( 66,257,759 ...  66,257,759 ) = 0
            program.Queries.Add(new QueryTriple("-", 822218158));
            program.Queries.Add(new QueryTriple("?", 806479414));
            program.Queries.Add(new QueryTriple("s", 548665149, 925635534));
            program.Queries.Add(new QueryTriple("s", 66257759, 66257759));
            program.Queries.Add(new QueryTriple("?", 234121006));
            program.Queries.Add(new QueryTriple("+", 663305907));
            program.Queries.Add(new QueryTriple("s", 314809050, 685231317));
            program.Queries.Add(new QueryTriple("-", 0));
            program.Queries.Add(new QueryTriple("s", 487458874, 602635501));
            program.Queries.Add(new QueryTriple("s", 66257759, 66257759));
            program.Queries.Add(new QueryTriple("?", 918193520));
            program.Queries.Add(new QueryTriple("?", 606474691));
            program.Queries.Add(new QueryTriple("s", 188185089, 774086933));
            program.Queries.Add(new QueryTriple("-", 322445571));
            program.Queries.Add(new QueryTriple("s", 66257759, 66257759));
            program.Queries.Add(new QueryTriple("-", 814123984));
            program.Queries.Add(new QueryTriple("s", 0, 0));
            program.Queries.Add(new QueryTriple("s", 0, 0));
            program.Queries.Add(new QueryTriple("s", 689260392, 827869844));
            program.Queries.Add(new QueryTriple("?", 204276815));
            program.Queries.Add(new QueryTriple("-", 66257759));
            program.Queries.Add(new QueryTriple("?", 488766408));
            program.Queries.Add(new QueryTriple("s", 412617563, 631410280));
            program.Queries.Add(new QueryTriple("-", 463415495));
            program.Queries.Add(new QueryTriple("+", 601030115));
            program.Queries.Add(new QueryTriple("?", 776513589));
            program.Queries.Add(new QueryTriple("s", 257003372, 887483600));
            program.Queries.Add(new QueryTriple("+", 154047223));
            program.Queries.Add(new QueryTriple("?", 154047223));
            program.Queries.Add(new QueryTriple("?", 219327735));
            program.Queries.Add(new QueryTriple("+", 978812473));
            program.Queries.Add(new QueryTriple("s", 978812473, 154047223));
            program.Queries.Add(new QueryTriple("?", 718062555));
            program.Queries.Add(new QueryTriple("?", 128066784));
            program.Queries.Add(new QueryTriple("-", 15718305));
            program.Queries.Add(new QueryTriple("?", 754978417));
            program.Queries.Add(new QueryTriple("s", 643892549, 819127300));
            program.Queries.Add(new QueryTriple("?", 192401474));
            program.Queries.Add(new QueryTriple("?", 643892549));
            program.Queries.Add(new QueryTriple("+", 638898307));
            program.Queries.Add(new QueryTriple("?", 973173529));
            program.Queries.Add(new QueryTriple("+", 506709268));
            program.Queries.Add(new QueryTriple("-", 506709268));
            program.Queries.Add(new QueryTriple("+", 744166533));
            program.Queries.Add(new QueryTriple("-", 638898307));
            program.Queries.Add(new QueryTriple("+", 95240753));
            program.Queries.Add(new QueryTriple("s", 997348833, 63778002));
            program.Queries.Add(new QueryTriple("?", 31190791));
            program.Queries.Add(new QueryTriple("s", 21011834, 570648768));
            program.Queries.Add(new QueryTriple("+", 217208615));
            program.Queries.Add(new QueryTriple("+", 401912531));
            program.Queries.Add(new QueryTriple("s", 0, 723886547));
            program.Queries.Add(new QueryTriple("?", 251082460));
            program.Queries.Add(new QueryTriple("+", 542593404));
            program.Queries.Add(new QueryTriple("s", 702430665, 542593404));
            program.Queries.Add(new QueryTriple("?", 48285749));
            program.Queries.Add(new QueryTriple("s", 831077135, 671239874));
            program.Queries.Add(new QueryTriple("+", 917941607));
            program.Queries.Add(new QueryTriple("?", 908494561));
            program.Queries.Add(new QueryTriple("?", 671239874));
            program.Queries.Add(new QueryTriple("s", 333354822, 490605331));
            program.Queries.Add(new QueryTriple("+", 261522346));
            program.Queries.Add(new QueryTriple("s", 170201520, 10364259));
            program.Queries.Add(new QueryTriple("-", 139162050));
            program.Queries.Add(new QueryTriple("-", 677374727));
            program.Queries.Add(new QueryTriple("?", 992422786));
            program.Queries.Add(new QueryTriple("?", 500171144));
            program.Queries.Add(new QueryTriple("-", 239436034));
            program.Queries.Add(new QueryTriple("+", 556867643));
#endregion
            program.Queries.Add(new QueryTriple("?", 992422786));  
            program.Queries.Add(new QueryTriple("+", 720003678)); // GOOD STATE
            program.Queries.Add(new QueryTriple("s", 220110584, 268880636)); // GOOD STATE
            program.Queries.Add(new QueryTriple("s", 31190791, 997548180)); // GOOD STATE
            program.Queries.Add(new QueryTriple("s", 898610232, 383552107)); // ? 601 pointing at wrong parent
            program.Queries.Add(new QueryTriple("-", 682670734));
            program.Queries.Add(new QueryTriple("+", 547596765));
            program.Queries.Add(new QueryTriple("s", 496810115, 875859347)); // stack overflow...?
            program.Queries.Add(new QueryTriple("?", 41728941));

            program.ExecuteQueries();

            var queryResultCount = program.Queries.Count(x => x.Operation == "s" || x.Operation == "?");
            queryResultCount.ShouldBe(61);

            program.QueryResults[0].ShouldBe("0");
            program.QueryResults[1].ShouldBe("0");
            program.QueryResults[2].ShouldBe("0");
            program.QueryResults[3].ShouldBe("0");
            program.QueryResults[4].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[5].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[6].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[7].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[8].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[9].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[10].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[11].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[12].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[13].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[14].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[15].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[16].ShouldBe("729563666   ".TrimEnd(' '));
            program.QueryResults[17].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[18].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[19].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[20].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[21].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[22].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[23].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[24].ShouldBe("66257759    ".TrimEnd(' '));
            program.QueryResults[25].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[26].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[27].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[28].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[29].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[30].ShouldBe("601030115   ".TrimEnd(' '));
            program.QueryResults[31].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[32].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[33].ShouldBe("1935950040  ".TrimEnd(' '));
            program.QueryResults[34].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[35].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[36].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[37].ShouldBe("1935950040  ".TrimEnd(' '));
            program.QueryResults[38].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[39].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[40].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[41].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[42].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[43].ShouldBe("31190791    ".TrimEnd(' '));
            program.QueryResults[44].ShouldBe("3328760130  ".TrimEnd(' '));
            program.QueryResults[45].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[46].ShouldBe("4200113661  ".TrimEnd(' '));
            program.QueryResults[47].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[48].ShouldBe("4200113661  ".TrimEnd(' '));
            program.QueryResults[49].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[50].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[51].ShouldBe("1860989273  ".TrimEnd(' '));
            program.QueryResults[52].ShouldBe("4440680541  ".TrimEnd(' ')); 
            program.QueryResults[53].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[54].ShouldBe("Not found   ".TrimEnd(' '));
            program.QueryResults[55].ShouldBe("Found       ".TrimEnd(' '));
            program.QueryResults[56].ShouldBe("0           ".TrimEnd(' '));
            program.QueryResults[57].ShouldBe("4220898514  ".TrimEnd(' '));
            program.QueryResults[58].ShouldBe("1565728674  ".TrimEnd(' '));
            program.QueryResults[59].ShouldBe("829624590   ".TrimEnd(' '));
            program.QueryResults[60].ShouldBe("Found       ".TrimEnd(' '));
        }

        [Test]
        public void Query_ProblemSet2()
        {
            var program = new Program();

            program.Queries.Add(new QueryTriple("+", 491572259));
            program.Queries.Add(new QueryTriple("?", 491572259));
            program.Queries.Add(new QueryTriple("?", 899375874));
            program.Queries.Add(new QueryTriple("s", 310971296, 877523306));
            program.Queries.Add(new QueryTriple("+", 352411209));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("Found");
            program.QueryResults[1].ShouldBe("Not found");
            program.QueryResults[2].ShouldBe("491572259");
        }

        [Test]
        public void Query_ProblemSet()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("?", 1)); // running sum = 0
            program.Queries.Add(new QueryTriple("+", 1));
            program.Queries.Add(new QueryTriple("?", 1));
            program.Queries.Add(new QueryTriple("+", 2));
            program.Queries.Add(new QueryTriple("s", 1, 2));
            program.Queries.Add(new QueryTriple("+", 1000000000)); // running sum = 3
            program.Queries.Add(new QueryTriple("?", 1000000000));
            program.Queries.Add(new QueryTriple("-", 1000000000));
            program.Queries.Add(new QueryTriple("?", 1000000000));
            program.Queries.Add(new QueryTriple("s", 999999999, 1000000000));
            program.Queries.Add(new QueryTriple("-", 2)); // running sum = 1
            program.Queries.Add(new QueryTriple("?", 2));
            program.Queries.Add(new QueryTriple("-", 0));
            program.Queries.Add(new QueryTriple("+", 9));
            program.Queries.Add(new QueryTriple("s", 0, 9));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("Not found"); // ?1 
            program.QueryResults[1].ShouldBe("Found"); //?1
            program.QueryResults[2].ShouldBe("3"); // s1,2
            program.QueryResults[3].ShouldBe("Found"); // ?1000000000
            program.QueryResults[4].ShouldBe("Not found"); // ?1000000000
            program.QueryResults[5].ShouldBe("1"); // s999999999,1000000000
            program.QueryResults[6].ShouldBe("Not found"); // ?2
            program.QueryResults[7].ShouldBe("10"); // ?2
        }

        [Test]
        public void QuerySum_Simple()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 60));
            program.Queries.Add(new QueryTriple("+", 70));
            program.Queries.Add(new QueryTriple("s", 40, 80));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("180");
        }

        [Test]
        public void QuerySum_Del_SplitNotNecessary_QuerySum()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 60));
            program.Queries.Add(new QueryTriple("+", 70));
            program.Queries.Add(new QueryTriple("+", 80));
            program.Queries.Add(new QueryTriple("-", 50));
            program.Queries.Add(new QueryTriple("s", 50, 80));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("210");
        }

        [Test]
        public void QuerySum_Del_SplitNecessary_QuerySum()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 60));
            program.Queries.Add(new QueryTriple("+", 70));
            program.Queries.Add(new QueryTriple("+", 80));
            program.Queries.Add(new QueryTriple("s", 50, 70));
            program.Queries.Add(new QueryTriple("s", 9999, 1000));
            program.Queries.Add(new QueryTriple("s", 60, 80));
            program.Queries.Add(new QueryTriple("s", 9999, 1000));
            program.Queries.Add(new QueryTriple("s", 60, 70));

            program.ExecuteQueries();

            program.QueryResults[0].ShouldBe("180");
            program.QueryResults[1].ShouldBe("0");
            program.QueryResults[2].ShouldBe("210");
            program.QueryResults[3].ShouldBe("0");
            program.QueryResults[4].ShouldBe("130");
        }

        [Test]
        public void SumRange_OneNode_OutOfRange()
        {
            var program = new Program();
            var parent = new TreeNode(4);
            program.Root = parent;

            var sum = program.SumRange(5, 9);

            sum.ShouldBe(0);
            program.Root.Value.ShouldBe(4);
        }

        [Test]
        public void SumRange_OneNode_InRange()
        {
            var program = new Program();
            var parent = new TreeNode(4, null, null, null, 4);
            program.Root = parent;

            var sum = program.SumRange(4, 9);

            sum.ShouldBe(4);
            program.Root.Value.ShouldBe(4);
        }

        [Test]
        public void SumRange_OneNode_GreaterThanRange()
        {
            var program = new Program();
            var parent = new TreeNode(10, null, null, null, 10);
            program.Root = parent;

            var sum = program.SumRange(4, 9);

            sum.ShouldBe(0);
            program.Root.Value.ShouldBe(10);
        }

        [Test]
        public void SumRange_FiveNodes_AllInRange()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);
            program.Root = root;

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var sum = program.SumRange(25, 75);

            sum.ShouldBe(250);
            program.Root.Value.ShouldBe(75);
        }

        [Test]
        public void SumRange_FiveNodes_ChopOffLowestElement()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);
            program.Root = root;

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var sum = program.SumRange(30, 75);

            sum.ShouldBe(225);
        }

        [Test]
        public void SumRange_FiveNodes_ChopOffTwoLowestElements()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);
            program.Root = root;

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var sum = program.SumRange(50, 75);

            sum.ShouldBe(185);
        }

        [Test]
        public void SumRange_FiveNodes_ChopOffTwoLowestElementsAndTopElement()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);
            program.Root = root;

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var sum = program.SumRange(50, 65);

            sum.ShouldBe(110);
        }

        [Test]
        public void SumRange_FiveNodes_ChopOffTwoLowestElementsAndTopTwoElements()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);
            program.Root = root;

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var sum = program.SumRange(49, 51);

            sum.ShouldBe(50);
        }

        [Test]
        public void Sum_OneNode()
        {
            var program = new Program();
            var node = new TreeNode(50);
            program.Root = node;

            program.UpdateSum(node);

            node.SubtreeSum.ShouldBe(50);
        }

        [Test]
        public void Sum_TwoNodes()
        {
            var program = new Program();
            var parent = new TreeNode(50);
            var child = new TreeNode(25, null, null, parent, 25);
            parent.LeftChild = child;
            program.Root = parent;

            program.UpdateSum(parent);
            parent.SubtreeSum.ShouldBe(75);
        }

        [Test]
        public void Sum_ThreeLeftNodes()
        {
            var program = new Program();
            var parent = new TreeNode(50);
            var child = new TreeNode(25, null, null, parent);
            var grandChild = new TreeNode(10, null, null, parent, 10);
            parent.LeftChild = child;
            child.LeftChild = grandChild;
            program.Root = parent;

            program.UpdateSum(child);
            program.UpdateSum(parent);

            parent.SubtreeSum.ShouldBe(85);
        }

        [Test]
        public void Sum_ThreeNodes()
        {
            var program = new Program();
            var parent = new TreeNode(50);
            var leftChild = new TreeNode(25, null, null, parent);
            var rightChild = new TreeNode(75, null, null, parent);
            parent.LeftChild = leftChild;
            leftChild.LeftChild = rightChild;
            parent.RightChild = rightChild;
            program.Root = parent;

            program.UpdateSum(leftChild);
            leftChild.SubtreeSum.ShouldBe(25);

            program.UpdateSum(rightChild);
            rightChild.SubtreeSum.ShouldBe(75);

            program.UpdateSum(parent);
            parent.SubtreeSum.ShouldBe(150);
        }

        [Test]
        public void SplaySplit_OneNodeEqual()
        {
            var program = new Program();
            var node = new TreeNode(50, null, null, null, 50);
            program.Root = node;

            var splitNodes = program.SplaySplit(50, node);

            splitNodes.RightRoot.Value.ShouldBe(50);
            splitNodes.RightRoot.SubtreeSum.ShouldBe(50);

            splitNodes.LeftRoot.ShouldBeNull();
        }

        [Test]
        public void SplaySplit_OneNodeLessThan()
        {
            var program = new Program();
            var node = new TreeNode(50, null, null, null, 50);
            program.Root = node;

            var splitNodes = program.SplaySplit(0, node);

            splitNodes.RightRoot.Value.ShouldBe(50);
            splitNodes.RightRoot.SubtreeSum.ShouldBe(50);

            splitNodes.LeftRoot.ShouldBeNull();
        }

        [Test]
        public void SplaySplit_OneNodeGreaterThan()
        {
            var program = new Program();
            var node = new TreeNode(50, null, null, null, 50);
            program.Root = node;

            var splitNodes = program.SplaySplit(100, node);

            splitNodes.RightRoot.ShouldBeNull();

            splitNodes.LeftRoot.Value.ShouldBe(50);
            splitNodes.LeftRoot.SubtreeSum.ShouldBe(50);
        }

        [Test]
        public void SplaySplit_ThreeNodesEqualToRoot()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 150);
            var left = new TreeNode(25, null, null, root, 25);
            var right = new TreeNode(75, null, null, root, 75);
            program.Root = root;

            root.LeftChild = left;
            root.RightChild = right;

            var splitNodes = program.SplaySplit(50, root);

            var leftRoot = splitNodes.LeftRoot;
            leftRoot.Value.ShouldBe(25);
            leftRoot.SubtreeSum.ShouldBe(25);
            leftRoot.Parent.ShouldBeNull();

            var rightRoot = splitNodes.RightRoot;
            rightRoot.Value.ShouldBe(50);
            rightRoot.SubtreeSum.ShouldBe(125);
            rightRoot.Parent.ShouldBeNull();
        }

        [Test]
        public void SplaySplit_ThreeNodesGreaterThanRoot()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 150);
            var left = new TreeNode(25, null, null, root, 25);
            var right = new TreeNode(75, null, null, root, 75);
            program.Root = root;
            root.LeftChild = left;
            root.RightChild = right;

            var splitNodes = program.SplaySplit(100, root);

            var leftRoot = splitNodes.LeftRoot;
            leftRoot.Value.ShouldBe(75);
            leftRoot.SubtreeSum.ShouldBe(150);
            leftRoot.Parent.ShouldBeNull();

            splitNodes.RightRoot.ShouldBeNull();
        }

        [Test]
        public void SplaySplit_ThreeNodesLessThanRoot()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 150);
            var left = new TreeNode(25, null, null, root, 25);
            var right = new TreeNode(75, null, null, root, 75);
            program.Root = root;
            root.LeftChild = left;
            root.RightChild = right;

            var splitNodes = program.SplaySplit(0, root);

            splitNodes.LeftRoot.ShouldBeNull();

            var rightRoot = splitNodes.RightRoot;
            rightRoot.Value.ShouldBe(25);
            rightRoot.SubtreeSum.ShouldBe(150);
            rightRoot.Parent.ShouldBeNull();
        }

        [Test]
        public void SplaySplit_ThreeNodesLessThanRootGreaterThanLeftChild()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 150);
            var left = new TreeNode(25, null, null, root, 25);
            var right = new TreeNode(75, null, null, root, 75);
            program.Root = root;
            root.LeftChild = left;
            root.RightChild = right;

            var splitNodes = program.SplaySplit(40, root);

            var leftRoot = splitNodes.LeftRoot;
            leftRoot.Value.ShouldBe(25);
            leftRoot.SubtreeSum.ShouldBe(25);
            leftRoot.Parent.ShouldBeNull();

            var rightRoot = splitNodes.RightRoot;
            rightRoot.Value.ShouldBe(50);
            rightRoot.SubtreeSum.ShouldBe(125);
            rightRoot.Parent.ShouldBeNull();
        }

        [Test]
        public void SplaySplit_ThreeNodesLessThanRootLessThanRightChild()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 150);
            var left = new TreeNode(25, null, null, root, 25);
            var right = new TreeNode(75, null, null, root, 75);
            program.Root = root;
            root.LeftChild = left;
            root.RightChild = right;

            var splitNodes = program.SplaySplit(60, root);

            var leftRoot = splitNodes.LeftRoot;
            leftRoot.Value.ShouldBe(50);
            leftRoot.SubtreeSum.ShouldBe(75);
            leftRoot.Parent.ShouldBeNull();

            var rightRoot = splitNodes.RightRoot;
            rightRoot.Value.ShouldBe(75);
            rightRoot.SubtreeSum.ShouldBe(75);
            rightRoot.Parent.ShouldBeNull();
        }

        [Test]
        public void MergeWithRoot_TwoNodes_AndRoot()
        {
            var program = new Program();
            var leftRoot = new TreeNode(10, null, null, null, 10);
            var middleRoot = new TreeNode(15, null, null, null, 15);
            var rightRoot = new TreeNode(20, null, null, null, 20);

            var root = program.MergeWithRoot(leftRoot, rightRoot, middleRoot);

            root.Value.ShouldBe(15);
            root.SubtreeSum.ShouldBe(45);

            var left = root.LeftChild;
            left.Value.ShouldBe(10);
            left.Parent.Value.ShouldBe(15);
            left.SubtreeSum.ShouldBe(10);

            var right = root.RightChild;
            right.Value.ShouldBe(20);
            right.Parent.Value.ShouldBe(15);
            right.SubtreeSum.ShouldBe(20);
        }

        [Test]
        public void MergeWithoutRoot_TwoTrees()
        {
            var program = new Program();

            var leftRoot = new TreeNode(10, null, null, null, 30);
            leftRoot.LeftChild = new TreeNode(5, null, null, leftRoot, 5);
            leftRoot.RightChild = new TreeNode(15, null, null, leftRoot, 15);

            var rightRoot = new TreeNode(20, null, null, null, 60);
            rightRoot.LeftChild = new TreeNode(17, null, null, rightRoot, 47);
            rightRoot.LeftChild.LeftChild = new TreeNode(11, null, null, rightRoot.LeftChild, 11);
            rightRoot.LeftChild.RightChild = new TreeNode(19, null, null, rightRoot.LeftChild, 19);
            rightRoot.RightChild = new TreeNode(23, null, null, rightRoot, 23);

            var root = program.Merge(leftRoot, rightRoot);

            root.Value.ShouldBe(11);
            root.SubtreeSum.ShouldBe(120);

            var right = root.RightChild;
            right.Value.ShouldBe(17);
            right.SubtreeSum.ShouldBe(79);

            var left = root.LeftChild;
            left.Value.ShouldBe(10);
            left.SubtreeSum.ShouldBe(30);
        }

        [Test]
        public void Split_Null()
        {
            var program = new Program();

            var splitRoots = program.Split(5, null);

            splitRoots.LeftRoot.ShouldBeNull();
            splitRoots.RightRoot.ShouldBeNull();
        }

        [Test]
        public void Split_SingleNode()
        {
            var program = new Program();
            var root = new TreeNode(42);

            var splitRoots = program.Split(42, root);

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.ShouldBeNull();

            splitRoots.RightRoot.Value.ShouldBe(42);
            splitRoots.RightRoot.SubtreeSum.ShouldBe(42);
        }

        [Test]
        public void Split_TwoNodes_SearchTermLessThanWholeTree()
        {
            var program = new Program();
            var root = new TreeNode(42);
            var child = new TreeNode(40, null, null, root);
            root.LeftChild = child;

            var splitRoots = program.Split(35, root);

            var rightRoot = splitRoots.RightRoot;
            rightRoot.Value.ShouldBe(42);
            rightRoot.Parent.ShouldBeNull();
            rightRoot.SubtreeSum.ShouldBe(82);

            splitRoots.LeftRoot.ShouldBeNull();
        }

        [Test]
        public void Split_TwoNodes_SearchTermGreaterThanWholeTree()
        {
            var program = new Program();
            var root = new TreeNode(42, null, null, null, 42);
            var child = new TreeNode(40, null, null, root, 40);
            root.LeftChild = child;

            var splitRoots = program.Split(45, root);

            splitRoots.RightRoot.ShouldBeNull();

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.Value.ShouldBe(42);
            leftRoot.Parent.ShouldBeNull();
            leftRoot.SubtreeSum.ShouldBe(82);
        }

        [Test]
        public void Split_TwoNodes_SearchTermEqual()
        {
            var program = new Program();
            var root = new TreeNode(42, null, null, null, 42);
            var child = new TreeNode(40, null, null, root, 40);
            root.LeftChild = child;

            var splitRoots = program.Split(42, root);

            splitRoots.RightRoot.Value.ShouldBe(42);
            splitRoots.RightRoot.SubtreeSum.ShouldBe(42);

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.Value.ShouldBe(40);
            leftRoot.SubtreeSum.ShouldBe(40);
        }

        [Test]
        public void Split_TwoNodes_SearchTermEqualLower()
        {
            var program = new Program();
            var root = new TreeNode(42);
            var child = new TreeNode(40, null, null, root);
            root.LeftChild = child;

            var splitRoots = program.Split(40, root);

            splitRoots.RightRoot.Value.ShouldBe(42);
            splitRoots.RightRoot.LeftChild.Value.ShouldBe(40);
            splitRoots.RightRoot.SubtreeSum.ShouldBe(82);

            splitRoots.LeftRoot.ShouldBeNull();
        }

        [Test]
        public void Split_Five_SearchEqualsNode()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var splitRoots = program.Split(50, root);

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.Value.ShouldBe(25);
            leftRoot.SubtreeSum.ShouldBe(65);

            leftRoot.RightChild.Value.ShouldBe(40);
            leftRoot.RightChild.SubtreeSum.ShouldBe(40);

            var rightRoot = splitRoots.RightRoot;
            rightRoot.Value.ShouldBe(50);
            rightRoot.SubtreeSum.ShouldBe(185);
            rightRoot.Parent.ShouldBeNull();

            var newRightChild = rightRoot.RightChild;
            newRightChild.Value.ShouldBe(75);
            newRightChild.LeftChild.SubtreeSum.ShouldBe(60);
        }

        [Test]
        public void Split_Five_SearchLessThanRoot()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var splitRoots = program.Split(30, root);

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.Value.ShouldBe(25);
            leftRoot.RightChild.ShouldBeNull();
            leftRoot.SubtreeSum.ShouldBe(25);

            var rightRoot = splitRoots.RightRoot;
            rightRoot.Value.ShouldBe(50);
            rightRoot.SubtreeSum.ShouldBe(225);

            var rightLeftChild = rightRoot.LeftChild;
            rightLeftChild.Value.ShouldBe(40);
            rightLeftChild.Parent.Value.ShouldBe(50);
            rightLeftChild.SubtreeSum.ShouldBe(40);

            var rightRightChild = rightRoot.RightChild;
            rightRightChild.Value.ShouldBe(75);
            rightRightChild.SubtreeSum.ShouldBe(135);
        }

        [Test]
        public void Split_Five_SearchEqualsLeftChild()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var splitRoots = program.Split(25, root);

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.ShouldBeNull();

            var rightRoot = splitRoots.RightRoot;
            rightRoot.Value.ShouldBe(50);
            rightRoot.SubtreeSum.ShouldBe(250);
            rightRoot.LeftChild.Value.ShouldBe(25);
            rightRoot.LeftChild.SubtreeSum.ShouldBe(65);
            rightRoot.LeftChild.Parent.Value.ShouldBe(50);
            rightRoot.RightChild.SubtreeSum.ShouldBe(135);
        }

        [Test]
        public void Split_Five_SearchGreaterThanRightGrandchild()
        {
            var program = new Program();
            var root = new TreeNode(50, null, null, null, 250);

            var leftChild = new TreeNode(25, null, null, root, 65);
            root.LeftChild = leftChild;
            var leftRightGrandchild = new TreeNode(40, null, null, leftChild, 40);
            leftChild.RightChild = leftRightGrandchild;

            var rightChild = new TreeNode(75, null, null, root, 135);
            root.RightChild = rightChild;
            var rightLeftGrandchild = new TreeNode(60, null, null, rightChild, 60);
            rightChild.LeftChild = rightLeftGrandchild;

            var splitRoots = program.Split(65, root);

            var leftRoot = splitRoots.LeftRoot;
            leftRoot.Value.ShouldBe(50);
            leftRoot.SubtreeSum.ShouldBe(175);

            leftRoot.RightChild.Value.ShouldBe(60);
            leftRoot.RightChild.Parent.Value.ShouldBe(50);
            leftRoot.RightChild.SubtreeSum.ShouldBe(60);

            leftRoot.LeftChild.Value.ShouldBe(25);
            leftRoot.LeftChild.SubtreeSum.ShouldBe(65);

            var rightRoot = splitRoots.RightRoot;
            rightRoot.Value.ShouldBe(75);
            rightRoot.SubtreeSum.ShouldBe(75);
        }

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
            firstTreeNode.SubtreeSum.ShouldBe(42);
        }

        [Test]
        public void SplayAdd_SmallTree()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 2));
            program.Queries.Add(new QueryTriple("+", 7));

            program.ExecuteQueries();

            var root = program.Root;
            root.Value.ShouldBe(7);
            root.LeftChild.Value.ShouldBe(2);
            root.RightChild.ShouldBeNull();
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(9);

            var oldRoot = root.LeftChild;
            oldRoot.Value.ShouldBe(2);
            oldRoot.Parent.Value.ShouldBe(7);
            oldRoot.LeftChild.ShouldBeNull();
            oldRoot.RightChild.ShouldBeNull();
            oldRoot.SubtreeSum.ShouldBe(2);
        }

        [Test]
        public void SplayFind_SmallTree()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 0));
            program.Queries.Add(new QueryTriple("+", 7));
            program.Queries.Add(new QueryTriple("?", 7));

            program.ExecuteQueries();

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
            root.SubtreeSum.ShouldBe(42);
        }

        [Test]
        public void Del_ZeroNodes()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("-", 42));

            program.ExecuteQueries();

            program.Root.ShouldBeNull();
        }

        [Test]
        public void Del_OneNode()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 42));
            program.Queries.Add(new QueryTriple("-", 42));

            program.ExecuteQueries();

            program.Root.ShouldBeNull();
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
            root.LeftChild.SubtreeSum.ShouldBe(25);
            root.RightChild.Value.ShouldBe(75);
            root.RightChild.SubtreeSum.ShouldBe(75);
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(160);
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
            root.SubtreeSum.ShouldBe(137);

            var rightChild = root.RightChild;
            rightChild.Value.ShouldBe(75);
            rightChild.RightChild.ShouldBeNull();
            rightChild.LeftChild.ShouldBeNull();
            rightChild.Parent.Value.ShouldBe(50);
            rightChild.SubtreeSum.ShouldBe(75);

            var leftChild = root.LeftChild;
            leftChild.Value.ShouldBe(12);
            leftChild.LeftChild.ShouldBeNull();
            leftChild.RightChild.ShouldBeNull();
            leftChild.Parent.Value.ShouldBe(50);
            leftChild.SubtreeSum.ShouldBe(12);
        }

        [Test]
        public void Del_Node_Variation()
        {
            var program = new Program();
            program.Queries.Add(new QueryTriple("+", 50));
            program.Queries.Add(new QueryTriple("+", 25));
            program.Queries.Add(new QueryTriple("+", 75));
            program.Queries.Add(new QueryTriple("+", 12));
            program.Queries.Add(new QueryTriple("-", 12));

            program.ExecuteQueries();

            var root = program.Root;
            root.Value.ShouldBe(25);
            root.LeftChild.ShouldBeNull();
            root.RightChild.Value.ShouldBe(75);
            root.Parent.ShouldBeNull();
            root.SubtreeSum.ShouldBe(150);

            var rightChild = root.RightChild;
            rightChild.Value.ShouldBe(75);
            rightChild.RightChild.ShouldBeNull();
            rightChild.LeftChild.Value.ShouldBe(50);
            rightChild.Parent.Value.ShouldBe(25);
            rightChild.SubtreeSum.ShouldBe(125);

            var grandchild = rightChild.LeftChild;
            grandchild.Value.ShouldBe(50);
            grandchild.RightChild.ShouldBeNull();
            grandchild.LeftChild.ShouldBeNull();
            grandchild.Parent.Value.ShouldBe(75);
            grandchild.SubtreeSum.ShouldBe(50);
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

            var great2Grandparent = new TreeNode(100, null, null, null, 126);
            var greatGrandparent = new TreeNode(3, null, null, great2Grandparent, 26);
            var grandparent = new TreeNode(5, null, null, greatGrandparent, 23);
            var parent = new TreeNode(8, null, null, grandparent, 18);
            var splay = new TreeNode(10, null, null, parent, 10);

            great2Grandparent.LeftChild = greatGrandparent;
            greatGrandparent.RightChild = grandparent;
            grandparent.RightChild = parent;
            parent.RightChild = splay;

            program.Splay(splay);

            splay.Value.ShouldBe(10);
            splay.LeftChild.Value.ShouldBe(greatGrandparent.Value);
            splay.RightChild.Value.ShouldBe(great2Grandparent.Value);
            splay.Parent.ShouldBeNull();
            splay.SubtreeSum.ShouldBe(126);

            parent.Value.ShouldBe(8);
            parent.LeftChild.Value.ShouldBe(grandparent.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(greatGrandparent.Value);
            parent.SubtreeSum.ShouldBe(13);

            grandparent.Value.ShouldBe(5);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);
            grandparent.SubtreeSum.ShouldBe(5);

            greatGrandparent.Value.ShouldBe(3);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(parent.Value);
            greatGrandparent.Parent.Value.ShouldBe(splay.Value);
            greatGrandparent.SubtreeSum.ShouldBe(16);

            great2Grandparent.Value.ShouldBe(100);
            great2Grandparent.LeftChild.ShouldBeNull();
            great2Grandparent.RightChild.ShouldBeNull();
            great2Grandparent.Parent.Value.ShouldBe(splay.Value);
            great2Grandparent.SubtreeSum.ShouldBe(100);
        }

        [Test]
        public void Splay_ZigZigLeft_ZigZigRight()
        {
            var program = new Program();

            var greatGrandparent = new TreeNode(40, null, null, null, 179);
            var grandparent = new TreeNode(50, null, null, greatGrandparent, 139);
            var parent = new TreeNode(45, null, null, grandparent, 89);
            var splay = new TreeNode(44, null, null, parent, 44);

            greatGrandparent.RightChild = grandparent;
            grandparent.LeftChild = parent;
            parent.LeftChild = splay;

            program.Splay(splay);

            splay.Value.ShouldBe(44);
            splay.LeftChild.Value.ShouldBe(greatGrandparent.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();
            splay.SubtreeSum.ShouldBe(179);

            parent.Value.ShouldBe(45);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);
            parent.SubtreeSum.ShouldBe(95);

            grandparent.Value.ShouldBe(50);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(parent.Value);
            grandparent.SubtreeSum.ShouldBe(50);

            greatGrandparent.Value.ShouldBe(40);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.Value.ShouldBe(splay.Value);
            greatGrandparent.SubtreeSum.ShouldBe(40);
        }

        [Test]
        public void Splay_ZigZagRight_ZigLeft()
        {
            var program = new Program();

            var greatGrandparent = new TreeNode(10, null, null, null, 28);
            var grandparent = new TreeNode(5, null, null, greatGrandparent, 18);
            var parent = new TreeNode(7, null, null, grandparent, 13);
            var splay = new TreeNode(6, null, null, parent, 6);

            greatGrandparent.LeftChild = grandparent;
            grandparent.RightChild = parent;
            parent.LeftChild = splay;

            program.Splay(splay);

            splay.Value.ShouldBe(6);
            splay.LeftChild.Value.ShouldBe(grandparent.Value);
            splay.RightChild.Value.ShouldBe(greatGrandparent.Value);
            splay.Parent.ShouldBeNull();
            splay.SubtreeSum.ShouldBe(28);

            parent.Value.ShouldBe(7);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(greatGrandparent.Value);
            parent.SubtreeSum.ShouldBe(7);

            grandparent.Value.ShouldBe(5);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);
            grandparent.SubtreeSum.ShouldBe(5);

            greatGrandparent.Value.ShouldBe(10);
            greatGrandparent.LeftChild.Value.ShouldBe(parent.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.Value.ShouldBe(splay.Value);
            greatGrandparent.SubtreeSum.ShouldBe(17);
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsRoot_SplayWithKids()
        {
            var program = new Program();

            var splay = new TreeNode(3, null, null, null, 10);

            var splayLeftChild = new TreeNode(2, null, null, splay, 2);
            splay.LeftChild = splayLeftChild;
            var splayRightChild = new TreeNode(5, null, null, splay, 5);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(1, null, splay, null, 11);
            splay.Parent = parent;

            var grandparent = new TreeNode(10, parent, null, null, 21);
            parent.Parent = grandparent;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(3);
            splay.LeftChild.Value.ShouldBe(parent.Value);
            splay.RightChild.Value.ShouldBe(grandparent.Value);
            splay.Parent.ShouldBeNull();
            splay.SubtreeSum.ShouldBe(21);

            splayLeftChild.Value.ShouldBe(2);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(parent.Value);
            splayLeftChild.SubtreeSum.ShouldBe(2);

            splayRightChild.Value.ShouldBe(5);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(grandparent.Value);
            splayRightChild.SubtreeSum.ShouldBe(5);

            parent.Value.ShouldBe(1);
            parent.LeftChild.ShouldBeNull();
            parent.RightChild.Value.ShouldBe(splayLeftChild.Value);
            parent.Parent.Value.ShouldBe(splay.Value);
            parent.SubtreeSum.ShouldBe(3);

            grandparent.Value.ShouldBe(10);
            grandparent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            grandparent.RightChild.ShouldBeNull();
            grandparent.Parent.Value.ShouldBe(splay.Value);
            grandparent.SubtreeSum.ShouldBe(15);
        }

        [Test]
        public void Zig_Zag_Right_GransparentIsRoot_SplayWithKids()
        {
            var program = new Program();

            var splay = new TreeNode(12, null, null, null, 38);

            var splayLeftChild = new TreeNode(11, null, null, splay, 11);
            splay.LeftChild = splayLeftChild;
            var splayRightChild = new TreeNode(15, null, null, splay, 15);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(20, splay, null, null, 58);
            splay.Parent = parent;

            var grandparent = new TreeNode(10, null, parent, null, 68);
            parent.Parent = grandparent;

            program.ZigZagRight(splay);

            splay.Value.ShouldBe(12);
            splay.LeftChild.Value.ShouldBe(grandparent.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.ShouldBeNull();
            splay.SubtreeSum.ShouldBe(68);

            splayLeftChild.Value.ShouldBe(11);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(grandparent.Value);
            splayLeftChild.SubtreeSum.ShouldBe(11);

            splayRightChild.Value.ShouldBe(15);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(parent.Value);
            splayRightChild.SubtreeSum.ShouldBe(15);

            parent.Value.ShouldBe(20);
            parent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(splay.Value);
            parent.SubtreeSum.ShouldBe(35);

            grandparent.Value.ShouldBe(10);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.Value.ShouldBe(splayLeftChild.Value);
            grandparent.Parent.Value.ShouldBe(splay.Value);
            grandparent.SubtreeSum.ShouldBe(21);
        }

        [Test]
        public void Zig_Zag_Left_GransparentIsNotRoot_ParentAndGrandparentWithChild()
        {
            var program = new Program();

            var splay = new TreeNode(15, null, null, null, 15);

            var parent = new TreeNode(10, null, splay, null, 29);
            splay.Parent = parent;
            var parentLeftChild = new TreeNode(4, null, null, parent, 4);
            parent.LeftChild = parentLeftChild;

            var grandparent = new TreeNode(20, parent, null, null, 74);
            parent.Parent = grandparent;
            var grandparentRightChild = new TreeNode(25, null, null, grandparent, 25);
            grandparent.RightChild = grandparentRightChild;

            var greatGrandparent = new TreeNode(18, null, grandparent, null, 1992);
            grandparent.Parent = greatGrandparent;
            program.Root = greatGrandparent;

            var greatGrandparentRightChild = new TreeNode(1000, null, null, greatGrandparent, 1900);
            greatGrandparent.RightChild = greatGrandparentRightChild;

            var greatGrandparentRightLeftChild = new TreeNode(900, null, null, greatGrandparentRightChild, 900);
            greatGrandparentRightChild.LeftChild = greatGrandparentRightLeftChild;

            program.ZigZagLeft(splay);

            splay.Value.ShouldBe(15);
            splay.LeftChild.Value.ShouldBe(10);
            splay.RightChild.Value.ShouldBe(20);
            splay.Parent.Value.ShouldBe(18);
            splay.SubtreeSum.ShouldBe(74);

            parent.Value.ShouldBe(10);
            parent.LeftChild.Value.ShouldBe(4);
            parent.RightChild.ShouldBeNull();
            parent.Parent.Value.ShouldBe(15);
            parent.SubtreeSum.ShouldBe(14);

            parentLeftChild.Value.ShouldBe(4);
            parentLeftChild.LeftChild.ShouldBeNull();
            parentLeftChild.RightChild.ShouldBeNull();
            parentLeftChild.Parent.Value.ShouldBe(10);
            parentLeftChild.SubtreeSum.ShouldBe(4);

            grandparent.Value.ShouldBe(20);
            grandparent.LeftChild.ShouldBeNull();
            grandparent.RightChild.Value.ShouldBe(25);
            grandparent.Parent.Value.ShouldBe(15);
            grandparent.SubtreeSum.ShouldBe(45);

            grandparentRightChild.Value.ShouldBe(25);
            grandparentRightChild.LeftChild.ShouldBeNull();
            grandparentRightChild.RightChild.ShouldBeNull();
            grandparentRightChild.Parent.Value.ShouldBe(20);
            grandparentRightChild.SubtreeSum.ShouldBe(25);

            greatGrandparent.Value.ShouldBe(18);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(15);
            greatGrandparent.Parent.ShouldBeNull();
            greatGrandparent.SubtreeSum.ShouldBe(1992);

            greatGrandparentRightChild.Value.ShouldBe(1000);
            greatGrandparentRightChild.LeftChild.Value.ShouldBe(900);
            greatGrandparentRightChild.RightChild.ShouldBeNull();
            greatGrandparentRightChild.Parent.Value.ShouldBe(18);
            greatGrandparentRightChild.SubtreeSum.ShouldBe(1900);

            greatGrandparentRightLeftChild.Value.ShouldBe(900);
            greatGrandparentRightLeftChild.LeftChild.ShouldBeNull();
            greatGrandparentRightLeftChild.RightChild.ShouldBeNull();
            greatGrandparentRightLeftChild.Parent.Value.ShouldBe(1000);
            greatGrandparentRightLeftChild.SubtreeSum.ShouldBe(900);

            program.Root.Value.ShouldBe(18);
        }

        [Test]
        public void Zig_Zig_Left_GrandparentIsNotRoot_SplayWithLeftAndRightChildren_ParentWithChild_Grandparent_WithChild()
        {
            var program = new Program();

            var splay = new TreeNode(5, null, null, null, 13);

            var splayLeftChild = new TreeNode(2, null, null, splay, 2);
            splay.LeftChild = splayLeftChild;

            var splayRightChild = new TreeNode(6, null, null, splay, 6);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(10, splay, null, null, 38);
            splay.Parent = parent;

            var parentRightChild = new TreeNode(15, null, null, parent, 15);
            parent.RightChild = parentRightChild;

            var grandparent = new TreeNode(20, parent, null, null, 88);
            parent.Parent = grandparent;

            var grandparentRightChild = new TreeNode(30, null, null, grandparent, 30);
            grandparent.RightChild = grandparentRightChild;

            var greatGrandparent = new TreeNode(10, grandparent, null, null, 98);
            grandparent.Parent = greatGrandparent;

            program.ZigZigLeft(splay);

            splay.Value.ShouldBe(5);
            splay.LeftChild.Value.ShouldBe(splayLeftChild.Value);
            splay.RightChild.Value.ShouldBe(parent.Value);
            splay.Parent.Value.ShouldBe(greatGrandparent.Value);
            splay.SubtreeSum.ShouldBe(88);

            splayLeftChild.Value.ShouldBe(2);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(splay.Value);
            splayLeftChild.SubtreeSum.ShouldBe(2);

            splayRightChild.Value.ShouldBe(6);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(parent.Value);
            splayRightChild.SubtreeSum.ShouldBe(6);

            parent.Value.ShouldBe(10);
            parent.LeftChild.Value.ShouldBe(splayRightChild.Value);
            parent.RightChild.Value.ShouldBe(grandparent.Value);
            parent.Parent.Value.ShouldBe(splay.Value);
            parent.SubtreeSum.ShouldBe(81);

            parentRightChild.Value.ShouldBe(15);
            parentRightChild.RightChild.ShouldBeNull();
            parentRightChild.LeftChild.ShouldBeNull();
            parentRightChild.Parent.Value.ShouldBe(grandparent.Value);
            parentRightChild.SubtreeSum.ShouldBe(15);

            grandparent.Value.ShouldBe(20);
            grandparent.LeftChild.Value.ShouldBe(parentRightChild.Value);
            grandparent.RightChild.Value.ShouldBe(grandparentRightChild.Value);
            grandparent.Parent.Value.ShouldBe(parent.Value);
            grandparent.SubtreeSum.ShouldBe(65);

            grandparentRightChild.Value.ShouldBe(30);
            grandparentRightChild.RightChild.ShouldBeNull();
            grandparentRightChild.LeftChild.ShouldBeNull();
            grandparentRightChild.Parent.Value.ShouldBe(grandparent.Value);
            grandparentRightChild.SubtreeSum.ShouldBe(30);

            greatGrandparent.Value.ShouldBe(10);
            greatGrandparent.LeftChild.Value.ShouldBe(splay.Value);
            greatGrandparent.RightChild.ShouldBeNull();
            greatGrandparent.Parent.ShouldBeNull();
            greatGrandparent.SubtreeSum.ShouldBe(98);
        }

        [Test]
        public void Zig_Zig_Right_GrandparentIsNotRoot_SplayWithLeftAndRightChildren_ParentWithChild_Grandparent_WithChild()
        {
            var program = new Program();

            var splay = new TreeNode(20, null, null, null, 60);

            var splayLeftChild = new TreeNode(15, null, null, splay, 15);
            splay.LeftChild = splayLeftChild;

            var splayRightChild = new TreeNode(25, null, null, splay, 25);
            splay.RightChild = splayRightChild;

            var parent = new TreeNode(10, null, splay, null, 74);
            splay.Parent = parent;

            var parentLeftChild = new TreeNode(4, null, null, parent, 4);
            parent.LeftChild = parentLeftChild;
            parent.RightChild = splay;
            splay.Parent = parent;

            var grandparent = new TreeNode(2, parent, null, null, 77);
            parent.Parent = grandparent;

            var grandparentLeftChild = new TreeNode(1, null, null, grandparent, 1);
            grandparent.LeftChild = grandparentLeftChild;

            var greatGrandparent = new TreeNode(0, null, grandparent, null, 77);
            grandparent.Parent = greatGrandparent;

            program.ZigZigRight(splay);

            splay.Value.ShouldBe(20);
            splay.LeftChild.Value.ShouldBe(10);
            splay.RightChild.Value.ShouldBe(25);
            splay.Parent.Value.ShouldBe(0);
            splay.SubtreeSum.ShouldBe(77);

            splayLeftChild.Value.ShouldBe(15);
            splayLeftChild.LeftChild.ShouldBeNull();
            splayLeftChild.RightChild.ShouldBeNull();
            splayLeftChild.Parent.Value.ShouldBe(10);
            splayLeftChild.SubtreeSum.ShouldBe(15);

            splayRightChild.Value.ShouldBe(25);
            splayRightChild.LeftChild.ShouldBeNull();
            splayRightChild.RightChild.ShouldBeNull();
            splayRightChild.Parent.Value.ShouldBe(20);
            splayRightChild.SubtreeSum.ShouldBe(25);

            parent.Value.ShouldBe(10);
            parent.LeftChild.Value.ShouldBe(2);
            parent.RightChild.Value.ShouldBe(15);
            parent.Parent.Value.ShouldBe(20);
            parent.SubtreeSum.ShouldBe(32);

            parentLeftChild.Value.ShouldBe(4);
            parentLeftChild.RightChild.ShouldBeNull();
            parentLeftChild.LeftChild.ShouldBeNull();
            parentLeftChild.Parent.Value.ShouldBe(2);
            parentLeftChild.SubtreeSum.ShouldBe(4);

            grandparent.Value.ShouldBe(2);
            grandparent.LeftChild.Value.ShouldBe(1);
            grandparent.RightChild.Value.ShouldBe(4);
            grandparent.Parent.Value.ShouldBe(10);
            grandparent.SubtreeSum.ShouldBe(7);

            grandparentLeftChild.Value.ShouldBe(1);
            grandparentLeftChild.RightChild.ShouldBeNull();
            grandparentLeftChild.LeftChild.ShouldBeNull();
            grandparentLeftChild.Parent.Value.ShouldBe(2);
            grandparentLeftChild.SubtreeSum.ShouldBe(1);

            greatGrandparent.Value.ShouldBe(0);
            greatGrandparent.LeftChild.ShouldBeNull();
            greatGrandparent.RightChild.Value.ShouldBe(20);
            greatGrandparent.Parent.ShouldBeNull();
            greatGrandparent.SubtreeSum.ShouldBe(77);
        }

        [Test]
        public void ZigLeft_RightAndLeftNodesOnSplayNode_RightNodeOnParent()
        {
            var program = new Program();

            var splayNode = new TreeNode(1, null, null, null, 5);
            var splayRightChildNode = new TreeNode(4, null, null, splayNode, 4);
            splayNode.RightChild = splayRightChildNode;
            var splayLeftChildNode = new TreeNode(0, null, null, splayNode);
            splayNode.LeftChild = splayLeftChildNode;

            var parentNode = new TreeNode(5, splayNode, null, null, 17);
            splayNode.Parent = parentNode;
            var parentRightChildNode = new TreeNode(7, null, null, parentNode, 7);
            parentNode.RightChild = parentRightChildNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.Value.ShouldBe(splayLeftChildNode.Value);
            splayNode.RightChild.Value.ShouldBe(parentNode.Value);
            splayNode.Parent.ShouldBeNull();
            splayNode.SubtreeSum.ShouldBe(17);

            splayRightChildNode.Value.ShouldBe(4);
            splayRightChildNode.LeftChild.ShouldBeNull();
            splayRightChildNode.RightChild.ShouldBeNull();
            splayRightChildNode.Parent.Value.ShouldBe(parentNode.Value);
            splayRightChildNode.SubtreeSum.ShouldBe(4);

            splayLeftChildNode.Value.ShouldBe(0);
            splayLeftChildNode.LeftChild.ShouldBeNull();
            splayLeftChildNode.RightChild.ShouldBeNull();
            splayLeftChildNode.Parent.Value.ShouldBe(splayNode.Value);
            splayLeftChildNode.SubtreeSum.ShouldBe(0);

            parentNode.Value.ShouldBe(5);
            parentNode.LeftChild.Value.ShouldBe(splayRightChildNode.Value);
            parentNode.RightChild.Value.ShouldBe(parentRightChildNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
            parentNode.SubtreeSum.ShouldBe(16);

            parentRightChildNode.Value.ShouldBe(7);
            parentRightChildNode.LeftChild.ShouldBeNull();
            parentRightChildNode.RightChild.ShouldBeNull();
            parentRightChildNode.Parent.Value.ShouldBe(parentNode.Value);
            parentRightChildNode.SubtreeSum.ShouldBe(7);
        }

        [Test]
        public void ZigRight_RightAndLeftNodesOnSplayNode_LeftNodeOnParent()
        {
            var program = new Program();

            var splayNode = new TreeNode(5, null, null, null, 16);
            var splayRightChildNode = new TreeNode(7, null, null, splayNode, 7);
            splayNode.RightChild = splayRightChildNode;
            var splayLeftChildNode = new TreeNode(4, null, null, splayNode, 4);
            splayNode.LeftChild = splayLeftChildNode;

            var parentNode = new TreeNode(1, null, splayNode, null, 17);
            splayNode.Parent = parentNode;
            var parentLeftChildNode = new TreeNode(0, null, null, parentNode);
            parentNode.LeftChild = parentLeftChildNode;

            program.ZigRight(splayNode);

            splayNode.Value.ShouldBe(5);
            splayNode.LeftChild.Value.ShouldBe(parentNode.Value);
            splayNode.RightChild.Value.ShouldBe(splayRightChildNode.Value);
            splayNode.Parent.ShouldBeNull();
            splayNode.SubtreeSum.ShouldBe(17);

            splayRightChildNode.Value.ShouldBe(7);
            splayRightChildNode.LeftChild.ShouldBeNull();
            splayRightChildNode.RightChild.ShouldBeNull();
            splayRightChildNode.Parent.Value.ShouldBe(splayNode.Value);
            splayRightChildNode.SubtreeSum.ShouldBe(7);

            splayLeftChildNode.Value.ShouldBe(4);
            splayLeftChildNode.LeftChild.ShouldBeNull();
            splayLeftChildNode.RightChild.ShouldBeNull();
            splayLeftChildNode.Parent.Value.ShouldBe(parentNode.Value);
            splayLeftChildNode.SubtreeSum.ShouldBe(4);

            parentNode.Value.ShouldBe(1);
            parentNode.LeftChild.Value.ShouldBe(parentLeftChildNode.Value);
            parentNode.RightChild.Value.ShouldBe(splayLeftChildNode.Value);
            parentNode.Parent.Value.ShouldBe(splayNode.Value);
            parentNode.SubtreeSum.ShouldBe(5);

            parentLeftChildNode.Value.ShouldBe(0);
            parentLeftChildNode.LeftChild.ShouldBeNull();
            parentLeftChildNode.RightChild.ShouldBeNull();
            parentLeftChildNode.Parent.Value.ShouldBe(parentNode.Value);
            parentLeftChildNode.SubtreeSum.ShouldBe(0);
        }

        [Test]
        public void DetermineZigZag_None()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.None);
        }

        [Test]
        public void DetermineZigZag_Zig_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var parentNode = new TreeNode(1, splayNode);
            splayNode.Parent = parentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigLeft);
        }

        [Test]
        public void DetermineZigZag_Zig_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var parentNode = new TreeNode(1, null, splayNode);
            splayNode.Parent = parentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigRight);
        }

        [Test]
        public void DetermineZigZag_ZigZig_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var parentNode = new TreeNode(1, splayNode);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, parentNode);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZigLeft);
        }

        [Test]
        public void DetermineZigZag_ZigZig_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var parentNode = new TreeNode(1, null, splayNode);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, null, parentNode);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZigRight);
        }

        [Test]
        public void DetermineZigZag_Zig_Zag_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var parentNode = new TreeNode(1, null, splayNode);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, parentNode);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZagLeft);
        }

        [Test]
        public void DetermineZigZag_Zig_Zag_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0);
            var parentNode = new TreeNode(1, splayNode);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, null, parentNode);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe(ZiggaZigAh.ZigZagRight);
        }

        [Test, TestCaseSource(nameof(Raw))]
        public void TransformArrayIntoInputTriple_Tests(string caseName, string[] input,
            List<QueryTriple> expected)
        {
            var program = new Program();
            program.AddRawInputToList(input);

            var operation = input[0];
            var low = int.Parse(input[1]);
            var high = -1;
            if (input.Length == 3)
                high = int.Parse(input[2]);

            var triple = program.Queries[0];
            triple.Operation.ShouldBe(operation);
            triple.Low.ShouldBe(low);
            triple.High.ShouldBe(high);
        }

        #region
        private static readonly object[] Raw =
        {
                new object[] { "A", new[] {"+","1"},  new List<QueryTriple> {
                        new QueryTriple("+", 1)
                    }
                },
                new object[] { "B", new[] {"?","5"},  new List<QueryTriple> {
                        new QueryTriple("?", 5)
                    }
                },
                new object[] { "C", new[] {"s","1","2"},  new List<QueryTriple> {
                        new QueryTriple("s", 1, 2)
                    }
                },
                new object[] { "D", new[] {"-","1"},  new List<QueryTriple> {
                        new QueryTriple("-", 1)
                    }
                },
                new object[] { "E", new[] {"s","999999999","1000000000"},  new List<QueryTriple> {
                        new QueryTriple("s",999999999,1000000000)
                    }
                },
        };
        #endregion
    }
}
