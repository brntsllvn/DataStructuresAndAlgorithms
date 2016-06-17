using System;
using System.Linq;

namespace FractionalKnapsack
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
            var numItems = input[0];
            var knapsackCapacity = input[1];

            var valueAndWeightInput = new ValueAndWeight[numItems];

            for (int i = 0; i < numItems; i++)
            {
                input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();

                valueAndWeightInput[i] = new ValueAndWeight
                {
                    Value = input[0],
                    Weight = input[1]
                };
            }



            Console.WriteLine(FractionalKnapsackCalculator(valueAndWeightInput));
        }

        public decimal FractionalKnapsackCalculator(ValueAndWeight[] valueWeightArray)
        {
            return 0;
        }

        public class ValueAndWeight
        {
            public long Value { get; set; }
            public long Weight { get; set; }
        }
    }
}
