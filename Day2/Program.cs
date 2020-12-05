using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            string[] allLines = File.ReadAllLines("textfile1.txt");
            int fitsRangeCount = 0;
            int matchOnePositionCount = 0;

            stopwatch.Start();

            foreach (string line in allLines)
            {
                var split = line.Split(':');
                var rule = new PasswordRule(split[0]);
                var password = split[1];
                fitsRangeCount += rule.FitsRange(password) ? 1: 0;
                matchOnePositionCount += rule.MatchesOnePosition(password) ? 1 : 0;
            }

            stopwatch.Stop();

            Console.WriteLine($"Ran in {stopwatch.ElapsedMilliseconds}.");
            Console.WriteLine($"{fitsRangeCount} passwords fit the range");
            Console.WriteLine($"{matchOnePositionCount} passwords match one position");
        }

    }

    class PasswordRule
    {
        char _match;
        int _min;
        int _max;

        public PasswordRule(string rule)
        {
            var ruleSplit = rule.Split(' ');
            var minMaxSplit = ruleSplit[0].Split('-');

            _match = Convert.ToChar(ruleSplit[1]);

            _min = Convert.ToInt16(minMaxSplit[0]);
            _max = Convert.ToInt16(minMaxSplit[1]);
        }

        internal bool FitsRange(string password)
        {
            var count = password.Count(x => x.Equals(_match));
            return count >= _min && count <= _max;
        }

        internal bool MatchesOnePosition(string password)
        {
            return password[_min].Equals(_match) ^ password[_max].Equals(_match);
        }
    }
}
