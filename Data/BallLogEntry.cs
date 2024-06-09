using System.Numerics;

namespace Data
{
    internal class BallLogEntry : LogEntry
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public BallLogEntry(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public override string ToString()
        {
            string formattedTimestamp = Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

            return $"{{ \"Position\": {{ \"X\": {Position.X}, \"Y\": {Position.Y} }}, \"Velocity\": {{ \"X\": {Velocity.X}, \"Y\": {Velocity.Y} }}, \"Timestamp\": \"{formattedTimestamp}\" }}";
        }
    }
}
