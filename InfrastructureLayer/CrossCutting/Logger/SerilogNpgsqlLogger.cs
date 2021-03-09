using System;
using Microsoft.Extensions.Logging;
using Npgsql.Logging;

namespace CrossCutting.Logger
{
    public class SerilogNpgsqlLoggingProvider : INpgsqlLoggingProvider
    {
        private ILoggerFactory _loggerFactory { get; }

        public SerilogNpgsqlLoggingProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public NpgsqlLogger CreateLogger(string name)
        {
            ILogger<SerilogNpgsqlLogger> logger = _loggerFactory.CreateLogger<SerilogNpgsqlLogger>();

            return new SerilogNpgsqlLogger(logger);
        }
    }

    public class SerilogNpgsqlLogger : NpgsqlLogger
    {
        private readonly ILogger _logger;

        public SerilogNpgsqlLogger(ILogger<SerilogNpgsqlLogger> logger)
        {
            _logger = logger;
        }

        public override bool IsEnabled(NpgsqlLogLevel level)
        {
            return _logger.IsEnabled(GetLogLevelFromNpgsqlLogLevel(level));
        }

        public override void Log(NpgsqlLogLevel level, int connectorId, string msg, Exception exception = null)
        {
            _logger.Log(GetLogLevelFromNpgsqlLogLevel(level), exception, msg);
        }

        private LogLevel GetLogLevelFromNpgsqlLogLevel(NpgsqlLogLevel level)
        {
            switch (level)
            {
                case NpgsqlLogLevel.Trace:
                    return LogLevel.Trace;
                case NpgsqlLogLevel.Debug:
                    return LogLevel.Debug;
                case NpgsqlLogLevel.Info:
                    return LogLevel.Information;
                case NpgsqlLogLevel.Warn:
                    return LogLevel.Warning;
                case NpgsqlLogLevel.Error:
                    return LogLevel.Error;
                case NpgsqlLogLevel.Fatal:
                    return LogLevel.Critical;
                default:
                    throw new Exception("Unknown Npgsql Log Level: " + level);
            }
        }
    }
}