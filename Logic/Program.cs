using Data;
using System;
using System.Numerics;

namespace Logic
{
    public abstract class LogicAPI
    {
        public abstract void CreateTable(float width, float height);
        public abstract void SpawnBalls(int amount, float radius);
        public abstract void CheckCollisions();
    }

    public class LogicService : LogicAPI
    {
        private DataAPI _dataAPI;

        public LogicService()
        {
            _dataAPI = new Data.DataService();
        }

        public override void CreateTable(float width, float height)
        {
            _dataAPI.CreateTable(width, height);
        }

        public override void SpawnBalls(int amount, float radius)
        {
            var table = _dataAPI.GetTableInfo();
            if (table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            Random random = new Random();

            int numRows = (int)Math.Ceiling(Math.Sqrt(amount));
            int numCols = (int)Math.Ceiling((float)amount / numRows);

            float maxDisplacement = radius * 2;

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    float x = (float)(random.NextDouble() * maxDisplacement) + col * (table.Width - maxDisplacement) / (numCols - 1);
                    float y = (float)(random.NextDouble() * maxDisplacement) + row * (table.Height - maxDisplacement) / (numRows - 1);

                    _dataAPI.CreateBall(new Vector2(x, y), GetRandomVelocity(random), radius);
                }
            }

            foreach (var ball in table.Balls)
            {
                ball.PositionUpdated += CheckCollisions;
            }
        }

        public override void CheckCollisions()
        {
            var table = _dataAPI.GetTableInfo();
            if (table == null)
            {
                Console.WriteLine("Table is not created yet.");
                return;
            }

            foreach (var ball in table.Balls)
            {
                if (ball.Position.X - ball.Radius <= 0 || ball.Position.X + ball.Radius >= table.Width)
                {
                    ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
                }
                if (ball.Position.Y - ball.Radius <= 0 || ball.Position.Y + ball.Radius >= table.Height)
                {
                    ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
                }

                foreach (var otherBall in table.Balls)
                {
                    if (ball != otherBall)
                    {
                        float distance = Vector2.Distance(ball.Position, otherBall.Position);
                        if (distance <= ball.Radius + otherBall.Radius)
                        {
                            Vector2 collisionNormal = Vector2.Normalize(otherBall.Position - ball.Position);
                            float relativeVelocityAlongNormal = Vector2.Dot(ball.Velocity - otherBall.Velocity, collisionNormal);
                            ball.Velocity -= 2 * relativeVelocityAlongNormal * collisionNormal;
                            otherBall.Velocity += 2 * relativeVelocityAlongNormal * collisionNormal;
                        }
                    }
                }
            }
        }

        private Vector2 GetRandomVelocity(Random random)
        {
            float angle = (float)(random.NextDouble() * 2 * Math.PI);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LogicService logicService = new LogicService();
            logicService.CreateTable(400, 300);
            logicService.SpawnBalls(10, 10);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
