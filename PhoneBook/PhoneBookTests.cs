using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace PhoneBook
{
    [TestFixture]
    class PhoneBookTests
    {
        [TestCase("A","hi","hi")]
        public void PhoneBook_Cases(string caseName, string input, string expected)
        {
            input.ShouldBe(expected);
        }

        [Test, TestCaseSource(nameof(ResultList))]
        public void A_Test(string caseName, List<InputTriple> input)
        {
            var firstInput = input[0];
            firstInput.Command.ShouldBe("add");
            firstInput.Number.ShouldBe("911");
            firstInput.Person.ShouldBe("police");
        }

        private static readonly object[] ResultList =
        {
            new object[] { "A", new List<InputTriple> { new InputTriple("add","911","police") } },
        };
    }
}
