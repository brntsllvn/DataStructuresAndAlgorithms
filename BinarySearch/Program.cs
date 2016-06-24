using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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

            var lowerBound = 0;
            var upperBound = searchableData.Length - 1;

            for (var i = 0; i < numActualSearchTerms; i++)
                resultString += BinarySearch(searchableData, lowerBound, upperBound, actualSearchTerms[i]) + " ";

            return resultString.Trim();
        }

        public long BinarySearch(long[] data, long low, long high, long key)
        {
            if (high < low)
                return -1;

            var mid = low + (high - low) / 2;

            if (key == data[mid])
            {
                return mid;
            }
            else if (key < data[mid])
            {
                return BinarySearch(data, low, mid - 1, key);
            }
            else
            {
                return BinarySearch(data, mid + 1, high, key);
            }
        }
    }
}
