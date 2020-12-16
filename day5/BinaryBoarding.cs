using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// A class that given an input of passwords, each field seperated by spaces and each passport seperated by an empty line it will check whether the password is valid or not.
    /// Makes sure all requried passport fields are there, and if they are, if the value is valid. Counts the number of valid passports from the input file.
    /// </summary>
    class BinaryBoarding
    {
        // constant variables that can be changed based on the size of the plane
        const int MinRow = 0;
        const int MaxRow = 127;
        const int MinCol = 0;
        const int MaxCol = 7;

        List<string> passes;
        List<Tuple<int, int>> seats;

        /// <summary>
        /// Reads the boarding passes from the input file.
        /// </summary>
        public void ReadInputFile()
        {
            string line;
            StreamReader file = new StreamReader(@"input.txt");
            passes = new List<string>();

            while ((line = file.ReadLine()) != null)
            {
                passes.Add(line);
            }

            file.Close();
        }

        /// <summary>
        /// Takes the input and gets the seats row and column data out, using the ReadBinary helper method.
        /// Also finds the largest seat ID (maximum row * 8 + maximum col)
        /// </summary>
        void PassProcessor()
        {
            seats = new List<Tuple<int, int>>();
            int maxRow = 0;
            int maxCol = 0;

            foreach (string pass in passes)
            {
                string row = pass.Substring(0, pass.Length - 3);
                string col = pass.Substring(pass.Length - 3, 3);

                // gets the current boarding passes row number (0 to 127)
                int rowNo = ReadBinary(row.ToCharArray(), MinRow, MaxRow, 'F');

                // gets the current boarding passes column number (0 to 7)
                int colNo = ReadBinary(col.ToCharArray(), MinCol, MaxCol, 'L');

                seats.Add(new Tuple<int, int>(rowNo, colNo));

                // helps find the maximum seat id
                if (rowNo > maxRow)
                {
                    maxRow = rowNo;
                    maxCol = colNo;
                }
                else if (rowNo == maxRow)
                {
                    if (colNo > maxCol)
                    {
                        maxCol = colNo;
                    }
                }
            }

            Console.WriteLine("The maximum seat ID is {0}.", (maxRow * 8) + maxCol);
        }

        /// <summary>
        /// Creates a list of ordered seat ids so I can easily find the missing seat, which is therefore mine.
        /// </summary>
        void FindMySeat()
        {
            // converts the list of seat rows and cols into a list of ordered seat ids
            var seatIds = seats.Select(seat =>
            {
                return (seat.Item1 * 8) + seat.Item2;
            }).OrderBy(id => id);

            int curr = seatIds.ElementAt(0);

            // uses the order seat list to find the missing seat id
            for (int i = 1; i < seatIds.Count(); i++)
            {
                // if + 1 doesnt exist, then that seat is missing
                if (seatIds.ElementAt(i) == curr + 1) curr++;
                else
                {
                    curr++;
                    break;
                }
            }

            int row = curr / 8;
            int col = curr - (curr / 8) * 8;
            Console.WriteLine("Your seat has the ID {0}. So it is row {1}, col {2}.", curr, row, col);
        }

        // ---------------------------------------
        // ----------- HELPER METHOD -------------
        // ---------------------------------------

        /// <summary>
        /// Helper method to read the boarding pass data into seat data.
        /// If the seat is in the lower half it will make the maximum the middle of the min and max,
        /// if the seat is in the higher half it will make the minimum the middle + 1, as middle is in the lower
        /// section as indexes start at 0.
        /// <param name="arr"> The current array of chars to read, specifies upper or lower. </param>
        /// <param name="min"> The minimum number row/col. </param>
        /// <param name="max"> The maximum number row/col. </param>
        /// <param name="lower"> Whether arr is a col or row array. </param>
        /// </summary>
        int ReadBinary(char[] arr, int min, int max, char lower)
        {
            foreach (char c in arr)
            {
                int middle = (min + max) / 2;

                if (c == lower) max = middle;
                else min = middle + 1;
            }

            return min;
        }

        static void Main(string[] args)
        {
            BinaryBoarding b = new BinaryBoarding();
            b.ReadInputFile();
            b.PassProcessor();
            b.FindMySeat();
        }
    }
}
