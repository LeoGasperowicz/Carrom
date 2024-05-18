using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Carrom
{
    public class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private Striker striker;
        private CarromPiece queen;
        private Player currentPlayer;
        private Score score;
        private List<Pawn> pawnsInHoles;

        public Board Board { get { return this.board; } }
        public Player Player1 { get { return this.player1; } }
        public Player Player2 { get { return this.player2; } }
        public CarromPiece Queen { get { return this.queen; } }
        public Striker Striker { get { return this.striker; } }

        public void InitializeGame(string namePlayer1, string namePlayer2)
        {
            this.player1 = new Player(namePlayer1);
            this.player2 = new Player(namePlayer2);

            List<int> scores = new List<int> { 0, 0 };
            this.score = new Score(scores);
            this.pawnsInHoles = new List<Pawn>();

            double centerX = 380; 
            double centerY = 380; 
            double distance = 20; 

            // Define positions in a hexagonal pattern
            List<Point> positions = new List<Point>
            {
                new Point(centerX, centerY),
                new Point(centerX - distance, centerY - distance),
                new Point(centerX, centerY - distance),
                new Point(centerX + distance, centerY - distance),
                new Point(centerX - distance * 2, centerY),
                new Point(centerX - distance, centerY),
                new Point(centerX + distance, centerY),
                new Point(centerX + distance * 2, centerY),
                new Point(centerX - distance, centerY + distance),
                new Point(centerX, centerY + distance),
                new Point(centerX + distance, centerY + distance),
                new Point(centerX - distance * 2, centerY + distance * 2),
                new Point(centerX - distance, centerY + distance * 2),
                new Point(centerX, centerY + distance * 2),
                new Point(centerX + distance, centerY + distance * 2),
                new Point(centerX + distance * 2, centerY + distance * 2)
            };

            // Add the queen
            this.queen = new CarromPiece(20, Colors.Red, positions[0],0);

            // Add player1's pieces
            for (int i = 1; i <= 8; i++)
            {
                Point point = positions[i];
                this.player1.Pieces.Add(new CarromPiece(20, Colors.White, positions[i], i));
            }

            // Add player2's pieces
            for (int i = 9; i < positions.Count; i++)
            {
                this.player2.Pieces.Add(new CarromPiece(20, Colors.Black, positions[i], i));
            }

            this.striker = new Striker(22, Colors.Blue, new Point(380, 595));

            List<Hole> holes = new List<Hole>
            {
                new Hole(new Point(63, 63), 42),
                new Hole(new Point(63, 685), 42),
                new Hole(new Point(685, 63), 42),
                new Hole(new Point(685, 685), 42)
            };

            this.board = new Board(820, 820, holes);
            this.currentPlayer = (new Random().Next(2) == 0) ? this.player1 : this.player2;
        }


        public void PlayTurn(Pawn selectedPawn, Hole selectedHole)
        {
            double probability = CalculateProbability(selectedPawn, selectedHole);
            bool success = DetermineSuccess(probability);

            if (success)
            {
                // Move the pawn to the hole and update score
                selectedPawn.InGame = false;
                score.UpdateScore(currentPlayer.Id, selectedPawn, currentPlayer.Pieces.First().Color);
            }
            else
            {
                // Let user reposition the pawn
                selectedPawn.Position = GetNewPawnPositionFromUser();
            }

            // Switch player turn
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        private double CalculateProbability(Pawn pawn, Hole hole)
        {
            double distanceToHole = Point.Subtract(pawn.Position, hole.Center).Length;
            double distanceToStriker = Point.Subtract(pawn.Position, striker.Position).Length;
            // Simplified probability calculation based on distances
            return Math.Max(0.1, 1 - (distanceToHole / (board.Width / 2)) * (distanceToStriker / (board.Width / 2)));
        }

        private bool DetermineSuccess(double probability)
        {
            Random random = new Random();
            return random.NextDouble() < probability;
        }

        private Point GetNewPawnPositionFromUser()
        {
            // Placeholder for user input
            return new Point(200, 200);
        }
    }
}
