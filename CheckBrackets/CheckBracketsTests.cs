using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace CheckBrackets
{
    [TestFixture]
    class CheckBracketsTests
    {
        [TestCase("[", "1")]
        [TestCase("}", "1")]
        [TestCase("b", "1")]
        [TestCase("[]", "Success")]
        [TestCase("[}", "2")]
        [TestCase("[](()", "3")]
        [TestCase("[]()", "Success")]
        [TestCase("([])", "Success")]
        [TestCase("(())", "Success")]
        [TestCase("([]!!!)?", "Success")]
        [TestCase("foo(bar);", "Success")]
        [TestCase("foo(bar[i);", "10")]
        public void AAA_CheckingBrackets(string input, string expected)
        {
            var f0 = new Launcher();
             f0.bracketsBalanced(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.bracketsBalanced(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }

        [Test]
        public void MorePushing()
        {
            var myStack = new BjsStack();

            myStack.Empty().ShouldBe(true);

            myStack.Push('h');
            myStack.Push('P');
            myStack.Push('q');
            myStack.Push('L');

            myStack.Empty().ShouldBe(false);

            myStack.TopNode().Payload.ShouldBe('L');
            myStack.TopNode().Next.Payload.ShouldBe('q');

            myStack.PopNode().Payload.ShouldBe('L');
            myStack.TopNode().Payload.ShouldBe('q');
            myStack.TopNode().Next.Payload.ShouldBe('P');

            myStack.PopNode().Payload.ShouldBe('q');
            myStack.TopNode().Payload.ShouldBe('P');
            myStack.TopNode().Next.Payload.ShouldBe('h');

            myStack.TopNode().Payload.ShouldBe('P');
            myStack.PopNode().Payload.ShouldBe('P');
            myStack.PopNode().Payload.ShouldBe('h');

            myStack.Empty().ShouldBe(true);
        }
    }
}
