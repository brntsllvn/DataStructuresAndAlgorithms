using NUnit.Framework;
using Shouldly;

namespace MakingChange
{

    [TestFixture]
    class MakingChangeTests
    {
        [TestCase("A USD", new int[] { 1, 5, 10, 25, 50 }, 0, 0)]
        [TestCase("B USD", new int[] { 1, 5, 10, 25, 50 }, 1, 1)]
        [TestCase("C USD", new int[] { 1, 5, 10, 25, 50 }, 2, 2)]
        [TestCase("D USD", new int[] { 1, 5, 10, 25, 50 }, 5, 1)]
        public void MakeChange(string caseName, int[] coinDenominations, int amountToChange, int expectedNumCoins)
        {
            var f0 = new Launcher();
            f0.DynamicProgrammingMakeChange(coinDenominations, amountToChange).ShouldBe(expectedNumCoins);
        }
    }
}
