using System;
using System.Linq;

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
                if (IsMajorityElement(n, n[low]))
                    return 1;
                
            if (high - low == 1)
                if(n[low] == n[high] && IsMajorityElement(n, n[low]))
                    return 1;

            var mid = low + (high - low) / 2;

            if (high - low > 1)
            {
                var lResult = Majority(n, low, mid);
                if (lResult == 1)
                    return 1;
                var rResult = Majority(n, mid + 1, high);
                if (rResult == 1)
                    return 1;
            }

            return 0;
        }

        private bool IsMajorityElement(long[] n, long candidate)
        {
            var candidateInstanceCounter = 0;
            for (int i = 0; i < n.Length; i++)
                if (n[i] == candidate)
                    candidateInstanceCounter++;

            var hurdle = n.Length / 2;

            return candidateInstanceCounter > hurdle;
        }
    }
}







