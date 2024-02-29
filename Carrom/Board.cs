using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrom
{
    public class Board
    {
        public double height { get; }
        public double width { get; }
        public List<Player> players { get; set; }

        public Board(double height, double width, List<Player> players)
        {
            this.height = height;
            this.width = width;
            this.players = players;
        }

        public Player ChangePlayer(Player currentPlayer)
        {
            int playerIndex = players.FindIndex(player => player.name == currentPlayer.name);
            Player newCurrentPlayer = players[(playerIndex+1)%2] ;
            return newCurrentPlayer;
        }
        public void InitializeGame(Player player1, Player player2)
        {

        }
    }
}
