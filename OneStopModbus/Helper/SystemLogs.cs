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
        public LogWindowSink sink;

        public ObservableCollection<LogMsg> logMsgs;

        public static SystemLogs Instance
        {
            get
            {
                return m_instance;
            }
        }

        public static void Initialization()
        {
            m_instance = new SystemLogs();
        }

        private static SystemLogs m_instance = null;

        private SystemLogs()
        {
            sink = new LogWindowSink();
            sink.AssignParent(this);
            logMsgs = new ObservableCollection<LogMsg>();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.File($"{FileHelpers.LogFolderPath}\\ModebusStop.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 1000000, rollOnFileSizeLimit: true, retainedFileCountLimit: 10)
                .WriteTo.Sink(sink)
                .CreateLogger();
        }
    }

    public class LogWindowSink() : ILogEventSink
    {
        private SystemLogs parent;

        public void AssignParent(SystemLogs systemLogs)
        {
            parent = systemLogs;
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write</param>
        public void Emit(LogEvent logEvent)
        {
            LogMsg msg = new LogMsg()
            {
                Text = logEvent.RenderMessage(),
                Lvl = logEvent.Level.ToString(),
                TimeStamp = logEvent.Timestamp.ToString("yyyy-MM-dd HH-mm-ss-ffff"),
            };
            parent.logMsgs.Add(msg);
        }
    }

    public struct LogMsg
    {
        public string Lvl;
        public string Text;
        public string TimeStamp;
    }
}