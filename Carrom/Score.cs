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

        public List<int> Scores 
        {
            get { return this.scores; }
            set { this.scores = value; }
        }
        public Score(List<int> scores)
        {
            this.scores = scores;
        }

        public void UpdateScore(int currentPlayerId, CarromPiece piece, Color currentPlayerColor)
        {
            if (piece.Color == Colors.Red)
            {
                this.scores[currentPlayerId] += 5;
            }
            else if (piece.Color == currentPlayerColor)
            {
                this.scores[currentPlayerId]++;
            }
            else
            {
                int opponentId = (currentPlayerId == 0) ? 1 : 0;
                this.scores[opponentId]++;
            }
        }


        public int GetScoreById(int playerId)
        {
            return (playerId-1 >= 0 && playerId-1 < scores.Count) ? scores[playerId-1] : 0;
        }
    }
}
