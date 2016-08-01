using NUnit.Framework;
using Shouldly;

namespace PhoneBook
{
    [TestFixture]
    class PhoneBookTests
    {
        [TestCase("A", new long[] { 0 }, "hello")]
        public void Test_1(string caseName, long[] input, string expected)
        {
            var f0 = new Program();
            var result = f0.MagicFunctionThatSolvesAllProblems();
            result.ShouldBe(expected);
        }
    }
}
