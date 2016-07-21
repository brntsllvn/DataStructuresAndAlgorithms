using NUnit.Framework;
using Shouldly;

namespace BeefyCases
{
    [TestFixture]
    class BeefyTestCases
    {
        [TestCase("A", 0, new[] { 0 })]
        [TestCase("B", 1, new[] { 0, 1 })]
        [TestCase("C", 3, new[] { 0, 1, 2 })]
        public void Standard_TestCases(string caseName, int expected, int[] input)
        {
            var f0 = new Launcher();
            f0.AddSimpleType(input).ShouldBe(expected);
        }

        #region
        //[TestCase("D", 0, new[] { new AwesomeObject(0) })]
        //[TestCase("E", 1, new[] { new AwesomeObject(0), new AwesomeObject(1) })]
        //[TestCase("F", 3, new[] { new AwesomeObject(0), new AwesomeObject(1), new AwesomeObject(2) })]
        //public void Broken_NonStandard_TestCases(string caseName, int expected, AwesomeObject[] input)
        //{
        //    var f0 = new Launcher();
        //    f0.AddComplicatedType(input).ShouldBe(expected);
        //}
        #endregion

        #region
        // http://www.nunit.org/index.php?p=testCaseSource&r=2.5
        [Test, TestCaseSource(nameof(FancyCases))]
        public void Working_Standard_TestCases(string caseName, int expected, AwesomeObject[] input)
        {
            var f0 = new Launcher();
            f0.AddComplicatedType(input).ShouldBe(expected);
        }

        private static readonly object[] FancyCases =
        {
            new object[] {"D", 0, new[] {new AwesomeObject(0)}},
            new object[] {"E", 1, new[] {new AwesomeObject(0), new AwesomeObject(1)}},
            new object[] {"F", 3, new[] {new AwesomeObject(0), new AwesomeObject(1), new AwesomeObject(2)}}
        };
        #endregion
    }
}
