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

        public void InitializeGame(string namePlayer1, string namePlayer2, string databaseChoice)
        {
            this.player1 = new Player(namePlayer1);
            this.player2 = new Player(namePlayer2);
            //List<Player> players = new List<Player>();
            //players.AddRange(new List<Player> { this.player1, this.player2 });
            // Change the Grid
            //ConfigGrid.Visibility = Visibility.Collapsed;
            //GameGrid.Visibility = Visibility.Visible;
            List<int> scores = new List<int> { 0, 0 };
            this.score = new Score(scores);
            //PrintScore(player1, player2, this.score);


            // For the moment I define them without paying attention to their place
            for (int i = 0; i < 9; i++)
            {
                this.player1.Pieces.Add(new CarromPiece(20, Colors.White, new Point(200, 200), new Vector(0, 0)));
                this.player2.Pieces.Add(new CarromPiece(20, Colors.Black, new Point(200, 200), new Vector(0, 0)));
            }

            this.queen = new CarromPiece(20, Colors.Red, new Point(200, 200), new Vector(0, 0));

            this.striker = new Striker(30, Colors.Beige, new Point(200, 200), new Vector(0, 0));

            List<Hole> holes = new List<Hole>();

            holes.Add(new Hole(new Point(11, 11), 20));
            holes.Add(new Hole(new Point(11, 409), 20));
            holes.Add(new Hole(new Point(409, 11), 20));
            holes.Add(new Hole(new Point(409, 409), 20));

            this.board = new Board(420, 420, holes);
            Random rnd = new Random();
            int playerTurn = rnd.Next(2);
            if(playerTurn == 0)
            {
                this.currentPlayer = this.player1;
            }
            else
            {
                this.currentPlayer = this.player2;
            }
            
        }

        public void Play()
        {
            while (!IsGameOver())
            {
                Console.WriteLine($"{this.currentPlayer.Name}'s turn.");

                // Simulate the striker hit
                Vector direction = GetStrikerDirectionFromUser();
                this.striker.SpeedVector = direction;

                List<Pawn> allPawns = new List<Pawn>();
                allPawns.Add(this.queen);
                allPawns.AddRange(this.player1.Pieces);
                allPawns.AddRange(this.player2.Pieces);

                board.UpdatePositions(this.striker, allPawns); 

                // to method return a list of all the pawns whiwh are in a hole during this turn
                var pawnsInHoles = CheckPawnsInHoles(allPawns);
                bool scoredThisTurn = false;
                // Get the color of the pawn of the current player
                Color color = Colors.White;
                foreach ( Pawn pawn in this.currentPlayer.Pieces)
                {
                     color = pawn.Color;
                }
                // Update the scores, verify the current player scores and see which are the pawns in the holes
                foreach (CarromPiece pawn in pawnsInHoles)
                {
                    Player owner = null;
                    if (pawn.Color == color) 
                    {
                        owner = this.currentPlayer;
                    }
                    else 
                    {
                        if (this.currentPlayer == this.player1)
                        {
                            owner = this.player2;
                        }
                        else { owner = this.player1; }
                        
                    }
                    if (pawn.Color == color || pawn.Color == Colors.Red) 
                    {
                        scoredThisTurn = true;
                    }

                    score.UpdateScore(currentPlayer.Id, pawn,color);
                    // Remove the pawn from its owner's list if not null and the pawn isn't the queen
                    if (owner != null && pawn.Color != Colors.Red)
                    {
                        owner.Pieces.Remove(pawn);
                    }
                    // Mark the pawn as not in game
                    pawn.InGame = false;
                }


                score.ValidateQueenScore();

                if (!scoredThisTurn)
                {
                    // Change the player only if he doesn't scoreds with his pawn or with the queen
                    currentPlayer = (currentPlayer == player1) ? player2 : player1;
                }
            }
        }

        private bool IsGameOver()
        {
            bool gameOver = false;
            if((this.player1.Pieces.Count==0) || (this.player2.Pieces.Count == 0) || (this.score.Scores[0] >=20) || (this.score.Scores[1] >= 20))
            {
                gameOver = true;
            }
                return gameOver;
        }
        public List<Pawn> CheckPawnsInHoles(List<Pawn> pawns)
        {
            foreach (Pawn pawn in pawns)
            {
                foreach (Hole hole in board.Holes) 
                {
                    if (IsPawnInHole(pawn, hole))
                    {
                        this.pawnsInHoles.Add(pawn);
                        break; 
                    }
                }
            }

            return this.pawnsInHoles;
        }

        private bool IsPawnInHole(Pawn pawn, Hole hole)
        {
            double distance = Point.Subtract(pawn.Position, hole.Center).Length;
            return distance <= (hole.Diameter / 2); 
        }

        private Vector GetStrikerDirectionFromUser()
        {
            // Get input from user to set direction and speed of striker
            return new Vector(1, 1); // Placeholder for user input
        }
    }
}
