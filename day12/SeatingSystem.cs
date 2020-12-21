using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// </summary>
    class RainRisk
    {

        char TurnLeft(char direction) {
            switch(direction) {
                case 'N':
                    return 'W';
                case 'E':
                    return 'N';
                case 'S':
                    return 'E';
                case 'W':
                    return 'S';
            }
        }

        char TurnRight(char direction) {
            switch(direction) {
                case 'N':
                    return 'E';
                case 'E':
                    return 'S';
                case 'S':
                    return 'W';
                case 'W':
                    return 'N';
            }
        }

        static void Main(string[] args)
        {
            RainRisk s = new RainRisk();
            char[,] entries = s.CreateSeatMap();
            s.FillSeatMap((char[,])entries.Clone(), false);
            s.FillSeatMap((char[,])entries.Clone(), true);
        }
    }
}
