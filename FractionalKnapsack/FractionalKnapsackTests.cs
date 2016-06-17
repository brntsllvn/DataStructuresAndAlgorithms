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
        public void One_Item_Fills_Knapsack()
        {
            var numberOfItems = 1;
            var capacityOfKnapsack = 50;

            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 60, Weight = 60},
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr).ShouldBe(50.0000);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }

        [Test]
        public void One_Item_Fills_Knapsack_Capacity()
        {
            var numberOfItems = 1;
            var capacityOfKnapsack = 50;

            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 60, Weight = 100},
                new ValueAndWeight {Value = 100, Weight = 50},
                new ValueAndWeight {Value = 120, Weight = 30},
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr).ShouldBe(30.0000);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }

        [Test]
        public void Test_2()
        {
            var numberOfItems = 3;
            var capacityOfKnapsack = 50;

            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 60, Weight = 20},
                new ValueAndWeight {Value = 100, Weight = 50},
                new ValueAndWeight {Value = 120, Weight = 30},
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr).ShouldBe(180.0000);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
