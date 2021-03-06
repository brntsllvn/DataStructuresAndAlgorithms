﻿using System.CodeDom;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook
{
    [TestFixture]
    class PhoneBookTests
    {
        [Test]
        public void AddOneContact()
        {
            var f0 = new Program();
            f0.Add(911, "police");
            f0.Names[911].ShouldBe("police");
        }

        [Test]
        public void AddOverwrite()
        {
            var f0 = new Program();
            f0.Add(911, "police");
            f0.Add(911, "zombie");
            f0.Names[911].ShouldBe("zombie");
        }

        [Test]
        public void FindContact()
        {
            var f0 = new Program();
            f0.Add(911, "police");
            var contact = f0.Find(911);
            contact.ShouldBe("police");
            f0.Names[911].ShouldBe("police");
            f0.ResultList[0].ShouldBe(contact);
        }

        [Test]
        public void FindNothing()
        {
            var f0 = new Program();
            var contact = f0.Find(911);
            contact.ShouldBe("not found");
            f0.ResultList[0].ShouldBe(contact);
        }

        [Test]
        public void DeleteContact()
        {
            var f0 = new Program();
            f0.Add(911, "police");
            f0.Delete(911);
            f0.Names[911].ShouldBe(null);
        }

        [Test, TestCaseSource(nameof(TestData))]
        public void A_Test(string caseName, List<InputTriple> input, List<string> expected)
        {
            var f0 = new Program { InputTriples = input };
            f0.MagicFunctionThatSolvesAllProblems();

            var countOfFinds = input.Count(x => x.Command == "find");
            countOfFinds.ShouldBe(expected.Count);

            for (var i = 0; i < expected.Count; i++)
            {
                if (!f0.ResultList.Any()) continue;
                var result = f0.ResultList[i];
                result.ShouldBe(expected[i]);
            }

        }

        private static readonly object[] TestData =
        {
            new object[] {"A",new List<InputTriple>{new InputTriple("add",911,"police"),}, new List<string>()},
            new object[] {"B",new List<InputTriple>{new InputTriple("find",911,null),}, new List<string> {"not found"}
            }
            ,
            new object[] {
                "B del",
                new List<InputTriple>
                {
                    new InputTriple("del",911, null),
                },
                new List<string>
                {

                }
            }
            ,
            new object[] {
                "C",
                new List<InputTriple>
                {
                    new InputTriple("add",911,"police"),
                    new InputTriple("find",911, null)
                },
                new List<string>
                {
                    "police"
                }
            }
            ,
            new object[] {
                "D",
                new List<InputTriple>
                {
                    new InputTriple("find",911, null),
                    new InputTriple("add",911,"police"),
                    new InputTriple("find",911, null),
                    new InputTriple("del",911, null),
                    new InputTriple("find",911, null),
                },
                new List<string>
                {
                    "not found",
                    "police",
                    "not found"
                }
            }
            ,
            new object[] {
                "E",
                new List<InputTriple>
                {
                    new InputTriple("add",911,"police"),
                    new InputTriple("add",76213,"Mom"),
                    new InputTriple("add",17239,"Bob"),
                    new InputTriple("find",76213,null),
                    new InputTriple("find",910,null),
                    new InputTriple("find",911,null),
                    new InputTriple("del",910,null),
                    new InputTriple("del",911,null),
                    new InputTriple("find",911,null),
                    new InputTriple("find",76213,null),
                    new InputTriple("add",76213,"daddy"),
                    new InputTriple("find",76213,null),
                },
                new List<string>
                {
                    "Mom",
                    "not found",
                    "police",
                    "not found",
                    "Mom",
                    "daddy",
                }
            }
            ,
            new object[] {
                "F",
                new List<InputTriple>
                {
                    new InputTriple("find",3839442,null),
                    new InputTriple("add",123456,"me"),
                    new InputTriple("add",0,"granny"),
                    new InputTriple("find",0,null),
                    new InputTriple("find",123456,null),
                    new InputTriple("del",0,null),
                    new InputTriple("del",0,null),
                    new InputTriple("find",0,null)
                },
                new List<string>
                {
                    "not found",
                    "granny",
                    "me",
                    "not found"
                }
            }
            ,
            new object[] {
                "G",
                new List<InputTriple>
                {
                    new InputTriple("add",1,"me"),
                    new InputTriple("add",1,"me"),
                    new InputTriple("add",1,"me"),
                    new InputTriple("del",1,null),
                    new InputTriple("find",1,null)
                },
                new List<string>
                {
                    "not found",
                }
            }
        };
    }
}
