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
            var upperBound = data.Length - 1;

            for (var i = 0; i < numActualSearchTerms; i++)
                resultString += BinarySearch(searchableData, lowerBound, upperBound, actualSearchTerms[i]) + " ";

            return resultString.Trim();
        }

        public long BinarySearch(long[] data, long lowerBound, long upperBound, long searchTerm)
        {
            if (upperBound == 1)
            {
                if (data[lowerBound] == searchTerm)
                    return lowerBound;
            }
            else
            {
                var leftLower = 0;
                var leftUpper =leftLower + (upperBound - leftLower) / 2;
                var rightLower = upperBound / 2 + 1;
                var rightUpper = rightLower + (upperBound - rightLower) / 2;

                if (searchTerm <= data[leftUpper])
                    return BinarySearch(data, leftLower, leftUpper, searchTerm);
                return BinarySearch(data, rightLower, rightUpper, searchTerm);
            }
            return -1;
        }
    }
}
