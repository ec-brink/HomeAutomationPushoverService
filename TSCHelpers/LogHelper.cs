using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data.SqlClient;

using Serilog;
using Serilog.Events;

namespace TSCHelpers
{
    public static class LogHelper
    {
        private static readonly string fallbackLogFilename = "error.log";

        private static LogEventLevel logLevelFile;

        public static void SetupLogging()
        {
            try
            {
                Directory.CreateDirectory(HelperSettings.Settings.LogDirectory);

                logLevelFile = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), HelperSettings.Settings.LogFileLevel, ignoreCase: true);

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("File", logLevelFile)
                    .WriteTo.File(
                        $"{HelperSettings.Settings.LogDirectory}/.log",
                        rollingInterval: RollingInterval.Day,
                        restrictedToMinimumLevel: logLevelFile,
                        fileSizeLimitBytes: null,
                        retainedFileCountLimit: null,
                        rollOnFileSizeLimit: true,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                    )
                    .CreateLogger();
            }

            catch (Exception ex)
            {
                WriteToFallbackLog($"Failed to setup Serilog. Error: {ex.Message}");
            }
        }

        public static void LogTrace(params object[] args)
        {
            if (logLevelFile <= LogEventLevel.Verbose)
            {
                try
                {
                    try
                    {
                        StackFrame frame = new StackFrame(1, true);
                        string callingMethodName = frame.GetMethod().Name;
                        string fileName = frame.GetFileName();
                        int lineNumber = frame.GetFileLineNumber();
                        string stackTrace = Environment.StackTrace;

                        string argsRepresentation = string.Join("; ", args.Select(arg =>
                        {
                            switch (arg)
                            {
                                default:
                                    return $"Type: {arg.GetType()}, Value: {arg}";
                            }
                        }));

                        StringBuilder traceBuilder = new StringBuilder();
                        traceBuilder.AppendLine("TRACE:");
                        traceBuilder.AppendLine($"Method: {callingMethodName}");
                        traceBuilder.AppendLine($"File: {fileName}");
                        traceBuilder.AppendLine($"Line: {lineNumber}");
                        traceBuilder.AppendLine($"Arguments: {argsRepresentation}");
                        traceBuilder.AppendLine($"Stack: {stackTrace}");
                    }

                    catch
                    {
                        Log.Verbose("Error getting the stack trace.");
                    }
                }
                catch (Exception ex)
                {
                    WriteToFallbackLog($"Failed to log trace. Error: {ex.Message}");
                }
            }
        }

        public static void LogDebug(string message)
        {
            try
            {
                Log.Debug(message);
            }
            catch (Exception ex)
            {
                WriteToFallbackLog($"Failed to log debug message: {message}. Error: {ex.Message}");
            }
        }

        public static void LogInfo(string message)
        {
            try
            {
                Log.Information(message);
            }
            catch (Exception ex)
            {
                WriteToFallbackLog($"Failed to log info message: {message}. Error: {ex.Message}");
            }
        }

        public static void LogWarning(string message)
        {
            try
            {
                Log.Warning(message);
            }
            catch (Exception ex)
            {
                WriteToFallbackLog($"Failed to log warning message: {message}. Error: {ex.Message}");
            }
        }

        public static void LogError(string message, Exception exception = null)
        {
            try
            {
                if (exception == null)
                    Log.Error(message);
                else
                    Log.Error(exception, message);
            }
            catch (Exception ex)
            {
                WriteToFallbackLog($"Failed to log error message: {message}, Exception: {exception?.Message}, Error: {ex.Message}");
            }
        }

        private static void WriteToFallbackLog(string message)
        {
            try
            {
                Directory.CreateDirectory(HelperSettings.Settings.LogDirectory);

                using (StreamWriter writer = new StreamWriter(Path.Combine(HelperSettings.Settings.LogDirectory, fallbackLogFilename), true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [Fallback] {message}");
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog($"Failed to write to fallback log: {ex.Message}");
            }
        }

        private static void WriteToEventLog(string message)
        {
            try
            {
                if (!EventLog.SourceExists(HelperSettings.Settings.WindowsEventLogApplicationName))
                {
                    EventLog.CreateEventSource(HelperSettings.Settings.WindowsEventLogApplicationName, "Application");
                }

                EventLog.WriteEntry(HelperSettings.Settings.WindowsEventLogApplicationName, message, EventLogEntryType.Error);
            }
            catch
            {
            }
        }
    }
}
