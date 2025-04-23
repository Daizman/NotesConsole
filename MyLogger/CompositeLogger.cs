using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLogger
{
    public class CompositeLogger : ILogger
    {
        private readonly IReadOnlyCollection<ILogger> _loggers;

        public CompositeLogger(IReadOnlyCollection<ILogger> loggers)
        {
            _loggers = loggers;
        }

        public void LogInformation(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.LogInformation(message);
            }
        }
    }
}
