using System;
using System.CodeDom;
using System.Linq;
using NUnit.Core;
using Shouldly.Configuration;

namespace MajorityElement
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
            var numInputs = long.Parse(Console.ReadLine());
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            Console.WriteLine(Majority(input, 0, input.Length - 1));
        }

        public long Majority(long[] n, long low, long high)
        {
            if (high - low == 0)
                return 1;

            if (high - low == 1)
            {
                return n[low] == n[high] ? 1 : 0;
            }

            var mid = low + (high - low) / 2;

            if (high - low > 0)
            {
                var lResult = Majority(n, low, mid);
                var rResult = Majority(n, mid + 1, high);

                if (IsMajorityElement(n, low, high, lResult))
                    return 1;

                if (IsMajorityElement(n, low, high, rResult))
                    return 1;
            }

            return 0;
        }

        private bool IsMajorityElement(long[] n, long low, long high, long candidate)
        {
            var subN = new long[high - low + 1];
            for (int i = 0, j = (int)low; i < subN.Length; i++, j++)
                subN[i] = n[j];

            var hurdle = (high - low) / 2;

            var candidateCount = subN.Select(x => x == candidate).Count();

            return candidateCount > hurdle;

        }
    }
}







