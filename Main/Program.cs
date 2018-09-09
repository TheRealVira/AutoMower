using System;
using Automower;

namespace Main
{
    internal class Program
    {
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);
        
        public static void Main(string[] args)
        {
            var sim1 = new Simulation();
            sim1.Initilize(
                new Field(20,10), 
                new Mower(
                    new Boundary(new Position(1,1),
                        new Field(GrassArea.DefaultSizeInCm,
                        GrassArea.DefaultSizeInCm)),
                    0,
                    2.5),
                Rand);
            sim1.Start();
            
            SimulationOnOnFinished(new FinishedEventArgs(sim1.));
        }

        private static void SimulationOnOnFinished(FinishedEventArgs args)
        {
            for (var i = 0; i < args.Area.GetLength(0); i++)
            {
                for (var j = 0; j < args.Area.GetLength(1); j++)
                {
                    Console.Write(args.Area[i,j].ToString());
                }
                
                Console.WriteLine();
            }
        }
    }
}