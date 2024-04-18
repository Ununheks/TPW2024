using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball
    {
        private Vector2 _pos;
        private Vector2 _velocity;

        public Vector2 Position { get => _pos; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }

        public event EventHandler PositionUpdated;

        public Ball(Vector2 pos, Vector2 velocity, Action callback = null)
        {
            _pos = pos;
            _velocity = velocity;

            // Subscribe the positionUpdatedCallback delegate to the PositionUpdated event
            if (callback != null)
                PositionUpdated += (sender, args) => callback();

            Task.Run(async () =>
            {
                while (true)
                {
                    Update();
                    await Task.Delay(1000);
                }
            });
        }

        private void Update()
        {
            Console.WriteLine($"Ball position: {_pos}");
            _pos += _velocity;
            OnPositionUpdated();
        }

        protected virtual void OnPositionUpdated()
        {
            PositionUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
