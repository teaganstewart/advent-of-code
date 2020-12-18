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
    class HandheldHalting
    {

        HashSet<int> visitedIndexes = new HashSet<int>();


        /// <summary>
        ///
        /// </summary>
        Tuple<string, int>[] ReadInputFile()
        {
            return Regex.Split(File.ReadAllText(@"input.txt"), @"\n")
            .Select(entry =>
            {
                string[] pairArr = Regex.Split(entry, @"\s");
                int change = int.Parse(pairArr[1].Substring(1, pairArr[1].Length - 1));
                if (pairArr[1].Substring(0, 1) == "-")
                {
                    change *= -1;
                }
                return new Tuple<string, int>(pairArr[0], change);
            }).ToArray();
        }

        bool FindLoop(Tuple<string, int>[] commands, bool print)
        {
            visitedIndexes = new HashSet<int>();

            int nextIndex = 0;
            int accumulator = 0;

            while (!visitedIndexes.Contains(nextIndex))
            {
                if (nextIndex == commands.Length)
                {
                    Console.WriteLine("End reached! The accumulator is at {0}.", accumulator);
                    return true;
                }

                visitedIndexes.Add(nextIndex);
                switch (commands[nextIndex].Item1)
                {
                    case "acc":
                        accumulator += commands[nextIndex].Item2;
                        nextIndex++;
                        break;
                    case "jmp":
                        nextIndex += commands[nextIndex].Item2;
                        break;
                    case "nop":
                        nextIndex++;
                        break;
                    default:
                        Console.WriteLine("Oh no! Something went wrong with the input.");
                        return false;
                }
            }

            if (print) Console.WriteLine("When the commands hit an infinite loop the accumulator was at {0}.", accumulator);
            return false;
        }

        void FindWrongCommand(Tuple<string, int>[] commands)
        {

            bool endReached = false;
            foreach (int index in visitedIndexes)
            {
                switch (commands[index].Item1)
                {
                    case "acc":
                        break;
                    case "jmp":
                        commands[index] = new Tuple<string, int>("nop", commands[index].Item2);
                        endReached = FindLoop(commands, false);
                        commands[index] = new Tuple<string, int>("jmp", commands[index].Item2);
                        break;
                    case "nop":
                        commands[index] = new Tuple<string, int>("jmp", commands[index].Item2);
                        endReached = FindLoop(commands, false);
                        commands[index] = new Tuple<string, int>("nop", commands[index].Item2);
                        break;
                    default:
                        Console.WriteLine("Oh no! Something went wrong with the input.");
                        return;

                }

                if (endReached) break;
            }
        }

        static void Main(string[] args)
        {
            HandheldHalting h = new HandheldHalting();
            Tuple<string, int>[] commands = h.ReadInputFile();
            h.FindLoop(commands, true);
            h.FindWrongCommand(commands);
        }
    }
}
