using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string allText = File.ReadAllText("textfile1.txt");
            var groups = new Groups(allText);
            Console.WriteLine($"Total is {groups.GetTotal()}.");
            Console.WriteLine($"Total fix is {groups.GetTotal1()}.");
        }
    }


    class Groups
    {
        IEnumerable<Group> _groups;
        public Groups(string data)
        {
            _groups = data.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Group(x));
        }

        public int GetTotal()
        {
            return _groups.Sum(x => x.GetTotal());
        }

        public int GetTotal1()
        {
            return _groups.Sum(x => x.GetTotal1());
        }

    }

    class Group
    {
        IEnumerable<Person> _people;
        public Group(string data) 
        {
            _people = data.Split(new string[] { Environment.NewLine},
                               StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Person(x));
        }

        internal int GetTotal()
        {
            return String.Join(string.Empty, _people.Select(x => x.Answers)).ToCharArray().Distinct().Count();
        }

        internal int GetTotal1()
        {
            var result = new Char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            var sortedAnswers = _people.Select(x => x.Answers).ToList();
            sortedAnswers.Sort((x, y) => x.Length.CompareTo(y.Length));
            foreach (var answers in sortedAnswers)
            {
                result = result.Intersect(answers).ToArray();
            }
            return result.Count();
        }
    }

    class Person
    {
        public string Answers { get; init; }

        public Person(string answers)
        {
            Answers = answers;
        }
    }
}
