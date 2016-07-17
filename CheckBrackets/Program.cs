using System;
using System.Linq;

namespace CheckBrackets
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
            //var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var input = Console.ReadLine();
            Console.WriteLine(CheckBrackets(input));
        }

        public string CheckBrackets(string input)
        {
            // design an algorithm that checks if every {,[,( has a matching },],)

            return "Success";
        }
    }
}
