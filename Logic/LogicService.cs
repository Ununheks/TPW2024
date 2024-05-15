using Data;
using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Threading;

namespace Logic
{
    internal class LogicService : LogicAPI
    {
        private DataAPI? _dataAPI;
        private Table _table;
        private float _ballRadius;
        private Timer _updateTimer;

        public override event EventHandler<List<Vector2>> OnBallsPositionsUpdated;

        private void RaiseBallPositionsUpdated(List<Vector2> positions)
        {
            OnBallsPositionsUpdated?.Invoke(this, positions);
        }

        public LogicService(DataAPI? dataAPI = null)
        {
            _dataAPI = dataAPI ?? DataAPI.CreateDataService();
        }

        public override void Start(int ballCount, float ballRadius, float tableWidth, float tableHeight)
        {
            CreateTable(tableWidth, tableHeight);
            SpawnBalls(ballCount, ballRadius);
            _updateTimer = new Timer(UpdateBallPositions, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
        }

        private void UpdateBallPositions(object state)
        {
            List<Vector2> positions = new List<Vector2>();

            // Create a snapshot of the balls list
            List<(object Ball, Vector2 Position, Vector2 Velocity)> ballsSnapshot = new List<(object Ball, Vector2 Position, Vector2 Velocity)>(_table.Balls);

            // Iterate through the snapshot of the balls and get their positions
            foreach ((object Ball, Vector2 Position, Vector2 Velocity) ball in ballsSnapshot)
            {
                positions.Add(ball.Position);
            }

            // Raise the BallPositionsUpdated event
            RaiseBallPositionsUpdated(positions);
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

            Action<object, Vector2, Vector2> positionUpdatedCallback = UpdateBall;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    float x = (float)(random.NextDouble() * maxDisplacement) + col * (_table.Width - maxDisplacement) / (numCols - 1);
                    float y = (float)(random.NextDouble() * maxDisplacement) + row * (_table.Height - maxDisplacement) / (numRows - 1);

                    Vector2 pos = new Vector2(x, y);
                    Vector2 vel = GetRandomVelocity(random) * 5;

                    _table.AddBall(_dataAPI.CreateBall(pos, vel, positionUpdatedCallback), pos, vel);
                }
            }
        }

        private void UpdateBall(object ball, Vector2 pos, Vector2 vel)
        {
            _table.UpdateBall(ball, pos, vel);
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            if (_table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            // Create a snapshot of the balls list
            List<(object Ball, Vector2 Position, Vector2 Velocity)> ballsSnapshot = new List<(object Ball, Vector2 Position, Vector2 Velocity)>(_table.Balls);

            // Iterate through the snapshot of the balls and check collisions
            foreach ((object Ball, Vector2 Position, Vector2 Velocity) ball in ballsSnapshot)
            {
                if (ball.Position.X - _ballRadius <= 0 || ball.Position.X + _ballRadius >= _table.Width)
                {
                    _dataAPI.SetBallVelocity(ball.Ball, new Vector2(-ball.Velocity.X, ball.Velocity.Y));
                }
                if (ball.Position.Y - _ballRadius <= 0 || ball.Position.Y + _ballRadius >= _table.Height)
                {
                    _dataAPI.SetBallVelocity(ball.Ball, new Vector2(ball.Velocity.X, -ball.Velocity.Y));
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
