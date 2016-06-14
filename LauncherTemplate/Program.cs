using System;

namespace LauncherTemplate
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
            var n = int.Parse(Console.ReadLine());
            Console.WriteLine(MyFunction(n));
        }

        public string MyFunction(long n)
        {
            return "hello";
        }
    }
}
