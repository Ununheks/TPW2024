using System;
using System.Collections.Concurrent;
using System.Numerics;
using System.Text.Json;
using System.Threading;

namespace Data
{
    internal class Logger
    {
        private static Logger _instance;
        private static readonly object _lock = new object();
        private ConcurrentQueue<LogEntry> _logQueue = new ConcurrentQueue<LogEntry>();
        private bool _loggingEnabled = true;
        private string _logFilePath = "log.json";
        private Thread _loggingThread;

        // Private constructor to prevent instantiation
        private Logger()
        {
            _loggingThread = new Thread(ProcessLogs);
            _loggingThread.Start();
        }

        public static Logger GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }

        public void CreateLog(LogEntry entry)
        {
            _logQueue.Enqueue(entry);
        }

        private void ProcessLogs()
        {
            while (_loggingEnabled)
            {
                if (_logQueue.TryDequeue(out LogEntry entry))
                {
                    WriteLogToFile(entry);
                }
                else
                {
                    // Sleep for a short period if the queue is empty
                    Thread.Sleep(10);
                }
            }
        }

        private void WriteLogToFile(LogEntry entry)
        {
            try
            {
                string jsonString = entry.ToString();
                File.AppendAllText(_logFilePath, jsonString + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Handle exceptions by issuing a warning
                System.Diagnostics.Debug.WriteLine($"Warning: Error writing log - {ex.Message}");
            }
        }


        public void StopLogging()
        {
            _loggingEnabled = false;
        }

        public void Dispose()
        {
            _loggingEnabled = false;
            _loggingThread.Join();
        }
    }
}
