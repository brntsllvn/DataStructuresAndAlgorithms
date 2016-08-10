using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace SetWithRangeSums
{
    [TestFixture]
    class aaaSetWithRangeSums
    {
        [Test, TestCaseSource(nameof(Data))]
        public void TransformArrayIntoInputTriple_Tests(string caseName, object[] input, 
            List<InputTriple> expected)
        {
            var program = new Program();
            program.AddRawInputToList(input);

            var operation = (string)input[0];
            var low = (int)input[1];
            var high = (int)input[2];
            program.InputTriples[0].ShouldBe(new InputTriple(operation, low, high));
        }

        private static readonly object[] Data =
        {
                new object[] { "A", new object[] { "+", 1 },  new List<InputTriple> {
                        new InputTriple("+", 1, -1)
                    }
                },
        };
    }
}
