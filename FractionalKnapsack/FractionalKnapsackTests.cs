using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace FractionalKnapsack
{
    [TestFixture]
    class FractionalKnapsackTests
    {

        //private Launcher.ValueAndWeight[1] _test1ValuesAndWeights;


        //        new Launcher.ValueAndWeight { Value = 15, Weight = 12 }

        public Launcher.ValueAndWeight[] Test1Array
        {
            get
            {
                var arr = new Launcher.ValueAndWeight[1];
                arr[0] = new Launcher.ValueAndWeight { Value = 15, Weight = 10 };
                return arr;
            }
        }


        [TestCase(0.0, FractionalKnapsackTests.Test1Array)]
        public void Test_1(decimal expected, Launcher.ValueAndWeight[] input)
        {
            var f0 = new Launcher();
            f0.FractionalKnapsackCalculator(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.FractionalKnapsackCalculator(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
