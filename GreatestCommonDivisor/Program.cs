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
            return 0;
        }
    }
}
