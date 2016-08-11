using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Reflection;

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
            var high = -1;
            if (input.Length == 3)
                high = (int)input[2];

            var triple = program.InputTriples[0];
            triple.Operation.ShouldBe(operation);
            triple.Low.ShouldBe(low);
            triple.High.ShouldBe(high);
        }

        private static readonly object[] Data =
        {
                new object[] { "A", new object[] {"+",1},  new List<InputTriple> {
                        new InputTriple("+", 1, -1)
                    }
                },
                new object[] { "B", new object[] {"?",5},  new List<InputTriple> {
                        new InputTriple("?", 5, -1)
                    }
                },
                new object[] { "C", new object[] {"s",1,2},  new List<InputTriple> {
                        new InputTriple("s", 1, 2)
                    }
                },
                new object[] { "D", new object[] {"-",1},  new List<InputTriple> {
                        new InputTriple("-", 1, -1)
                    }
                },
        };
    }
}
