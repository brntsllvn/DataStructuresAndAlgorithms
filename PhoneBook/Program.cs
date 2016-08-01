using System;
using System.Linq;

namespace PhoneBook
{
    public class Program
    {
        public string MagicFunctionThatSolvesAllProblems()
        {
            return "hello";
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
        public string Command { get; set; }
        public string Number { get; set; }
        public string Person { get; set; }

        public InputTriple(string command, string number, string person)
        {
            Command = command;
            Number = number;
            Person = person;
        }
    }
}
