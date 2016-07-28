using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace ParallelQueue
{
    [TestFixture]
    class QueueTests
    {
        [Test, TestCaseSource(nameof(ResultList))]
        public void QueueTestCases(string caseName, long numWorkers, long numJobs, long[] jobs, List<ResultPair> expected)
        {
            var jobQueue = new JobQueue(numWorkers, numJobs, jobs);
            jobQueue.AssignJobs();
            jobQueue.ConvertSolutionToResultPairs();

            var results = jobQueue.ResultPairs;
            for (int i = 0; i < numJobs; i++)
            {
                results[i].ThreadNumber.ShouldBe(expected[i].ThreadNumber);
                results[i].StartTime.ShouldBe(expected[i].StartTime);
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
        };
    }
}
