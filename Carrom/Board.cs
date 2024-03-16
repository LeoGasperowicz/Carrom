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
        private double height { get; }
        private double width { get; }
        private List<Hole> holes { get; set; }
        public List<Hole> Holes
        {
            get { return this.holes; }
        }
        public Board(int height, int width,List<Hole> holes)
        {
            this.height = height;
            this.width = width;

            this.holes = holes;
        }

        public void UpdatePositions(Striker striker, List<Pawn> allPawns)
        {
            // Update striker position first
            UpdatePawnPosition(striker);

            // Check collision between striker and pawns
            foreach (var pawn in allPawns.Where(p => p.InGame))
            {
                double dx = pawn.Position.X - striker.Position.X;
                double dy = pawn.Position.Y - striker.Position.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance <= (striker.Diameter + pawn.Diameter) / 2)
                {
                    // Simple collision response: transfer striker's speed to the pawn
                    double speedFactor = 0.5; // Adjust based on desired interaction strength
                    Vector collisionVector = new Vector(dx, dy);
                    collisionVector.Normalize();
                    pawn.SpeedVector = new Vector(collisionVector.X * striker.SpeedVector.Length * speedFactor, collisionVector.Y * striker.SpeedVector.Length * speedFactor);
                }
            }

            // Update positions of all pawns
            foreach (var pawn in allPawns.Where(p => p.InGame))
            {
                UpdatePawnPosition(pawn);
            }
        }

        private void UpdatePawnPosition(Pawn pawn)
        {
            double frictionCoefficient = 0.98;
            pawn.SpeedVector = new Vector(pawn.SpeedVector.X * frictionCoefficient, pawn.SpeedVector.Y * frictionCoefficient);

            // New position calculation
            double newX = pawn.Position.X + pawn.SpeedVector.X;
            double newY = pawn.Position.Y + pawn.SpeedVector.Y;

            // Edge collisions
            newX = Math.Clamp(newX, 0, this.width);
            newY = Math.Clamp(newY, 0, this.height);

            // Update speed vector for edge collisions
            if (newX <= 0 || newX >= this.width)
            {
                pawn.SpeedVector = new Vector(-pawn.SpeedVector.X, pawn.SpeedVector.Y);
            }
            if (newY <= 0 || newY >= this.height)
            {
                pawn.SpeedVector = new Vector(pawn.SpeedVector.X, -pawn.SpeedVector.Y);
            }

            // Check hole collisions and mark pawn as not in game if needed
            foreach (Hole hole in this.holes)
            {
                double holeDist = Math.Sqrt(Math.Pow(newX - hole.Center.X, 2) + Math.Pow(newY - hole.Center.Y, 2));
                if (holeDist <= hole.Diameter / 2)
                {
                    pawn.InGame = false;
                    break;
                }
            }

            // Apply the new position if pawn is still in game
            if (pawn.InGame)
            {
                pawn.Position = new Point(newX, newY);
            }
        }


    }
}
