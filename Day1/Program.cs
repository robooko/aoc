using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");

            var sum = get2020Total(allLines);
            Console.WriteLine($"{sum} is the answer for question one");

            var intList = allLines.Select(t => Convert.ToInt32(t));
            sum = get2020TotalFromThreeItems(intList);
            Console.WriteLine($"{sum} is the answer for question two");

        }

        static long get2020Total(string[] allLines)
        {
            for (int i = 0; i < allLines.Length; i++)
            {
                var options = allLines.Where((source, index) => index != i).ToArray();
                var sum = get2020Total(options, Convert.ToInt16(allLines[i]));
                if (sum > 0)
                {
                    return sum;
                }
            }

            return 0;
        }

        static int get2020Total(string[] options, int currentOption)
        {
            foreach (var addOption in options)
            {
                var addOptionNumber = Convert.ToInt16(addOption);
                if (currentOption + addOptionNumber == 2020)
                {
                    return currentOption * addOptionNumber;
                }
            }

            return 0;
        }

        static int get2020TotalFromThreeItems(IEnumerable<int> options)
        {
            foreach (var addOption in GetCombinations(options, 3))
            {
                if (addOption.Sum() == 2020)
                {
                    return addOption.ElementAt(0) * addOption.ElementAt(1) * addOption.ElementAt(2);
                }
            }
            return 0;
        }

        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> options, int count)
        {
            if (count == 1) return options.Select(option => new T[] { option });

            return GetCombinations(options, count - 1)
                .SelectMany(t => options, (option1, option2) => option1.Concat(new T[] { option2 }));
        }
    }
}
