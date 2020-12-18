using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Creates a class that reads a series of commands from a broken handheld device, and tries to fix the problem. Commands are given as a list in a file, which can be read and interpretted.
    /// </summary>
    class HandheldHalting
    {

        HashSet<int> visitedIndexes = new HashSet<int>();
        Tuple<string, int>[] commands;

        /// <summary>
        /// Reads each line of the input file. Each line desribes a command: acc, jmp or nop. acc updates the accumulator, jmp jumps to a new position and
        /// nop does nothing. Stores the commands so we can find the problem later.
        /// </summary>
        void ReadInputFile()
        {
            commands = Regex.Split(File.ReadAllText(@"input.txt"), @"\n")
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

        /// <summary>
        /// Looks through the stored commands, following the respective commands. If an index is reached twice, this means a command has been read twice 
        /// so will end in an infinite loop. This method checks whether the given commands result in an infinite loop, or the end is reached.
        /// </summary>
        /// <param name="print"> If true, a print statement will occur when an infinite loop is reached. </param>
        /// <returns> Returns true if the end is reached, false if an infinite loop occurs. </returns>
        bool FindLoop(bool print)
        {
            visitedIndexes = new HashSet<int>();

            int nextIndex = 0;
            int accumulator = 0;

            // as soon as a visited index is revisited, we know an inifinite loop is going to be formed.
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

        /// <summary>
        /// As the list of commands I have been given results in an inifinite loop, this means that one command is probably wrong. If I can find a
        /// command that can be changed, and the infinite loop is fixed I will change it. This method finds the problem command and changes it, to 
        /// work out the correct acucmulator value.
        /// </summary>
        void FindWrongCommand()
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
                        endReached = FindLoop(false);
                        // sets commands back, so it is not mutated for later.
                        commands[index] = new Tuple<string, int>("jmp", commands[index].Item2);
                        break;
                    case "nop":
                        commands[index] = new Tuple<string, int>("jmp", commands[index].Item2);
                        endReached = FindLoop(false);
                        commands[index] = new Tuple<string, int>("nop", commands[index].Item2);
                        break;
                    default:
                        Console.WriteLine("Oh no! Something went wrong with the input.");
                        return;

                }

                // if one command change works, then the rest don't need to be checked.
                if (endReached) break;
            }
        }

        static void Main(string[] args)
        {
            HandheldHalting h = new HandheldHalting();
            h.ReadInputFile();
            h.FindLoop(true);
            h.FindWrongCommand();
        }
    }
}
