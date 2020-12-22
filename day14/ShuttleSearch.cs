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
        List<string> buses;

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

            buses = Regex.Split(file.ReadLine(), ",").ToList();


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
                foreach (string bus in buses)
                {
                    if (bus != "x")
                    {
                        int busNo;
                        if (int.TryParse(bus, out busNo))
                        {
                            if (currTime % busNo == 0)
                            {
                                found = true;
                                id = busNo;
                                break;
                            }
                        }

                    }

                }

                if (!found) currTime++;
            }

            Console.WriteLine("Bus ID you need to take: {0}", id);
            Console.WriteLine("Time the bus departs: {0}", currTime);
            Console.WriteLine("Wait time: {0}", currTime - earlistStart);

        }

        void FindSequenceTime()
        {
            
            bool found = true;
            int increment = int.Parse(buses[0]);
            long time = 100000000000000;

            while (found)
            {
                found = false;
                for (int i = 1; i < buses.Count(); i++)
                {
                    int busNo;
                    if (buses[i] != "x" && int.TryParse(buses[i], out busNo))
                    {
                        if((time + i) % busNo != 0) found = true;
                    }
                }

                if (found) time += increment;
            }

            Console.WriteLine(time);
        }

        static void Main(string[] args)
        {
            ShuttleSearch s = new ShuttleSearch();
            s.ReadInputFile();
            s.FindBestBus();
            s.FindSequenceTime();
        }
    }
}
