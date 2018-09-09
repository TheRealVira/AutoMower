namespace Automower
{
    public class GrassArea:Boundary
    {
        public static int DefaultSizeInCm = 10;

        public int GrassGrowth { get; private set; }
        
        public GrassArea(Position position) : base(position, new Field(DefaultSizeInCm, DefaultSizeInCm))
        {
            GrassGrowth = 0;
        }

        public void DrivingOver()
        {
            GrassGrowth++;
        }

        public override string ToString()
        {
            return GrassGrowth.ToString();
        }
    }
}