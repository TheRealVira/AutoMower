using System;
using System.Threading;
using System.Threading.Tasks;

namespace Automower
{
    public class Simulation
    {
        public static Random Rand;
        private static object Locker = new object();
        private bool _initialized;
        
        private readonly object _pauseOpject = new object();
        private bool _paused = true;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public event Events.FinishedEventHandler OnFinished;

        private Garden myGarden;
        private Mower myMower;

        /// <summary>
        /// Initializes the current simulation.
        /// </summary>
        /// <param name="field">Field (defined in count of grass areas).</param>
        /// <param name="mower"></param>
        /// <param name="rand"></param>
        public void Initilize(Field field, Mower mower, Random rand)
        {
            lock (Locker)
            {
                if (Rand!=null)return;

                Rand = rand;
            }
            
            if(_initialized)return;
            
            myGarden = new Garden(field);
            myMower = mower;

            _initialized = true;
        }

        public void Start()
        {
            _paused = false;
            Task.Factory.StartNew(() =>
            {
                while (myGarden.LeftToMowe.Count > 0)
                {
                    Update();
                    Console.WriteLine("X: "+ myMower.Boundary.Position.X + "\t Y: "+myMower.Boundary.Position.Y);
                }
            
                OnFinished?.Invoke(new FinishedEventArgs(Garden.ToArray(myGarden)));
            }, _cancellationTokenSource.Token);
        }

        private void Update()
        {
            myGarden.Update(myMower, Rand);
        }

        public void Pause()
        {
            if (_paused) return;
            
            Monitor.Exit(_pauseOpject);
            _paused = true;
        }

        public void Resume()
        {
            if (_paused) return;
            
            Monitor.Exit(_pauseOpject);
            _paused = true;
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}