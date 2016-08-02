using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook
{
    public class Program
    {
        public List<InputTriple> InputTriples { get; set; }
        public string[] Names { get; set; }
        public List<string> ResultList { get; set; }

        public Program()
        {
            InputTriples = new List<InputTriple>();
            Names = new string[10000000];
            ResultList = new List<string>();
        }

        public void MagicFunctionThatSolvesAllProblems()
        {
            foreach (var inputTriple in InputTriples)
            {
                if (inputTriple.Command == "add")
                    Add(inputTriple.Number, inputTriple.Person);
                else if (inputTriple.Command == "find")
                {
                    Find(inputTriple.Number);
                }

                else if (inputTriple.Command == "del")
                    Delete(inputTriple.Number);
            }
        }

        internal string Find(int address)
        {
            var name = "not found";
            if (Names[address] != null)
            {
                name = Names[address];
            }
            ResultList.Add(name);
            return name;
        }

        internal void Delete(int address)
        {
            Names[address] = null;
        }

        internal void Add(int address, string name)
        {
            Names[address] = name;
        }

        public void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var numberCommands = input[0];

            for (int i = 0; i < numberCommands; i++)
            {
                var rawInputTriple = Console.ReadLine().Split(' ').Select(n => Convert.ToString(n)).ToArray();
                var command = rawInputTriple[0];
                var number = Int32.Parse(rawInputTriple[1]);
                string person = null;
                if (command == "add")
                    person = rawInputTriple[2];
                InputTriples.Add(new InputTriple(command, number, person));
            }
        }

        public void WriteResponse(List<string> resultList)
        {
            foreach (var result in resultList)
                Console.WriteLine(result);
        }

        public void Run()
        {
            ReadData();
            MagicFunctionThatSolvesAllProblems();
            WriteResponse(ResultList);
            ReadData();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }

    public class InputTriple
    {
        public string Command { get; set; }
        public int Number { get; set; }
        public string Person { get; set; }

        public InputTriple(string command, int number, string person)
        {
            Command = command;
            Number = number;
            Person = person;
        }
    }
}
