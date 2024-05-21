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
        private int probabilityMode = 1;

        public Board Board { get { return this.board; } }
        public Player Player1 
        { 
            get { return this.player1; }
            set { this.player1 = value; }
        }
        public Player Player2 
        { 
            get { return this.player2; }
            set { this.player2 = value; }
        }
        public CarromPiece Queen { get { return this.queen; } }
        public Striker Striker { get { return this.striker; } }
        public Score Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        public void InitializeGame(string namePlayer1, string namePlayer2)
        {
            this.player1 = new Player(0,namePlayer1);
            this.player2 = new Player(1,namePlayer2);

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
                new Hole(new Point(63, 63), 42,1),
                new Hole(new Point(63, 685), 42,2),
                new Hole(new Point(685, 63), 42,3),
                new Hole(new Point(685, 685), 42,4)
            };

            this.board = new Board(730, 730, holes);
            this.currentPlayer = (new Random().Next(2) == 0) ? this.player1 : this.player2;
            if (this.currentPlayer == this.player2)
            {
                this.striker.Position = new Point(380, 155);
            }

        }
        public void SetProbabilityMode(int mode)
        {
            this.probabilityMode = mode;
        }

        public void PlayTurn(CarromPiece selectedPawn, Hole selectedHole)
        {
            double probability = CalculateProbability(selectedPawn, selectedHole);
            bool success = DetermineSuccess(probability);

            if (success)
            {
                // Move the pawn to the hole and update score
                selectedPawn.InGame = false;
                score.UpdateScore(currentPlayer.Id, selectedPawn, currentPlayer.Pieces.First().Color);
                MessageBox.Show("Well Done! The pawn is in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Let user reposition the pawn
                selectedPawn.Position = GetNewPawnPositionFromUser(selectedPawn);
                MessageBox.Show("Too bad, maybe next time.", "Failure", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Check for pawns in holes
            //CheckPawnsInHoles();

        }
        private void MoveStrikerToOppositePosition()
        {
            if (this.currentPlayer == this.player2)
            {
                striker.Position = new Point(380, 155);
            }
            else
            {
                striker.Position = new Point(380, 595);
            }
            
        }
        private double CalculateProbability(CarromPiece pawn, Hole hole)
        {
            if (probabilityMode == 1)
            {
                // Probability to score of 1
                return 1.0;
            }
            else
            {
                // More realistic probability calculation
                double distanceToHole = Math.Sqrt(Math.Pow(pawn.Position.X - hole.Center.X, 2) + Math.Pow(pawn.Position.Y - hole.Center.Y, 2));
                double distanceToStriker = Math.Sqrt(Math.Pow(pawn.Position.X - striker.Position.X, 2) + Math.Pow(pawn.Position.Y - striker.Position.Y, 2));

                // Normalize distances to be within the range [0, 1]
                double normalizedDistanceToHole = distanceToHole / (board.Width / 2);
                double normalizedDistanceToStriker = distanceToStriker / (board.Width / 2);

                // Combine the distances in a way that makes sense for the game
                double combinedDistanceFactor = (normalizedDistanceToHole + normalizedDistanceToStriker) / 2;

                // Adjust probability based on combined distance factor
                double baseProbability = 1 - Math.Pow(combinedDistanceFactor, 1.3); 

                if (pawn == queen)
                {
                    baseProbability /= 2; 
                }

                return baseProbability; 
            }
        }
        public bool IsGameOver()
        {
            return player1.Pieces.All(p => !p.InGame) || player2.Pieces.All(p => !p.InGame);
        }
        private bool DetermineSuccess(double probability)
        {
            if (probability < 0)
            {
                probability = -probability;
            }
            Random random = new Random();
            return random.NextDouble() < probability;
        }

        private Point GetNewPawnPositionFromUser(Pawn pawn)
        {
            Random rand = new Random();
            double radius = 75;

            // Generate a random angle (in radians)
            double angle = rand.NextDouble() * 2 * Math.PI;

            // Generate a random distance within the radius
            double distance = rand.NextDouble() * radius;

            // Calculate the new position
            double newX = pawn.Position.X + distance * Math.Cos(angle);
            double newY = pawn.Position.Y + distance * Math.Sin(angle);

            // Ensure the new position is within the board boundaries
            newX = Math.Clamp(newX, 0, Board.Width);
            newY = Math.Clamp(newY, 0, Board.Height);

            return new Point(newX, newY);
        }

        private void CheckPawnsInHoles()
        {
            foreach (var pawn in player1.Pieces.Concat(player2.Pieces).Concat(new[] { queen }).Where(p => p.InGame))
            {
                foreach (var hole in board.Holes)
                {
                    double distanceToHole = Point.Subtract(pawn.Position, hole.Center).Length;
                    if (distanceToHole <= hole.Diameter / 2)
                    {
                        // Pawn is in the hole
                        pawn.InGame = false;
                        // Update the score
                        score.UpdateScore(this.currentPlayer.Id, pawn, this.currentPlayer.Pieces.First().Color);
                        break;
                    }
                }
            }
        }
        // Method to switch player turn and move the striker to the opposite position
        public void SwitchPlayerTurn()
        {
            // Switch player turn
            if(this.currentPlayer == this.Player1)
            {
                this.currentPlayer=this.Player2;
            }
            else
            {
                this.currentPlayer = this.Player1;
            }
            // Move striker to the opposite position
            MoveStrikerToOppositePosition();
        }
    }
}
