namespace FA.JustBlog.Interface
{
    public interface ILoggingRepository
    {
        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="exception"></param>
        void LogError(Exception exception);
        /// <summary>
        /// LogInfo
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);
    }
}
