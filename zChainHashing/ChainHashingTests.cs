using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace zChainHashing
{
    [TestFixture]
    class ChainHashingTests
    {
        [Test, TestCaseSource(nameof(ResultList))]
        public void QueryToBucket(string caseName, long numberBuckets,
            List<InputPair> queries, List<string> expectedQueryResults)
        {
            var f0 = new Program();
            f0.NumberOfQueries = queries.Count;
            f0.Queries = queries;
            f0.NumberOfBuckets = numberBuckets;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();

            f0.MagicFunctionThatSolvesAllProblems();

            for (var i = 0; i < expectedQueryResults.Count; i++)
            {
                if (f0.QueryResults.Count == 0)
                    continue;

                var act = f0.QueryResults[i];
                var exp = expectedQueryResults[i];
                act.ShouldBe(exp);
            }
        }

        private static readonly object[] ResultList =
        { 
                new object[] { "A", 5, new List<InputPair> {
                                            new InputPair("add","HellO")
                                        },
                                        new List<string> {
                                        }
                },
                new object[] { "B", 5, new List<InputPair> {
                                            new InputPair("add","HellO"),
                                            new InputPair("find","HellO"),
                                        },
                                        new List<string> {
                                            "yes",
                                        }
                },
                new object[] { "C", 5, new List<InputPair> {
                                            new InputPair("find","HellO"),
                                            new InputPair("add","HellO"),
                                            new InputPair("find","HellO"),
                                            new InputPair("add","world"),
                                            new InputPair("check","4"),
                                            new InputPair("check","1"),
                                            new InputPair("del","HellO"),
                                            new InputPair("check","4"),
                                            new InputPair("check","3"),
                                       },
                                        new List<string> {
                                            "no",
                                            "yes",
                                            "world HellO",
                                            "",
                                            "world",
                                            ""
                                        }
                },
                new object[] { "D", 5, new List<InputPair> {
                                            new InputPair("add","world"),
                                            new InputPair("add","HellO"),
                                            new InputPair("check","4"),
                                            new InputPair("find","World"),
                                            new InputPair("find","world"),
                                            new InputPair("del","world"),
                                            new InputPair("check","4"),
                                            new InputPair("del","HellO"),
                                            new InputPair("add","luck"),
                                            new InputPair("add","GooD"),
                                            new InputPair("check","2"),
                                            new InputPair("del","GooD"),
                                            new InputPair("check","2"),
                                            new InputPair("del","luck"),
                                            new InputPair("check","2"),
                },
                                        new List<string> {
                                            "HellO world",
                                            "no",
                                            "yes",
                                            "HellO",
                                            "GooD luck",
                                            "luck",
                                            ""
                                        }
                },
                new object[] { "E", 4, new List<InputPair> {
                                            new InputPair("add","test"),
                                            new InputPair("add","test"),
                                            new InputPair("find","test"),
                                            new InputPair("del","test"),
                                            new InputPair("find","test"),
                                            new InputPair("find","Test"),
                                            new InputPair("add","Test"),
                                            new InputPair("find","Test"),
                },
                                        new List<string> {
                                            "yes",
                                            "no",
                                            "no",
                                            "yes",
                                        }
                },
                new object[] { "F", 5, new List<InputPair> {
                                            new InputPair("check","0"),
                                            new InputPair("check","1"),
                                            new InputPair("check","2"),
                                            new InputPair("find","world"),
                                            new InputPair("find","world"),
                                       },
                                        new List<string> {
                                            "",
                                            "",
                                            "",
                                            "no",
                                            "no"
                                        }
                },
                new object[] { "G", 5, new List<InputPair> {
                                            new InputPair("add","stuff"),
                                            new InputPair("add","otherStuff"),
                                       },
                                        new List<string> {
                                        }
                },
                new object[] { "H", 5, new List<InputPair> {
                                            new InputPair("add","world"),
                                            new InputPair("add","HellO"),
                                            new InputPair("del","world"),
                                            new InputPair("del","world"),
                                            new InputPair("check","4"),
                                       },
                                        new List<string> {
                                            "HellO",
                                        }
                },
                new object[] { "I", 5, new List<InputPair> {
                                            new InputPair("check","0"),
                                       },
                                        new List<string> {
                                            "",
                                        }
                },
        };

        [Test]
        public void Add()
        {
            var input = "HellO";
            var f0 = new Program();
            f0.NumberOfBuckets = 3;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();

            var bucketNumber = f0.MapStringToBucket(input);

            f0.Add(input, bucketNumber);
            f0.BucketList[bucketNumber].ShouldContain(input);
        }

        [Test]
        public void AddDuplicate()
        {
            var input = "HellO";
            var f0 = new Program();
            f0.NumberOfBuckets = 3;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();
            var bucketNumber = f0.MapStringToBucket(input);
            f0.Add(input, bucketNumber);

            f0.Add(input, bucketNumber);
            f0.BucketList[bucketNumber].Count.ShouldBe(1);
            f0.BucketList[bucketNumber].ShouldContain(input);
        }

        [Test]
        public void AddNonDuplicateInProperOrder()
        {
            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            var bucketNumber = f0.MapStringToBucket("HellO");
            f0.InitializeBuckets();
            f0.Add("HellO", bucketNumber);

            f0.Add("world", bucketNumber);
            f0.BucketList[bucketNumber].Count.ShouldBe(2);
            f0.BucketList[bucketNumber][0].ShouldBe("world");
            f0.BucketList[bucketNumber][1].ShouldBe("HellO");
        }

        [Test]
        public void DeleteNothing()
        {
            var input = "HellO";
            var f0 = new Program();
            f0.NumberOfBuckets = 3;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            var bucketNumber = f0.MapStringToBucket(input);

            f0.Delete(input, bucketNumber);
            f0.BucketList[bucketNumber].ShouldBeNull();
        }

        [Test]
        public void DeleteCommand()
        {
            var input = "HellO";
            var f0 = new Program();
            f0.NumberOfBuckets = 3;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();
            var bucketNumber = f0.MapStringToBucket(input);
            f0.Add(input, bucketNumber);

            f0.Delete(input, bucketNumber);
            f0.BucketList[bucketNumber].Count.ShouldBe(0);
            f0.BucketList[bucketNumber].ShouldNotContain(input);
        }

        [Test]
        public void FindNothing()
        {
            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            var bucketNumber = f0.MapStringToBucket("HellO");

            f0.Find("HellO", bucketNumber);
            f0.QueryResults.ShouldContain("no");
        }

        [Test]
        public void FindEntry()
        {
            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();
            var bucketNumber = f0.MapStringToBucket("HellO");
            f0.Add("HellO", bucketNumber);

            f0.Find("HellO", bucketNumber);
            f0.QueryResults.ShouldContain("yes");
        }

        [Test]
        public void FindNothingInPopulatedList()
        {
            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();
            var bucketNumber = f0.MapStringToBucket("HellO");
            f0.Add("HellO", bucketNumber);

            f0.Find("world", bucketNumber);
            f0.QueryResults.ShouldContain("no");
        }

        [TestCase()]
        public void CheckEmptyBucket()
        {
            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];

            f0.Check(1);
            f0.BucketList[1].ShouldBeNull();
            f0.QueryResults.ShouldContain("");
        }

        [Test]
        public void CheckBucketWithOneItem()
        {
            var inputString = "HellO";

            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();
            var bucketNum = f0.MapStringToBucket(inputString);
            f0.Add(inputString, bucketNum);

            f0.Check(bucketNum);
            f0.QueryResults.ShouldContain(inputString);
        }

        [Test]
        public void CheckBucketWithMultipleItems()
        {
            var inputString1 = "HellO";
            var inputString2 = "world";

            var f0 = new Program();
            f0.NumberOfBuckets = 5;
            f0.NumberOfQueries = 1;
            f0.BucketList = new List<string>[f0.NumberOfBuckets];
            f0.InitializeBuckets();
            var bucketNum1 = f0.MapStringToBucket(inputString1);
            f0.Add(inputString1, bucketNum1);
            var bucketNum2 = f0.MapStringToBucket(inputString2);
            f0.Add(inputString2, bucketNum2);

            bucketNum1.ShouldBe(bucketNum2);

            f0.Check(bucketNum1);
            f0.QueryResults.ShouldContain(inputString2 + ' ' + inputString1);
        }

        [TestCase("a", 3, 1)]
        [TestCase("a", 12, 1)]
        [TestCase("b", 12, 2)]
        [TestCase("`", 7, 5)]
        [TestCase("cd", 7, 2)]
        [TestCase("world", 7, 1)]
        [TestCase("world", 5, 4)]
        [TestCase("HellO", 5, 4)]
        [TestCase("HellO", 13, 3)]
        public void DetermineBucket(string inputString, long numBuckets, long expectedBucket)
        {
            var f0 = new Program();
            f0.NumberOfBuckets = numBuckets;
            var bucketNum = f0.MapStringToBucket(inputString);
            bucketNum.ShouldBe(expectedBucket);
        }

        [TestCase("a", 97)]
        [TestCase("x", 120)]
        [TestCase("H", 72)]
        [TestCase("^", 94)]
        public void Get_ASCII(string something, byte expected)
        {
            var f0 = new Program();
            f0.GetAscii(something).ShouldBe(new byte[] { expected });
        }
    }
}
