using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;


namespace Carrom
{
    public class Score
    {
        private List<int> scores;
        public List<int> Scores => scores;

        private int queenScorer = -1;

        private bool queenScoreNeedsValidation = false;

        public Score(List<int> scores)
        {
            this.scores = scores;
        }

        public void UpdateScore(int currentPlayerId, Pawn piece, Color currentPlayerColor)
        {
            if (piece.Color == Colors.Red)
            {
                queenScorer = currentPlayerId;
                queenScoreNeedsValidation = true; 
            }
            else if (piece.Color == currentPlayerColor) 
            {
                // Condition to verify if another pawn has been score just after the queen
                if (queenScorer == currentPlayerId && queenScoreNeedsValidation)
                {
                    this.scores[currentPlayerId] += 5; 
                    queenScoreNeedsValidation = false; 
                }
                this.scores[currentPlayerId]++;
            }
            else 
            {
                int opponentId = 1 - currentPlayerId; 
                this.scores[opponentId]++;
            }
        }

        // Method to reset the point if not any pawn has been score after score with the queen
        public void ValidateQueenScore()
        {
            if (queenScoreNeedsValidation)
            {
                queenScoreNeedsValidation = false;
                queenScorer = -1; 
            }
        }

        public int GetScoreById(int playerId)
        {
            return (playerId >= 0 && playerId < scores.Count) ? scores[playerId] : 0;
        }
    }
}
