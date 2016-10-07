﻿using Library.ComponentModel.Logic;
using NLog;
using System;
using System.Diagnostics;
using System.Threading;

namespace HomeApplication.Logic
{

    public interface ILogicService
    {
        IOption Option { get; set; }

        void Start();

    }

    public interface IOption
    {

    }
    public interface IMultiThreadingOption : IOption
    {
        int ThreadCount { get; set; }
        int BatchSize { get; set; }
    }
    public interface IOptionCommandBuilder
    {
        void RumCommandLine();
        IOption GetOption();
    }
    public interface IOptionCommandBuilder<TOption> : IOptionCommandBuilder where TOption : IOption
    {


        new TOption GetOption();
    }
    public sealed class EmptyOption : IOption
    {
        static EmptyOption()
        {
            Epmty = new EmptyOption();

        }
        public static EmptyOption Epmty { get; set; }
    }
    public class EmptyOptionCommandBuilder : IOptionCommandBuilder
    {
        public IOption GetOption()
        {
            return EmptyOption.Epmty;
        }

        public void RumCommandLine()
        {

        }
    }
    public class LogicServiceProgress : EventArgs
    {


        public LogicServiceProgress(long totalRecord, long completedRecord, long beginIndex, long endIndex)
        {
            TotalRecord = totalRecord;
            CompletedRecord = completedRecord;
            BeginIndex = beginIndex;
            EndIndex = endIndex;
        }

        public long TotalRecord { get; protected set; }
        public long CompletedRecord { get; protected set; }

        public long BeginIndex { get; protected set; }
        public long EndIndex { get; protected set; }
    }

    public class LogicServiceFailure : EventArgs
    {
        public LogicServiceFailure(Exception error, long beginIndex, long endIndex)
        {
            Error = error;
            BeginIndex = beginIndex;
            EndIndex = endIndex;
        }

        public Exception Error { get; protected set; }
        public long BeginIndex { get; protected set; }
        public long EndIndex { get; protected set; }
    }
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
                var endindex = index + size;
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

                ThreadPross(0, (int)TotalRecord);

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

                        ThreadPross(beging, end);

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
            Logger = LogManager.GetCurrentClassLogger();
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
                Logger.Warn(string.Format("驗證失敗"), 1);
                return;
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            Logger.Info(string.Format("開始執行"), 1);
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