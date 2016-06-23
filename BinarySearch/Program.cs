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

            for (var i = 0; i < numActualSearchTerms; i++)
            {
                resultString += BinarySearch(searchableData, actualSearchTerms[i]) + " ";
            }

            return resultString.Trim();
        }


        //var resultString = "";
        //if (numSearchableDataElements == 1)
        //{
        //    for (int i = 0; i < numActualSearchTerms; i++)
        //    {
        //        if (actualSearchTerms[i] == searchableData[0])
        //        {
        //            resultString += "0 ";
        //        }
        //        else
        //        {
        //            resultString += "-1 ";
        //        }
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < numActualSearchTerms; i++)
        //    {
        //        var leftHalfLength = searchableData.Length / 2;
        //        var rightHalfLength = searchableData.Length / 2;
        //        var leftHalf = new long[leftHalfLength];
        //        var rightHalf = new long[rightHalfLength];

        //        for (int j = 0; j < leftHalfLength; j++)
        //        {
        //            leftHalf[j] = searchableData[j];
        //        }

        //        for (int k = 0; k < rightHalfLength; k++)
        //        {
        //            rightHalf[k] = searchableData[k + leftHalfLength];
        //        }

        //        if (actualSearchTerms[i] <= searchableData[numSearchableDataElements/2-1])
        //        {
        //            resultString += BinarySearchSetup(leftHalf, new long[1] {actualSearchTerms[i]});
        //        }
        //        else
        //        {
        //            resultString += BinarySearchSetup(rightHalf, new long[1] { actualSearchTerms[i] });
        //        }
        //    }


        public long BinarySearch(long[] data, long searchTerm)
        {
            if (data.Length == 1)
            {
                if (data[0] == searchTerm)
                    return 0;
                return -1;
            }

            return -1;
        }

    }
}
