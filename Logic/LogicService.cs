using Data;
using System.Numerics;

namespace Logic
{
    internal class LogicService : LogicAPI
    {
        private DataAPI? _dataAPI;
        private Table _table;
        private float _ballRadius;

        public LogicService(DataAPI? dataAPI = null)
        {
            _dataAPI = dataAPI ?? DataAPI.CreateDataService();
        }

        public override void CreateTable(float width, float height)
        {
            _table = new Table(width, height);
        }

        public override Vector2 GetTableDimensions()
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return Vector2.Zero;
            }

            return new Vector2(_table.Width, _table.Height);
        }

        public override void SpawnBalls(int amount, float radius)
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            Random random = new Random();

            int numRows = (int)Math.Ceiling(Math.Sqrt(amount));
            int numCols = (int)Math.Ceiling((float)amount / numRows);

            float maxDisplacement = radius * 2;

            Action checkCollisionsAction = CheckCollisions;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    float x = (float)(random.NextDouble() * maxDisplacement) + col * (_table.Width - maxDisplacement) / (numCols - 1);
                    float y = (float)(random.NextDouble() * maxDisplacement) + row * (_table.Height - maxDisplacement) / (numRows - 1);

                    _table.AddBall(_dataAPI.CreateBall(new Vector2(x, y), GetRandomVelocity(random), checkCollisionsAction));
                }
            }
        }

        public override List<object> GetBallData()
        {
            if (_table == null)
            {
                throw new InvalidOperationException("Table is not created yet.");
            }

            return _table.Balls;
        }

        public override void CheckCollisions()
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            foreach (var ball in _table.Balls)
            {
                Vector2 ballPosition = _dataAPI.GetBallPosition(ball);
                Vector2 ballVelocity = _dataAPI.GetBallVelocity(ball);
                if (ballPosition.X - _ballRadius <= 0 || ballPosition.X + _ballRadius >= _table.Width)
                {
                    _dataAPI.SetBallVelocity(ball,new Vector2(-ballVelocity.X, ballVelocity.Y));
                }
                if (ballPosition.Y - _ballRadius <= 0 || ballPosition.Y + _ballRadius >= _table.Height)
                {
                    _dataAPI.SetBallVelocity(ball, new Vector2(ballVelocity.X, -ballVelocity.Y));
                }
            }
        }

        private Vector2 GetRandomVelocity(Random random)
        {
            float angle = (float)(random.NextDouble() * 2 * Math.PI);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
