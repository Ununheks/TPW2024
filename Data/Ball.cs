using System;
using System.Numerics;
using System.Threading;

namespace Data
{
    internal class Ball
    {
        private Vector2 _pos;
        private Vector2 _velocity;
        private Timer _moveTimer;

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

            _moveTimer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
        }

        private void Update(object state)
        {
            _pos += _velocity;
            OnPositionUpdated();
        }

        protected virtual void OnPositionUpdated()
        {
            PositionUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
