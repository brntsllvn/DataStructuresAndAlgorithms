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

        public int[] PrimitiveCalculatorOperationCounter(int n)
        {
            if (n == 1)
                return new int[0];

            var integerSequence = new int[n + 1];
            integerSequence[0] = 0;
            integerSequence[1] = 1;

            var minNumOperations = new int[n + 1];
            minNumOperations[0] = 0;
            minNumOperations[1] = 0;

            for (int i = 2; i <= n; i++)
            {
                if (IsPowerOfX(i,3))
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 1;
                    //integerSequence[i] = integerSequence[i / 3], 3x;
                }
                else if (IsPowerOfX(i,2))
                {
                    minNumOperations[i] = minNumOperations[i / 2] + 1;
                    //integerSequence[i] = integerSequence[i / 3], 2x;
                }
                else if (i % 3 == 0)
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 1;
                    //integerSequence[i] = integerSequence[i / 3], 3x;
                }
                else if (i % 3 == 1)
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 2;
                    //integerSequence[i] = integerSequence[i / 3], 3x, x+1;
                }
                else if (i % 2 == 0)
                {
                    minNumOperations[i] = minNumOperations[i / 2] + 1;
                    //integerSequence[i] = integerSequence[i / 3], 2x;
                }
                else if (i % 3 == 2)
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 3;
                    //integerSequence[i] = integerSequence[i / 3], 3x, x+1, x+1;
                }
            }

            return integerSequence.Select(x => x != 0).ToArray; // remove all trailing zeros
        }

        public bool IsPowerOfX(int n, int x)
        {
            if (n == 0)
                return false;

            for (int i = 1; i < Math.Log(n,x); i++)
                if (n % Math.Pow(x,i) != 0)
                    return false;
            return true;
        }
    }
}
