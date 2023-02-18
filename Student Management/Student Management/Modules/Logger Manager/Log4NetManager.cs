using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net;
using System;

namespace Student_Management.Modules.LoggerManager
{
    internal class Log4NetManager
    {
        private static ILog logger;
        private static ConsoleAppender ConsoleAppender;
        private static RollingFileAppender RollingFileAppender;
        private static string layout = "%date [%thread] [%-5level] %logger: %message%newline";

        public static string Layout { set => layout = value; }

        private static PatternLayout GetPatternLayout()
        {
            var PatternLayout = new PatternLayout()
            {
                ConversionPattern = layout
            };
            PatternLayout.ActivateOptions();
            return PatternLayout;
        }

        private static ConsoleAppender GetConsoleAppender()
        {
            var ConsoleAppender = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.Error
            };
            ConsoleAppender.ActivateOptions();
            return ConsoleAppender;
        }

        private static RollingFileAppender GetRollingFileAppender()
        {
            var RollingFileAppender = new RollingFileAppender()
            {
                Name = "Rolling File Appender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                MaximumFileSize = "10MB",
                MaxSizeRollBackups = 15,
                StaticLogFileName = false,
                AppendToFile = true,
                File = "../../Logs/",
            };
            RollingFileAppender.ActivateOptions();
            return RollingFileAppender;
        }

        public static ILog GetLogger(Type type)
        {
            if (ConsoleAppender == null)
                ConsoleAppender = GetConsoleAppender();
            if (RollingFileAppender == null)
                RollingFileAppender = GetRollingFileAppender();
            if (logger != null)
                return logger;

            BasicConfigurator.Configure(ConsoleAppender, RollingFileAppender);
            logger = log4net.LogManager.GetLogger(type);
            return logger;
        }
    }
}
