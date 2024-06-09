namespace Data
{
    internal class StringLogEntry : LogEntry
    {
        public string Message { get; set; }

        public StringLogEntry(string message, DateTime timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            string formattedTimestamp = Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

            return $"{{ \"Message\": \"{Message}\", \"Timestamp\": \"{formattedTimestamp}\" }}";
        }
    }
}
