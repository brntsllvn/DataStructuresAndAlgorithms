using NUnit.Framework;
using Shouldly;

namespace zChainHashing
{
    [TestFixture]
    class ChainHashingTests
    {

        [TestCase("a", 3, 1)]
        [TestCase("a", 12, 1)]
        [TestCase("b", 12, 2)]
        [TestCase("`", 7, 5)]
        [TestCase("cd", 7, 2)]
        [TestCase("world", 7, 1)]
        [TestCase("HellO", 13, 3)]
        public void Calculate_Hash_Fn(string inputString, int numBuckets, int expectedBucket)
        {
            var f0 = new Program();
            var bucketNum = f0.MapStringToBucket(inputString, f0.BigPrime, numBuckets);
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
