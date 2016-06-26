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
            var numInputs = int.Parse(Console.ReadLine());
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var sortedInput = input.OrderBy(x => x).ToArray();
            var majorityCandidate = sortedInput[(sortedInput.Length - 1) / 2];
            Console.WriteLine(HasMajorityElement(sortedInput, 0, sortedInput.Length - 1, majorityCandidate));
        }

        public int HasMajorityElement(long[] n, int low, int high, long candidate)
        {
            var sum = 0;
            for (int i = 0; i < n.Length; i++)
            {
                if (n[i] == candidate)
                    sum++;
            }

            var hurdle = 0;

            if (n.Length % 2 == 0)
                hurdle = high / 2 + 1;
            else
                hurdle = high / 2;

            if (sum > hurdle)
                return 1;

            return 0;
        }
    }
}







