﻿using System;
using System.Linq;

namespace BinarySearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Launcher().Run(args);
        }
    }

    public class Launcher
    {
        public void Run(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var search = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            Console.WriteLine(BinarySearchSetup(input, search));
        }

        public string BinarySearchSetup(long[] data, long[] searchTerms)
        {
            var resultString = "";

            var numSearchableDataElements = data[0];
            var searchableData = new long[numSearchableDataElements];
            for (var i = 0; i < numSearchableDataElements; i++)
                searchableData[i] = data[i + 1];

            var numActualSearchTerms = searchTerms[0];
            var actualSearchTerms = new long[numActualSearchTerms];
            for (var i = 0; i < numActualSearchTerms; i++)
                actualSearchTerms[i] = searchTerms[i + 1];

            for (var i = 0; i < numActualSearchTerms; i++)
            {
                resultString += BinarySearch(searchableData, actualSearchTerms[i]) + " ";
            }

            return resultString.Trim();
        }

        public long BinarySearch(long[] data, long searchTerm)
        {
            if (data.Length == 1)
            {
                if (data[0] == searchTerm)
                    return 0;
                return -1;
            }
            else
            {
                var leftHalf = GetLeftHalfArray(data);
                var rightHalf = GetRightHalfArray(data, leftHalf.Length);

                if (searchTerm <= leftHalf.Last())
                {
                    return BinarySearch(leftHalf, searchTerm);
                }
                else
                {
                    return BinarySearch(rightHalf, searchTerm);
                }
            }
        }

        private long[] GetLeftHalfArray(long[] data)
        {
            var leftHalfLength = data.Length/2;
            var leftHalf = new long[leftHalfLength];

            for (int j = 0; j < leftHalfLength; j++)
            {
                leftHalf[j] = data[j];
            }
            return leftHalf;
        }

        private long[] GetRightHalfArray(long[] data, long leftHalfLength)
        {
            var rightHalfLength = data.Length / 2;
            var rightHalf = new long[rightHalfLength];

            for (int k = 0; k < rightHalfLength; k++)
            {
                rightHalf[k] = data[k + leftHalfLength];
            }
            return rightHalf;
        }
    }
}
