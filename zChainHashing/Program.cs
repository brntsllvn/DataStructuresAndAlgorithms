using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zChainHashing
{
    public class Program
    {
        public long NumberOfBuckets { get; set; }
        public long NumberOfQueries { get; set; }
        public long BigPrime { get; set; }
        public long MagicMultiplier { get; set; }
        public List<string>[] BucketList { get; set; }
        public List<InputPair> Queries { get; set; }
        public List<string> QueryResults { get; set; }

        public Program()
        {
            BigPrime = 1000000007;
            MagicMultiplier = 263;
            QueryResults = new List<string>();
            Queries = new List<InputPair>();
        }

        internal void Add(string input, long bucketNumber)
        {
            //if (BucketList[bucketNumber] == null)
            //    BucketList[bucketNumber] = new List<string>();

            var alreadyInBucket = false;
            foreach (var bucketMember in BucketList[bucketNumber])
                if (input == bucketMember)
                    alreadyInBucket = true;

            if (!alreadyInBucket)
                BucketList[bucketNumber].Insert(0, input);
        }

        internal void Delete(string input, long bucketNumber)
        {
            if (BucketList[bucketNumber] == null)
                return;

            for (var i = 0; i < BucketList[bucketNumber].Count; i++)
                if (input == BucketList[bucketNumber][i])
                    BucketList[bucketNumber].Remove(BucketList[bucketNumber][i]);
        }

        internal void Find(string input, long bucketNumber)
        {
            var notFoundMsg = "no";
            var foundMsg = "yes";

            if (BucketList[bucketNumber] == null)
            {
                QueryResults.Add(notFoundMsg);
                return;
            }

            for (var i = 0; i < BucketList[bucketNumber].Count; i++)
                if (input == BucketList[bucketNumber][i])
                {
                    QueryResults.Add(foundMsg);
                    return;
                }

            QueryResults.Add(notFoundMsg);
        }

        internal void Check(long bucketNumber)
        {
            if (BucketList[bucketNumber] == null)
            {
                QueryResults.Add("");
                return;
            };

            var chainedString = new StringBuilder();
            for (var i = 0; i < BucketList[bucketNumber].Count; i++)
            {
                chainedString.Append(BucketList[bucketNumber][i]);
                chainedString.Append(" ");
            }

            QueryResults.Add(chainedString.ToString().TrimEnd(' '));
        }

        public void MagicFunctionThatSolvesAllProblems()
        {
            for (var i = 0; i < Queries.Count; i++)
            {
                var command = Queries[i].Command;
                var payload = Queries[i].Value;

                var bucketNumber = MapStringToBucket(payload);

                switch (command)
                {
                    case "add":
                        Add(payload, bucketNumber);
                        break;
                    case "del":
                        Delete(payload, bucketNumber);
                        break;
                    case "find":
                        Find(payload, bucketNumber);
                        break;
                    case "check":
                        Check(long.Parse(payload));
                        break;
                }
            }
        }
        
        public void ReadData()
        {
            NumberOfBuckets = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray()[0];
            BucketList = new List<string>[NumberOfBuckets];

            NumberOfQueries = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray()[0];
            for (long i = 0; i < NumberOfQueries; i++)
            {
                var inputString = Console.ReadLine().Split(' ').Select(n => Convert.ToString(n)).ToArray();
                var command = inputString[0];
                var value = inputString[1];
                Queries.Add(new InputPair(command, value));
            }
        }

        public void WriteResponse()
        {
            foreach (var qResult in QueryResults)
                Console.WriteLine(qResult);
        }

        public void Run()
        {
            ReadData();
            BucketList = new List<string>[NumberOfBuckets];
            InitializeBuckets();
            MagicFunctionThatSolvesAllProblems();
            WriteResponse();
            Console.ReadLine();
        }

        public void InitializeBuckets()
        {

            for (int i = 0; i < NumberOfBuckets; i++)
                BucketList[i] = new List<string>();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }

        internal byte[] GetAscii(string something)
        {
            return Encoding.ASCII.GetBytes(something);
        }

        internal long MapStringToBucket(string inputString)
        {
            var ascii = GetAscii(inputString);

            long asciiSum = 0;
            for (long i = 0; i < inputString.Length; i++)
            {
                var asciiMap = (long)ascii[i];
                var powMagicMultiplier = (long)Math.Pow(MagicMultiplier, i);
                long asciiProd = asciiMap * powMagicMultiplier;
                asciiSum += asciiProd;
            }

            var modPrime = asciiSum % BigPrime;
            return (long)modPrime % NumberOfBuckets;
        }
    }

    public class InputPair
    {
        public string Command { get; set; }
        public string Value { get; set; }

        public InputPair(string command, string value)
        {
            Command = command;
            Value = value;
        }
    }
}
