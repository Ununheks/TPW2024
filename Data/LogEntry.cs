namespace Data
{
    internal abstract class LogEntry
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public abstract override string ToString();
    }
}
