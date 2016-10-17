using Library.ComponentModel.Logic;
using NLog;
using System;
using System.Diagnostics;
using System.Threading;

namespace HomeApplication.Logic
{


    public abstract class BaseMultiThreadingLogicService : BaseLogicService
    {
        public BaseMultiThreadingLogicService()
        {


        }
        #region property
        public bool ThrowError { get; set; }

        int _threadCount = 3;
        int _batSize = 20;
        public int ThreadCount
        {
            get
            {
                return _threadCount;
            }
            protected set
            {
                if (value < 1 || value > 10) throw new Exception();
                if (_threadCount != value) _threadCount = value;
            }
        }
        protected long TotalRecord { get; set; }
        protected long CompletedRecord { get; set; }

        public int BatchSize
        {
            get
            {
                return _batSize;
            }
            protected set
            {
                if (value < 2 || value > 100) throw new Exception();
                if (_batSize != value) _batSize = value;
            }
        }
        #endregion
        #region event


        public event EventHandler<LogicServiceProgress> Progress;
        public event EventHandler<LogicServiceFailure> Failure;



        protected void OnFailure(Exception error, long beginIndex, long endIndex)
        {
            var handler = Failure;
            if (handler == null) return;

            SynchronizationContext.Current.Post(n =>
            {
                handler.Invoke(this, new LogicServiceFailure(error, beginIndex, endIndex));
            }, null);
        }
        protected void OnProgress(long beginIndex, long endIndex)
        {
            var handler = Progress;
            if (handler == null) return;

            SynchronizationContext.Current.Post(n =>
            {
                handler.Invoke(this, new LogicServiceProgress(TotalRecord, CompletedRecord, beginIndex, endIndex));
            }, null);
        }
        #endregion
        protected virtual void ThreadPross(long begin, long end)
        {
            int size = BatchSize;
            //      Logger.Info(string.Format("begin:{0} end:{1}", begin, end - 1));
            for (long index = begin; index < end; index = index + size)
            {
                var endindex = index + size - 1;
                if (endindex >= end) endindex = end;
                try
                {
                    ThreadProssSize((int)index, (int)endindex);
                    CompletedRecord = CompletedRecord + (endindex - index);
                    OnProgress(index, endindex);
                }
                catch (Exception ex)
                {

                    OnFailure(ex, index, endindex);
                    if (ThrowError) throw new Exception(string.Format("{0} - {1}:處理失敗", index, endindex), ex);
                }



            }
        }

        protected sealed override void OnDowrok()
        {

            IMultiThreadingOption option = this.ServiceOption as IMultiThreadingOption;
            if (option != null)
            {
                ThreadCount = option.ThreadCount;
                BatchSize = option.BatchSize;
            }
            TotalRecord = GetTotalRecord();
            ThreadPross();




        }
        protected abstract int GetTotalRecord();
        protected virtual void ThreadPross()
        {


            var total = TotalRecord / BatchSize;
            if (total <= ThreadCount || ThreadCount == 1)
            {

                ThreadPross(0, (int)TotalRecord - 1);

            }
            else
            {
                Thread[] thrads = new Thread[ThreadCount];
                var totalsize = (decimal)TotalRecord / ThreadCount;
                for (int i = 0; i < ThreadCount; i++)
                {
                    int currentindex = i;
                    thrads[currentindex] = new Thread(n =>
                    {
                        var beging = (long)Math.Ceiling(totalsize * currentindex);
                        var end = (long)Math.Floor(beging + totalsize);
                        if (end >= TotalRecord)
                        {
                            end = (int)TotalRecord;
                        }

                        ThreadPross(beging, end - 1);

                    });

                }
                for (int i = 0; i < ThreadCount; i++)
                {
                    thrads[i].Start();
                }
                for (int i = 0; i < ThreadCount; i++)
                {
                    thrads[i].Join();
                }
            }
        }
        protected abstract void ThreadProssSize(int beginindex, int endindex);
    }
    public abstract class BaseLogicService : ILogicService
    {
        public BaseLogicService()
        {
            Logger = LogManager.GetLogger(this.GetType().FullName);
            //   logerName = this.GetType().FullName;
        }

        protected NLog.ILogger Logger { get; set; }
        IOption ILogicService.Option
        {
            get
            {
                return ServiceOption;
            }

            set
            {
                ServiceOption = value;
            }
        }
        protected abstract IOption ServiceOption { get; set; }

        public event EventHandler Completed;
        protected void OnCompleted(TimeSpan usetime)
        {

            Logger.Trace("完成執行");
            var handler = Completed;
            if (handler == null) return;
            SynchronizationContext.Current.Post(n =>
            {

                handler.Invoke(this, EventArgs.Empty);
            }, null);
        }

        public void Start()
        {
            if (!OnVerification())
            {
                Logger.Warn("驗證失敗");

                return;
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Logger.Trace("開始執行");

            OnDowrok();
            watch.Stop();
            OnCompleted(watch.Elapsed);
        }
        Thread threadPool;
        public void StartAsyn()
        {
            if (threadPool != null) return;
            threadPool = new Thread(n =>
          {
              Start();
          });
            threadPool.Start();
        }
        protected abstract void OnDowrok();
        protected virtual bool OnVerification()
        {
            return true;
        }

        public void StopAsyn()
        {
            if (threadPool != null)
            {
                threadPool.Abort();
                threadPool = null;
            }
        }
    }
}