using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Carrom
{
    public class Pawn
    {
        public double diameter { get; set; }
        public System.Drawing.Color color { get; set; }
        public Point position;
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector speedVector { get; set; }
        public bool inGame { get; set; }

        public Pawn(double diameter, System.Drawing.Color color, Point position, Vector speedVector)
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
