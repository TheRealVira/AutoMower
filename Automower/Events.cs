using System;

namespace Automower
{
    public class Events
    {
        public delegate void FinishedEventHandler(FinishedEventArgs args);
    }

    public class FinishedEventArgs:EventArgs
    {
        public FinishedEventArgs(GrassArea[,] area1)
        {
            Area = area1;
        }

        public GrassArea[,] Area { get; }
    }
}