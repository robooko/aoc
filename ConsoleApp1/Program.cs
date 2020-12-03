using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");
            long fitRange = 0;
            long matchOnePosition = 0;
            foreach (string line in allLines)
            {
                var split = line.Split(':');
                var rule = new PasswordRule(split[0].Trim());
                var password = split[1].Trim();

                fitRange = rule.FitsRange(password) ? fitRange + 1 : fitRange;
                matchOnePosition = rule.MatchesOnePosition(password) ? matchOnePosition + 1 : matchOnePosition;
            }

            Console.WriteLine($"{fitRange} passwords fit the range");
            Console.WriteLine($"{matchOnePosition} passwords match one position");
        }

    }

    class PasswordRule
    {
        public string match;
        public int min;
        public int max;

        public PasswordRule(string rule)
        {
            var ruleSplit = rule.Split(' ');
            this.match = ruleSplit[1].Trim();
            var minMaxSplit = ruleSplit[0].Trim().Split('-');
            this.min = int.Parse(minMaxSplit[0]);
            this.max = int.Parse(minMaxSplit[1]);
        }

        internal bool FitsRange(string password)
        {
            var count = Regex.Matches(password, this.match).Count;
            return (count >= min && count <= max);
        }

        internal bool MatchesOnePosition(string password)
        {
            var count = Regex.Matches(password, this.match).Count;
            return ((password.ToCharArray()[min - 1].ToString() == match) ^ (password.ToCharArray()[max - 1].ToString() == match));
        }
    }
}
