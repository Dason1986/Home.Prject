using System;
using Library.Domain.Data;
using Library.Domain.DomainEvents;
using NLog;

namespace HomeApplication.DomainServices
{
    public abstract class DomainService : IDomainService, IDisposable
    {
        public DomainService()
        {
            Logger = NLog.LogManager.GetLogger(this.GetType().FullName);
        }

        protected NLog.ILogger Logger { get; private set; }

        protected abstract IDomainModuleProvider Provider { get; set; }

        IDomainModuleProvider IDomainService.DomainModuleProvider
        {
            get { return Provider; }
            set { Provider = value; }
        }

        protected abstract void Handle(IDomainEventArgs args);

        void IDomainService.Handle(IDomainEventArgs args)
        {
            this.Handle(args);
        }

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected void Dispose(bool disposing)
        {
            if (disposedValue) return;
            disposedValue = true;
            if (!disposing) return;
            OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

        ~DomainService()
        {
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}