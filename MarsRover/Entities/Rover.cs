using MarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRover.Entities
{
    public class Rover
    {
        private Coordinate coordinate;
        private CompassDirection compassDirection;
        private String command;

        public Rover(int axis,int ordinate, string compassDirection,string command) {
            this.Coordinate = new Coordinate { Axis = axis, Ordinate = ordinate };
            this.CompassDirection = (CompassDirection)Enum.Parse(typeof(CompassDirection), compassDirection);
            this.Command = command;
        }

        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
        public CompassDirection CompassDirection { get => compassDirection; set => compassDirection = value; }
        public string Command { get => command; set => command = value; }

        public void CalculateNewDirection(int angle, char rotationDirection, Rover rover)
        {
            int newAngle = 0;
            if (rotationDirection.Equals('R'))
                newAngle = (short)rover.CompassDirection + (angle % 360);
            else if (rotationDirection.Equals('L'))
                newAngle = (short)rover.CompassDirection - (angle % 360);
            rover.CompassDirection = (CompassDirection)((newAngle % 360) + (newAngle < 0 ? 360 : 0));
        }

        public MovingResult CalculateNewLocation(Rover rover, Plateau plateau, List<Coordinate> busyCoordinates)
        {
            MovingResult returnValue = MovingResult.Success;
            switch (rover.CompassDirection)
            {
                case CompassDirection.N: {
                        returnValue = CheckPointAvailability(rover.Coordinate.Axis, rover.Coordinate.Ordinate + 1, plateau, busyCoordinates);
                        if (returnValue == MovingResult.Success)
                        {
                            busyCoordinates.RemoveAll(x => x.Axis == rover.Coordinate.Axis && x.Ordinate == rover.Coordinate.Ordinate);
                            rover.Coordinate.Ordinate = rover.Coordinate.Ordinate + 1;
                        }
                        break; 
                    }
                case CompassDirection.E: {
                        returnValue = CheckPointAvailability(rover.Coordinate.Axis + 1, rover.Coordinate.Ordinate, plateau, busyCoordinates);
                        if (returnValue == MovingResult.Success)
                        {
                            busyCoordinates.RemoveAll(x => x.Axis == rover.Coordinate.Axis && x.Ordinate == rover.Coordinate.Ordinate);
                            rover.Coordinate.Axis = rover.Coordinate.Axis + 1;
                        }
                        break;
                    }
                case CompassDirection.S:
                    {
                        returnValue = CheckPointAvailability(rover.Coordinate.Axis, rover.Coordinate.Ordinate - 1, plateau,busyCoordinates);
                        if (returnValue == MovingResult.Success)
                        {
                            busyCoordinates.RemoveAll(x => x.Axis == rover.Coordinate.Axis && x.Ordinate == rover.Coordinate.Ordinate);
                            rover.Coordinate.Ordinate = rover.Coordinate.Ordinate - 1;
                        }
                        break;
                    }
                case CompassDirection.W:
                    {
                        returnValue = CheckPointAvailability(rover.Coordinate.Axis - 1, rover.Coordinate.Ordinate, plateau,busyCoordinates);
                        if (returnValue == MovingResult.Success)
                        {
                            busyCoordinates.RemoveAll(x => x.Axis == rover.Coordinate.Axis && x.Ordinate == rover.Coordinate.Ordinate);
                            rover.Coordinate.Axis = rover.Coordinate.Axis - 1;
                        }
                        break;
                    }
            }

            if (returnValue == MovingResult.Success)
            {
                busyCoordinates.Add(new Coordinate { Axis = rover.Coordinate.Axis, Ordinate = rover.Coordinate.Ordinate });
            }

            return returnValue;
        }

        public MovingResult CheckPointAvailability(int axis, int ordinate, Plateau plateau, List<Coordinate> busyCoordinates)
        {
            MovingResult movingResult = MovingResult.Success;
            movingResult = CheckPlateauBorder(axis, ordinate, plateau);
            if (movingResult == MovingResult.Success)
                movingResult = CheckPointIsBusy(axis, ordinate, busyCoordinates);
            return movingResult;
        }

        private MovingResult CheckPlateauBorder(int axis, int ordinate, Plateau plateau)
        {
            if (axis > plateau.MaxCoordinate.Axis || axis < plateau.MinCoordinate.Axis
            || ordinate > plateau.MaxCoordinate.Ordinate || ordinate < plateau.MinCoordinate.Ordinate)
                return MovingResult.OutOfPlateau;
            else
                return MovingResult.Success;
        }

        private MovingResult CheckPointIsBusy(int axis, int ordinate, List<Coordinate> busyCoordinates)
        {
            if (busyCoordinates.Any(c => c.Axis == axis && c.Ordinate == ordinate))
                return MovingResult.BusyPoint;
            else
                return MovingResult.Success;
        }

    }
}
