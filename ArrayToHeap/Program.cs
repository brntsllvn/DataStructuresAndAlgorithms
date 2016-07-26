using System;
using System.Linq;

namespace ArrayToHeap
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

    public class Swap
    {
        public int i { get; set; }
        public int j { get; set; }

        public Swap(int passed_i, int passed_j)
        {
            i = passed_i;
            j = passed_j;
        }
    }
}
