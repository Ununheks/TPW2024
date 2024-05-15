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

        private Action<object,Vector2, Vector2> _positionUpdatedCallback;

        public Ball(Vector2 pos, Vector2 velocity, Action<object,Vector2, Vector2> positionUpdatedCallback = null)
        {
            _pos = pos;
            _velocity = velocity;
            _positionUpdatedCallback = positionUpdatedCallback;

            _moveTimer = new Timer(Update, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
        }

        private void Update(object state)
        {
            _pos += _velocity;
            _positionUpdatedCallback?.Invoke(this, _pos, _velocity);
        }
    }
}
