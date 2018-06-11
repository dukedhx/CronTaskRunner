using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
namespace WebApplication2.Utils
{
    internal class LoggerContainer
    {

        private readonly ILogger<LoggerContainer> _logger;

        public LoggerContainer(ILogger<LoggerContainer> logger)
        {
            _logger = logger;
        }

        public void debug(string msg)
        {
            _logger.LogDebug(20, $"[Action]{msg}");
        }
    }



    public static class Logger
    {

        private static IServiceProvider servicesProvider
        {
            get
            {
                if (_servicesProvider == null)
                {
                    _servicesProvider = new ServiceCollection().

                    AddTransient<LoggerContainer>().

                    AddSingleton<ILoggerFactory, LoggerFactory>()
                    .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                    .AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace))

                 .BuildServiceProvider();

                    var loggerFactory = _servicesProvider.GetRequiredService<ILoggerFactory>();

                    loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    NLog.LogManager.LoadConfiguration(@"config\nlog.config");

                }
                return _servicesProvider;
            }
        }

        private static IServiceProvider _servicesProvider;
        private static readonly LoggerContainer runner = servicesProvider.GetRequiredService<LoggerContainer>();



        public static void WriteToConsole(Object obj)
        {
            WriteToConsole(obj?.ToString());
        }

        public static void WriteToConsole(String obj)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(obj);
                Trace.WriteLine(obj);
            }
            else
                runner.debug(obj);
        }


        public static void Log(Exception ex)
        {
            Log($"[{ex.Message}][{ex.StackTrace}]");
            if ((ex = ex.InnerException) != null)
                Log($"[{ex.Message}][{ex.StackTrace}]");


        }

        public static void Log(String obj)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(obj);
                Trace.WriteLine(obj);
            }
            else
                runner.debug(obj);

        }
    }
}