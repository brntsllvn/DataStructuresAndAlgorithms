namespace BubbleSortDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class Launcher
    {
        public void BubbleSort(int[] n)
        {
            for (int i = 0; i < n.Length; i++)
            {
                for (int j = 0; j < n.Length - 1; j++)
                {
                    if (n[j] > n[j+1])
                    {
                        var temp = n[j];
                        n[j] = n[j + 1];
                        n[j + 1] = temp;
                    }
                }
            }
        }
    }
}
