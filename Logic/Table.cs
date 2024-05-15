using System;
using System.Collections.Generic;
using System.Numerics;

namespace Logic
{
    internal class Table
    {
        private float _width;
        private float _height;
        private List<(object Ball, Vector2 Position, Vector2 Velocity)> _balls;

        public float Width => _width;
        public float Height => _height;
        public List<(object Ball, Vector2 Position, Vector2 Velocity)> Balls => _balls;

        public Table(float width, float height)
        {
            _width = width;
            _height = height;
            _balls = new List<(object Ball, Vector2 Position, Vector2 Velocity)>();
        }

        public void AddBall(object ball, Vector2 position, Vector2 velocity)
        {
            _balls.Add((ball, position, velocity));
        }

        public void UpdateBall(object ball, Vector2 newPosition, Vector2 newVelocity)
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                if (ReferenceEquals(_balls[i].Ball, ball))
                {
                    _balls[i] = (ball, newPosition, newVelocity);
                    return;
                }
            }
            // If the ball object is not found in the list, you can throw an exception or handle it accordingly.
            throw new ArgumentException("Specified ball object not found in the table.");
        }
    }
}
