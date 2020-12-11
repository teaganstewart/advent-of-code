using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class DodgyPasswords
    {

        int[] lowerRanges;
        int[] higherRanges;
        char[] letters;
        string[] passwords;


        public void ReadInputFile()
        {

            // initalisation of storage variables
            int counter = 0;
            lowerRanges = new int[1000];
            higherRanges = new int[1000];
            letters = new char[1000];
            passwords = new string[1000];

            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");

            while ((line = file.ReadLine()) != null)
            {
                // splits each line read into the seperate data attributes
                string[] sections = line.Split(" ");

                // stores the range [0] = lower [1] = higher 
                string[] ranges = sections[0].Split("-");

                lowerRanges[counter] = int.Parse(ranges[0]);
                higherRanges[counter] = int.Parse(ranges[1]);

                // the second data attribute on each line is the corresponding letter for the password
                letters[counter] = sections[1].ToCharArray()[0];

                // the corresponding passowrd is the third attribute on the line
                passwords[counter] = sections[2];

                counter++;
            }

            file.Close();
            Console.WriteLine("There were {0} lines.", counter);

        }

        void CheckPasswords()
        {

            int validPasswords = 0;

            for (int i = 0; i < 1000; i++)
            {   
                int counter = 0;
                char[] passwordLetters = passwords[i].ToCharArray();

                for (int j = 0; j < passwordLetters.Length; j++)
                {
                    if(passwordLetters[j] == letters[i]) {
                        counter++;
                    }
                }

                if(counter >= lowerRanges[i] && counter <= higherRanges[i]) {
                    validPasswords++;
                }

            }

            Console.WriteLine("There are {0} valid passowrds", validPasswords);
        }

        static void Main(string[] args)
        {
            DodgyPasswords d = new DodgyPasswords();
            d.ReadInputFile();
            d.CheckPasswords();
        }
    }
}
