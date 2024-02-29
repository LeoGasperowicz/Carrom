using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
namespace Carrom
{
    public class Pawn
    {
        public double diameter { get; set; }
        public Color color { get; set; }
        public Point position { get; set; }
        public Vector2 speedVector { get; set; }
        public bool inGame { get; set; }

        public Pawn(double diameter, Color color, Point position, Vector2 speedVector)
        {
            this.diameter = diameter;
            this.color = color;
            this.position = position;
            this.speedVector = speedVector;
        }

        public void Move(Point newPosition)
        {
        }

        public bool isInHole()
        {
            return true;
        }

    }
}
