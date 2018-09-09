using System;
using System.Runtime.InteropServices;

namespace Automower
{
    public class Mower
    {
        public double Speed { get; }

        public double Rotation { get; set; }

        public Boundary Boundary { get; }

        public Mower(Boundary boundary, double rotation, double speed)
        {
            Speed = speed;
            Boundary = boundary;
            Rotation = rotation;
        }

        public void Update()
        {
            Boundary.Position.X += Speed * Math.Sin(Rotation);
            Boundary.Position.Y += Speed * Math.Cos(Rotation);
        }

        public void SetRandomOtherDirection(Random rand, Position direction)
        {
            if(direction==null)return;
            
            if (direction.X < 0 && direction.Y == 0)
            {
                Rotation = DegreeToRadian(rand.Next(0,181));
                return;
            }
            
            if (direction.X == 0 && direction.Y < 0)
            {
                Rotation = rand.Next(2) == 0 ? DegreeToRadian(-rand.Next(91)) : DegreeToRadian(rand.Next(91));
                return;
            }

            if (direction.X > 0 && direction.Y == 0)
            {
                Rotation = DegreeToRadian(-rand.Next(0,181));
                return;
            }
            
            if (direction.X == 0 && direction.Y > 0)
            {
                Rotation = DegreeToRadian(90 + rand.Next(0, 181));
                return;
            }
        }
        
        private static double DegreeToRadian(float angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}