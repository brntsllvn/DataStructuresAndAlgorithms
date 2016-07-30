using System;
using System.Collections.Generic;
using System.Linq;

namespace ParallelQueue
{
    class JobQueue
    {
        // input
        public long NumThreads;
        public long NumJobs;
        public long[] TimeRequiredToCompleteEachJob;

        // output
        public long[] ThreadAssignedToJob;
        public long[] JobStartTime;

        public List<ResultPair> ResultPairs { get; set; }

        public JobQueue()
        {
            ResultPairs = new List<ResultPair>();
        }

        public JobQueue(long numThreads, long numJobs, long[] jobCompletionTimes)
        {
            NumThreads = numThreads;
            NumJobs = numJobs;
            TimeRequiredToCompleteEachJob = jobCompletionTimes;
            ResultPairs = new List<ResultPair>();
        }

        public void AssignThreads()
        {
            var nextTimeThreadWillBeAvailable = new long[NumThreads];

            var launcher = new ArrayToHeap.Launcher();
            // O(n)
            launcher.BuildHeap(nextTimeThreadWillBeAvailable);

            ThreadAssignedToJob = new long[NumJobs];
            JobStartTime = new long[NumJobs];

            for (var i = 0; i < NumJobs; i++)
            {
                var duration = TimeRequiredToCompleteEachJob[i];

                var threadSelectedForTheJob = 0L;

                // ExtractMin: O(1)
                ThreadAssignedToJob[i] = launcher.ExtractMin(nextTimeThreadWillBeAvailable);

                JobStartTime[i] = nextTimeThreadWillBeAvailable[threadSelectedForTheJob];

                // update nextTimeThreadWillBeAvailable[threadSelectedForTheJob]
                nextTimeThreadWillBeAvailable[threadSelectedForTheJob] += duration;

                // ###################################################
                // ###################################################
                // #################TODO##############################
                // ###################################################
                // ###################################################

                // ChangePriority: O(tree height) <= O(logn) 
                // implement ChangePriority
                //// Thread with the nearest start time has the highest priority
                // implament SiftUp (for MIN binary heap)
            }
        }

        //public void AssignThreads()
        //{
        //    ThreadAssignedToJob = new long[NumJobs];
        //    JobStartTime = new long[NumJobs];

        //    var nextTimeThreadWillBeAvailable = new long[NumThreads];
        //    for (var i = 0; i < NumJobs; i++)
        //    {
        //        long duration = TimeRequiredToCompletedEachJob[i];
        //        long threadSelectedForTheJob = 0;
        //        // This loop-check finds the minimum time another thread is available...
        //        // ... and chooses that thread for the next job
        //        for (var threadNumber = 0; threadNumber < NumThreads; ++threadNumber)
        //        {
        //            // Check when each thread (low up to high index) is available...
        //            // Select the lowest-index thread with the minimum next available time
        //            if (nextTimeThreadWillBeAvailable[threadNumber] 
        //                    < nextTimeThreadWillBeAvailable[threadSelectedForTheJob]) 
        //                threadSelectedForTheJob = threadNumber;
        //        }
        //        // Assign the thread to the job officially (this array is used to construct the solution)
        //        ThreadAssignedToJob[i] = threadSelectedForTheJob;
        //        // Note when the job starts given thread availability (this array is used to construct the solution)
        //        JobStartTime[i] = nextTimeThreadWillBeAvailable[threadSelectedForTheJob];
        //        // When a thread is selected for a job, add the time required to complete...
        //        // ... the job to the next time the thread will become available
        //        nextTimeThreadWillBeAvailable[threadSelectedForTheJob] += duration;
        //    }
        //}

        private void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            NumThreads = input[0];
            NumJobs = input[1];
            TimeRequiredToCompleteEachJob = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
        }

        public void ConvertSolutionToResultPairs()
        {
            for (int i = 0; i < NumJobs; i++)
            {
                var newPair = new ResultPair(ThreadAssignedToJob[i], JobStartTime[i]);
                ResultPairs.Add(newPair);
            }
        }

        private void WriteResponse()
        {

        }

        public void Run(string[] args)
        {
            ReadData();
            AssignThreads();
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
