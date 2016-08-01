using NUnit.Framework;
using Shouldly;

namespace LauncherTemplate
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

    //[TestFixture]
    //class PhoneBookTests
    //{
    //    [TestCase("A", "hi", "hi")]
    //    public void PhoneBook_Cases(string caseName, string input, string expected)
    //    {
    //        input.ShouldBe(expected);
    //    }

    //    [Test, TestCaseSource(nameof(ResultList))]
    //    public void A_Test(string caseName, List<InputTriple> input)
    //    {
    //        var firstInput = input[0];
    //        firstInput.Command.ShouldBe("add");
    //        firstInput.Number.ShouldBe("911");
    //        firstInput.Person.ShouldBe("police");
    //    }

    //    private static readonly object[] ResultList =
    //    {
    //        new object[] { "A", new List<InputTriple> { new InputTriple("add","911","police") } },
    //    };
    //}
}
