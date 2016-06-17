using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace ChangingMoney
{
    [TestFixture]
    class CMTests
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 1)]
        [TestCase(6, 2)]
        [TestCase(8, 4)]
        [TestCase(10, 1)]
        [TestCase(14, 5)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(28, 6)]
        public void Test_1(int input, int expected)
        {
            var f0 = new Launcher();
            f0.MinimumNumberOfCoinsToChangeMoney(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.MinimumNumberOfCoinsToChangeMoney(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
