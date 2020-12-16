using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    ///
    /// A class that given an input of passwords, each field seperated by spaces and each passport seperated by an empty line it will check whether the password is valid or not.
    /// Makes sure all requried passport fields are there, and if they are, if the value is valid. Counts the number of valid passports from the input file.
    /// <author name="Teagan Stewart"> 
    ///
    class PassportProcessor
    {

        List<string> seperatedPassports;

        /// Reads the input file and splits the passport into seperate strings. Each empty line "" represents a new passport.
        public void ReadInputFile()
        {
            string line;
            string passport = "";
            StreamReader file = new StreamReader(@"input.txt");
            seperatedPassports = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                if (line == "")
                {
                    seperatedPassports.Add(passport);
                    passport = "";

                }
                else
                {
                    passport += line + " ";
                }
            }

            seperatedPassports.Add(passport);
            file.Close();
        }

        /// Creates the list of dictionaries for each of the passports, then checks if the passports are valid.
        public void CheckPassports()
        {

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

            // counts the passports that have all the required fields
            Console.WriteLine("There are {0} valid passports.", passports
                .Count(passport => requiredFields.IsSubsetOf(passport.Keys)));

            // counts the passports with the required fields and valid fields
            Console.WriteLine("There are {0} completely valid passports", passports.Count(passport =>
                {

                    bool correct = true;

                    // for each passport field
                    foreach (KeyValuePair<string, string> entry in passport)
                    {
                        // check the field is valid
                        if (!CheckFields(entry.Value, entry.Key))
                        {
                            correct = false;
                        }
                    }

                    // makes sure both the field value and the fields are correct, if so add one to count
                    return correct && requiredFields.IsSubsetOf(passport.Keys);
                }
                ));

        }

        // ---------------------------------------
        // ---------- HELPER METHODS -------------
        // ---------------------------------------
        
        /// <summary>
        /// A helper method to check whether a given string is actually a number.
        /// <param name="value"> The value that needs to be parsed into an interger. </param>
        /// <returns> The parsed number version of the given string "value". </param>
        /// </summary>
        int CheckNumber(string value)
        {
            int number;

            if (int.TryParse(value, out number))
            {
                return number;
            }
            else
            {

                return -100000;
            }
        }

        /// <summary>
        // A helper method that checks the type of a given value, and returns whether the given value is correct based on the type.
        /// <param name="value"> The value of the current field. </param>
        /// <returns> Whether the given value is valid or not. </param>
        /// </summary>
        bool CheckFields(string value, string type)
        {

            int number = CheckNumber(value);

            if (number != -100000)
            {
                switch (type)
                {
                    case "iyr":
                        return number >= 2010 && number <= 2020;
                    case "byr":
                        return number >= 1920 && number <= 2002;
                    case "eyr":
                        return number >= 2020 && number <= 2030;
                    case "pid":
                        return CheckPassportId(value);
                    case "cid":
                        return true;
                    default:
                        return false;

                }
            }
            else
            {
                switch (type)
                {
                    case "hgt":
                        return CheckHeight(value);
                    case "hcl":
                        return CheckHairColor(value);
                    case "ecl":
                        return CheckEyeColor(value);
                    default:
                        return false;
                }
            }

        }

        /// <summary>
        // A helper method for the CheckFields method, checks specifically whether the height field is valid.
        /// <param name="value"> The value of the current field. </param>
        /// <returns> Whether the given value is valid or not. </param>
        /// </summary>
        bool CheckHeight(string value)
        {

            if (value.Contains("cm"))
            {
                int number = CheckNumber(value.Substring(0, value.Length - 2));
                return number >= 150 && number <= 193;
            }
            else if (value.Contains("in"))
            {
                int number = CheckNumber(value.Substring(0, value.Length - 2));
                return number >= 59 && number <= 76;
            }

            return false;
        }

        /// <summary>
        // A helper method for the CheckFields method, checks specifically whether the hair color field is valid.
        /// <param name="value"> The value of the current field. </param>
        /// <returns> Whether the given value is valid or not. </param>
        /// </summary>
        bool CheckHairColor(string value)
        {
            return Regex.Match(value, @"^#[0-9a-f]{6}$").Success;
        }

        /// <summary>
        // A helper method for the CheckFields method, checks specifically whether the eye color field is valid.
        /// <param name="value"> The value of the current field. </param>
        /// <returns> Whether the given value is valid or not. </param>
        /// </summary>
        bool CheckEyeColor(string value)
        {
            string[] validColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            return validColors.Contains(value);
        }

        /// <summary>
        // A helper method for the CheckFields method, checks specifically whether the passport id field is valid.
        /// <param name="value"> The value of the current field. </param>
        /// <returns> Whether the given value is valid or not. </param>
        /// </summary>
        bool CheckPassportId(string value)
        {
            return Regex.Match(value, @"^\d{9}$").Success;
        }

        static void Main(string[] args)
        {

            PassportProcessor p = new PassportProcessor();
            p.ReadInputFile();
            p.CheckPassports();
        }
    }
}
