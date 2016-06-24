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
            Console.WriteLine(HasMajorityElement(sortedInput, numInputs));
        }

        public int HasMajorityElement(long[] n, int numElementsUnderConsideration)
        {
            if (numElementsUnderConsideration == 1)
                return 1;

            var midPoint = numElementsUnderConsideration / 2;

            if (numElementsUnderConsideration == 2)
            {
                if (n[midPoint - 1] == n[midPoint])
                {
                    return 1;
                }
                return 0;
            }

            if (n[midPoint] != n[midPoint + 1])
                return 0;
            return HasMajorityElement(n, numElementsUnderConsideration / 2);
        }
    }
}
