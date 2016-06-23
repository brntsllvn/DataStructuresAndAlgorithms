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
            Console.WriteLine(BinarySearch(input, search));
        }

        public string BinarySearch(long[] data, long[] searchTerms)
        {
            var numSearchableDataElements = data[0];
            var searchableData = new long[numSearchableDataElements];
            for (int i = 0; i < numSearchableDataElements; i++)
            {
                searchableData[i] = data[i + 1];
            }

            var numSearchTerms = searchTerms[0];
            var actualSearchTerms = new long[numSearchTerms];
            for (int i = 0; i < numSearchTerms; i++)
            {
                actualSearchTerms[i] = searchTerms[i + 1];
            }

            var resultString = "";
            if (numSearchableDataElements == 1)
            {
                for (int i = 0; i < numSearchTerms; i++)
                {
                    if (actualSearchTerms[i] == searchableData[0])
                    {
                        resultString += "0 ";
                    }
                    else
                    {
                        resultString += "-1 ";
                    }
                }
            }

            return resultString.Trim();
        }
    }
}
