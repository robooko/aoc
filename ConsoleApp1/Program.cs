using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            string[] allLines = File.ReadAllLines("textfile1.txt");
            long fitRange = 0;
            long matchOnePosition = 0;
            stopwatch.Start();
            foreach (string line in allLines)
            {
                var split = line.Split(':');
                var rule = new PasswordRule(split[0]);
                var password = split[1];
                fitRange = rule.FitsRange(password) ? fitRange + 1 : fitRange;
                matchOnePosition = rule.MatchesOnePosition(password) ? matchOnePosition + 1 : matchOnePosition;
            }
            stopwatch.Stop();
            Console.WriteLine($"Ran in {stopwatch.ElapsedMilliseconds}.");
            Console.WriteLine($"{fitRange} passwords fit the range");
            Console.WriteLine($"{matchOnePosition} passwords match one position");
        }

    }

    class PasswordRule
    {
        string match;
        int min;
        int max;

        public PasswordRule(string rule)
        {
            var ruleSplit = rule.Split(' ');
            match = ruleSplit[1];
            var minMaxSplit = ruleSplit[0].Split('-');
            min = Convert.ToInt16(minMaxSplit[0]);
            max = Convert.ToInt16(minMaxSplit[1]);
        }

        internal bool FitsRange(string password)
        {
            var count = Regex.Matches(password, match).Count;
            return count >= min && count <= max;
        }

        internal bool MatchesOnePosition(string password)
        {
            return password.Substring(min, 1).Equals(match) ^ password.Substring(max, 1).Equals(match);
        }
    }
}
