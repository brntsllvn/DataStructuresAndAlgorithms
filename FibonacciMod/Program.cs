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
            var nthTermInFibonacciSequence = n[0];
            var modDivisor = n[1];

            if (nthTermInFibonacciSequence == 1 || nthTermInFibonacciSequence == 2)
                return 1;

            if (nthTermInFibonacciSequence == 3 || nthTermInFibonacciSequence == 4)
                return (nthTermInFibonacciSequence - 1) % modDivisor;

            // calculate Pisano period length
            //// Pisano period length < m^2
            //// Pisano period always starts with "01"
            //// Calculate Fi
            //// Calculate Fi % modDivisor
            //// When F(i) % modDivisor == 1 && F(i-1) % modDivisor == 0, then i-1 = Pisano period length

            var pisanoPeriodLength = 0;

            for (int i = 0; i < modDivisor*modDivisor-1; i++)
            {
                //var fibonacciTerm = CalculateFibonacci(i);
                var fibonacciMod = CalculateFibonacciMod(new []{ i, modDivisor });

                if (fibonacciMod == 1)
                {
                    var fibonacciLessOne = CalculateFibonacci(i-1) % modDivisor;
                    if (fibonacciLessOne == 0)
                    {
                        pisanoPeriodLength = i;
                    }
                }
            }

            // reduce problem by using: fn mod m = fr mod m
            // where r = n mod Pisano period length
            var reducedFibonacciTerm = nthTermInFibonacciSequence%pisanoPeriodLength;
            var result = CalculateFibonacciMod(new[] {reducedFibonacciTerm, modDivisor}); 
             
            return result;

        }

        public long CalculateFibonacci(long n)
        {
            if (n == 0)
                return 0;

            if (n == 1)
                return 1;

            var fibonacciArray = new long[n];
            fibonacciArray[0] = 0;
            fibonacciArray[1] = 1;

            for (long i = 2; i < n; i++)
            {
                fibonacciArray[i] = fibonacciArray[i - 1] + fibonacciArray[i - 2];
            }

            return fibonacciArray[n - 1] + fibonacciArray[n - 2];
        }
    }

}
