using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MarsRover.Enums
{
    public enum MovingResult
    {
       [Description("This coordinate is busy.")]
       BusyPoint,
       [Description("Out Of Plateau!.")]
       OutOfPlateau,
       Success
       
    }
}
