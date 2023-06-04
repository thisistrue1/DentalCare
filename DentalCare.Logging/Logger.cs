using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;

namespace DentalCare.Logging
{
    public class Logger
    {
        private static ILog FileLog;

        static Logger()
        {
            FileInfo logFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Logger.config");
            XmlConfigurator.ConfigureAndWatch(logFile);
            FileLog = LogManager.GetLogger("File");
        }

        /// <summary>
        /// Initializes the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
        }

        public void Debug(string message)
        {
            FileLog.Debug(string.Format(Constants.DEF_DEBUG_MESSAGE, message));
        }

        public void Info(string message)
        {
            FileLog.Info(string.Format(Constants.DEF_INFO_MESSAGE, message));
        }

        public void Warn(string message)
        {
            FileLog.Warn(string.Format(Constants.DEF_WARRNING_MESSAGE, message));
        }

        public static void Error(string message)
        {
            FileLog.Error(string.Format(Constants.DEF_ERROR_MESSAGE, message));
        }

        public void Fatal(string message)
        {
            FileLog.Fatal(string.Format(Constants.DEF_FATALERROR_MESSAGE, message));
        }
    }
}
