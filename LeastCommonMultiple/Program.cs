using System;
using System.Linq;

namespace LeastCommonMultiple
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
            Console.WriteLine(CalculateLcm(input));
        }

        public long CalculateLcm(long[] inputLongs)
        {
            var Gcd = CalculateGcd(new long[] {inputLongs[0], inputLongs[1]});

            return (long) ((double)(inputLongs[0] * inputLongs[1]) / (double)Gcd);
        }

        public long CalculateGcd(long[] inputLongs)
        {
            if (inputLongs[0] == 1 || inputLongs[1] == 1)
                return 1;

            if (inputLongs[0] == inputLongs[1])
                return inputLongs[0];

            var maxInput = inputLongs[0] > inputLongs[1] ? inputLongs[0] : inputLongs[1];
            var minInput = inputLongs[0] > inputLongs[1] ? inputLongs[1] : inputLongs[0];

            var remainder = maxInput % minInput;

            if (remainder == 0)
                return minInput;

            return CalculateGcd(new long[] { remainder, minInput });
        }
    }
}
