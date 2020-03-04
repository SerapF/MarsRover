using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Entities
{
    public class Plateau
    {
        private Coordinate minCoordinate;
        private Coordinate maxCoordinate;

        public Coordinate MinCoordinate { get => minCoordinate; set => minCoordinate = value; }
        public Coordinate MaxCoordinate { get => maxCoordinate; set => maxCoordinate = value; }

        public Plateau(int minAxis, int minOrdinate, int maxAxis, int maxOrdinate)
        {
            this.minCoordinate = new Coordinate { Axis = minAxis, Ordinate = minOrdinate };
            this.maxCoordinate = new Coordinate { Axis = maxAxis, Ordinate = maxOrdinate };
        }

        public void SetPlateau(int minAxis,int minOrdinate,int maxAxis,int maxOrdinate, Plateau plateau)
        {
            plateau.minCoordinate = new Coordinate { Axis = minAxis, Ordinate = minOrdinate };
            plateau.maxCoordinate = new Coordinate { Axis = maxAxis, Ordinate = maxOrdinate };

        }
}
}
