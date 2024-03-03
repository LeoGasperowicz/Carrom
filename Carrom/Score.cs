using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrom
{
    public class Score
    {
        public List<int> scores { get; set; }

        public Score(List<int> scores)
        {
            this.scores = scores;
        }

        public void UpdateScore(int playerId, Pawn piece)
        {
            if (piece.color == Color.Red)
            {
                this.scores[playerId] += 5;
            }
            else if (piece.color == Color.Black || piece.color== Color.White) 
            {
                this.scores[playerId]++;
            }
        }
        public int GetScoreById(int playerId)
        {
            if (playerId >= 0 && playerId < scores.Count)
            {
                return scores[playerId];
            }
            else
            {
                // Return 0 if the ID of the player isn't valid
                return 0;
            }
        }

    }
}
