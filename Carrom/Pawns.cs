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
        private double diameter;
        public double Diameter 
        {
            get { return this.diameter; }
            set { this.diameter = value; } 
        }
        private System.Drawing.Color color;
        public System.Drawing.Color Color
        {
            get {return this.color; }
            set { this.color = value; } 
        }
        
        private Point position;
        public Point Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        private Vector speedVector;
        public Vector SpeedVector
        {
            get { return this.speedVector; }
            set { this.speedVector = value; }
        }
        private bool inGame;
        public bool InGame
        {
            get { return this.inGame; }
            set { this.inGame = value; }
        }
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
