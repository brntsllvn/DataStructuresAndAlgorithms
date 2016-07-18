using System;
using System.Linq;

namespace CheckBrackets
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
            //var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var input = Console.ReadLine();

            Console.WriteLine(bracketsBalanced(input));
        }

        public string bracketsBalanced(string input)
        {
            if (input.Length == 1)
                return "1";

            var bracketStack = new BjsStack();

            char[] openBracketChars = { '{', '[', '(' };
            char[] closeBracketChars = { '}', ']', ')' };

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];

                if (openBracketChars.Contains(c))
                    bracketStack.Push(c);
                else
                {
                    if (bracketStack.Empty())
                        return (i + 1).ToString();

                    if (!closeBracketChars.Contains(c))
                        continue;

                    var top = bracketStack.PopNode().Payload;

                    if ((top == '[' && c != ']')
                        || (top == '{' && c != '}')
                        || (top == '(' && c != ')'))
                    {
                        return (i+1).ToString();
                    }
                }
            }

            return "Success";
        }
    }

    public class BjsStack
    {
        public BjsNode Top { get; set; }

        public bool Empty()
        {
            if (Top == null)
                return true;
            return false;
        }

        public void Push(char value)
        {
            var newListNode = new BjsNode(value);

            if (Top != null)
                newListNode.Next = Top;

            Top = newListNode;
        }

        public BjsNode TopNode()
        {
            return Top;
        }

        public BjsNode PopNode()
        {
            var topToPop = Top;
            var newTop = Top.Next;
            Top = newTop;

            return topToPop;
        }
    }

    public class BjsNode
    {
        public char Payload { get; set; }
        public BjsNode Next { get; set; }

        public BjsNode(char payload)
        {
            Payload = payload;
        }
    }
}
