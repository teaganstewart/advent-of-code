using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{

    /// <summary>
    /// 
    /// </summary>
    class AdapterArray
    {
        int oneJoltDifference;
        int threeJoltDifference;
        List<int> entries;

        /// <summary>
        ///
        /// </summary>
        void ReadInputFile()
        {
            entries = Regex.Split(File.ReadAllText(@"input.txt"), @"\n")
            .Select(entry => int.Parse(entry)).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        void CheckJolts()
        {
            int current = 0;
            oneJoltDifference = 0;
            threeJoltDifference = 1;
            while (entries.Count() != 0)
            {
                switch (entries.Min() - current)
                {
                    case 1:
                        oneJoltDifference++;
                        break;
                    case 2:
                        break;
                    case 3:
                        threeJoltDifference++;
                        break;
                    default:
                        break;

                }

                current = entries.Min();
                entries.Remove(entries.Min());

            }

            Console.WriteLine(oneJoltDifference * threeJoltDifference);
        }

        /// <summary>
        ///
        /// </summary>
        void DistinctArrangements()
        {
           
        }

        static void Main(string[] args)
        {
            AdapterArray a = new AdapterArray();
            a.ReadInputFile();
            a.DistinctArrangements();
            a.CheckJolts();
        }
    }
}
