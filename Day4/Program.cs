using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string allText = File.ReadAllText("textfile1.txt");
            string[] passports = allText.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

            int totalValidPassports = 0;
            int totalValidPassportsWithData = 0;
            foreach (var strPassport in passports)
            {
                var passport = new Passport(strPassport);
                totalValidPassports += passport.HasRequiredFields() ? 1 : 0;
                totalValidPassportsWithData += passport.IsValid() ? 1 : 0;
            }

            Console.WriteLine($"{totalValidPassports} valid passports.");
            Console.WriteLine($"{totalValidPassportsWithData} valid passports with data.");
        }
    }

    class Passport
    {
        Dictionary<string, string> _requiredFields = new Dictionary<string, string>() {
            { "byr" , "^(19[2-8][0-9]|199[0-9]|200[0-2])$" },
            { "iyr" , "^(201[0-9]|2020)$" },
            { "eyr" , "^(202[0-9]|2030)$" },
            { "hgt" , "^(1[5-8][0-9]|19[0-3])cm$|^(59|6[0-9]|7[0-6])in$" },
            { "hcl" , "^#([a-f0-9]{6})$" },
            { "ecl" , "^(amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)$" }, 
            { "pid" , "^([0-9]{9})$" } 
        };

        public Dictionary<string, string> _fields;

        public Passport(string strPassport)
        {
            strPassport = strPassport.Replace(Environment.NewLine, ",")
                .Replace(" ", ",")
                .Trim(',');

            _fields = strPassport.Split(",")
                .Select(x => x.Split(":"))
                .ToDictionary(x => x[0], x => x[1]);
        }

        internal bool HasRequiredFields()
        {
            return _requiredFields.Select(x => _fields.ContainsKey(x.Key))
                .All(x => x);
        }

        internal bool IsValid()
        {
            return _requiredFields.Select(x => _fields.ContainsKey(x.Key) 
            && Regex.Match(_fields[x.Key], x.Value).Success)
                .All(x => x);
        }
    } 
}
