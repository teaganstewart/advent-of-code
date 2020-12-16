﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{

    /// <summary>
    /// A class that given an input of passwords, each field seperated by spaces and each passport seperated by an empty line it will check whether the password is valid or not.
    /// Makes sure all requried passport fields are there, and if they are, if the value is valid. Counts the number of valid passports from the input file.
    /// </summary>
    class CustomCustoms
    {

        IEnumerable<IEnumerable<char[]>> groupAnswers;

        /// <summary>
        /// Reads the input file into a collection of char arrays, each char array is a set of "yes" answers.
        /// </summary>
        void ReadInputFile()
        {
            // "\r\n\r\n"  is the regex to split when there is an empty line!
            groupAnswers = Regex.Split(File.ReadAllText("Input.txt"), @"\r\n\r\n")
                .Select(group => Regex.Split(group, @"\n")
                // stores each set of answers as a char array instead
                .Select(answer => answer.ToCharArray()));

        }

        /// <summary>
        /// Processes the amount of distinct "yes" answers there are from each group, then sums all the groups numbers together.
        /// </summary>
        void ProcessYesAnswers()
        {
            int sum = 0;

            foreach (IEnumerable<char[]> group in groupAnswers)
            {
                List<char> superAnswer = new List<char>();
                foreach (char[] answer in group)
                {
                    foreach (char c in answer)
                    {
                        if (c != '\r') superAnswer.Add(c); //makes sure new line characters are not accounted for
                    }
                }

                sum += superAnswer.Distinct().Count();
            }

            Console.WriteLine("The sum of the number of quesitons answered yes is {0}.", sum);

        }

        void ProcessIntersectAnswer()
        {

        }

        static void Main(string[] args)
        {
            CustomCustoms c = new CustomCustoms();
            c.ReadInputFile();
            c.ProcessYesAnswers();
            c.ProcessIntersectAnswer();
        }
    }
}
