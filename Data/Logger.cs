﻿using System.Collections.Concurrent;

namespace Data
{
    internal class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();
        private ConcurrentQueue<LogEntry> _logQueue = new ConcurrentQueue<LogEntry>();
        private string _logFilePath = "log.json";
        private Task? _loggingTask;
        private int _maxQueueSize = 101;
        private bool _queueFullWarning = false;

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
            lock (_lock)
            {
                if (_logQueue.Count < _maxQueueSize-1)
                {
                    _logQueue.Enqueue(entry);
                    _queueFullWarning = false;

                    if (_loggingTask == null || _loggingTask.IsCompleted)
                    {
                        _loggingTask = Task.Run(ProcessLogs);
                    }
                }
                else if(!_queueFullWarning)
                {
                    var stringLogEntry = new StringLogEntry("Queue full", entry.Timestamp);
                    _logQueue.Enqueue(stringLogEntry);
                    _queueFullWarning = true;
                }
            }
        }

        private async Task ProcessLogs()
        {
            while (_logQueue.TryDequeue(out LogEntry entry))
            {
                await WriteLogToFileAsync(entry);
            }
        }

        private async Task WriteLogToFileAsync(LogEntry entry)
        {
            try
            {
                string jsonString = entry.ToString();
                await File.AppendAllTextAsync(_logFilePath, jsonString + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Warning: Error writing log - {ex.Message}");
            }
        }
    }
}
