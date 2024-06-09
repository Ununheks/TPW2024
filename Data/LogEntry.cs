using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class LogEntry
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public DateTime Timestamp { get; set; }

        public LogEntry(Vector2 position, Vector2 velocity, DateTime timestamp)
        {
            Position = position;
            Velocity = velocity;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"{{ \"Position\": {{ \"X\": {Position.X}, \"Y\": {Position.Y} }}, \"Velocity\": {{ \"X\": {Velocity.X}, \"Y\": {Velocity.Y} }}, \"Timestamp\": \"{Timestamp}\" }}";
        }

    }

}
