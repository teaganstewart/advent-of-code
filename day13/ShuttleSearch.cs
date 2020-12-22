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
    class ShuttleSearch
    {

        int earlistStart;
        List<int> buses;

        /// <summary>
        /// 
        /// </summary>
        void ReadInputFile()
        {
            StreamReader file = new StreamReader(@"input.txt");
            if (!int.TryParse(file.ReadLine(), out earlistStart))
            {
                Console.WriteLine("Not a valid file input.");
            }

            buses = Regex.Split(file.ReadLine(), ",")
                .Where(entry => entry != "x")
                .Select(entry => int.Parse(entry)).ToList();

            file.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        void FindBestBus()
        {
            bool found = false;
            int currTime = earlistStart;
            int id = -10;

            while (!found)
            {
                foreach (int bus in buses)
                {
                    if (currTime % bus == 0)
                    {
                        found = true;
                        id = bus;
                        break;
                    }
                }

                if(!found) currTime++;
            }

            Console.WriteLine("Bus ID you need to take: {0}", id);
            Console.WriteLine("Time the bus departs: {0}", currTime);
            Console.WriteLine("Wait time: {0}", currTime - earlistStart);
        }

        static void Main(string[] args)
        {
            ShuttleSearch s = new ShuttleSearch();
            s.ReadInputFile();
            s.FindBestBus();
        }
    }
}
