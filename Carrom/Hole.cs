using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carrom
{
    public class Hole
    {
        private Point center;
        private int diameter;

        public Point Center { get { return center; } }
        public int Diameter { get { return diameter; } }
        public Hole(Point center, int diameter) 
        {
            this.center = center;
            this.diameter = diameter;   
        }
        public override string ToString()
        {
            return $"Hole at {Center}";
        }
    }
}
