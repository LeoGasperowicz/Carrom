using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Carrom
{
    public class Striker : Pawn
    {
        public Striker(double diameter, Color color, Point position)
            : base(diameter, color, position)
        {
            this.Diameter = diameter;
            this.Color = color;
            this.Position = position;
        }

        

    }
}
