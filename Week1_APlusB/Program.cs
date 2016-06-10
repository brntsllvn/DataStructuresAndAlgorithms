using System;
using System.Linq;
//using NUnit.Framework;
//using Shouldly;

namespace APlusB
{
    public class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

            var sum = input[0] + input[1];

            Console.WriteLine(sum);
            Console.Read();
        }
    }

    //[TestFixture]
    //class ProgramTests
    //{
    //    [Test]
    //    public void Program_Sum_Works()
    //    {
    //        var a = 3;
    //        var b = 17;
    //        var sum = a + b;

    //        sum.ShouldBe(20);
    //    }
    //}
}
