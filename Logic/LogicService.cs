using Data;
using System.Numerics;

namespace Logic
{
    internal class LogicService : LogicAPI
    {
        private DataAPI? _dataAPI;
        private Table _table;
        private float _ballRadius;
        private float _ballMass = 1.0f;
        private float _ballSpeed = 50f;

        public override event EventHandler<ImmutableVector2> OnBallPositionUpdated;

        private void RaiseBallPositionUpdated(Vector2 position)
        {
            ImmutableVector2 immutablePosition = new ImmutableVector2(position.X, position.Y);
            OnBallPositionUpdated?.Invoke(this, immutablePosition);
        }


        public LogicService(DataAPI? dataAPI = null)
        {
            _dataAPI = dataAPI ?? DataAPI.CreateDataService();
        }

        public override void Start(int ballCount, float ballRadius, float tableWidth, float tableHeight)
        {
            _ballRadius = ballRadius;
            CreateTable(tableWidth, tableHeight);
            SpawnBalls(ballCount, ballRadius);
        }

        public override object GetTableInfo()
        {
            return _table;
        }

        private void CreateTable(float width, float height)
        {
            _table = new Table(width, height);
        }

        private void SpawnBalls(int amount, float radius)
        {
            Random random = new Random();

            int numRows = (int)Math.Ceiling(Math.Sqrt(amount));
            int numCols = (int)Math.Ceiling((float)amount / numRows);

            float maxDisplacement = radius * 2;

            Action<IDataBall> positionUpdatedCallback = UpdateBall;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    float x = (float)(random.NextDouble() * maxDisplacement) + col * (_table.Width - maxDisplacement) / (numCols - 1);
                    float y = (float)(random.NextDouble() * maxDisplacement) + row * (_table.Height - maxDisplacement) / (numRows - 1);

                    Vector2 pos = new Vector2(x, y);
                    Vector2 vel = GetRandomVelocity(random) * _ballSpeed;

                    _table.AddBall(_dataAPI.CreateBall(pos, vel, positionUpdatedCallback));
                }
            }
        }

        private void UpdateBall(IDataBall ball)
        {
            lock (this)
            {
                CheckWallCollision(ball);
                CheckBallCollision(ball);
                RaiseBallPositionUpdated(ball.Position);
            }
        }

        private void CheckWallCollision(IDataBall ball)
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            Vector2 position = ball.Position;
            Vector2 velocity = ball.Velocity;

            // Check collisions with the walls
            if (position.X - _ballRadius <= 0 && velocity.X < 0 || position.X + _ballRadius >= _table.Width && velocity.X > 0)
            {
                ball.Velocity = new Vector2(-velocity.X, velocity.Y);
            }

            if (position.Y - _ballRadius <= 0 && velocity.Y < 0 || position.Y + _ballRadius >= _table.Height && velocity.Y > 0)
            {
                ball.Velocity = new Vector2(velocity.X, -velocity.Y);
            }
        }


        private void CheckBallCollision(IDataBall ball)
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            foreach (var otherBall in _table.Balls)
            {
                if (otherBall != ball)
                {
                    var otherPosition = otherBall.Position;
                    var distance = Vector2.Distance(ball.Position, otherPosition);
                    var totalRadius = 2 * _ballRadius;

                    if (distance <= totalRadius)
                    {
                        ResolveBallCollision(ball, otherBall);
                    }
                }
            }
        }

        private void ResolveBallCollision(IDataBall ball1, IDataBall ball2)
        {
            var pos1 = ball1.Position;
            var vel1 = ball1.Velocity;
            var pos2 = ball2.Position;
            var vel2 = ball2.Velocity;

            var direction = Vector2.Normalize(pos2 - pos1);

            var relativeVelocity = vel2 - vel1;

            var relativeSpeed = Vector2.Dot(relativeVelocity, direction);

            // Check if the balls are moving towards each other
            if (relativeSpeed < 0)
            {
                var velocityChange = relativeSpeed * direction * (_ballMass / (_ballMass + _ballMass));

                var newVel1 = vel1 - velocityChange;
                var newVel2 = vel2 + velocityChange;

                var finalVel1 = Vector2.Normalize(newVel1) * _ballSpeed;
                var finalVel2 = Vector2.Normalize(newVel2) * _ballSpeed;

                ball1.Velocity = finalVel2;
                ball2.Velocity = finalVel1;
            }
        }


        private Vector2 GetRandomVelocity(Random random)
        {
            float angle = (float)(random.NextDouble() * 2 * Math.PI);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
