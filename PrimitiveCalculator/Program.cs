using System;
using System.Linq;

namespace PrimitiveCalculator
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
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            Console.WriteLine(PrimitiveCalculatorOperationCounter(input[0]));
        }

        public int PrimitiveCalculatorOperationCounter(int n)
        {
            if (n == 1)
                return 0;

            var minNumOperations = new int[n+1];
            minNumOperations[0] = 0;
            minNumOperations[1] = 0;

            for (int i = 2; i <= n; i++)
            {
                if (i % 2 != 0 && i % 3 != 0)
                {
                    minNumOperations[i] = minNumOperations[i / 2] + 2;
                }
                else if (i % 2 == 0)
                {
                    minNumOperations[i] = minNumOperations[i / 2] + 1;
                }
                else if (i % 3 == 0)
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 1;
                }                    
            }

            return minNumOperations[n];
        }
    }
}
