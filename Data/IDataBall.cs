using System.Numerics;

namespace Data
{
    public interface IDataBall
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
    }
}
