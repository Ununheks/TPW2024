using System;
using System.Numerics;

namespace Data
{
    internal class DataService : DataAPI
    {
        public override object CreateBall(Vector2 pos, Vector2 velocity, Action callback)
        {
            return new Ball(pos, velocity, callback);
        }

        public override Vector2 GetBallPosition(object ball)
        {
            if (ball is Ball)
            {
                Ball castedBall = (Ball)ball;
                return castedBall.Position;
            }
            else
            {
                throw new ArgumentException("The provided object is not a valid ball.");
            }
        }

        public override Vector2 GetBallVelocity(object ball)
        {
            if (ball is Ball)
            {
                Ball castedBall = (Ball)ball;
                return castedBall.Velocity;
            }
            else
            {
                throw new ArgumentException("The provided object is not a valid ball.");
            }
        }

        public override void SetBallVelocity(object ball, Vector2 newVelocity)
        {
            if (ball is Ball)
            {
                Ball castedBall = (Ball)ball;
                castedBall.Velocity = newVelocity;
            }
            else
            {
                throw new ArgumentException("The provided object is not a valid ball.");
            }
        }
    }
}
