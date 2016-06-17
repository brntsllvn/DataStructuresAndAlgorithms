using System;
using System.Linq;

namespace ChangingMoney
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
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).First();
            Console.WriteLine(MinimumNumberOfCoinsToChangeMoney(input));
        }

        public int MinimumNumberOfCoinsToChangeMoney(int n)
        {
            var countOf10s = 0;
            var countOf5sAnd1s = 0;

            if (n < 5)
            {
                return n;
            }
            else if (n >= 5 && n <= 10)
            {
                return n % 5 + 1;
            }
            else if (n > 10)
            {
                countOf10s = n / 10;
                countOf5sAnd1s = MinimumNumberOfCoinsToChangeMoney(n % 10);
            }

            var coinCount = countOf10s + countOf5sAnd1s;

            return coinCount;
        }
    }
}