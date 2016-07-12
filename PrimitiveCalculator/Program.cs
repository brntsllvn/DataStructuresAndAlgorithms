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
            Console.WriteLine(MyFunction(input[0]));
        }

        public int MyFunction(int n)
        {
            return -1;
        }
    }
}
