using System;
using NUnit.Framework;
using Shouldly;

namespace APlusB
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a: ");
            var a = int.Parse(Console.ReadLine());

            Console.WriteLine("b: ");
            var b = int.Parse(Console.ReadLine());

            var sum = a + b;

            Console.WriteLine("sum: {0}", sum);
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
