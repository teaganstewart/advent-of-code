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
    class TreeMaze
    {

        string[] lines;
        char[,] treeMap;

        public void ReadInputFile()
        {

            // initalisation of storage variables
            int counter = 0;
            string line;
            lines = new string[323];

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");

            while ((line = file.ReadLine()) != null)
            {
                lines[counter] = line;
                counter++;
            }

            file.Close();
            Console.WriteLine("There were {0} lines read.", counter);

        }

        void SetupTreeMap()
        {
            treeMap = new char[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                char[] linesLetters = lines[i].ToCharArray();

                for (int j = 0; j < linesLetters.Length; j++)
                {
                    treeMap[i, j] = linesLetters[j];
                }

            }

        }

        void FindPath()
        {
            int x = 0; int y = 0;
            int treesHit = 0;

            while (y < lines.Length - 1)
            {
                x = (x + 3) % lines[0].Length;
                y++;

                if (treeMap[y, x] == '#')
                {
                    treesHit++;
                }
            }

            Console.WriteLine("On your path you will hit {0} trees.", treesHit);

        }

        static void Main(string[] args)
        {
            TreeMaze t = new TreeMaze();
            t.ReadInputFile();
            t.SetupTreeMap();
            t.FindPath();

        }
    }
}
