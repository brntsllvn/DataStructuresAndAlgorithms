using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zChainHashing
{
    public class Program
    {
        public int NumberOfBuckets { get; set; }
        public int NumberOfQueries { get; set; }
        public int BigPrime { get; set; }
        public int MagicMultiplier { get; set; }
        public List<string>[] BucketList { get; set; }
        public List<InputPair> Queries { get; set; }

        public Program()
        {
            BigPrime = 1000000007;
            MagicMultiplier = 263;
        }

        internal void Add(string input, int bucketNumber)
        {
            if (BucketList[bucketNumber] == null)
                BucketList[bucketNumber] = new List<string>();

            var alreadyInBucket = false;
            foreach (var bucketMember in BucketList[bucketNumber])
                if (input == bucketMember)
                    alreadyInBucket = true;

            if (!alreadyInBucket)
                BucketList[bucketNumber].Insert(0, input);
        }

        public string MagicFunctionThatSolvesAllProblems()
        {
            return "hello";
        }
        
        public void ReadData()
        {
            NumberOfBuckets = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray()[0];
            BucketList = new List<string>[NumberOfBuckets];

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
            BucketList = new List<string>[NumberOfBuckets];
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

        internal int MapStringToBucket(string inputString)
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
            return (int)modPrime % NumberOfBuckets;
        }
    }
}
