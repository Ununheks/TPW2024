using Data;

namespace Logic
{
    internal class Table
    {
        private float _width;
        private float _height;
        private List<IDataBall> _balls;

        public float Width => _width;
        public float Height => _height;
        public IReadOnlyList<IDataBall> Balls => _balls;

        public Table(float width, float height)
        {
            _width = width;
            _height = height;
            _balls = new List<IDataBall>();
        }

        public void AddBall(IDataBall ball)
        {
            if (!_balls.Contains(ball))
            {
                _balls.Add(ball);
            }
            else
            {
                throw new ArgumentException("The ball is already present in the table.");
            }
        }

        public IDataBall GetBall(IDataBall ball)
        {
            int index = _balls.IndexOf(ball);
            if (index != -1)
            {
                return _balls[index];
            }
            else
            {
                throw new ArgumentException("Specified ball object not found in the table.");
            }
        }

        public int GetBallIndex(IDataBall ball)
        {
            return _balls.IndexOf(ball);
        }
    }
}
