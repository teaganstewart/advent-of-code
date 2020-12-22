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
        List<Tuple<string, int>> commands;

        void ReadInputFile()
        {
            commands = Regex.Split(File.ReadAllText(@"input.txt"), @"\s\n")
            .Select(command =>
            {
                int number;
                if (int.TryParse(command.Substring(1, command.Length - 1), out number))
                {
                    return new Tuple<string, int>(command.Substring(0, 1), number);
                }

                // shouldn"t reach this - null pointer exception means file reading failed
                return null;
            }).ToList();
        }

        void ProcessCommands()
        {

            int horizontal = 0;
            int vertical = 0;
            // the ship starts facing east, then follows the commands
            int currDirection = 1;

            foreach (Tuple<string, int> command in commands)
            {
                switch (command.Item1)
                {
                    case "N":
                        vertical += command.Item2;
                        break;
                    case "E":
                        horizontal += command.Item2;
                        break;
                    case "S":
                        vertical -= command.Item2;
                        break;
                    case "W":
                        horizontal -= command.Item2;
                        break;
                    case "L":
                        currDirection -= (command.Item2 / 90);
                        currDirection %= 4;
                        if (currDirection < 0) currDirection += 4;

                        break;
                    case "R":
                        currDirection += (command.Item2 / 90);
                        currDirection %= 4;
                        break;
                    case "F":
                        if (currDirection == 0)
                        {
                            vertical += command.Item2;
                        }
                        else if (currDirection == 1)
                        {
                            horizontal += command.Item2;
                        }
                        else if (currDirection == 2)
                        {
                            vertical -= command.Item2;
                        }
                        else if (currDirection == 3)
                        {
                            horizontal -= command.Item2;
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong!");
                            Console.WriteLine(currDirection);
                        }
                        break;

                    default:
                        break;

                }
            }

            Console.WriteLine(horizontal + " " + vertical);
        }

        void ProcessWaypointCommands()
        {
            Tuple<int, int> waypointCoords = new Tuple<int, int>(10, 1);
            Tuple<int, int> shipCoords = new Tuple<int, int>(0, 0);

            foreach (Tuple<string, int> command in commands)
            {
                switch (command.Item1)
                {
                    case "N":
                        waypointCoords = new Tuple<int, int>(waypointCoords.Item1, waypointCoords.Item2 + command.Item2);
                        break;
                    case "E":
                        waypointCoords = new Tuple<int, int>(waypointCoords.Item1 + command.Item2, waypointCoords.Item2);
                        break;
                    case "S":
                        waypointCoords = new Tuple<int, int>(waypointCoords.Item1, waypointCoords.Item2 - command.Item2);
                        break;
                    case "W":
                        waypointCoords = new Tuple<int, int>(waypointCoords.Item1 - command.Item2, waypointCoords.Item2);
                        break;
                    case "L":
                        waypointCoords = rotateWaypoint(waypointCoords, command.Item2, true);
                        break;
                    case "R":
                        waypointCoords = rotateWaypoint(waypointCoords, command.Item2, false);
                        break;
                    case "F":
                        shipCoords = new Tuple<int, int>(shipCoords.Item1 + waypointCoords.Item1 * command.Item2,
                            shipCoords.Item2 + waypointCoords.Item2 * command.Item2);
                        break;
                    default:
                        break;

                }

                Console.WriteLine(command.Item1 + command.Item2 + " " + shipCoords.Item1 + " " + shipCoords.Item2);
                Console.WriteLine(command.Item1 + command.Item2 + " " + waypointCoords.Item1 + " " + waypointCoords.Item2);


            }

        }

        Tuple<int, int> rotateWaypoint(Tuple<int, int> waypointCoords, int degrees, bool left)
        {
            if (!left)
            {
                if (degrees == 90) degrees = 270;
                else if (degrees == 270) degrees = 90;

            }
            switch (degrees %= 360)
            {
                case 90:
                    waypointCoords = new Tuple<int, int>(waypointCoords.Item2 * -1, waypointCoords.Item1 * 1);
                    break;
                case 180:
                    waypointCoords = new Tuple<int, int>(waypointCoords.Item1 * -1, waypointCoords.Item2 * -1);
                    break;
                case 270:
                    waypointCoords = new Tuple<int, int>(waypointCoords.Item2, waypointCoords.Item1 * -1);
                    break;
                default:
                    break;
            }

            return waypointCoords;
        }

        static void Main(string[] args)
        {
            RainRisk r = new RainRisk();
            r.ReadInputFile();
            r.ProcessCommands();
            r.ProcessWaypointCommands();
        }
    }
}
