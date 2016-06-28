using System;
using System.Linq;

namespace MajorityElement
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
            var numInputs = long.Parse(Console.ReadLine());
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            Console.WriteLine(Majority(input, 0, input.Length - 1) > 0 ? 1 : 0);
        }

        public long Majority(long[] n, long low, long high)
        {
            if (n.Length == 1)
                return n[low];

            if (high - low == 0)
                return n[low];

            var mid = low + (high - low)/2;

            long mL = Majority(n, low, mid);
            long mR = Majority(n, mid + 1, high);

            int hurdle;
            if ((high - low)%2 == 0)
                hurdle = (int) ((high - low)/2);
            else
                hurdle = (int) ((high - low)/2 + 1);

            int count = 0;
            for (int i = (int) low; i < high+1; i++)
                if (n[i] == mL)
                    count++;
            if (count > hurdle)
                return mL;

            count = 0;
            for (int i = (int) low; i < high+1; i++)
                if (n[i] == mR)
                    count++;
            if (count > hurdle)
                return mR;
        
            return 0;
        }
    }
}







