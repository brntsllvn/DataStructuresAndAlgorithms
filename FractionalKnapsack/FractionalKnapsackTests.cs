﻿using NUnit.Framework;
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
            //var numberOfItems = 1;
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
            //var numberOfItems = 1;
            var capacityOfKnapsack = 50;

            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 60, Weight = 100},
                new ValueAndWeight {Value = 100, Weight = 100},
                new ValueAndWeight {Value = 200, Weight = 100},
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr).ShouldBe(100.0000);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }

        [Test]
        public void FractionalKnapsack_MultipleItems()
        {
            //var numberOfItems = 3;
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


        [Test]
        public void FractionalKnapsack_2()
        {
            //var numberOfItems = 1;
            var capacityOfKnapsack = 1000;

            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 500, Weight = 30}
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr).ShouldBe(500.000);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }

        [Test]
        public void FractionalKnapsack_3()
        {
            //var numberOfItems = 4;
            var capacityOfKnapsack = 15;

            var valAndWeightArr = new[]
            {
                new ValueAndWeight {Value = 1, Weight = 100},
                new ValueAndWeight {Value = 100, Weight = 1},
                new ValueAndWeight {Value = 1, Weight = 50},
                new ValueAndWeight {Value = 50, Weight = 1},
            };

            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr).ShouldBe(150.26);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(capacityOfKnapsack, valAndWeightArr), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
