using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrom
{
    public class Player
    {
        public int id { get; }
        public string name { get; set; }
        public List<CarromPiece> pieces { get; set; }

        public Player(int id, string name, List<CarromPiece> pieces)
        {
            this.id = id;
            this.name = name;
            this.pieces = pieces;
        }

        public void UpdateListPawn(CarromPiece piece)
        {
        }

    }
}
