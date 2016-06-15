using System;
using System.Linq;

namespace GreatestCommonDivisor
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
            Console.WriteLine(CalculateGcd(input));
        }

        public long CalculateGcd(long[] inputLongs)
        {
            if (inputLongs[0] == 1 || inputLongs[1] == 1)
                return 1;

            if (inputLongs[0] == inputLongs[1])
                return inputLongs[0];

            // find larger of two inputs
            var maxInput = inputLongs[0] > inputLongs[1] ? inputLongs[0] : inputLongs[1];
            var minInput = inputLongs[0] > inputLongs[1] ? inputLongs[1] : inputLongs[0];

            // modulo larger by smaller
            var remainder = maxInput%minInput;

            if (remainder == 0)
                return minInput;

            // recursively call CalculateGcd again this time with smaller of the inputs and remainer
            return CalculateGcd(new long[] {remainder, minInput});
        }
    }
}
