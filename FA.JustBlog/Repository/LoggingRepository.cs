using FA.JustBlog.Interface;
using log4net;

namespace FA.JustBlog.Repository
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly ILog _logger;
        /// <summary>
        /// LoggingService
        /// </summary>
        public LoggingRepository()
        {
            _logger = LogManager.GetLogger(typeof(LoggingRepository));
        }
        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="exception"></param>
        public void LogError(Exception exception)
        {
            _logger.Error(exception);
        }
        /// <summary>
        /// LogInfo
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
