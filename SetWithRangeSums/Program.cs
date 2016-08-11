using System;
using System.Collections.Generic;
using System.Linq;

namespace SetWithRangeSums
{
    public class Program
    {
        public List<InputTriple> InputTriples { get; set; }
        public List<string> QueryResults { get; set; }

        public Program()
        {
            InputTriples = new List<InputTriple>();
        }

        public void MagicFunctionThatSolvesAllProblems()
        {
            return;
        }

        internal void AddRawInputToList(object[] input)
        {
            var operation = (string)input[0];
            var low = (int)input[1];
            var high = -1;

            if (input.Length == 3)
                high = (int)input[2];

            InputTriples.Add(new InputTriple(operation, low, high));
        }

        public void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
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
    }

    public class InputTriple
    {
        public string Operation { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        
        public InputTriple(string op, int val)
        {
            Operation = op;
            Low = val;
            High = -1;
        }

        public InputTriple(string op, int low, int high)
        {
            Operation = op;
            Low = low;
            High = high;
        }
    }
}
