using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carrom
{
    public class Striker : Pawn
    {
        public Striker(double diameter, System.Drawing.Color color, Point position, Vector speedVector)
            : base(diameter, color, position, speedVector)
        {
            this.Diameter = diameter;
            this.Color = color;
            this.Position = position;
            this.SpeedVector = speedVector;
        }

        public void HitPiece(Pawn targetPawn)
        {
        }

    }
}
