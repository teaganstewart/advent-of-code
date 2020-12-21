using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// </summary>
    class SeatingSystem
    {

        /// <summary> 
        /// Reads the input file and sorts it into a map of the occupied and empty seats, and the non-seat portions of the ferry. 
        /// </summary>
        /// <returns> Returns the completed seat map. To be used in other methods. </returns>
        char[,] CreateSeatMap()
        {
            List<char[]> rows = Regex.Split(File.ReadAllText(@"input.txt"), @"\s\n")
            .Select(row => row.ToCharArray()).ToList();
            char[,] entries = new char[rows.Count(), rows[0].Length];

            for (int row = 0; row < rows.Count(); row++)
            {
                for (int col = 0; col < rows[0].Length; col++)
                {
                    entries[row, col] = rows[row][col];
                }
            }

            return entries;
        }

        /// <summary> 
        /// The seat map gets fulled based on some rules:
        /// 1. If a seat is empty ('L') and there are no adjacent occupied seats, then the seat becomes occupied.
        /// 2. If a seat is occupied ('#') and there are four (five for advanced search) or more adjacent occupied seats, then the seat becomes empty. 
        ///
        /// In normal search, the search is based on the 8 directly adjacent seats. In advanced searh, the seats are based on the first seat customers can see in a given direction.
        /// All changes are based on the previous seat map. 
        /// </summary>
        /// <param name="entries"> The seat map. </param>
        /// <param name="advanced"> </param>
        void FillSeatMap(char[,] entries, bool advanced)
        {
            bool start = true;

            char[,] temp = new char[entries.GetLength(0), entries.GetLength(1)];

            while (!SeatEquals(entries, temp))
            {
                if (start)
                {
                    temp = (char[,])entries.Clone();
                    start = false;
                }
                else
                {
                    entries = (char[,])temp.Clone();
                }

                // entries.GetLength(1) returns the number of rows in the multidimensional array entries.
                for (int row = 0; row < entries.GetLength(0); row++)
                {
                    // entries.GetLength(1) returns the number of columns.
                    for (int col = 0; col < entries.GetLength(1); col++)
                    {

                        int count = 0;
                        if (temp[row, col] != '.')
                        {
                            //checks adjacent seats
                            if (advanced) count = AdvancedSearch(entries, row, col);
                            else count = NormalSearch(entries, row, col);

                            if (count == 0) temp[row, col] = '#';
                            if (count >= ((advanced) ? 5 : 4)) temp[row, col] = 'L';
                        }

                    }
                }
            }

            int seatCount = 0;
            foreach (char c in temp)
            {
                if (c == '#') seatCount++;
            }

            Console.WriteLine("There are {0} occupied seats.", seatCount);
        }

        /// <summary> 
        /// A helper method for the advanced seat fill method. Checks each direction until it hits the edge of the map, or a seat.
        /// If the seat is empty, it doesn't add one to the count of occupied seats, otherwise it adds 1. 
        /// </summary>
        /// <param name="entries"> The seat map. </param>
        /// <param name="row"> The current row we are searching adjacent to. </param>
        /// <param name="col"> The current col we are searching adjacent to. </param>
        /// <returns> The number of seats adjacent to the current seat. </returns>
        int NormalSearch(char[,] entries, int row, int col)
        {
            int count = 0;

            // loops through the adjacent seats
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // checks that index is in range of seat map
                    if ((row + i) >= 0 && (row + i) < entries.GetLength(0) && (col + j) >= 0
                            && (col + j) < entries.GetLength(1) && !(i == 0 && j == 0))
                    {
                        if (entries[row + i, col + j] == '#') count++;
                    }
                }

            }

            return count;
        }

        /// <summary> 
        /// A helper method for the advanced seat fill method. Checks each direction until it hits the edge of the map, or a seat.
        /// If the seat is empty, it doesn't add one to the count of occupied seats, otherwise it adds 1. 
        /// </summary>
        /// <param name="entries"> The seat map. </param>
        /// <param name="row"> The current row we are searching adjacent to. </param>
        /// <param name="col"> The current col we are searching adjacent to. </param>
        /// <returns> The number of seats next to the current seat. </returns>
        int AdvancedSearch(char[,] entries, int row, int col)
        {
            int count = 0;

            // loop through all directions viewable from current seat
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int currRow = row + i;
                    int currCol = col + j;

                    // checks that index is in range of seat map
                    while ((currRow >= 0 && currRow < entries.GetLength(0) && currCol >= 0 && currCol < entries.GetLength(1) && !(i == 0 && j == 0)))
                    {
                        if (entries[currRow, currCol] == '#')
                        {
                            count++;
                            break;
                        }
                        else if (entries[currRow, currCol] == 'L') break;

                        // conitnues to next spot for a possible seat
                        currRow += i;
                        currCol += j;
                    }
                }
            }

            return count;
        }

        /// <summary> 
        /// Helper method for the seat filler method. Checks if two contigous steps of the seat map are the same, if so it
        /// has reached equilibrium. 
        /// </summary>
        /// <param name="entries"> The last step's seat map. </param>
        /// <param name="temp"> The latest seat map. </param>
        /// <returns> Returns true if the seat maps are the same. </returns>
        bool SeatEquals(char[,] entries, char[,] temp)
        {
            for (int row = 0; row < entries.GetLength(0); row++)
            {
                for (int col = 0; col < entries.GetLength(1); col++)
                {
                    if (temp[row, col] != entries[row, col]) return false;
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            SeatingSystem s = new SeatingSystem();
            char[,] entries = s.CreateSeatMap();
            s.FillSeatMap((char[,])entries.Clone(), false);
            s.FillSeatMap((char[,])entries.Clone(), true);
        }
    }
}
