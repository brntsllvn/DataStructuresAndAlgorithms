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
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            Console.WriteLine(MyFunction(input));
        }

        public string MyFunction(long[] n)
        {
            return "hello";
        }
    }
}
