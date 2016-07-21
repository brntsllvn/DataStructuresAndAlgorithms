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

        public int AddSimpleType(int[] arrayOfIntegers)
        {
            return arrayOfIntegers.Sum();
        }

        public decimal AddDecimalType(decimal[] arrayOfDecimals)
        {
            return arrayOfDecimals.Sum(x => x);
        }

        public int AddComplicatedType(AwesomeObject[] arrayOfAwesomeObjects)
        {
            return arrayOfAwesomeObjects.Sum(x => x.Payload);
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
