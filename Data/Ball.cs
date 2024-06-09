using System.Diagnostics;
using System.Numerics;

namespace Data
{
    internal class Ball : IDataBall
    {
        private Vector2 _pos;
        private Vector2 _vel;
        private readonly object _lock = new object();
        private Action<IDataBall, Vector2, Vector2> _positionUpdatedCallback;
        private Logger _logger;

        public Vector2 Position
        {
            get
            {
                lock (_lock)
                {
                    return _pos;
                }
            }
        }

        public Vector2 Velocity
        {
            get
            {
                lock (_lock)
                {
                    return _vel;
                }
            }
            set
            {
                lock (_lock)
                {
                    _vel = value;
                }
            }
        }

        public Ball(Vector2 pos, Vector2 velocity, Action<IDataBall, Vector2, Vector2> positionUpdatedCallback = null)
        {
            _pos = pos;
            _vel = velocity;
            _positionUpdatedCallback = positionUpdatedCallback;
            _logger = Logger.GetInstance();

            Task.Run(UpdateAsync);
        }

        private async Task UpdateAsync()
        {
            float timeStep = 1f / 60f;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                TimeSpan previousTime = stopwatch.Elapsed;
                await Task.Delay(TimeSpan.FromSeconds(timeStep));

                lock (_lock)
                {
                    _pos += _vel * (float)(stopwatch.Elapsed - previousTime).TotalSeconds;
                }

                _logger.CreateLog(new BallLogEntry(_pos, _vel));

                _positionUpdatedCallback?.Invoke(this, _pos, _vel);
            }
        }
    }
}
