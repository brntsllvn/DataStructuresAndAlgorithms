using System;
using System.Linq;

namespace FibonacciMod
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
            Console.WriteLine((input));
        }

        public long CalculateFibonacciMod(long[] n)
        {
            var fibonacciTerm = n[0];
            var mod = n[1];

            if (fibonacciTerm == 1 || fibonacciTerm == 2)
                return 1;

            if (fibonacciTerm == 3)
                return 2 % mod;



                return 0;
        }

        //public long CalculateFibonacci(long n)
        //{
        //    if (n == 0)
        //        return 0;

        //    if (n == 1)
        //        return 1;

        //    var fibonacciArray = new long[n];
        //    fibonacciArray[0] = 0;
        //    fibonacciArray[1] = 1;

        //    for (long i = 2; i < n; i++)
        //    {
        //        fibonacciArray[i] = fibonacciArray[i - 1] + fibonacciArray[i - 2];
        //    }

        //    return fibonacciArray[n - 1] + fibonacciArray[n - 2];
        //}
    }

}
