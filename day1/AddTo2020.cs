using System;
using System.Collections.Generic;

namespace AdventOfCode
{

    /// <summary>
    /// Checks input for two numbers that add up to 2020. Day 1 of the Advent of Code challenge.
    /// </summary>
    class AddTo2020
    {

        List<int> numbers;

        /// <summary>
        /// Reads numbers in input file for use later.
        /// </summary>
        public void ReadInputFile()
        {
            int counter = 0;
            string line;
            numbers = new List<int>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"input.txt");
            while ((line = file.ReadLine()) != null)
            {
                numbers.Add(int.Parse(line));
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            System.Console.WriteLine(numbers.Count);

        }

        /// <summary>
        /// Checks to see if there are two numbers that add to 2020 from the input file.
        /// </summary>
        int CheckFor2020Two()
        {

            foreach (int i in numbers)
            {
                foreach (int j in numbers)
                {
                    if (i + j == 2020)
                    {
                        return i * j;
                    }
                }
            }

            return 0;

        }

        /// <summary>
        /// Checks to see if there are three numbers that add to 2020 from the input file.
        /// </summary>
        int CheckFor2020Three()
        {

            foreach (int i in numbers)
            {
                foreach (int j in numbers)
                {
                    foreach (int k in numbers)
                    {
                        if (i + j + k == 2020)
                        {
                            return i * j * k;
                        }
                    }
                }
            }

            return 0;

        }

        static void Main(string[] args)
        {
            AddTo2020 a = new AddTo2020();
            a.ReadInputFile();
            Console.WriteLine(a.CheckFor2020Two());
            Console.WriteLine(a.CheckFor2020Three());
        }
    }
}
