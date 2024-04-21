using System.Numerics;

namespace Logic
{
    internal class Table
    {
        private float _width;
        private float _height;
        private List<object> _balls;

        public float Width => _width;
        public float Height => _height;
        public List<object> Balls => _balls;

        public Table(float width, float height)
        {
            _width = width;
            _height = height;
            _balls = new List<object>();
        }

        public void AddBall(object ball)
        {
            _balls.Add(ball);
        }
    }
}
