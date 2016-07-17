using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace CheckBrackets
{
    [TestFixture]
    class CheckBracketsTests
    {
        [TestCase("[]", "Success")]
        //[TestCase("{", "1")]
        //[TestCase("(", "1")]
        //[TestCase("[", "1")]
        //[TestCase("[\"\"]", "Success")]
        //[TestCase("[\'\"]\'", "Success")]
        public void Test_1(string input, string expected)
        {
            var sanitizedInput = input.ToCharArray();
            //sanitizedInput.ShouldBe(new char[5] { '[', '\'', '"', ']', '\'' });

            var f0 = new Launcher();
            f0.CheckBrackets(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CheckBrackets(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
