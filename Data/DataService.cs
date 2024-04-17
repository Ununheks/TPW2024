using System.Numerics;

namespace Data
{
    internal class DataService : DataAPI
    {
        public override object CreateBall(Vector2 pos, Vector2 velocity)
        {
            return new Ball(pos, velocity);
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
    }
}
