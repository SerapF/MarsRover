using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Entities
{
    public class Coordinate
    {
        private int axis;
        private int ordinate;

        public int Axis { get => axis; set => axis = value; }
        public int Ordinate { get => ordinate; set => ordinate = value; }
    }
}
