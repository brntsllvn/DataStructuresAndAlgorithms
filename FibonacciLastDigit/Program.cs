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

        public int CalculateFibonacciLastDigit(long n)
        {
            if (n == 0)
                return 0;

            if (n == 1)
                return 1;

            var fibonacciArray = new int[n];
            fibonacciArray[0] = 0;
            fibonacciArray[1] = 1;

            for (int i = 2; i < n; i++)
            {
                fibonacciArray[i] = (fibonacciArray[i - 1] + fibonacciArray[i - 2]) % 10;
            }

            return (fibonacciArray[n - 1] + fibonacciArray[n - 2]) % 10;
        }
    }
}
