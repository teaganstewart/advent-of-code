using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Author: Teagan Stewart.
    /// Checks the path to see the number of trees in the way. Day 3 of the Advent of Code challenge.
    /// The main idea is to check the path the Toboggan will travel down for trees. The toboggan can only move 3 to the right and 1 to the left.
    /// ..##....... 
    /// #..O#...#.. is an example field, where #'s represent trees and dots are clear paths.
    /// </summary>
    class TreeMaze
    {
        List<string> lines;
        char[,] treeMap;

        /// <summary>
        /// Reads the Tree Maze from the input file.
        /// </summary>
        public void ReadInputFile()
        {
            // initalisation of storage variables
            int counter = 0;
            string line;
            lines = new List<string>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"input.txt");

            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
                counter++;
            }

            file.Close();
            Console.WriteLine("There were {0} lines read.", counter);

        }

        /// <summary>
        /// Creates the tree map based on the input file. Tree Map loops infinitely horizontally to create the tree field.
        /// </summary>
        void SetupTreeMap()
        {
            treeMap = new char[lines.Count, lines[0].Length];

            for (int i = 0; i < lines.Count; i++)
            {
                char[] linesLetters = lines[i].ToCharArray();

                for (int j = 0; j < linesLetters.Length; j++)
                {
                    treeMap[i, j] = linesLetters[j];
                }

            }

        }

        /// <summary>
        /// Checks the required slopes to find the slope that has the least trees in the way.
        /// </summary>
        void CheckAllSlopes()
        {
            int[] xCoords = new int[] { 1, 3, 5, 7, 1 };
            int[] yCoords = new int[] { 1, 1, 1, 1, 2 };

            long res = 1;

            for (int i = 0; i < xCoords.Length; i++)
            {
                res *= FindPath(xCoords[i], yCoords[i]);
            }

            Console.WriteLine("The number of trees multiplied is {0}", res);
        }

        /// <summary>
        /// Finds a path through the trees based on the slope, uses the modulo of the index to work out where in the tree field you currently are. 
        /// </summary>
        /// <param name="xChange"> The x change of the slope. </param>
        /// <param name="yChange"> The y change of the slope. </param>
        /// <returns> The number of trees hit on the given slope. x/y. </returns>
        int FindPath(int xChange, int yChange)
        {
            int x = 0; int y = 0;
            int treesHit = 0;

            while (y < lines.Count - yChange)
            {
                x = (x + xChange) % lines[0].Length;
                y += yChange;

                if (treeMap[y, x] == '#')
                {
                    treesHit++;
                }
            }

            Console.WriteLine("On your path {0} right, {1} down you will hit {2} trees.",
                xChange, yChange, treesHit);

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
