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
        public double height { get; }
        public double width { get; }
        public List<Player> players { get; set; }
        public List<Point> Holes { get; set; }

        public Board(int height, int width, List<Player> players)
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
        public void UpdatePawnPosition(Pawn pawn)
        {
            // Mettre à jour la position basée sur le vecteur vitesse
            double newX = pawn.position.X + pawn.speedVector.X;
            double newY = pawn.position.Y + pawn.speedVector.Y;

            // Collision avec le bord gauche ou droit
            if (pawn.position.X < 0 || pawn.position.X > this.width)
            {
                pawn.speedVector = new Vector(-pawn.speedVector.X, pawn.speedVector.Y); // Inverse la composante X pour rebondir
                pawn.position.X = Math.Clamp(pawn.position.X, 0, this.width); // Assure que le pion reste dans les limites
            }

            // Collision avec le bord supérieur ou inférieur
            if (pawn.position.Y < 0 || pawn.position.Y > this.height)
            {
                pawn.speedVector = new Vector(pawn.speedVector.X, -pawn.speedVector.Y);
                pawn.position.Y = Math.Clamp(pawn.position.Y, 0, this.height); // Assure que le pion reste dans les limites
            }
        }
    }
}
