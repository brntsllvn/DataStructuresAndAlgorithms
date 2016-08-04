using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace zChainHashing
{
    [TestFixture]
    class ChainHashingTests
    {
        [TestCase("HellO", 3)]
        public void Add(string input, int numBuckets)
        {
            var f0 = new Program();
            f0.NumberOfBuckets = numBuckets;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];

            var bucketNumber = f0.MapStringToBucket(input);

            f0.Add(input, bucketNumber);
            f0.BucketList[bucketNumber].ShouldContain(input);
        }

        [TestCase("HellO", 3)]
        public void AddDuplicate(string input, int numBuckets)
        {
            var f0 = new Program();
            f0.NumberOfBuckets = numBuckets;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            var bucketNumber = f0.MapStringToBucket(input);
            f0.Add(input, bucketNumber);

            f0.Add(input, bucketNumber);
            f0.BucketList[bucketNumber].Count.ShouldBe(1);
            f0.BucketList[bucketNumber].ShouldContain(input);
        }

        [Test]
        public void AddNonDuplicateInProperOrder()
        {
            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            var bucketNumber = f0.MapStringToBucket("HellO");
            f0.Add("HellO", bucketNumber);

            f0.Add("world", bucketNumber);
            f0.BucketList[bucketNumber].Count.ShouldBe(2);
            f0.BucketList[bucketNumber][0].ShouldBe("world");
            f0.BucketList[bucketNumber][1].ShouldBe("HellO");
        }

        [TestCase()]
        public void Del_Command()
        {

        }

        [TestCase()]
        public void Find_Command()
        {

        }

        [TestCase()]
        public void Check_Command()
        {

        }

        [TestCase("a", 3, 1)]
        [TestCase("a", 12, 1)]
        [TestCase("b", 12, 2)]
        [TestCase("`", 7, 5)]
        [TestCase("cd", 7, 2)]
        [TestCase("world", 7, 1)]
        [TestCase("world", 5, 4)]
        [TestCase("HellO", 5, 4)]
        [TestCase("HellO", 13, 3)]
        public void DetermineBucket(string inputString, int numBuckets, int expectedBucket)
        {
            var f0 = new Program();
            f0.NumberOfBuckets = numBuckets;
            var bucketNum = f0.MapStringToBucket(inputString);
            bucketNum.ShouldBe(expectedBucket);
        }

        [TestCase("a", 97)]
        [TestCase("x", 120)]
        [TestCase("H", 72)]
        [TestCase("^", 94)]
        public void Get_ASCII(string something, byte expected)
        {
            var f0 = new Program();
            f0.GetAscii(something).ShouldBe(new byte[] { expected });
        }

        [TestCase("A", new long[] { 0 }, "hello")]
        public void Test_1(string caseName, long[] input, string expected)
        {
            var f0 = new Program();
            var result = f0.MagicFunctionThatSolvesAllProblems();
            result.ShouldBe(expected);
        }

        //[TestCase("A", "hi", "hi")]
        //public void PhoneBook_Cases(string caseName, string input, string expected)
        //{
        //    input.ShouldBe(expected);
        //}

        //[Test, TestCaseSource(nameof(ResultList))]
        //public void A_Test(string caseName, List<InputTriple> input)
        //{
        //    var firstInput = input[0];
        //    firstInput.Command.ShouldBe("add");
        //    firstInput.Number.ShouldBe("911");
        //    firstInput.Person.ShouldBe("police");
        //}

        //private static readonly object[] ResultList =
        //{
        //        new object[] { "A", new List<InputTriple> { new InputTriple("add","911","police") } },
        //};
    }
}
