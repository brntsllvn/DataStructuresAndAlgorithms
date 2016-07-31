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
        public long[] threadHeap;
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

            threadHeap = new long[NumThreads];
            for (int j = 0; j < threadHeap.Length; j++)
                threadHeap[j] = j;

            JobStartTime = new long[NumJobs];

            for (var i = 0; i < NumJobs; i++)
            {
                // 1) set default threadSelectedForTheJob = 0L;
                var threadSelectedForTheJob = threadHeap[0];

                // 2) create result pair
                ResultPairs.Add(new ResultPair(threadSelectedForTheJob, nextTimeThreadWillBeAvailable[threadSelectedForTheJob]));

                //if (i == NumJobs - 1) // don't need to update values after last job is assigned
                //    break;

                // 3) update next available time for thread
                nextTimeThreadWillBeAvailable[threadSelectedForTheJob] += TimeRequiredToCompleteJob[i];

                // 4) SiftDown thread now based on next available time: O(tree height) <= O(logn)
                //// Always SiftDown the 0th element...?
                SiftDown(threadHeap, 0, nextTimeThreadWillBeAvailable);
            }
        }

        // modify this method to sift based on next available time
        public void SiftDown(long[] threadHeap, long thread, long[] nextTimeThreadWillBeAvailable)
        {
            var size = threadHeap.Length - 1;
            var minElementIndex = thread;

            var leftChildIndex = GetLeftChildIndex(thread);
            if (leftChildIndex <= size 
                && (
                    nextTimeThreadWillBeAvailable[leftChildIndex] < nextTimeThreadWillBeAvailable[threadHeap[minElementIndex]]
                    || 
                    nextTimeThreadWillBeAvailable[leftChildIndex] == nextTimeThreadWillBeAvailable[threadHeap[minElementIndex]] && threadHeap[leftChildIndex] < threadHeap[minElementIndex]
                    )
                )
                minElementIndex = leftChildIndex;

            var rightChildIndex = GetRightChildIndex(thread);
            if (rightChildIndex <= size
                && (
                    nextTimeThreadWillBeAvailable[rightChildIndex] < nextTimeThreadWillBeAvailable[threadHeap[minElementIndex]]
                    ||
                    nextTimeThreadWillBeAvailable[rightChildIndex] == nextTimeThreadWillBeAvailable[threadHeap[minElementIndex]] && threadHeap[rightChildIndex] < threadHeap[minElementIndex]
                    )
                )
                minElementIndex = rightChildIndex;

            if (thread != minElementIndex)
            {
                SwapElements(threadHeap, thread, minElementIndex);
                SiftDown(threadHeap, minElementIndex, nextTimeThreadWillBeAvailable);
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
