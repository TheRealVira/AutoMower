namespace Automower
{
    public class Field
    {   
        public Field(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}