using MarsRover.Entities;
using MarsRover.Enums;
using MarsRover.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rover> rovers = new List<Rover>();
            Plateau plateau = new Plateau(0, 0, 0, 0);
            List<Coordinate> busyCoordinates = new List<Coordinate>();
            GetRoverData(Environment.CurrentDirectory + "..\\..\\..\\..\\" + "RoverData.txt", rovers, plateau, busyCoordinates);
            if (rovers != null)
            {
                Console.WriteLine("Results:");
                int index = 0;
                string errorMessage = String.Empty;
                foreach (Rover rover in rovers)
                {
                    errorMessage = String.Empty;
                    index++;
                    char[] commandList = rover.Command.ToCharArray();
                    foreach (char commandItem in commandList)
                    {
                        if (commandItem.Equals('L') || commandItem.Equals('R'))
                        {
                            rover.CalculateNewDirection(90, commandItem, rover);
                        }
                        else if (commandItem.Equals('M'))
                        {
                            MovingResult locationResult = rover.CalculateNewLocation(rover, plateau, busyCoordinates);
                            if (locationResult != MovingResult.Success)
                            {
                                errorMessage = EnumHelper.GetDescription(locationResult);
                                break;
                            }

                        }
                    }

                    Console.WriteLine("Rover " + index + " location information is: " + rover.Coordinate.Axis.ToString() + " " + rover.Coordinate.Ordinate.ToString() + " " + Enum.GetName(typeof(CompassDirection), rover.CompassDirection).ToString() + " " + errorMessage);
                }

                Console.ReadLine();
            }
            else
                Console.WriteLine("No rover data!");
        }

        private static void GetRoverData(string filePath, List<Rover> rovers, Plateau plateau, List<Coordinate> busyCoordinates)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                int axis, maxAxis, minAxis = 0;
                int ordinate, maxOrdinate, minOrdinate = 0;
                string direction = String.Empty;
                string command = String.Empty;
                if (lines != null && lines.Length >= 3)
                {
                    maxAxis = Int32.Parse((lines[0].Split(' ')[0]));
                    maxOrdinate = Int32.Parse((lines[0].Split(' ')[1]));
                    plateau.SetPlateau(minAxis, minOrdinate, maxAxis, maxOrdinate, plateau);
                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (i % 2 == 1)
                        {
                            String[] roverData = lines[i].Split(' ');
                            axis = Int32.Parse(roverData[0]);
                            ordinate = Int32.Parse(roverData[1]);
                            direction = roverData[2];
                            i++;
                            command = lines[i];
                            Rover rover = new Rover(axis, ordinate, direction, command);
                            MovingResult pointAvailabilityResult = rover.CheckPointAvailability(axis, ordinate, plateau, busyCoordinates);
                            if (pointAvailabilityResult == MovingResult.Success)
                            {
                                busyCoordinates.Add(new Coordinate { Axis = axis, Ordinate = ordinate });
                                rovers.Add(new Rover(axis, ordinate, direction, command));
                            }

                            else
                                Console.WriteLine("Rover " + (rovers.Count + 1) + " can not be located. " + EnumHelper.GetDescription(pointAvailabilityResult));

                        }
                    }
                }

                else
                    Console.WriteLine("Wrong Input");
            }
            catch (Exception e)
            {

                Console.WriteLine("Something went wrong while getting data. Please check your file and its content format.");

            }
        }



    }


}
