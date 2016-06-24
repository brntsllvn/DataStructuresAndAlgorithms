using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var numData = (int)input[0];
            var data = new List<long>(input).GetRange(1, numData).ToArray();

            var searchTerms = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var numSearchTerms = (int)searchTerms[0];
            var search = new List<long>(searchTerms).GetRange(1, numSearchTerms).ToArray();

            Console.WriteLine(BinarySearchSetup(data, search));
        }

        public string BinarySearchSetup(long[] data, long[] searchTerms)
        {
            var lowerBound = 0;
            var upperBound = data.Length - 1;

            var cache = new Dictionary<long, long>();
            var resultBuilder = new StringBuilder();

            for (var i = 0; i < searchTerms.Length; i++)
            {
                var searchTerm = searchTerms[i];
                long result;
                if (!cache.TryGetValue(searchTerm, out result))
                {
                    result = BinarySearch(data, lowerBound, upperBound, searchTerm);
                    cache.Add(searchTerm, result);
                }
                resultBuilder.Append(result);
                resultBuilder.Append(" ");
            }

            return resultBuilder.ToString().Trim();
        }

        public long BinarySearch(long[] data, long low, long high, long key)
        {
            if (high < low)
                return -1;

            var mid = low + (high - low) / 2;

            if (key == data[mid])
                return mid;

            else if (key < data[mid])
                return BinarySearch(data, low, mid - 1, key);

            else
                return BinarySearch(data, mid + 1, high, key);
        }

        // iterative
        //public long BinarySearch(long[] data, long low, long high, long key)
        //{
        //    while (low <= high)
        //    {
        //        long mid = (low + high) / 2;
        //        if (key == data[mid])
        //            return mid;

        //        else if (key < data[mid])
        //            high = mid - 1;

        //        else
        //            low = mid + 1;
        //    }
        //    return -1;
        //}
    }
}
