using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");
            for (int i = 0; i < allLines.Length; i++)
            {
                var options = allLines.Where((source, index) => index != i).ToArray();
                var sum = get2020Position(options, int.Parse(allLines[i]));
                if (sum > 0)
                {
                    Console.WriteLine($"{sum} is the answere");
                }
            }
        }

        private static int get2020Position(string[] options, int v)
        {
            for (int i = 0; i < options.Length; i++)
            {
                var a = int.Parse(options[i]);
                if (v + a == 2020)
                {
                    Console.WriteLine($"{a} and {v}");
                    return v * a;
                }
            }
            return 0;
        }
    }
}
