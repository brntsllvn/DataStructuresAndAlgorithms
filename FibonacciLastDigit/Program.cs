using System;

namespace FibonacciLastDigit
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
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine(CalculateFibonacciLastDigit(n));
        }

        public string CalculateFibonacciLastDigit(long n)
        {
            if (n == 0)
                return "0";

            if (n == 1)
                return "1";

            var fibonacciArray = new long[n];
            fibonacciArray[0] = 0;
            fibonacciArray[1] = 1;

            for (long i = 2; i < n; i++)
            {
                fibonacciArray[i] = fibonacciArray[i - 1] + fibonacciArray[i - 2];
            }

            var result = fibonacciArray[n - 1] + fibonacciArray[n - 2];
            var fibString = result.ToString();
            var lastDigit = fibString.Substring(fibString.Length-1);
            return lastDigit;
        }
    }
}
