﻿using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace Fibonacci
{
    [TestFixture]
    class FibonacciTests
    {
        [TestCase(0,0)]
        [TestCase(1,1)]
        [TestCase(2,1)]
        [TestCase(3,2)]
        [TestCase(10,55)]
        [TestCase(20,6765)]
        [TestCase(50,12586269025)]
        public void Fibonacci(long input, long expected)
        {
            var f0 = new Launcher();
            f0.CalculateFibonacci(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CalculateFibonacci(input), TimeSpan.FromMilliseconds(1500));

            // not correct...? Off by a couple orders of magnitude. Answer on instructor machine: 8667136/536870912
            //long memory = GC.GetTotalMemory(true);
            //memory.ShouldBe(15);

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
