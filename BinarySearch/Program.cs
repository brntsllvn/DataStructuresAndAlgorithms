using System;
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
            var numData = (int)input[0];
            //var data = input.Skip(1).Take((int) (numData)).ToArray();
            var data = new ArraySegment<long>(input, 1, numData).ToArray();

            var searchTerms = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var numSearchTerms = (int)searchTerms[0];
            //var search = searchTerms.Skip(1).Take((int)(numSearchTerms)).ToArray();
            var search = new ArraySegment<long>(searchTerms, 1, numSearchTerms).ToArray();

            Console.WriteLine(BinarySearchSetup(data, search));
        }

        public string BinarySearchSetup(long[] data, long[] searchTerms)
        {
            var resultString = "";

            var lowerBound = 0;
            var upperBound = data.Length - 1;

            for (var i = 0; i < searchTerms.Length; i++)
                resultString += BinarySearch(data, lowerBound, upperBound, searchTerms[i]) + " ";

            return resultString.Trim();
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
    }
}
