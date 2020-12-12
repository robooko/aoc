using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] allLines = File.ReadAllLines("textfile1.txt");
            var bags = new Bags(allLines);
            Console.WriteLine($"Q1 answer is {bags.FindBags("shiny gold bag").Length}");
        }
    }

    class Bags
    {
        Collection<Bag> _allBags;

        public Bags(string[] data)
        {
            _allBags = new Collection<Bag>();

            foreach(string strBags in data)
            {
                _allBags.Add(new Bag(strBags));
            }

            foreach (var bag in _allBags)
            {
                bag.SetBags(_allBags);
            }
        }

        public Bag[] FindBags(string bagName)
        {
            var foundBags = new List<Bag>();

            foreach (var bag in _allBags)
            {
                var hasBag = bag.HasBag(bagName);
                if (hasBag)
                {
                    foundBags.Add(bag);
                }
            }

            return foundBags.ToArray();
        }
    }


    class Bag
    {
        string _bagName;
        string[] _bags;
        Bag[] _bagArray;

        public Bag(string data)
        {
            var splits = data.Split(" contain");
            _bagName = splits[0].TrimEnd('s');
            _bags = splits[1].Split(',').Select(x => {
                var ret = x.Trim();
                ret = ret.TrimEnd('.');
                return ret.Substring(ret.IndexOf(" ")); 
            }).ToArray();
        }

        public void SetBags(Collection<Bag> AllBags)
        {
            var joinedBags = string.Join("", _bags);
            _bagArray = AllBags.Where(x => joinedBags.Contains(x._bagName)).ToArray();
        }

        internal bool HasBag(string v)
        {
            foreach(var bag in this._bagArray)
            {
                if (bag._bagName.Contains(v))
                    return true;
                else if (bag.HasBag(v))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
