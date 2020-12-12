using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /*
    * Checks the path to see the number of trees in the way. Day 3 of the Advent of Code challenge.
    *
    * The main idea is to check the path the Toboggan will travel down for trees. The toboggan can only move 3 to the right and 1 to the left.
    *
    * ..##.......
    * #..O#...#.. is an example field, where #'s represent trees and dots are clear paths.
    */
    class PassportProcessor
    {

        List<int> lines;

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

        static void Main(string[] args)
        {
            PassportProcessor p = new PassportProcessor();

        }
    }
}
