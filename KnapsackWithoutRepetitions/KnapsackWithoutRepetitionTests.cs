﻿using System;
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
            var potentialItemValuesAndWeights = new[,] { { 30, 6 }, { 14, 3 }, { 16, 4 }, { 9, 2 } };
            //var expectedItemsInKnapsack = new[] { 1, 0, 1, 0 };
            var expectedValue = 48;

            var f0 = new Launcher();
            f0.KnapsackNoRep(knapsackCapacity, potentialItemValuesAndWeights).ShouldBe(expectedValue);

            Should.CompleteIn(
                () => f0.KnapsackNoRep(knapsackCapacity, potentialItemValuesAndWeights), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
