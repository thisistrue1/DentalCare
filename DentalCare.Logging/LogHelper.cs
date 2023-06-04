
namespace DentalCare.Logging
{
    public class LogHelper : ILogHelper
    {
        private Logger logger;
        public LogHelper()
        {
            logger = new Logger();
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }
    }
}