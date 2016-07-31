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
        public long[] TimeRequiredToCompleteJob;

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
            TimeRequiredToCompleteJob = jobCompletionTimes;
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
                var threadSelectedForTheJob = 0L;

                // ExtractMin: O(1)
                ThreadAssignedToJob[i] = launcher.ExtractMin(nextTimeThreadWillBeAvailable);

                JobStartTime[i] = nextTimeThreadWillBeAvailable[threadSelectedForTheJob];

                ResultPairs.Add(new ResultPair(threadSelectedForTheJob, nextTimeThreadWillBeAvailable[threadSelectedForTheJob]));

                // update nextTimeThreadWillBeAvailable[threadSelectedForTheJob]
                nextTimeThreadWillBeAvailable[threadSelectedForTheJob] += TimeRequiredToCompleteJob[i];

                // ChangePriority: O(tree height) <= O(logn) 
                launcher.ChangePriority(nextTimeThreadWillBeAvailable, threadSelectedForTheJob, TimeRequiredToCompleteJob[i]);
            }
        }

        // modify this method to sift based on next available time
        public void SiftDown(long[] H, long thread, long[] nextTimeThreadWillBeAvailable)
        {
            var size = H.Length - 1;
            var minElementIndex = thread;

            var leftChildIndex = GetLeftChildIndex(thread);
            if (leftChildIndex <= size 
                && nextTimeThreadWillBeAvailable[leftChildIndex] < nextTimeThreadWillBeAvailable[minElementIndex])
                minElementIndex = leftChildIndex;

            var rightChildIndex = GetRightChildIndex(thread);
            if (rightChildIndex <= size 
                && nextTimeThreadWillBeAvailable[rightChildIndex] < nextTimeThreadWillBeAvailable[minElementIndex])
                minElementIndex = rightChildIndex;

            if (thread != minElementIndex)
            {
                SwapElements(H, thread, minElementIndex);
                SiftDown(H, minElementIndex, nextTimeThreadWillBeAvailable);
            }
        }

        public void SwapElements(long[] H, long index1, long index2)
        {
            var temp = H[index1];
            H[index1] = H[index2];
            H[index2] = temp;
        }

        public long GetLeftChildIndex(long parentIndex)
        {
            return 2 * parentIndex + 1; // "+1" makes the index math 0-based
        }

        public long GetRightChildIndex(long parentIndex)
        {
            return 2 * parentIndex + 2; // "+2" makes the index math 0 - based
        }

        private void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            NumThreads = input[0];
            NumJobs = input[1];
            TimeRequiredToCompleteJob = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
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
