using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Helper
{
    public class SystemLogs

    {
        public ObservableCollection<LogMsg> logMsgs;
        public LogMsg LastLogMsg;
        private LogWindowSink sink;

        public static SystemLogs Instance
        {
            get
            {
                return m_instance;
            }
        }

        public static void Initialize()
        {
            if (m_instance == null)
            {
                m_instance = new SystemLogs();
                Log.Logger.Information("SystemLogs initialized");
            }
        }

        private static SystemLogs m_instance = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemLogs"/> class.
        /// </summary>
        /// <remarks>
        /// do not log anything in the constructor as it will cause a deadlock
        /// </remarks>
        private SystemLogs()
        {
            sink = new LogWindowSink();
            logMsgs = new ObservableCollection<LogMsg>();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose()
                .WriteTo.File($"{FileHelpers.LogFolderPath}\\ModebusStop.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1000000, rollOnFileSizeLimit: true, retainedFileCountLimit: 10)
                .WriteTo.Sink(sink)
                .CreateLogger();
        }
    }

    public class LogWindowSink() : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
        {
            LogMsg msg = new LogMsg()
            {
                Message = logEvent.RenderMessage(),
                Level = logEvent.Level.ToString(),
                TimeStamp = logEvent.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.ffff")
            };
            if (logEvent.Exception != null)
            {
                msg.exception = logEvent.Exception.Message;
            }
            SystemLogs.Instance.logMsgs.Add(msg);
        }
    }

    public struct LogMsg
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public string TimeStamp { get; set; }
        public string? exception { get; set; }
    }
}