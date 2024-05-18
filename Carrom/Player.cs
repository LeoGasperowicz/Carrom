using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrom
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CarromPiece> Pieces { get; set; }

        public int Score { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.Pieces = new List<CarromPiece>(); // Ensure Pieces list is initialized
        }

        public Player(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Pieces = new List<CarromPiece>(); // Ensure Pieces list is initialized
        }
    }
}
