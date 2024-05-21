using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Carrom
{
    public class Board
    {
        private double height { get; }
        private double width { get; }
        private List<Hole> holes { get; set; }
        public List<Hole> Holes
        {
            get { return this.holes; }
        }
        public double Width
        {
            get { return this.width; }
        }
        public double Height
        {
            get { return this.height; }
        }
        public Board(int height, int width,List<Hole> holes)
        {
            this.height = height;
            this.width = width;

            this.holes = holes;
        }

        

        


    }
}
