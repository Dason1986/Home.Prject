using System;
using Library.Domain.DomainEvents;
using NLog;

namespace HomeApplication.DomainServices
{
    public abstract class DomainService
    {
        public DomainService()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }
        protected NLog.ILogger Logger { get; private set; }
    }
}