using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carrom
{
    public class CarromPiece : Pawn
    {
        public CarromPiece(double diameter, System.Drawing.Color color, Point position, Vector speedVector)
        : base(diameter, color, position, speedVector)
        {
            this.diameter = diameter;
            this.color = color;
            this.position = position;
            this.speedVector = speedVector;
        }

    }
}
