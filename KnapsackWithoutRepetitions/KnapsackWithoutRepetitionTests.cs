using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace KnapsackWithoutRepetitions
{
    [TestFixture]
    class KnapsackWithoutRepetitionTests
    {
        [Test]
        public void ClassExample()
        {
            var knapsackCapacity = 10;
            var potentialItemsValuesAndWeights = new int[,] {{30, 6}, {14, 3}, {16, 4}, {9, 2}};
            var expectedItemsInKnapsack = new int[] {0, 1, 0, 1};

            var f0 = new Launcher();
            f0.KnapsackNoRep(knapsackCapacity, potentialItemsValuesAndWeights).ShouldBe(expectedItemsInKnapsack);

            Should.CompleteIn(
                () => f0.KnapsackNoRep(knapsackCapacity, potentialItemsValuesAndWeights), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
