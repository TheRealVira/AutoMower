using System;
using System.Collections.Generic;

namespace Automower
{
    internal class Garden
    {
        public Garden(Field size)
        {
            LeftToMowe = new List<GrassArea>();
            GrassInTotal = new List<GrassArea>();
            CurrentlyUnderMower = new List<GrassArea>();
            
            for (var x = 0; x < size.Width; x++)
            {
                for (var y = 0; y < size.Height; y++)
                {
                    GrassInTotal.Add(new GrassArea(new Position(x*GrassArea.DefaultSizeInCm,y*GrassArea.DefaultSizeInCm)));
                    LeftToMowe.Add(GrassInTotal[GrassInTotal.Count-1]);
                }
            }

            MyField = new Boundary(new Position(0, 0),
                new Field(size.Width * GrassArea.DefaultSizeInCm, size.Height * GrassArea.DefaultSizeInCm));
        }
        
        public Boundary MyField { get; }
        public List<GrassArea> LeftToMowe { get; }
        public List<GrassArea> GrassInTotal { get; }
        private List<GrassArea> CurrentlyUnderMower;

        public void Update(Mower mower, Random rand)
        {
            foreach (var grassArea in GrassInTotal)
            {

                if (!mower.Boundary.Intersects(grassArea))
                {
                    if (CurrentlyUnderMower.Contains(grassArea))
                    {
                        CurrentlyUnderMower.Remove(grassArea);
                    }
                    
                    continue;
                }
                
                if(CurrentlyUnderMower.Contains(grassArea))continue;
                grassArea.DrivingOver();
                LeftToMowe.Remove(grassArea);
                CurrentlyUnderMower.Add(grassArea);
            }

            mower.Update();
            if (MyField.Contains(mower.Boundary)) return;
                
            mower.SetRandomOtherDirection(rand,mower.Boundary.ClipInside(MyField));
        }

        public static GrassArea[,] ToArray(Garden source)
        {
            var toRet = new GrassArea[source.MyField.Width / GrassArea.DefaultSizeInCm,
                source.MyField.Height / GrassArea.DefaultSizeInCm];

            foreach (var grassArea in source.GrassInTotal)
            {
                toRet[(int) grassArea.Position.X / GrassArea.DefaultSizeInCm,
                    (int) grassArea.Position.Y / GrassArea.DefaultSizeInCm] = grassArea;
            }

            return toRet;
        }
    }
}