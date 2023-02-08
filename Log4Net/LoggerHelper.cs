using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4Net
{
    internal class LoggerHelper
    {
        private static ILog logger;
        private static ConsoleAppender ConsoleAppender;
        private static FileAppender FileAppender;
        private static RollingFileAppender RollingFileAppender;
        private static string layout = "%date{dd-MM-yyyy-HH:mm:ss} [%class] [%level] [%method] - %message%newline";

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

        private static FileAppender GetFileAppender()
        {
            var FileAppender = new FileAppender()
            {
                Name = "fileAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                AppendToFile = false,
                File = "Logs/FileAppender.log"
            };
            FileAppender.ActivateOptions();
            return FileAppender;
        }

        private static RollingFileAppender GetRollingFileAppender()
        {
            var RollingFileAppender = new RollingFileAppender()
            {
                Name = "Rolling File Appender",
                AppendToFile = true,
                File = "Logs/RollingFileAppender.log",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                MaximumFileSize = "1MB",
                MaxSizeRollBackups = 15
            };
            RollingFileAppender.ActivateOptions();
            return RollingFileAppender;
/*
<appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender">
			<lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
			<file value="Logs\" />
			<datePattern value="dd.MM.yyyy'.log'" />
			<staticLogFileName value="false" />
			<appendToFile value="true" />
			<rollingStyle value="Composite" />
			<maxSizeRollBackups value="1000" />
			<maximumFileSize value="100MB" />
			<layout type="log4net.Layout.PatternLayout">
				<conversionPattern value="%date [%thread] [%-5level] %logger: %message%newline" />
			</layout>
		</appender>
*/
        }

        public static ILog GetLogger(Type type)
        {
            if (ConsoleAppender == null)
                ConsoleAppender = GetConsoleAppender();
            if(FileAppender == null)
                FileAppender = GetFileAppender();
            if(RollingFileAppender == null)
                RollingFileAppender = GetRollingFileAppender();
            if (logger != null)
                return logger;

            BasicConfigurator.Configure(ConsoleAppender, FileAppender, RollingFileAppender);
            logger = LogManager.GetLogger(type);
            return logger;
        }
    }
}
