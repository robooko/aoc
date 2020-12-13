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
            Console.WriteLine($"Q2 answer is {bags.CountContainedBags("shiny gold bag")}");
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

        public long CountContainedBags(string bagName)
        {
            var bag = _allBags.FirstOrDefault(x => x.BagName == bagName);
            var bags = bag.GetContainedBags(new List<BagCount>(), 1, bag.BagName);
            return bags.Sum(x => x.OwnerCount * x.Count);
        }
    }


    class Bag
    {
        public string BagName { get; init; }
        string[] _bags;
        string[] _bagsWithCount;
        Bag[] _bagArray;
        public int Number { get; set; }

        public Bag(string data)
        {
            var splits = data.Split(" contain");
            BagName = splits[0].TrimEnd('s');
            _bags = splits[1].Split(',').Select(x => {
                var ret = x.Trim();
                ret = ret.TrimEnd('.');
                ret = ret.TrimEnd('s');
                ret = ret.Substring(ret.IndexOf(" "));
                ret = ret.Trim();
                return ret;
            }).ToArray();
            _bagsWithCount = splits[1].Split(',');
        }
        public Bag()
        {

        }

        public void SetBags(Collection<Bag> AllBags)
        {
            var joinedBags = string.Join("", _bags);
            var newBags = AllBags.Where(x => joinedBags.Contains(x.BagName))
                .ToArray();
            _bagArray = newBags;
        }

        internal bool HasBag(string v)
        {
            foreach(var bag in this._bagArray)
            {
                if (bag.BagName.Contains(v))
                    return true;
                else if (bag.HasBag(v))
                {
                    return true;
                }
            }
            return false;
        }

        internal long CountContainedBags(long count, long multiply, string[] counted)
        {
            foreach (var bag in this._bagArray)
            {
                if (string.Join("", counted).Contains(bag.BagName))
                    continue;
                counted = counted.Append(bag.BagName).ToArray();
                var bagWithCount = _bagsWithCount.FirstOrDefault(x => x.Contains(bag.BagName));
                bagWithCount = bagWithCount.Trim();
                bagWithCount = bagWithCount.Substring(0, bagWithCount.IndexOf(" "));
                var containedCount = bag.CountContainedBags(count, Convert.ToInt32(bagWithCount), counted);

                count = count + (Convert.ToInt32(bagWithCount) * multiply);
                count = count + containedCount;
            }
            return count;
        }

        internal List<BagCount> GetContainedBags(List<BagCount> bags, int ownerCount, string owner)
        {
            foreach (var bag in _bagArray)
            {
                var bagWithCount = _bagsWithCount.FirstOrDefault(x => x.Contains(bag.BagName));
                bagWithCount = bagWithCount.Trim();
                bagWithCount = bagWithCount.Substring(0, bagWithCount.IndexOf(" "));
                var bagCount = new BagCount() {
                    Count = Convert.ToInt32(bagWithCount),  
                    Owner = owner,
                    OwnerCount = ownerCount,
                    Name = bag.BagName
                };
                bags.Add(bagCount);
                bag.GetContainedBags(bags, Convert.ToInt32(bagWithCount) * ownerCount, bag.BagName);
            }

            return bags;
        }
    }

    class BagCount
    {
        public long Count { get; set; }
        public long OwnerCount { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}
