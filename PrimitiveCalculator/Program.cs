using System;
using System.Collections.Generic;
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
            var minCalcInteger = input[0];

            var minNumCalculations = PrimitiveCalculatorOperationCounter(minCalcInteger).Length - 1;
            var calculationSequence = string.Join(" ", PrimitiveCalculatorOperationCounter(minCalcInteger));

            if (minCalcInteger == 1)
            {
                Console.WriteLine(0);
                Console.WriteLine(1);
            }
            else
            {
                Console.WriteLine(minNumCalculations);
                Console.WriteLine(calculationSequence);
            }

            Console.Read();
        }

        public int[] PrimitiveCalculatorOperationCounter(int n)
        {
            if (n == 1)
                return new int[0];

            var integerSequence = new List<int?>();
            integerSequence.Add(null);
            integerSequence.Add(0);

            var minNumOperations = new int[n + 1];
            minNumOperations[0] = 0;
            minNumOperations[1] = 0;

            for (int i = 2; i <= n; i++)
            {
                if (IsPowerOfX(i,3))
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 1;
                    integerSequence.Add(3);
                }
                else if (i % 3 == 0)
                {
                    minNumOperations[i] = minNumOperations[i / 3] + 1;
                    integerSequence.Add(3);
                }
                else if (IsPowerOfX(i, 2))
                {
                    minNumOperations[i] = minNumOperations[i / 2] + 1;
                    integerSequence.Add(2);
                }
                else if (i % 3 == 1)
                {
                    // I think "+2" is wrong
                    minNumOperations[i] = minNumOperations[i / 3] + 2;
                    integerSequence.Add(1);
                }
                else if (i % 2 == 0)
                {
                    minNumOperations[i] = minNumOperations[i / 2] + 1;
                    integerSequence.Add(2);
                }
                else if (i % 3 == 2)
                {
                    // I think "+3" is wrong 
                    minNumOperations[i] = minNumOperations[i / 3] + 3;
                    integerSequence.Add(1);
                }
            }

            var j = n;
            var outputSequence = new List<int>();

            while (j > 1)
            {
                outputSequence.Add(j);

                if (integerSequence[j] == 3)
                    j = j/3;
                else if (integerSequence[j] == 2)
                    j = j/2;
                else
                    j--;
            }

            outputSequence.Reverse();
            outputSequence.Insert(0, 1);
            return outputSequence.ToArray();
        }

        public bool IsPowerOfX(int n, int x)
        {
            if (n == 0)
                return false;

            if (n < x && n != 1)
                return false;

            for (int i = 1; i < Math.Log(n,x); i++)
                if (n % Math.Pow(x,i) != 0)
                    return false;
            return true;
        }
    }
}
