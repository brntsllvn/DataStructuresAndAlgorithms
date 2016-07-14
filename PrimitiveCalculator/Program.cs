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
            var sequence = new List<int>();

            int[] arr = new int[n + 1];

            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = arr[i - 1] + 1;
                if (i % 2 == 0) arr[i] = Math.Min(1 + arr[i / 2], arr[i]);
                if (i % 3 == 0) arr[i] = Math.Min(1 + arr[i / 3], arr[i]);

            }

            for (int i = n; i > 1;)
            {
                sequence.Add(i);
                if (arr[i - 1] == arr[i] - 1)
                    i = i - 1;
                else if (i % 2 == 0 && (arr[i / 2] == arr[i] - 1))
                    i = i / 2;
                else if (i % 3 == 0 && (arr[i / 3] == arr[i] - 1))
                    i = i / 3;
            }
            sequence.Add(1);

            sequence.Reverse();
            return sequence.ToArray();
        }
    }
}
