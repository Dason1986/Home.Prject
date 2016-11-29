namespace HomeApplication.Jobs
{
    public abstract class ServiceJob
    {
        public ServiceJob()
        {
            logger = NLog.LogManager.GetLogger(this.GetType().FullName);
        }
        NLog.ILogger logger;
        protected NLog.ILogger Logger { get { return logger; } }
    }
}