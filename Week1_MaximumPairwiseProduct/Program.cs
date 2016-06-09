using System;
using NUnit.Framework;
using Shouldly;

namespace Week1_MaximumPairwiseProduct
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello, world!");
            Console.Read();
        }
    }

    [TestFixture]
    class ProgramTests
    {
        [Test]
        public void Program_Sum_Works()
        {
            var a = 3;
            var b = 17;
            var sum = a + b;

            sum.ShouldBe(20);
        }
    }
}
