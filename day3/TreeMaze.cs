using System;

namespace AdventOfCode
{
    /*
    * Checks the path to see the number of trees in the way. Day 3 of the Advent of Code challenge.
    *
    * The main idea is to check the path the Toboggan will travel down for trees. The toboggan can only move 3 to the right and 1 to the left.
    *
    * ..##.........##.........##.........##.........##.........##.......  --->
    * #..O#...#..#...#...#..#...#...#..#...#...#..#...#...#..#...#...#.. is an example field, where #'s represent trees and dots are clear paths.
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

        void CheckAllSlopes()
        {
            int[] xCoords = new int[] { 1, 3, 5, 7, 1 };
            int[] yCoords = new int[] { 1, 1, 1, 1, 2 };

            int res = 1;

            for (int i = 0; i < xCoords.Length; i++)
            {
                res *= FindPath(xCoords[i], yCoords[i]);
            }

            Console.WriteLine("The number of trees multiplied is {0}", res);
        }

        int FindPath(int xChange, int yChange)
        {
            int x = 0; int y = 0;
            int treesHit = 0;

            while (y < lines.Length - yChange)
            {
                x = (x + xChange) % lines[0].Length;
                y += yChange;

                if (treeMap[y, x] == '#')
                {
                    treesHit++;
                }
            }

            Console.WriteLine("On your path {0} right, {1} down you will hit {2} trees.", xChange, yChange, treesHit);

            return treesHit;

        }

        static void Main(string[] args)
        {
            TreeMaze t = new TreeMaze();
            t.ReadInputFile();
            t.SetupTreeMap();
            t.CheckAllSlopes();

        }
    }
}
