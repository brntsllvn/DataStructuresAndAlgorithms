using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace FibonacciLastDigit
{
    [TestFixture]
    class FibonacciTests
    {
        [TestCase(0,  "0")]
        [TestCase(1,  "1")]
        [TestCase(2,  "1")]
        [TestCase(3,  "2")]
        [TestCase(10, "5")]
        [TestCase(20, "5")]
        [TestCase(50, "5")]
        [TestCase(99, "6")]
        public void Fibonacci(long input, string expected)
        {
            var f0 = new Launcher();
            f0.CalculateFibonacciLastDigit(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CalculateFibonacciLastDigit(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
