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
            Console.WriteLine($"Total is {groups.GetDistictTotal()}.");
            Console.WriteLine($"Total fix is {groups.GetDistictTotalByPerson()}.");
        }
    }


    class Groups
    {
        IEnumerable<Group> _groups;
        public Groups(string data)
        {
            _groups = data.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Group(x));
        }

        public int GetDistictTotal()
        {
            return _groups.Sum(x => x.GetDistictTotal());
        }

        public int GetDistictTotalByPerson()
        {
            return _groups.Sum(x => x.GetDistictTotalByPerson());
        }

    }

    class Group
    {
        IEnumerable<Person> _people;
        public Group(string data) 
        {
            _people = data.Split(new string[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Person(x));
        }

        internal int GetDistictTotal()
        {
            var joinedAnswers = String.Join(string.Empty, _people.Select(x => x.Answers));
            return joinedAnswers.ToCharArray().Distinct().Count();
        }

        internal int GetDistictTotalByPerson()
        {
            var result = _people.First().Answers.ToCharArray();
            foreach (var answers in _people.Select(x => x.Answers))
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
