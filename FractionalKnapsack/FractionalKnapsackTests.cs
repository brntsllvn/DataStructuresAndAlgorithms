using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;
using static FractionalKnapsack.Launcher;

namespace FractionalKnapsack
{
    [TestFixture]
    public class FractionalKnapsackTests
    {
        [Test]
        public void Test_1()
        {
            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 60, Weight = 20},
                new ValueAndWeight {Value = 100, Weight = 50},
                new ValueAndWeight {Value = 120, Weight = 30},
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(valAndWeightArr).ShouldBe(180.0000);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
