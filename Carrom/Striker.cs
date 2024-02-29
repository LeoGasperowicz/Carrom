using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace Carrom
{
    public class Striker : Pawn
    {
        public Striker(double diameter, Color color, Point position, Vector2 speedVector)
            : base(diameter, color, position, speedVector)
        {
            this.diameter = diameter;
            this.color = color;
            this.position = position;
            this.speedVector = speedVector;
        }

        public void HitPiece(Pawn targetPawn)
        {
        }

    }
}
