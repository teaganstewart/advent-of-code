﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.RegexOptions;
using System.Linq;

namespace AdventOfCode
{
    /*
    *
    *
    */
    class PassportProcessor
    {
        

        public void CheckPassports()
        {

            string line;
            string passport = "";
            StreamReader file = new StreamReader(@"input.txt");
            List<string> seperatedPassports = new List<string>();

            while((line = file.ReadLine()) != null) {
                if(line == "") {
                    seperatedPassports.Add(passport);
                    passport = "";

                }
                else {
                    passport += line + " ";
                }  
            }

            seperatedPassports.Add(passport);

            // creates a list of dictionaries, containing the fields of each passport
            var passports = seperatedPassports
                .Select(passport => Regex.Split(passport, @"\s+")
                .Where(field => !string.IsNullOrEmpty(field))
                .Select(field =>
                {
                    string[] pairValues = field.Split(":");
                    return new KeyValuePair<string, string>(pairValues[0], pairValues[1]);

                }).ToImmutableDictionary()

                ).ToImmutableList();

                var requiredFields = new[] { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" }.ToImmutableHashSet();
                Console.WriteLine("There are {0} valid passports.", passports.Count(passport => requiredFields.IsSubsetOf(passport.Keys)));
        }

        static void Main(string[] args)
        {

            PassportProcessor p = new PassportProcessor();
            p.CheckPassports();

        }
    }
}
