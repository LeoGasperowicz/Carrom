using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
        private Color color;
        public Color Color
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
        private bool inGame;
        public bool InGame
        {
            get { return this.inGame; }
            set { this.inGame = value; }
        }
        
        public Pawn(double diameter, Color color, Point position)
        {
            this.diameter = diameter;
            this.color = color;
            this.position = position;
            this.inGame=true;
        }
        public override string ToString()
        {
            return $"CarromPiece {Color}";
        }
    }
}
