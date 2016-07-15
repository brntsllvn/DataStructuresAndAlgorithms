using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace KnapsackWithoutRepetitions
{
    [TestFixture]
    class KnapsackWithoutRepetitionTests
    {
        [TestCase("AA",  99, new[] { 0, 100 }, new[] { 0, 100 }, 0 )]
        [TestCase("AB", 100, new[] { 0, 100 }, new[] { 0, 100 }, 100 )]
        [TestCase("A", 10, new[] { 0, 6, 3, 4, 2 }, new[] { 0, 30, 14, 16, 9 }, 46 )]
        [TestCase("B", 10, new[] { 0, 1, 4, 8 }, new[] { 0, 1, 4, 8 }, 9)]
        [TestCase("C", 1000, new[] { 0, 1000, 4, 8 }, new[] { 0, 1, 4, 8 }, 12)]
        public void KnapNoRep(string testCase, int knapsackCapacity, int[] weights, int[] values, int expectedValue)
        {
            weights.Length.ShouldBe(values.Length);

            var f0 = new Launcher();
            f0.KnapsackNoRep(knapsackCapacity, weights, values).ShouldBe(expectedValue);

            Should.CompleteIn(
                () => f0.KnapsackNoRep(knapsackCapacity, weights, values), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
