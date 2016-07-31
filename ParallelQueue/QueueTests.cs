using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace ParallelQueue
{
    [TestFixture]
    class QueueTests
    {
        [Test, TestCaseSource(nameof(ResultList))]
        public void QueueTestCases(string caseName, long numThreads, long numJobs, long[] jobs, List<ResultPair> expected)
        {
            var jobQueue = new JobQueue(numThreads, numJobs, jobs);
            jobQueue.AssignThreads();

            var results = jobQueue.ResultPairs;
            for (int i = 0; i < numJobs; i++)
            {
                results[i].ThreadNumber.ShouldBe(expected[i].ThreadNumber, $"Job {i}, incorrect ThreadNumber");
                results[i].StartTime.ShouldBe(expected[i].StartTime, $"Job {i}, incorrect StartTime");
            }
        }

        private static readonly object[] ResultList =
        {
            new object[] { "A", 1, 1, new long[] { 1 },
                new List<ResultPair> { new ResultPair(0,0) } },
            new object[] { "B", 8, 1, new long[] { 1 },
                new List<ResultPair> { new ResultPair(0,0) } },
            new object[] { "C", 1, 2, new long[] { 1, 1 },
                new List<ResultPair> { new ResultPair(0,0), new ResultPair(0,1) } },
            new object[] { "D", 1, 2, new long[] { 2, 2 },
                new List<ResultPair> { new ResultPair(0,0), new ResultPair(0,2) } },
            new object[] { "E", 2, 2, new long[] { 1, 1 },
                new List<ResultPair> { new ResultPair(0,0), new ResultPair(1,0) } },
            new object[] { "F", 2, 3, new long[] { 1, 1, 2 },
                new List<ResultPair> { new ResultPair(0,0), new ResultPair(1,0), new ResultPair(0, 1) } },
            new object[] { "G", 2, 5, new long[] { 1, 2, 3, 4, 5 },
                new List<ResultPair> { new ResultPair(0,0), new ResultPair(1,0), new ResultPair(0, 1), new ResultPair(1, 2), new ResultPair(0, 4) } },
            new object[] { "Long A", 4, 4, new long[] { 1, 1, 1, 1 },
                new List<ResultPair> {  new ResultPair(0,0),
                                        new ResultPair(1,0),
                                        new ResultPair(2,0),
                                        new ResultPair(3,0)} },

        };

        [Test]
        public void QueueTestCases_Long()
        {
            var numThreads = 4L;
            var numJobs = 20L;

            var jobCompletionTimes = new long[numJobs];
            for (int i = 0; i < numJobs; i++)
                jobCompletionTimes[i] = 1;

            var expectedResultPairs = new List<ResultPair>();
            for (int i = 0; i < numJobs / numThreads; i++)
                for (int j = 0; j < numThreads; j++)
                    expectedResultPairs.Add(new ResultPair(i, j));

            var jobQueue = new JobQueue(numThreads, numJobs, jobCompletionTimes);
            jobQueue.AssignThreads();

            var results = jobQueue.ResultPairs;
            for (int i = 0; i < numJobs; i++)
            {
                results[i].ThreadNumber.ShouldBe(expectedResultPairs[i].ThreadNumber, $"Job {i}, incorrect ThreadNumber");
                results[i].StartTime.ShouldBe(expectedResultPairs[i].StartTime, $"Job {i}, incorrect StartTime");
            }
        }

        [TestCase("A", new long[] { 0          }, 0, new long[] { 0          }, new long[] { 0          })]
        [TestCase("B", new long[] { 0, 1       }, 0, new long[] { 1, 0       }, new long[] { 1, 0       })]
        [TestCase("C", new long[] { 0, 1, 2    }, 0, new long[] { 1, 0, 0    }, new long[] { 1, 0, 2    })]
        [TestCase("D", new long[] { 0, 1, 2    }, 0, new long[] { 1, 1, 0    }, new long[] { 2, 1, 0    })]
        [TestCase("E", new long[] { 0, 1, 2    }, 0, new long[] { 1, 1, 1    }, new long[] { 0, 1, 2    })]
        [TestCase("F", new long[] { 0, 1, 2, 3 }, 0, new long[] { 1, 0, 0, 0 }, new long[] { 1, 3, 2, 0 })]
        [TestCase("G", new long[] { 0, 1, 2, 3 }, 0, new long[] { 1, 1, 0, 0 }, new long[] { 2, 1, 0, 3 })]
        [TestCase("H", new long[] { 0, 1, 2, 3 }, 0, new long[] { 1, 0, 1, 0 }, new long[] { 1, 3, 2, 0 })]
        [TestCase("I", new long[] { 0, 1, 2, 3 }, 0, new long[] { 1, 1, 1, 0 }, new long[] { 0, 1, 2, 3 })]
        [TestCase("K", new long[] { 1, 0       }, 0, new long[] { 1, 1       }, new long[] { 0, 1       })]
        [TestCase("L", new long[] { 2, 3, 1, 0 }, 0, new long[] { 1, 1, 1, 0 }, new long[] { 3, 0, 1, 2 })]
        public void SiftDown_Works(string caseName, long[] heapOfThreads, long threadToReprioritize,
            long[] nextAvailableTimeForThread, long[] expected)
        {
            var f0 = new JobQueue();
            f0.SiftDown(heapOfThreads, threadToReprioritize, nextAvailableTimeForThread);
            heapOfThreads.ShouldBe(expected);
        }
    }
}
