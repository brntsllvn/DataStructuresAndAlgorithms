using System;
using System.Linq;

namespace BeefyCases
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
            Console.WriteLine(AddSimpleType(input));
        }

        public int AddSimpleType(int[] n)
        {
            return n.Sum();
        }

        public int AddComplicatedType(AwesomeObject[] n)
        {
            return n.Sum(x => x.Payload);
        }
    }

    public class AwesomeObject
    {
        public int Payload { get; set; }

        public AwesomeObject(int payload)
        {
            Payload = payload;
        }
    }
}
