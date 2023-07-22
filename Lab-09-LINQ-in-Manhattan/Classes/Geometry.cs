using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_09_LINQ_in_Manhattan.Classes
{
    public class Geometry
    {
        public string type { get; set; }
        public List<double>? coordinates { get; set; }

        public string ConcatinateCoordinates()
        {
            if (coordinates == null || coordinates.Count < 2)
            {
                return "No coordinates";
            }

            return string.Join(", ", coordinates);
        }
    }
}
