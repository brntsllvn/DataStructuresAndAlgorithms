using System;
using System.Linq;

namespace Week1_MaximumPairwiseProduct
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
            int[] userInts;

            while (true)
            {
                Console.WriteLine("Number of integers: ");
                var numOfInts = int.Parse(Console.ReadLine());
                Console.WriteLine("Integers: ");
                userInts = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                if (!InputHasErrors(numOfInts, userInts))
                    break;
            }

            Console.WriteLine(FindMaxPairwiseProduct(userInts));
        }

        public int FindMaxPairwiseProduct(int[] userInts)
        {
            if (userInts.Length == 1)
            {
                return userInts[0];
            }

            var maxPairwiseProduct = 0;
            var length = userInts.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Int32 product = userInts[i]*userInts[j];
                    if (i != j && product > maxPairwiseProduct)
                    {
                        maxPairwiseProduct = product;
                    }
                }    
            }

            return maxPairwiseProduct;
        }


        private bool InputHasErrors(int numOfInts, int[] userInts)
        {
            if (numOfInts < 2 || numOfInts > 2 * 100000)
            {
                Console.WriteLine("Number of integers out of bounds");
                return true;
            }

            if (userInts.Length != numOfInts)
            {
                Console.WriteLine("Number of integers incorrect");
                return true;
            }

            foreach (var userInt in userInts)
            {
                if (userInt > 100000)
                {
                    Console.WriteLine("individual integer input too big");
                    return true;
                }


                if (userInt < 0)
                {
                    Console.WriteLine("individual integer input less than zero");
                    return true;
                }
            }

            return false;
        }
    }
}
