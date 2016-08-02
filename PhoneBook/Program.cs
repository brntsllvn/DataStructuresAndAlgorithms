using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBook
{
    public class Program
    {
        public List<InputTriple> InputTriples { get; set; }
        public List<string[]> LookupTable { get; set; }
        public List<string> ResultList { get; set; }

        public Program()
        {
            InputTriples = new List<InputTriple>();
            LookupTable = new List<string[]>();
            ResultList = new List<string>();
        }

        public void MagicFunctionThatSolvesAllProblems()
        {
            foreach (var inputTriple in InputTriples)
            {
                if (inputTriple.Command == "add")
                {
                    var alreadyExistsInLoopupTable = false;
                    foreach (var t in LookupTable)
                        if (t[0] == inputTriple.Number)
                        {
                            t[1] = inputTriple.Person;
                            alreadyExistsInLoopupTable = true;
                        }
                    if (!alreadyExistsInLoopupTable)
                        LookupTable.Add(new[] { inputTriple.Number, inputTriple.Person });   
                }
                else if (inputTriple.Command == "find")
                {
                    var resultString = "not found";
                    foreach (var t in LookupTable)
                        if (t[0] == inputTriple.Number)
                            resultString = t[1];

                    ResultList.Add(resultString);
                }
                else if (inputTriple.Command == "del")
                {
                    for (var i = 0; i < LookupTable.Count; i++)
                        if (LookupTable[i][0] == inputTriple.Number)
                            LookupTable.Remove(LookupTable[i]);
                }
            }
        }

        public void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var numberCommands = input[0];

            for (int i = 0; i < numberCommands; i++)
            {
                var rawInputTriple = Console.ReadLine().Split(' ').Select(n => Convert.ToString(n)).ToArray();
                var command = rawInputTriple[0];
                var number = rawInputTriple[1];
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
