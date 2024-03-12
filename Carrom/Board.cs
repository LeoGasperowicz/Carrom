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
        //Changer les attributs en privé et faire les get et set 
        private double height { get; }
        private double width { get; }
        private List<Player> players { get; set; }
        private List<Point> Holes { get; set; }

        public Board(int height, int width, List<Player> players)
        {
            this.height = height;
            this.width = width;
            this.players = players;
        }

        public Player ChangePlayer(Player currentPlayer)
        {
            int playerIndex = players.FindIndex(player => player.Name == currentPlayer.Name);
            Player newCurrentPlayer = players[(playerIndex+1)%2] ;
            return newCurrentPlayer;
        }
        public void InitializeGame(Player player1, Player player2)
        {

        }
        public void UpdatePawnPosition(Pawn pawn)
        {
            // Mettre à jour la position basée sur le vecteur vitesse
            double newX = pawn.Position.X + pawn.SpeedVector.X;
            double newY = pawn.Position.Y + pawn.SpeedVector.Y;

            // Collision avec le bord gauche ou droit
            if (pawn.Position.X < 0 || pawn.Position.X > this.width)
            {
                pawn.SpeedVector = new Vector(-pawn.SpeedVector.X, pawn.SpeedVector.Y); // Inverse la composante X pour rebondir
                pawn.Position.X = Math.Clamp(pawn.Position.X, 0, this.width); // Assure que le pion reste dans les limites
            }

            // Collision avec le bord supérieur ou inférieur
            if (pawn.Position.Y < 0 || pawn.Position.Y > this.height)
            {
                pawn.SpeedVector = new Vector(pawn.SpeedVector.X, -pawn.SpeedVector.Y);
                pawn.Position.Y = Math.Clamp(pawn.Position.Y, 0, this.height); // Assure que le pion reste dans les limites
            }
        }
    }
}
