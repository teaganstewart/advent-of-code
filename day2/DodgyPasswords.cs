using System;

namespace AdventOfCode
{
    /*
    * Creates a password check for a make believe Toboggan factory. Day 2 of the Advent of Code challenge.
    *
    * Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character, and so on. (Be careful; Toboggan Corporate Policies have no concept of "index zero"!) 
    * Exactly one of these positions must contain the given letter. Other occurrences of the letter are irrelevant for the purposes of policy enforcement.
    *
    * 1-3 a: abcde is valid: position 1 contains a and position 3 does not.
    * 1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
    * 2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.
    */
    class DodgyPasswords
    {

        int[] lowerRanges;
        int[] upperRanges;
        char[] letters;
        string[] passwords;


        public void ReadInputFile()
        {

            // initalisation of storage variables
            int counter = 0;
            lowerRanges = new int[1000];
            upperRanges = new int[1000];
            letters = new char[1000];
            passwords = new string[1000];

            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");

            while ((line = file.ReadLine()) != null)
            {
                // splits each line read into the seperate data attributes
                string[] sections = line.Split(" ");

                // stores the range [0] = lower [1] = upper 
                string[] ranges = sections[0].Split("-");

                lowerRanges[counter] = int.Parse(ranges[0]);
                upperRanges[counter] = int.Parse(ranges[1]);

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
                    if (passwordLetters[j] == letters[i]) counter++;

                }

                if (counter >= lowerRanges[i] && counter <= upperRanges[i]) validPasswords++;
            }

            Console.WriteLine("There are {0} valid passowrds", validPasswords);
        }

        void CorrectPasswordCheck()
        {

            int validPasswords = 0;

            for (int i = 0; i < 1000; i++)
            {

                bool lowerCheck, upperCheck;

                char[] passwordLetters = passwords[i].ToCharArray();

                // checks both indexes to see if they match the given letter, -1 accounts for the fact indexes start at 1 not 0.
                lowerCheck = passwordLetters[lowerRanges[i] - 1] == letters[i];
                upperCheck = passwordLetters[upperRanges[i] - 1] == letters[i];

                if (lowerCheck != upperCheck) validPasswords++;

            }

            Console.WriteLine("There are {0} valid passowrds", validPasswords);
        }

        static void Main(string[] args)
        {
            DodgyPasswords d = new DodgyPasswords();
            d.ReadInputFile();
            d.CheckPasswords();
            d.CorrectPasswordCheck();
        }
    }
}
