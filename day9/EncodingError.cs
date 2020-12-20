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
    class EncodingError
    {
        const int PreambleLength = 25;
        long[] entries;
        long invalidNumber;

        void ReadInputFile()
        {
            entries = Regex.Split(File.ReadAllText(@"input.txt"), @"\n")
            .Select(entry => long.Parse(entry)).ToArray();
        }

        void FindWrongEncryption()
        {
            for (long i = PreambleLength; i < entries.Length; i++)
            {
                bool found = false;
                for (long j = i - PreambleLength; j < i; j++)
                {
                    for (long k = i - PreambleLength; k < i; k++)
                    {
                        if (i != j)
                        {
                            if (entries[j] + entries[k] == entries[i])
                            {
                                found = true;
                            }
                        }
                    }
                }

                if (!found)
                {
                    invalidNumber = entries[i];
                    Console.WriteLine(invalidNumber);
                }

            }
        }

        void FindContiguousSet()
        {
            for (int i = 0; i < entries.Length - 1; i++)
            {
                long sum = entries[i];
                for (int j = i + 1; j < entries.Length; j++)
                {
                    sum += entries[j];
                    if (sum > invalidNumber)
                    {

                        break;
                    }
                    if (sum == invalidNumber)
                    {
                        long min = long.MaxValue;
                        long max = long.MinValue;
                        for (int k = i; k <= j; k++)
                        {
                            if (entries[k] > max) max = entries[k];
                            if (entries[k] < min) min = entries[k];
                        }
                        Console.WriteLine("The answer is {0}", max + min);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            EncodingError e = new EncodingError();
            e.ReadInputFile();
            e.FindWrongEncryption();
            e.FindContiguousSet();
        }
    }
}
