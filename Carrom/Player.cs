using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrom
{
    public class Player
    {
        //Changer les attributs en privé et faire les get et set 
        private int id;
        public int Id
        {
            get { return this.id;}
            set { this.id = value; }
        }
        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        private List<CarromPiece> pieces { get; set; }


        public Player(string name)
        {
            this.name = name;
        }
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
