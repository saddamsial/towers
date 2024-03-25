// Written By Asaf Benjaminov @ JStudios 2022

using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JStudios.Scripts.Helpers
{
    internal static class JStudiosLog
    {
        private const string LOGFilePath = "Assets\\JStudios";
        private static JLog _log;

        public static bool WriteDebugLog
        {
            get => _log.WriteDebugLog;
            set => _log.WriteDebugLog = value;
        }
        public static bool WriteFileLog
        {
            get => _log.WriteFileLog;
            set => _log.WriteFileLog = value;
        }
        
        static JStudiosLog()
        {
            _log = new JLog(LOGFilePath, "JGeneralLog");
        }

        public static void Error(string error, params object[] args)
        {
            _log.Error(error, args);
        }
        
        public static void Error(string error, Object context, params object[] args)
        {
            _log.Error(error, context, args);
        }

        public static void Info(string message, params object[] args)
        {
            _log.Info(message, args);
        }
    }
    internal class JLog
    {
        private readonly string _logFileName;
        private readonly string _logFilePath;

        public bool WriteDebugLog = false;
        public bool WriteFileLog = true;
        
        public JLog(string logFilePath ,string logName)
        {
            _logFileName = $"{logName}.log";
            
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            _logFilePath = Path.Combine(logFilePath, _logFileName);

            if (!File.Exists(_logFilePath))
            {
                File.WriteAllText(_logFilePath, $"Logs for {logName}");
            }
        }

        private string GetTimeString()
        {
            var date = DateTime.Now;

            return date.ToString("dddd, dd MMMM yyyy");
        }
        
        public void Error(string error, params object[] args)
        {
            var message = $"** {GetTimeString()} - Error {string.Format(error, args)}\n";
            if(WriteDebugLog) Debug.LogError(message);
            if(WriteFileLog) File.AppendAllText(_logFilePath, message);
        }
        
        public void Error(string error, Object context, params object[] args)
        {
            var message = $"** {GetTimeString()} - Error {string.Format(error, args)}\n";
            if(WriteDebugLog) Debug.LogError(message, context);
            if(WriteFileLog) File.AppendAllText(_logFilePath, message);
        }
        
        public void Info(string message, params object[] args)
        {
            var info = $"** {GetTimeString()} - Info {string.Format(message, args)}\n";
            if(WriteDebugLog) Debug.Log(info);
            if(WriteFileLog) File.AppendAllText(_logFilePath, info);
        }
    }
}