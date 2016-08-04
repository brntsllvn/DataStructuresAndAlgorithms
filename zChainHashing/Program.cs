using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zChainHashing
{
    public class Program
    {

        // create array of lists
        //// append new items to the front of each list

        public int NumberOfBuckets { get; set; }
        public int NumberOfQueries { get; set; }
        public int BigPrime { get; set; }
        public int MagicMultiplier { get; set; }
        public List<string>[] ArrayOfLists { get; set; }
        public List<InputPair> Queries { get; set; }

        public Program()
        {
            BigPrime = 1000000007;
            MagicMultiplier = 263;
        }

        public string MagicFunctionThatSolvesAllProblems()
        {
            return "hello";
        }
        
        public void ReadData()
        {
            NumberOfBuckets = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray()[0];
            ArrayOfLists = new List<string>[NumberOfBuckets];

            NumberOfQueries = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray()[0];
            for (int i = 0; i < NumberOfQueries; i++)
            {
                var inputString = Console.ReadLine().Split(' ').Select(n => Convert.ToString(n)).ToArray();
                Queries.Add(new InputPair(inputString[0], inputString[1]));
            }
        }

        public void WriteResponse(string result)
        {
            Console.WriteLine(result);
        }

        public void Run()
        {
            ReadData();
            var result = MagicFunctionThatSolvesAllProblems();
            WriteResponse(result);
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }

        internal byte[] GetAscii(string something)
        {
            return Encoding.ASCII.GetBytes(something);
        }

        internal int MapStringToBucket(string inputString, int bigPrime, int numBuckets)
        {
            var ascii = GetAscii(inputString);

            long asciiSum = 0;
            for (int i = 0; i < inputString.Length; i++)
            {
                var asciiMap = (int)ascii[i];
                var powMagicMultiplier = (long)Math.Pow(MagicMultiplier, i);
                long asciiProd = asciiMap * powMagicMultiplier;
                asciiSum += asciiProd;
            }

            var modPrime = asciiSum % BigPrime;
            return (int)modPrime % numBuckets;
        }
    }
}
