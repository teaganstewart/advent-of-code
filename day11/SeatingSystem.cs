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
        /// 
        /// </summary>
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

        /// <summary> The seat map gets fulled based on some rules:
        /// 1. If a seat is empty ('L') and there are no adjacent occupied seats, then the seat becomes occupied.
        /// 2. If a seat is occupied ('#') and there are four or more adjacent occupied seats, then the seat becomes empty. 
        /// All changes are based on the previous seat map. </summary>
        void FillSeatMap(char[,] entries)
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

                            if (count == 0) temp[row, col] = '#';
                            if (count >= 4) temp[row, col] = 'L';
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

        void FillAdvancedSeatMap(char[,] entries)
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
                                            currRow = -1000;
                                            currCol = -1000;
                                        }
                                        else if (entries[currRow, currCol] == 'L')
                                        {
                                            currRow = -1000;
                                            currCol = -1000;
                                        }

                                        currRow += i;
                                        currCol += j;
                                    }
                                }
                            }

                            if (count == 0) temp[row, col] = '#';
                            if (count >= 5) temp[row, col] = 'L';
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
            s.FillSeatMap((char[,])entries.Clone());
            s.FillAdvancedSeatMap((char[,])entries.Clone());
        }
    }
}
