using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Carrom
{
    public class CarromPiece : Pawn
    {
        private int number;
        public int Number
        {
            get { return this.number; }
            set { this.number = value; }
        }
        public CarromPiece(double diameter, Color color, Point position, int number)
        : base(diameter, color, position)
        {
            this.Diameter = diameter;
            this.Color = color;
            this.Position = position;
            this.Number = number;
        }

    }
}
