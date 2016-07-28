using System;
using System.Collections.Generic;
using System.Linq;

namespace ParallelQueue
{
    class JobQueue
    {
        public long NumWorkers;
        public long NumJobs;
        public long[] Jobs;

        public long[] ThreadNumber;
        public long[] JobStartTime;

        public List<ResultPair> ResultPairs { get; set; }

        public JobQueue()
        {
            ResultPairs = new List<ResultPair>();
        }

        public JobQueue(long numWorkers, long numJobs, long[] jobs)
        {
            NumWorkers = numWorkers;
            NumJobs = numJobs;
            Jobs = jobs;
            ResultPairs = new List<ResultPair>();
        }

        public void AssignJobs()
        {
            ThreadNumber = new long[NumJobs];
            JobStartTime = new long[NumJobs];

            var nextFreeTime = new long[NumWorkers];
            for (var i = 0; i < NumJobs; i++)
            {
                long duration = Jobs[i];
                long bestWorker = 0;
                for (var j = 0; j < NumWorkers; ++j)
                {
                    if (nextFreeTime[j] < nextFreeTime[bestWorker])
                        bestWorker = j;
                }
                ThreadNumber[i] = bestWorker;
                JobStartTime[i] = nextFreeTime[bestWorker];
                nextFreeTime[bestWorker] += duration;
            }
        }

        private void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            NumWorkers = input[0];
            NumJobs = input[1];
            Jobs = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
        }

        public void ConvertSolutionToResultPairs()
        {
            for (int i = 0; i < NumJobs; i++)
            {
                var newPair = new ResultPair(ThreadNumber[i], JobStartTime[i]);
                ResultPairs.Add(newPair);
            }
        }

        private void WriteResponse()
        {

        }

        public void Run(string[] args)
        {
            ReadData();
            AssignJobs();
            WriteResponse();
        }

        static void Main(string[] args)
        {
            new JobQueue().Run(args);
        }
    }

    public class ResultPair
    {
        public long ThreadNumber { get; set; }
        public long StartTime { get; set; }

        public ResultPair(long threadNum, long startTime)
        {
            ThreadNumber = threadNum;
            StartTime = startTime;
        }
    }
}
