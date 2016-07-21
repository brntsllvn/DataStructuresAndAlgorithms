using System;
using System.Linq;

namespace MakingChange
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
        }

        public int DynamicProgrammingMakeChange(int[] coinDenominations, int amountToChange)
        {
            if (amountToChange == 0)
                return 0;

            var minNumberCoins = new int[amountToChange + 1];

            for (int m = 1; m <= amountToChange; m++)
            {
                minNumberCoins[m] = int.MaxValue;

                for (int i = 0; i < coinDenominations.Length; i++)
                {
                    if (m >= coinDenominations[i])
                    {
                        var numCoins = minNumberCoins[m-coinDenominations[i]] + 1;

                        if (numCoins < minNumberCoins[m])
                        {
                            minNumberCoins[m] = numCoins;
                        }
                    }
                }
            }

            return minNumberCoins[amountToChange];
        }
    }
}
