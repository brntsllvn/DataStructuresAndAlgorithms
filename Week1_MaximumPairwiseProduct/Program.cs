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
            long[] userInts;

            while (true)
            {
                var numOfInts = int.Parse(Console.ReadLine());
                userInts = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();

                if (!InputHasErrors(numOfInts, userInts))
                    break;
            }

            Console.WriteLine(FindMaxPairwiseProduct(userInts));
        }

        public long FindMaxPairwiseProduct(long[] userInts)
        {
            var arrLength = userInts.Length;

            switch (arrLength)
            {
                case 1:
                    return userInts[0];
                case 2:
                    return userInts[0]*userInts[1];
                default:

                    var maxIndex1 = 0;

                    for (int i = 0; i < arrLength; i++)
                    {
                        if (userInts[i] > userInts[maxIndex1])
                            maxIndex1 = i;
                    }

                    var maxIndex2 = 0;

                    for (int j = 0; j < arrLength; j++)
                    {
                        if (j != maxIndex1 && userInts[j] > userInts[maxIndex2])
                            maxIndex2 = j;
                    }

                    return userInts[maxIndex1]*userInts[maxIndex2];
            }
        }


        private bool InputHasErrors(int numOfInts, long[] userInts)
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
