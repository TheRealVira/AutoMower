using System.Runtime;

namespace Automower
{
    public class Boundary:Field
    {
        public Position Position { get; set;  }
        
        public Boundary(Position position, Field field) : base(field.Width,field.Height)
        {
            Position = position;
        }

        public bool Intersects(Boundary b)
        {
            return b.Contains(Position) ||
                   b.Contains(new Position(Position.X + Width, Position.Y)) ||
                   b.Contains(new Position(Position.X + Width, Position.Y + Height)) ||
                   b.Contains(new Position(Position.X, Position.Y + Height));
        }

        public bool Contains(Position pos)
        {
            return pos.X >= Position.X && pos.X <= Position.X + Width &&
                   pos.Y >= Position.Y && pos.Y <= Position.Y + Height;
        }

        public bool Contains(Boundary b)
        {
            return Contains(b.Position) &&
                   Contains(new Position(b.Position.X + b.Width, b.Position.Y)) &&
                   Contains(new Position(b.Position.X + b.Width, b.Position.Y + b.Height)) &&
                   Contains(new Position(b.Position.X, b.Position.Y + b.Height));
        }

        public Position ClipInside(Boundary b)
        {
            if(b.Contains(this))return null;
            
            var toRet = new Position(0,0);

            if (b.Position.X > Position.X)
            {
                Position.X = b.Position.X;
                toRet.X--;
            }else if (b.Position.X + b.Width < Position.X + Width)
            {
                Position.X = b.Width - b.Position.X - 1 - Height;
                toRet.X++;
            }else if (b.Position.Y > Position.Y)
            {
                Position.Y = b.Position.Y;
                toRet.Y--;
            }else if (b.Position.Y + b.Height < Position.Y + Height)
            {
                toRet.Y++;
                Position.Y = b.Height - b.Position.Y - 1 - Width;
            }
            
            return toRet;
        }
    }
}