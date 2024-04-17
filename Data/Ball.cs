using System.Numerics;

namespace Data
{
    internal class Ball
    {
        private Vector2 _pos;
        private Vector2 _velocity;

        public Vector2 Position { get => _pos; set => _pos = value; }
        public Vector2 Velocity { get => _velocity; set => _velocity = value; }

        public delegate void PositionUpdatedEventHandler();
        public event PositionUpdatedEventHandler PositionUpdated;

        public Ball(Vector2 pos, Vector2 velocity)
        {
            _pos = pos;
            _velocity = velocity;

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
            PositionUpdated?.Invoke();
        }
    }
}
