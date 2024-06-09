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

        public override event EventHandler<BallPositionEventArgs> OnBallPositionUpdated;

        private void RaiseBallPositionUpdated(IDataBall ball, Vector2 pos)
        {
            ImmutableVector2 immutablePosition = new ConcreteImmutableVector2(pos.X, pos.Y);
            OnBallPositionUpdated?.Invoke(this, new ConcreteBallPositionEventArgs(_table.GetBallIndex(ball),immutablePosition));
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

            Action<IDataBall, Vector2, Vector2> positionUpdatedCallback = UpdateBall;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    float x = (float)(random.NextDouble() * maxDisplacement) + col * (_table.Width - maxDisplacement) / ((numCols > 1) ? (numCols - 1) : numCols);
                    float y = (float)(random.NextDouble() * maxDisplacement) + row * (_table.Height - maxDisplacement) / ((numRows > 1) ? (numRows - 1) : numRows);

                    Vector2 pos = new Vector2(x, y);
                    Vector2 vel = GetRandomVelocity(random) * _ballSpeed;

                    _table.AddBall(_dataAPI.CreateBall(pos, vel, positionUpdatedCallback));
                }
            }
        }

        private void UpdateBall(IDataBall ball, Vector2 pos, Vector2 vel)
        {
            lock (this)
            {
                CheckWallCollision(ball,pos,vel);
                CheckBallCollision(ball);
                RaiseBallPositionUpdated(ball, pos);
            }
        }

        private void CheckWallCollision(IDataBall ball, Vector2 pos, Vector2 vel)
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            // Check collisions with the walls
            if (pos.X - _ballRadius <= 0 && vel.X < 0 || pos.X + _ballRadius >= _table.Width && vel.X > 0)
            {
                ball.Velocity = new Vector2(-vel.X, vel.Y);
            }

            if (pos.Y - _ballRadius <= 0 && vel.Y < 0 || pos.Y + _ballRadius >= _table.Height && vel.Y > 0)
            {
                ball.Velocity = new Vector2(vel.X, -vel.Y);
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
