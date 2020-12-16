using System;
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

        IEnumerable<string[]> groupAnswers;

        void ReadInputFile()
        {
            // "\r\n\r\n"  is the regex to split when there is an empty line!
            groupAnswers = Regex.Split(File.ReadAllText("Input.txt"), @"\r\n\r\n")
            .Select(group => Regex.Split(group, @"\n"));

        }

        void ProcessAnswers()
        {


        }

        static void Main(string[] args)
        {
            CustomCustoms c = new CustomCustoms();
            c.ReadInputFile();
            c.ProcessAnswers();
        }
    }
}
